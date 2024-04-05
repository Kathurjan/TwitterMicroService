using Application.Interfaces;
using Application.Services;
using Sockets;
using Infrastructure.Helpers;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using EasyNetQ;
using NetQ;

var builder = WebApplication.CreateBuilder(args);

var connectionStr = "amqp://guest:guest@localhost";

builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus(connectionStr)));
builder.Services.AddHostedService<MessageHandler>();

var _connectionStringUseSqlServer = builder.Configuration.GetValue<string>("ConnectionStrings:AuthDatabase");
builder.Services.AddDbContext<UserInteractionDbContext>(options =>
    options.UseSqlServer(_connectionStringUseSqlServer));

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
Infastructure.DependencyResolver.DependencyResolverInfrastruce.RegistInfrastructure(builder.Services);

builder.Services.Configure<InfrastructureSettings>(builder.Configuration.GetSection("InfrastructureSettings"));

//var _connectionString = builder.Configuration.GetValue<string>("InfrastructureSettings:DefaultConnection");
//builder.Services.AddDbContext<DbContextManagement>(options =>
//   options.UseSqlite(_connectionString));

var app = builder.Build();


app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationSocket>("/SocketNotification");
app.Run();
