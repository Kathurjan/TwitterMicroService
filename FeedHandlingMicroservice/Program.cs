using System.Text;
using AutoMapper;
using FeedHandlingMicroservice.App;
using FeedHandlingMicroservice.DataAccess;
using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using EasyNetQ;
using NetQ;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Add DbContext
builder.Services.AddDbContext<PostDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FeedDb")));

// Add AutoMapper
var config = new MapperConfiguration(conf =>
{
    conf.CreateMap<PostDto, Post>();
    conf.CreateMap<HashtagDto, Hashtag>();
});
builder.Services.AddSingleton(config.CreateMapper());

var connectionStr = "amqp://guest:guest@rabbitmq";

builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus(connectionStr)));
builder.Services.AddHostedService<MessageHandler>();
// Add services
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IHashTagRepo, HashTagRepo>();
builder.Services.AddScoped<IHashTagService, HashTagService>();
builder.Services.AddControllers();


// Configure authentication
var secretKey = builder.Configuration.GetValue<string>("AppSettings:Token");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), 
            ValidateIssuer = false,
            ValidateAudience = false,
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                // Log the authentication failure
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                // Log the success of token validation
                Console.WriteLine("Token validated successfully");
                return Task.CompletedTask;
            },
        };
    });

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
