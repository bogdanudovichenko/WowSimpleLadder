using System;
using System.Web;
using WowSimpleLadder.BLL.Services.Concrete;

namespace WowSimpleLadder.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var wowPvpLadderDownloadScheduler = new WowPvpLadderDownloadScheduler();
            wowPvpLadderDownloadScheduler.Start();
        }
    }
}