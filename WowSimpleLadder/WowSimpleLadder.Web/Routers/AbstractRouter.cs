using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using WowSimpleLadder.Web.Helpers;

namespace WowSimpleLadder.Web.Routers
{
    public abstract class AbstractRouter : IDisposable
    {
        protected HttpContext HttpContext { get; }
        protected HttpTaskAsyncHandler HttpHandler { get; }
        public IDictionary<string, string> QueryStringParsed { get; }

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

            QueryStringParsed = QueryStringParser.Parse(HttpContext.Request.Url.Query);
        }

        public abstract Task ExecuteAsync(string methodName);

        public abstract void Dispose();
    }
}