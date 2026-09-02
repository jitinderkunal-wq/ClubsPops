using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubPOS.Services.Services;
using ClubPOS.Services.IServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClubPOS.Infrastructure.Data;

namespace ClubPOS.Services.JobScheduler
{
    public class SchedulerService : IHostedService, IDisposable
    {
        private int executionCount = 0;

        private System.Threading.Timer _timerNotification;
        public IConfiguration _iconfiguration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env; 

        public SchedulerService(IConfiguration iconfiguration, IServiceScopeFactory serviceScopeFactory, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _iconfiguration = iconfiguration;
            _serviceScopeFactory = serviceScopeFactory;
            _env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Calculate the time until 4 AM
            var now = DateTime.Now;
            //var timeUntil4AM = new DateTime(now.Year, now.Month, now.Day, 21, 1, 0) - now;
            var timeUntil4AM = new DateTime(now.Year, now.Month, now.Day, 4, 0, 0) - now;

            // If it's already past 4 AM today, schedule it for 4 AM tomorrow
            if (timeUntil4AM < TimeSpan.Zero)
            {
                timeUntil4AM = timeUntil4AM.Add(TimeSpan.FromDays(1));
            }
            _timerNotification = new Timer(RunJob, null, timeUntil4AM, TimeSpan.FromDays(1));

            //var timeUntil4AM = new DateTime(now.Year, now.Month, now.Day, 18, 10, 0) - now;
            //_timerNotification = new Timer(RunJob, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }
        private async void RunJob(object state)
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var _accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                    // Call Services here
                    var result = await _accountService.EndPreviousDayShiftAutomatically();
                }
                catch (Exception)
                {

                    throw;
                }
                Interlocked.Increment(ref executionCount);
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timerNotification?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
             _timerNotification?.Dispose();
        }
    }
}
