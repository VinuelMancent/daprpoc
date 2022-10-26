using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Events;

namespace Subscriber.Controllers;

[ApiController]
[Route("")]
public class SubscriberController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SubscriberController> _logger;

    public SubscriberController(ILogger<SubscriberController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpPost("receiveMessage")]
    [Topic("pubsub", "sender")]
    public void ReceiveMessage([FromBody]MessageReceived msg)
    {
        Console.WriteLine($"{msg.sender}: {msg.message}");
    }
}