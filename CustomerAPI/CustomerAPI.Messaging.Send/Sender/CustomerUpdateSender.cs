using CustomerAPI.Data.Models;
using CustomerAPI.Messaging.Send.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAPI.Messaging.Send.Sender
{
    public class CustomerUpdateSender : ICustomerUpdateSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _queueName;
        private readonly string _username;
        private IConnection _connection;

        public CustomerUpdateSender(IOptions<RabbitConfiguration> rabbitMqOptions)
        {
            _queueName = rabbitMqOptions.Value.QueueName;
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            CreateConnection();
        }

        public void SendCustomer(CustomerModel customer)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var customerJson = JsonConvert.SerializeObject(customer);
                var customerBody = Encoding.UTF8.GetBytes(customerJson);
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: customerBody);
            }
        }

        private bool CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                };
                _connection = factory.CreateConnection();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
                return false;
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            return CreateConnection();
        }
    }
}
