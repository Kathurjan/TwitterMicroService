using RabbitMq.RabbitMqIServices;
using UserInteractionMicroservice.Interface;
using UserInteractionMicroservice.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
var queueName = "UserInteractionQueue"; 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddScoped<INotificationService, NotificationService>();

// Register your RabbitMQ receiver
RabbitMq.DependencyResolver.DependencyResolverRabbitMq.RegisterRabbitMqLayer(builder.Services, queueName);



var app = builder.Build();


app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});



    
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Resolve IRabbitMqReceiver within a scope
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var rabbitMqReceiver = serviceProvider.GetRequiredService<IRabbitMqReceiver>();

    // Start receiving messages
    rabbitMqReceiver.Receive();
}
app.MapHub<NotificationSocket>("/SocketNotification");
app.Run();
