using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"], 
                                                    Port = int.Parse( _configuration["RabbitMQPort"] ) };
            try{
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("--> Connected to message bus. Conectado al bus de mensajes de Rabbit");

            }
            catch(Exception ex){
                Console.WriteLine($"--> No se pudo conectar al Bus de mensajes: {ex.Message}");
            }
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);
            if (_connection.IsOpen)
            {
                Console.WriteLine("--> Coneccion RabbitMQ Abierta. Enviando mensaje...");
                SendMessage(message);

            }
            else { 
                Console.WriteLine("--> Coneccion RabbitMQ Cerrada. NO Enviando mensaje...");

            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(  exchange: "trigger", 
                                    routingKey:"",
                                    body: body );
            Console.WriteLine($"--> Se ha enviando mesaje '{message}'");
        }

        public void Dispose()
        {
            Console.WriteLine($"--> Mensaje BUS Disposed.");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Conexion a RabbitMQ apagada (Shutdown)");
        }
    }
}