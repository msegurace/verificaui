using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Users.Persistence.Database;
using Users.Service.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Clases
builder.Services.AddTransient<IUsuarioQueryService, UsuarioQueryService>();

//BDD
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseNpgsql(builder.Configuration.GetConnectionString("VerificaConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                );

//MEDIATOR
var mediaConf = new MediatRServiceConfiguration();
mediaConf.RegisterServicesFromAssembly(Assembly.Load("User.Service.EventHandlers"));
builder.Services.AddMediatR(mediaConf);


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
