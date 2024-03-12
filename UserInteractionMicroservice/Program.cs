using RabbitMq.RabbitMqIServices;
using Application.Interfaces;
using Application.Services;
using Sockets;
using Infrastructure.Helpers;
using Infrastructure.Contexts;
using RabbitMq.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
RabbitMq.DependencyResolver.DependencyResolverRabbitMq.RegisterRabbitMqLayer(builder.Services);
Infastructure.DependencyResolver.DependencyResolverInfastructure.RegisterInfastructureLayer(builder.Services);

builder.Services.Configure<InfrastructureSettings>(builder.Configuration.GetSection("InfrastructureSettings"));
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

var _connectionString = builder.Configuration.GetValue<string>("InfrastructureSettings:DefaultConnection");
builder.Services.AddDbContext<DbContextManagement>(options =>
    options.UseSqlite(_connectionString));


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
