using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Data;
using OnlineShop.Interfaces;
using OnlineShop.Repositories;
using OnlineShop.Services;
using OnlineShop.Models;
using OnlineShop.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ShopDbConnection") ?? throw new InvalidOperationException("Connection string 'ShopDbConnection' not found."); ;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProductsRepository>();
builder.Services.AddScoped<OrdersRepository>();
builder.Services.AddScoped<UsersRepository>();

builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<OrdersService>();
builder.Services.AddScoped<UsersService>();

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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal",
      policy => policy
        .WithOrigins(viteConnectionString)
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowLocal");

app.UseSwagger();
app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
