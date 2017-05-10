using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;

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

            string controllerName = segments[2]?.Replace("/", "").ToLower();

            if (string.IsNullOrEmpty(controllerName))
            {
                throw new NullReferenceException(nameof(controllerName));
            }

            string methodName = segments[3]?.Replace("/", "").ToLower();

            if (string.IsNullOrEmpty(methodName))
            {
                throw new NullReferenceException(nameof(methodName));
            }

            AbstractRouter router = null;

            try
            {
                if (controllerName.ToLower() == "wowpvpladder")
                {
                    router = new WowPvpLadderControllerRouter(httpContext, httpHandler);
                    await router.ExecuteAsync(methodName);
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
                router?.Dispose();
            }
        }
    }
}