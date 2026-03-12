using System.IO;
using CookingProject.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Берем строку подключения из конфига (там должна быть база SmarterASP)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ВКЛЮЧАЕМ SWAGGER ВСЕГДА (чтобы точно открылся на защите)
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cooking API V1");
    c.RoutePrefix = "swagger"; 
});

app.UseCors("AllowAll");

// НАСТРОЙКА СТАТИКИ (Простой и надежный вариант)
app.UseDefaultFiles(); // Позволяет открывать index.html или default.html автоматически
app.UseStaticFiles();  // Раздает файлы из папки wwwroot

app.UseRouting();
app.UseAuthorization();

// Перенаправляем пустой адрес на твой каталог (или на swagger, как хочешь)
app.MapGet("/", context => {
    context.Response.Redirect("/catalog.html");
    return Task.CompletedTask;
});

app.MapControllers();

app.Run();
