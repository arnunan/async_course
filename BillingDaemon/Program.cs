using Quartz;
using Quartz.Impl;

namespace BillingDaemon
{
    internal static class Program
    {
        private static async Task Main()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();

            await scheduler.Start();

            var debtBalanceJob = JobBuilder.Create<DebtBalance>()
                .WithIdentity("debtBalance", "group1")
                .Build();

            var debtBalanceTrigger = TriggerBuilder.Create()
                .WithIdentity("triggerDebtBalance", "group1")
                .StartAt(DateTime.Now.Date.AddDays(1))
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            var calculateMostExpensiveTaskJob = JobBuilder.Create<CalculateMostExpensiveTask>()
                .WithIdentity("calculateMostExpensiveTask", "group2")
                .Build();

            var calculateMostExpensiveTaskTrigger = TriggerBuilder.Create()
                .WithIdentity("triggerCalculateMostExpensiveTask", "group2")
                .StartAt(DateTime.Now.Date.AddDays(1))
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            var calculateAmountMoneyEarnedJob = JobBuilder.Create<CalculateAmountMoneyEarned>()
                .WithIdentity("calculateAmountMoneyEarned", "group3")
                .Build();

            var calculateAmountMoneyEarnedTrigger = TriggerBuilder.Create()
                .WithIdentity("triggerCalculateAmountMoneyEarned", "group3")
                .StartAt(DateTime.Now.Date.AddDays(1))
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(debtBalanceJob, debtBalanceTrigger);
            await scheduler.ScheduleJob(calculateMostExpensiveTaskJob, calculateMostExpensiveTaskTrigger);
            await scheduler.ScheduleJob(calculateAmountMoneyEarnedJob, calculateAmountMoneyEarnedTrigger);

            await Task.Delay(TimeSpan.FromSeconds(60));
            await scheduler.Shutdown();
        }
    }
}