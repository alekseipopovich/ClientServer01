using Microsoft.EntityFrameworkCore;
using WebAPI;

internal class Program
{
    private static void Main(string[] args)
    {
        // ������� ������� ��� ��������� ���������� (�������, ������������, DI)
        var builder = WebApplication.CreateBuilder(args);

        // ������������� SQLitePCL ��� EF Core SQLite
        SQLitePCL.Batteries.Init();

        // ����������� ApplicationContext � DI ����������
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

        // ����������� Swagger
        builder.Services.AddEndpointsApiExplorer(); // ��� ����� ���������� API
        builder.Services.AddSwaggerGen(); // ��������� Swagger-������������

        // ������ ��� ���������� ���������� �������, ��������� ���������, ��������� ��������, ������� ����������
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger(); // ���������� JSON-�������� Swagger
            app.UseSwaggerUI(); // ���������� ������������� UI
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // ������������� ���� ������ � ���������� �������
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            dbContext.Database.EnsureCreated(); // ������� ���� ������, ���� ��� ��� �� ����������
            dbContext.SeedData(); // �������� ����� ��� �������� ��������� ������
        }

        app.Run();
    }
}