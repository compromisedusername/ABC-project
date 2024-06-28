using System.Text;
using ABC.Controllers;
using ABC.Data;
using ABC.Middlewares;
using ABC.Repositories.Addresses;
using ABC.Repositories.Clients;
using ABC.Repositories.Contracts;
using ABC.Repositories.Discounts;
using ABC.Repositories.Payment;
using ABC.Repositories.SoftwareSystems;
using ABC.Services;
using ABC.Services.Clients;
using ABC.Services.Contracts;
using ABC.Services.ExchangeRates;
using ABC.Services.Revenue;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var movieApiKey = builder.Configuration["Rates:ServiceApiKey"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]));
var issuer = (builder.Configuration["Jwt:Issuer"]);
var audience = (builder.Configuration["Jwt:Audience"]);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<IContractsRepository, ContractsRepository>();
builder.Services.AddScoped<ISoftwareSystemsRepository, SoftwareSystemsRepository>();
builder.Services.AddScoped<IDiscountsRepository, DiscountsRepository>();
builder.Services.AddScoped<IAddressesRepository, AddressesRepository>();
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IContractsService, ContractsService>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();
builder.Services.AddScoped<IRevenueService, RevenueService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddDbContext<AppDatabaseContext>(
    options =>
    {
        options.UseSqlServer("Name=ConnectionStrings:Default");
    },ServiceLifetime.Scoped);




builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {


        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2),
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = key
        };

        opt.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-expired", "true");
                }

                return Task.CompletedTask;
            }
        };
    })
    .AddJwtBearer("IgnoreTokenExpirationScheme",opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,   //by who
            ValidateAudience = true, //for whom
            ValidateLifetime = false,
            ClockSkew = TimeSpan.FromMinutes(2),
            ValidIssuer = issuer, //should come from configuration
            ValidAudience = audience, //should come from configuration
            IssuerSigningKey = key
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

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();
//app.UseMiddleware<BasicAuthMiddleware>();

app.MapControllers();

app.Run();

