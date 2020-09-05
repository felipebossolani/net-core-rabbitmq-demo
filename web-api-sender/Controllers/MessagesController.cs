using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using web_api_sender.Models;

namespace web_api_sender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Utilize o método Post para enviar uma mensagem";
        }

        [HttpPost]
        public object Post([FromServices]RabbitMQConfigurations configurations, [FromBody] MessageDTO message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configurations.HostName,
                Port = configurations.Port,
                UserName = configurations.UserName,
                Password = configurations.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "web-api-test-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string formattedMessage =
                    $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - {Guid.NewGuid()} - Conteúdo da Mensagem: {message.Content}";
                var body = Encoding.UTF8.GetBytes(formattedMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: "web-api-test-queue",
                                     basicProperties: null,
                                     body: body);
            }

            return new
            {
                Result = "Mensagem encaminhada com sucesso"
            };
        }
    }
}
