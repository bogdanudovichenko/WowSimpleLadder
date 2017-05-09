using System;
using System.Web;

namespace WowSimpleLadder.Web.Controllers
{
    public abstract class BaseController
    {
        protected HttpContext HttpContext { get; }
        protected HttpTaskAsyncHandler HttpHandler { get; }
        protected HttpRequest HttpRequest { get; }
        protected HttpResponse HttpResponse { get; set; }

        protected BaseController(HttpContext httpContext, HttpTaskAsyncHandler httpHandler)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpHandler));
            }

            HttpContext = httpContext;
            HttpHandler = httpHandler;
            HttpRequest = httpContext.Request;
            HttpResponse = httpContext.Response;
        }
    }
}