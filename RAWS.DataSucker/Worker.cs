using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RAWS.Common;

namespace RAWS.DataSucker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqConnectionOptions _options;
        private IConnection _connection;
        private IModel _channel;
        private ConnectionFactory _factory;
        public Worker(ILogger<Worker> logger, RabbitMqConnectionOptions options)
        {
            _options = options;
            _logger = logger;           
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _factory = new ConnectionFactory()
                {
                    HostName = _options.HostName,
                    UserName = _options.Username,
                    Password = _options.Password,
                    Port = AmqpTcpEndpoint.UseDefaultPort
                };
                using (_connection = _factory.CreateConnection())
                using (_channel = _connection.CreateModel())
                    _channel.QueueDeclare(queue: "binance",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                using (var socket = new ClientWebSocket())
                {
                    
                    Uri uri = new Uri("wss://stream.binance.com:9443/ws/!bookTicker");
                    var cts = new CancellationTokenSource();
                    await socket.ConnectAsync(uri, cts.Token);
                    await Task.Factory.StartNew(
                      async () =>
                      {
                          var rcvBytes = new byte[128];
                          var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                          while (socket.State == WebSocketState.Open)
                          {
                              WebSocketReceiveResult rcvResult = await socket.ReceiveAsync(rcvBuffer, cts.Token);
                              byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                              string rcvMsg = Encoding.UTF8.GetString(msgBytes);
                              _logger.LogInformation(rcvMsg);
                              _channel.BasicPublish(exchange: "binance", routingKey: "binance",  body: msgBytes, basicProperties: null);
                          }
                      }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                }
                
            }
        }
        
    }
}
