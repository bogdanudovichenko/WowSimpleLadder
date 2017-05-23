using Quartz;
using Quartz.Impl;
using WowSimpleLadder.BLL.QuartzJobs;
using WowSimpleLadder.BLL.Services.Interfaces;

namespace WowSimpleLadder.BLL.Services.Concrete
{
    public class WowPvpLadderDownloadScheduler : IWowPvpLadderDownloadScheduler
    {
        public void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<WowLadderDownloadJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
