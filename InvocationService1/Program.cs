var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient(builder => builder
    .UseHttpEndpoint($"http://localhost:3604")
    .UseGrpcEndpoint($"http://localhost:60004"));
// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.UseCloudEvents();
app.MapSubscribeHandler();
app.Run("http://0.0.0.0:6004");