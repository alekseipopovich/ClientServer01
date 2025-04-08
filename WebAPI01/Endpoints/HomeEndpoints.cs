using Microsoft.AspNetCore.Http;

namespace GradesApi.Endpoints;

public static class HomeEndpoints
{
    public static void MapHomeEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => Results.Text(@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>Система учета оценок</title>
                <style>
                    body { font-family: Arial, sans-serif; margin: 40px; }
                    .nav-section { margin: 20px 0; }
                    h2 { color: #2c3e50; }
                    a { color: #3498db; text-decoration: none; }
                    a:hover { text-decoration: underline; }
                </style>
            </head>
            <body>
                <h1>Система учета оценок студентов</h1>
                
                <div class='nav-section'>
                    <h2>Студенты</h2>
                    <p><a href='/students'>Список всех студентов</a></p>
                </div>

                <div class='nav-section'>
                    <h2>Предметы</h2>
                    <p><a href='/subjects'>Список всех предметов</a></p>
                </div>

                <div class='nav-section'>
                    <h2>Оценки</h2>
                    <p><a href='/grades'>Все оценки</a></p>
                </div>

                <div class='nav-section'>
                    <h2>API Документация</h2>
                    <p><a href='/swagger'>Swagger UI</a></p>
                </div>
            </body>
            </html>
        ", "text/html; charset=utf-8"));
    }
} 