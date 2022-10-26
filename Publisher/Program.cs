var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient(builder => builder
    .UseHttpEndpoint($"http://localhost:3601")
    .UseGrpcEndpoint($"http://localhost:60001"));
builder.Services.AddHttpClient();
builder.Services.AddControllers();
var app = builder.Build();



// configure routing
app.MapControllers();
app.UseCloudEvents();
app.MapSubscribeHandler();

app.Run("http://0.0.0.0:6001");