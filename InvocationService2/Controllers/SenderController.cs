using System.ComponentModel.DataAnnotations;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace InvocationService2.Controllers;

[ApiController]
[Route("")]
public class SenderController : ControllerBase
{

    [HttpPost("receiveMessage")]
    public async Task<ActionResult> ReceiveInvocation([FromBody]string message)
    {
        Console.WriteLine($"Message: {message}");
        return Ok();
    }
}