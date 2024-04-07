using AuthMicroservice.DataAccess.Interfaces;
using AuthMicroservice.DataAccess.Repositories;
using AuthMicroservice.Model;
using AuthMicroservice.Services.Implentations;
using AuthMicroservice.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DbContext = AuthMicroservice.DataAccess.DbContext;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Add services to the container.
var config = new MapperConfiguration(conf =>
{
    conf.CreateMap<UserDto, User>();
    
});

var mapper = config.CreateMapper();
builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDatabase")));

builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AuthMicroservice.Services.Utility.HashingLogic>();
builder.Services.AddScoped<AuthMicroservice.Services.Utility.Authentication>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<DbContext>(options =>
    options.UseInMemoryDatabase("AuthDatabase"));
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
