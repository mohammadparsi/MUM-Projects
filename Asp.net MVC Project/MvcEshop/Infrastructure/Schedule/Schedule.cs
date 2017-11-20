using Quartz;
using System;
using System.Net;
using System.Net.Mail;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Schedule
{
    public class ResendUnsentRequestsJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

            MVC.Assignment.ResendUnsentRequests();

        }
    }

    public class FollowUpUnreceivedReportsJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

            MVC.Assignment.FollowUpUnreceivedReports();

        }
    }


    public class JobScheduler
    {

        public static void Start()
        {

            MvcEshop.Models.DFEntities oDFEntities = new MvcEshop.Models.DFEntities();

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();


            IJobDetail resendJob = JobBuilder.Create<ResendUnsentRequestsJob>().Build();
            IJobDetail followUpJob = JobBuilder.Create<FollowUpUnreceivedReportsJob>().Build();


            ITrigger resendTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes
                     (oDFEntities.Settings.FirstOrDefault()
                     .IntervalInMinutesForResendingUnsentRequests.Value)

                  )
                .Build();


            ITrigger followUpTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes
                     (oDFEntities.Settings.FirstOrDefault()
                     .IntervalInMinutesForFollowingUpUnreceivedReports.Value)

                  )
                .Build();


            scheduler.ScheduleJob(resendJob, resendTrigger);
            scheduler.ScheduleJob(followUpJob, followUpTrigger);
        }
    }

}




