using SubscriberNoDapr.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<SubscriberController>();
builder.Services.AddControllers();
var app = builder.Build();



// configure routing
app.MapControllers();

app.Run("http://0.0.0.0:6002");