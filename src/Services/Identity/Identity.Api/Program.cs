using Identity.Api.Helpers;
using Identity.Persistence.Database;
using Identity.Service.Proxies;
using Identity.Service.Proxies.User;
using Identity.Service.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Clases
builder.Services.AddTransient<IIdentityQueryService, IdentityQueryService>();
builder.Services.AddHttpClient<IUserProxy, UserProxy>();
string? key = builder.Configuration.GetSection("JWTKey")["key"];
builder.Services.AddSingleton<IJwtAuthenticationService>(new JwtAuthenticationService(key));

//BDD
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseNpgsql(builder.Configuration.GetConnectionString("VerificaConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                );

//Urls
builder.Services.Configure<ApiUrls>(
        opts => builder.Configuration.GetSection("ApiUrls").Bind(opts)
    );

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
