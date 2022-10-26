using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Publisher.Events;
using Publisher.Models;

namespace Publisher.Controllers;

[ApiController]
[Route("")]
public class PublisherController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PublisherController> _logger;

    public PublisherController(ILogger<PublisherController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpPost("sendMessage")]
    public async Task<ActionResult> SendMessage(MessageReceived msg, [FromServices] DaprClient daprClient)
    {
        await daprClient.PublishEventAsync("pubsub", "sender", msg);

        return Ok();
    }
}