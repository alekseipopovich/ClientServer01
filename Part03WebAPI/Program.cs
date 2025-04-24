using Microsoft.EntityFrameworkCore;
using WebAPI;

internal class Program
{
    private static void Main(string[] args)
    {
        // Создние объекта для настройки приложения (сервисы, конфигурация, DI)
        var builder = WebApplication.CreateBuilder(args);

        // Инициализация SQLitePCL для EF Core SQLite
        SQLitePCL.Batteries.Init();

        // Регистрация ApplicationContext в DI контейнере
        builder.Services.AddDbContext<ApplicationContext>(
            options =>
            {
                var config = builder.Configuration;
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // Регистрация Swagger
        builder.Services.AddEndpointsApiExplorer(); // Для сбора метаданных API
        builder.Services.AddSwaggerGen(); // Генерация Swagger-документации

        // Объект для управления обработкой запроса, установки маршрутов, получения сервисов, запуска приложения
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger(); // Генерирует JSON-документ Swagger
            app.UseSwaggerUI(); // Отображает интерактивный UI
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Инициализация базы данных и заполнение данными
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            dbContext.Database.EnsureCreated(); // Создает базу данных, если она еще не существует
            dbContext.SeedData(); // Вызывает метод для внесения начальных данных
        }

        app.Run();
    }
}