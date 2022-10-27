using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace InvocationService1.Controllers;

[ApiController]
[Route("")]
public class SenderController : ControllerBase
{

    [HttpPost("sendMessage")]
    public async Task<ActionResult> SendInvocation([FromHeader] string appId, [FromHeader] string methodName)
    {
        using var client = new DaprClientBuilder().Build();
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken cancellationToken = source.Token;
        //Using Dapr SDK to invoke a method
        var result = client.CreateInvokeMethodRequest(HttpMethod.Get, appId, methodName, cancellationToken);
        await client.InvokeMethodAsync(result);
        Console.WriteLine("Sent message successfully");
        return Ok();
    }
}