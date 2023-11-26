using Azure.Core;
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
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddScoped<IStudentServeice, StudentService>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();

builder.Services.AddHealthChecks()
    .AddCheck<HealthDbConnection>("SqlServer")
    // .AddSqlServer(builder.Configuration["ConnectionStrings:MyStudentDbConnectionString"])
    .AddCheck<HealthCheckConiGeckoApi>("ConinGeckoApi");
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoinGecko_Phase2.API", Version = "v1" });

    //c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Description = "Please enter a valid token",
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.Http,
    //    BearerFormat = "JWT",
    //    Scheme = "Bearer"
    //});

    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Name = "Bearer",
    //            In = ParameterLocation.Header,
    //            Reference = new OpenApiReference
    //            {
    //                Id = "Bearer",
    //                Type = ReferenceType.SecurityScheme
    //            }
    //        },
    //        new string[]{ }
    //    }
    //});
});

builder.Services.AddQuartz();

builder.Services.AddDbContext<MyContext>();

builder.Services.AddDbContext<MyContextCrypto>();


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
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.Console()
//    .WriteTo.File("logs/demo.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

builder.Services.AddCors(p => p.AddPolicy("corsPplicy", build =>
{
    build.WithOrigins("https://www.google.com").AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromSeconds(10)); ///کاربرد این رو نفهمیدم دقیقا : الان فهمیدم منتظر میمونه که پاسخ از سمت سرور بیاد که اجازه اوریجین ریکوئست هست یا خیر. اگر بذاریم یک میکرو ثانیه با اینکه دسترسی دادیم . ارور میگیریم.
}));

/////// فردا سه تا چیز رو به حسین بگو حتما
///1. اینکه درخواست ارسال میشه، داده ها هم گرفته میشن و بعدش مرورگر استپ میکنه
///2. تنظیمات Cors برای مروگر ها هستش نه برای نرم افزارهایی مثل Postman
///3. لینک زیر
///4. دیروز تنظیمات میدلور ها کار رو خراب کرده بود
///5. کنسول پست من
////درباره اینکه چرا تو پست من کار نمیکنه//////////////////////////////////////https://stackoverflow.com/questions/36250615/cors-with-postman 
////درباره اینکه چرا تو پست من کار نمیکنه//////////////////////////////////////https://requestly.io/blog/how-postman-web-handles-cors/
//// در حالت عادی پست من روی چه origin کار میکنه


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
});



//app.UseCors

app.UseRouting();

app.UseCors("corsPplicy");

app.UseAuthentication();
app.UseAuthorization();

//app.Run(async context =>
//{
//    Console.WriteLine(context.Request.Headers.Origin);
//    Console.WriteLine(context.Request.Headers.AccessControlAllowOrigin);
//    Console.WriteLine(context.Request.Headers.Server);
//    Console.WriteLine(context.Request.Headers.AccessControlRequestHeaders);
//});
//////// توجه که این تیکه کد رو نباید اینجا بذارید وگرنه که برنامه اصلا به End point نمیرسد

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

app.Run(async context =>
{
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine(context.Request.Headers.Origin);
    Console.WriteLine(context.Request.Headers.AccessControlAllowOrigin);
    Console.WriteLine(context.Request.Headers.Server);
    Console.WriteLine(context.Request.Headers.AccessControlRequestHeaders);
    Console.WriteLine("--------------------------------------------");
});


