using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Urls a Microservicioes
builder.Services.Configure<ApiUrls>(
        opts => builder.Configuration.GetSection("ApiUrls").Bind(opts)
    );

builder.Services.AddHttpClient<IIdentityProxy, IdentityProxy>();
builder.Services.AddHttpClient<IUserProxy, UserProxy>();
builder.Services.AddHttpClient<ITokenProxy, TokenProxy>();
builder.Services.AddHttpClient<IApplicationProxy, ApplicationProxy>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
app.UseAuthentication();

app.MapControllers();

app.Run();
