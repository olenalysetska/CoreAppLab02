using AppCore.Repositories;
using AppCore.Services;
using AutoMapper;
using Infrastructure.Memory;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// --- СЕКЦИЯ 1: РЕГИСТРАЦИЯ (До builder.Build()) ---

builder.Services.AddControllers(); // Чтобы работали твои контроллеры API
builder.Services.AddOpenApi(); // Поддержка OpenAPI/Swagger

// Регистрация твоих хранилищ (Repositories)
builder.Services.AddSingleton<IPersonRepository, MemoryPersonRepository>();
builder.Services.AddSingleton<ICompanyRepository, MemoryCompanyRepository>();
builder.Services.AddSingleton<IOrganizationRepository, MemoryOrganizationRepository>();

// Регистрация Unit of Work
builder.Services.AddSingleton<IContactUnitOfWork, MemoryContactUnitOfWork>();

// Регистрация Сервиса
builder.Services.AddSingleton<IPersonService, MemoryPersonService>();
builder.Services.AddControllers();

// --- СЕКЦИЯ 2: СБОРКА ПРИЛОЖЕНИЯ (Один раз!) ---

var app = builder.Build();

// --- СЕКЦИЯ 3: НАСТРОЙКА ПРАВИЛ (Middleware) ---

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();

// Маппинг контроллеров — это "связывает" URL с твоим ContactsController
app.MapControllers();

// Тестовый эндпоинт погоды (можно оставить для проверки)
var summaries = new[]
    { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();
    return forecast;
}).WithName("GetWeatherForecast");

// --- СЕКЦИЯ 4: ЗАПУСК (Один раз!) ---

app.Run();

// Рекорд для погоды (всегда в самом низу)
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}