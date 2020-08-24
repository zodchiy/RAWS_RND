using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using RAWS.Web.Models;

namespace RAWS.Web.Services
{
    public  class RabbitSubscriber : IRabbitMQService
    {
        ConnectionFactory _factory { get; set; }
        IConnection _connection { get; set; }
        IModel _channel { get; set; }
        private readonly IServiceProvider _serviceProvider;

        public RabbitSubscriber(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
             _factory = new ConnectionFactory()
            {
                HostName = configuration.GetSection("RabbitMqConnection").GetValue<string>("HostName"),
                UserName = configuration.GetSection("RabbitMqConnection").GetValue<string>("Username"),
                Password = configuration.GetSection("RabbitMqConnection").GetValue<string>("Password"),
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
        }      

        public virtual void Connect()        
        {
            var collector = (DataCollector)_serviceProvider.GetService(typeof(DataCollector));
            using (_connection = _factory.CreateConnection())
            using(_channel = _connection.CreateModel())
            {
                _channel.QueueDeclare(queue: "binance",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                _channel.BasicQos(prefetchSize: 0, prefetchCount: 3, global: false);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var tiket = JsonConvert.DeserializeObject<SymbolBookTicket>(message);                    
                    collector.Add(tiket);
                    _channel.BasicAck(ea.DeliveryTag, false);
                };

                _channel.BasicConsume(queue: "binance",
                                        autoAck: false,
                                        consumer: consumer);
            }
            
        }
    }
}
