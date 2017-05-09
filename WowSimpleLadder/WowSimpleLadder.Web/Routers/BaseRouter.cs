using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.Web.Controllers;

namespace WowSimpleLadder.Web.Routers
{
    public static class BaseRouter
    {
        public static async Task CallControllerAsync(HttpTaskAsyncHandler httpHandler, HttpContext httpContext)
        {
            if (httpHandler == null)
            {
                throw new ArgumentNullException(nameof(httpHandler));
            }

            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            Uri url = httpContext.Request.Url;
            string[] segments = url.Segments;

            if (segments.Length < 4)
            {
                throw new Exception("url segments length < 4");
            }

            string apiParam = segments[1];
            if (apiParam?.ToLower() != "api/")
            {
                throw new Exception("you enter invalid url");
            }

            string controllerName = segments[2].Replace("/", "");

            if (string.IsNullOrEmpty(controllerName))
            {
                throw new NullReferenceException(nameof(controllerName));
            }

            string methodName = segments[3].Replace("/", "");

            if (string.IsNullOrEmpty(methodName))
            {
                throw new NullReferenceException(nameof(methodName));
            }

            BaseController baseController = null;

            try
            {

                if (controllerName.ToLower() == "wowpvpladder")
                {
                    var controller = new WowPvpLadderController(httpContext, httpHandler,
                        new WowLadderLiteDbRepository(SimpleLadderConfig.WowLadderLiteDbConnection));

                    baseController = controller;


                    if (methodName.ToLower() == "getpvpladder")
                    {
                        string jsonResult = await controller.GetPvpLadder();
                        httpContext.Response.ContentType = "application/json";
                        httpContext.Response.Write(jsonResult);
                    }
                    else
                    {
                        throw new Exception(
                            $"Can`t find method with name '{methodName}' in controller '{controllerName}'");
                    }
                }
                else
                {
                    throw new Exception($"Can`t find controller with name '{controllerName}'");
                }
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.Write("Internal Server Error: " + ex.Message);
            }
            finally
            {
                baseController?.Dispose();
            }
        }
    }
}