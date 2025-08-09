using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DotNetEnv;
using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Repositories;
using BackEnd.Services;
using BackEnd.Models;
using BackEnd.Extensions;
using BackEnd.Middlewares;
using BackEnd.Interfaces.Services;
using BackEnd.Interfaces.Repositories;

//Copy api.env to enviroment
var aspEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
if (aspEnv == "Development" && File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "api.env")))
{
    Env.NoClobber().Load("api.env");
}

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ShopDbConnection") ?? throw new InvalidOperationException("Connection string 'ShopDbConnection' not found."); ;

builder.Services.AddControllersWithViews();

builder.Services.AddHttpLogging(_ => { });

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<JwtProvider>();

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtOptions"));

var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOption>() ?? throw new InvalidOperationException("JWT options not found in configuration.");
builder.Services.AddApiAuthentication(Options.Create(jwtOptions));

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.LogTo(Console.WriteLine);
    options.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var viteConnectionString = builder.Configuration.GetConnectionString("ViteConnection") ?? throw new InvalidOperationException("Connection string 'ViteConnection' not found."); ;
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocal",
//      policy => policy
//        .WithOrigins(viteConnectionString, "http://localhost:47378")
//        .AllowAnyHeader()
//        .AllowAnyMethod());
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseException();

app.UseHttpsRedirection();
app.MapStaticAssets();

app.UseRouting();

app.UseCors("AllowLocal");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
