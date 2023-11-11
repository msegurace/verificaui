using Application.Persistence.Database;
using Application.Service.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Clases
builder.Services.AddTransient<IApplicationQueryService, ApplicationQueryService>();

//BDD
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseNpgsql(builder.Configuration.GetConnectionString("VerificaConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                );

//MEDIATOR
var mediaConf = new MediatRServiceConfiguration();
mediaConf.RegisterServicesFromAssembly(Assembly.Load("Application.Service.EventHandlers"));
builder.Services.AddMediatR(mediaConf);

var secretKey = Encoding.ASCII.GetBytes(
               builder.Configuration?.GetValue<string>("JWTKey")!
           );
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

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
