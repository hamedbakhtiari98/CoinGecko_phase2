using CoinGecko_Phase2.API;
using CoinGecko_Phase2.API.Health;
using CoinGecko_Phase2.API.Reposiroty;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

//builder.Services.AddTransient<IStudentServeice, StudentService>();
//builder.Services.AddTransient<ICryptoService, CryptoService>();
//builder.Services.AddTransient<IStudentRepository, StudentRepository>();
//builder.Services.AddTransient<ICryptoRepository, CryptoRepository>();

builder.Services.AddHealthChecks()
    .AddCheck<HealthDbConnection>("SqlServer")
    //.AddSqlServer(builder.Configuration["ConnectionStrings:MyConnectionString"])
    .AddCheck<HealthCheckConiGeckoApi>("ConinGeckoApi");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddTransient<StudentService, IStudentServeice>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoinGecko_Phase2.API", Version = "v1" });

    c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Name = "Bearer",
            In = ParameterLocation.Header,
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        },
        new string[]{ }
    }
});

    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type=ReferenceType.SecurityScheme,
    //                Id="Bearer"
    //            }
    //        },
    //        new string[]{}
    //    }
    //});

});

builder.Services.AddQuartz();
//builder.Services.AddDbContext<MyContext>(o =>
//{
//    o.UseSqlServer(config.GetConnectionString("Server=DESKTOP-G7IA4K7; Initial Catalog=StudentDb; Integrated Security=True; TrustServerCertificate=True"));
//});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddHostedService<AppHostService>();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(x =>
{
    x.TokenValidationParameters = new()
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireClaim("UserName", "Admin"));
});

//Console.WriteLine("Hello World");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/demo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.MapHealthChecks("/_health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}) ;

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
