using System.ComponentModel.DataAnnotations;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace InvocationService1.Controllers;

[ApiController]
[Route("")]
public class SenderController : ControllerBase
{

    [HttpPost("sendMessage")]
    public async Task<ActionResult> SendInvocation([FromHeader][Required] string appId, [FromHeader][Required] string methodName)
    {
        DaprClientBuilder b = new DaprClientBuilder();
        b.UseGrpcEndpoint("http://localhost:60004");
        b.UseHttpEndpoint("http://localhost:3604");
        using var client = b.Build();
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken cancellationToken = source.Token;
        //Using Dapr SDK to invoke a method
        Console.WriteLine($"Sending request to appId '{appId}' with methodName '{methodName}'.");
        //await client.InvokeMethodAsync(HttpMethod.Post, appId, methodName, cancellationToken);
        var header = new Dictionary<string, string>();
        header.Add("message", "test");
        var result = client.CreateInvokeMethodRequest(
            httpMethod: HttpMethod.Post, 
            appId: appId,
            methodName: methodName,
            data: header);
        result.Headers.Add("message", "test");
        await client.InvokeMethodAsync(result);
        Console.WriteLine("Sent message successfully");
        return Ok();
    }
}