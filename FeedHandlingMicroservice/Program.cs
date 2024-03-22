using System.Text;
using AutoMapper;
using FeedHandlingMicroservice.App;
using FeedHandlingMicroservice.DataAccess;
using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FeedDb")));
builder.Services.AddControllers();
var config = new MapperConfiguration(conf =>
{
    conf.CreateMap<PostDto, Post>();
    
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey")),
            ValidateIssuer = false, // Set to true and specify if you have a specific issuer
            ValidateAudience = false, // Set to true and specify if you have a specific audience
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
