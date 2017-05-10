using System;
using System.Threading.Tasks;
using System.Web;

namespace WowSimpleLadder.Web.Routers
{
    public abstract class AbstractRouter : IDisposable
    {
        protected HttpContext HttpContext { get; }
        protected HttpTaskAsyncHandler HttpHandler { get; }

        protected AbstractRouter(HttpContext httpContext, HttpTaskAsyncHandler httpHandler)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (httpHandler == null)
            {
                throw new ArgumentNullException(nameof(httpHandler));
            }

            HttpContext = httpContext;
            HttpHandler = httpHandler;
        }

        public abstract Task ExecuteAsync(string methodName);

        public abstract void Dispose();
    }
}