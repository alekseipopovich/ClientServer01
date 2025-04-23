using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.DataProtection;
using Part03GrpcConsoleApp1;

// Генерируем JWT-токен
var token = TokenGenerator.GenerateJwtToken("Alice");

// Настраиваем заголовки с токеном
var headers = new Metadata
            {
                { "Authorization", $"Bearer {token}" }
            };

using var channel = GrpcChannel.ForAddress("http://localhost:5059");

// Название конкретного класса клиента зависит от определения сервиса и устанавливается по шаблону
// [имя_сервиса].[имя_сервиса]Client
var client = new Greeter.GreeterClient(channel);
var studentClient = new StudentService.StudentServiceClient(channel);
var client_s = new SecureService.SecureServiceClient(channel);

try
{
    // Вызываем защищённый метод
    var response = await client_s.GetSecretMessageAsync(
        new SecretRequest { RequestId = "12345" },
        headers);

    Console.WriteLine(response.Message);
}
catch (RpcException ex)
{
    Console.WriteLine($"Error: {ex.StatusCode}, {ex.Status.Detail}");
}

//Console.WriteLine("Press any key to exit...");
//Console.ReadKey();

//Console.Write("Введите ID студента: ");
//var id = Console.ReadLine();


//var student = studentClient.GetStudent(new GetStudentRequest { StudentId = Convert.ToInt32(id) });

//Console.WriteLine("Получен запрос по студенту: " + student.LastName);


Console.WriteLine("\nВывести всех студентов: ");
using var streamAllStudents = studentClient.GetAllStudents(new GetAllStudentRequest
{
    Limit = 1
});

await foreach(var studentResponse in streamAllStudents.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"{studentResponse.StudentId}: {studentResponse.FirstName} {studentResponse.LastName}");
}

////обмениваемся сообщениями с сервером
//var reply = await client.SayHelloAsync(new HelloRequest { Name = name });



//Console.WriteLine($"Ответ сервера: {reply.Message}");
Console.ReadKey();
