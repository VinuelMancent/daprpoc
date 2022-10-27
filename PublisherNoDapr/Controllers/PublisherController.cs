using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace PublisherNoDapr.Controllers;

[ApiController]
[Route("")]
public class PublisherController : ControllerBase
{
    private IModel channel;
    private int counter;
    
    public PublisherController()
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        var connection = factory.CreateConnection();
        this.channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "sender", 
            type: "fanout",
            durable: true);
        channel.QueueDeclare(queue: "PublisherToSubscriber",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        channel.QueueBind("PublisherToSubscriber", "sender", "", null);
        this.counter = 0;
    }

    [HttpPost("sendMessage")]
    public void SendMessage()
    {
        string message = $"Hello World {counter}!";
        var body = Encoding.UTF8.GetBytes(message);
        //Achtung: bisher nur publish über queue, nicht über exchange
        this.channel.BasicPublish(exchange: "sender",
            routingKey: "",
            basicProperties: null,
            body: body);
        counter++;
    }
}