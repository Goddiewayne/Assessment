using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zedcrest.Data.RabbitQueue;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Rabbit
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _busControl;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _busControl = RabbitHutch.CreateBus("localhost");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _busControl.ReceiveAsync<IEnumerable<User>>(Queue.Processing, x =>
            {
                Task.Run(() => { DidJob(x); }, stoppingToken);
            });
        }

        private void DidJob(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                _logger.LogInformation($"Id: {user.Id}, " +
                    $"UniqueReference: {user.UniqueReference}, " +
                    $"Name: {user.Name}, " +
                    $"Username: {user.Username}, " +
                    $"EmailAddress: {user.EmailAddress}, " +
                    $"Phone Number: {user.PhoneNumber}, " +
                    $"Date Created: {user.DateCreated}");
            }
        }
    }
}
