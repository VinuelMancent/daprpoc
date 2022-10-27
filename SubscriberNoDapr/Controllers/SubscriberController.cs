using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SubscriberNoDapr.Controllers;

[ApiController]
[Route("")]
public class SubscriberController : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private EventingBasicConsumer _consumer;

    public SubscriberController()
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        this._connection = factory.CreateConnection();
        this._channel = this._connection.CreateModel();
        this._channel.QueueDeclare(queue: "PublisherToSubscriber",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        this._channel.QueueBind("PublisherToSubscriber", "sender", "", null);
    }

    [HttpPost("receiveMessage")]
    public void ReceiveMessage([FromBody]string msg)
    {
        Console.WriteLine($"msg");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
        {
            _channel.Dispose();
            _connection.Dispose();
            return Task.CompletedTask;
        }
        this._consumer = new EventingBasicConsumer(this._channel);
        this._consumer.Received += (model, ea) =>
        {
            // read the message bytes
            var body = ea.Body.ToArray();
                
            // convert back to the original string
            // {index}|SuperHero{10000+index}|Fly,Eat,Sleep,Manga|1|{DateTime.UtcNow.ToLongDateString()}|0|0
            // is received here
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };
        _channel.BasicConsume(queue: "PublisherToSubscriber",
            autoAck: true,
            consumer: _consumer);
            
            
            
            
        return Task.CompletedTask;
    }
}