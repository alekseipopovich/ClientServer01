using Grpc.Core;
using Part03GrpcService1;
using System.Collections.Concurrent;

namespace Part03GrpcService1.Services;

public class StudentService : Part03GrpcService1.StudentService.StudentServiceBase
{
    private readonly ILogger<GreeterService> _logger;
    private static int _nextId = 1;
    private static readonly ConcurrentDictionary<int, StudentObjectResponse> _students = new();
    public StudentService(ILogger<GreeterService> logger)
    {
        _logger = logger;

        if (_students.IsEmpty)
        {
            SeedData();
        }
    }

    void SeedData()
    {
        var testListStudents = new List<StudentObjectRequest>
        {
            new() { FirstName = "Alex", LastName = "Orlov", Age = 18 },
            new() { FirstName = "Vasya", LastName = "Pupkin", Age = 20 },
            new() { FirstName = "Igor", LastName = "Kalinkin", Age = 20 },
            new() { FirstName = "Serg", LastName = "Sidorov", Age = 20 },
        };

        foreach (var s in testListStudents)
        {
            var id = _nextId++;
            _students.TryAdd(id, new StudentObjectResponse
            {
                StudentId = id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Age = s.Age
            });

        }

    }

    public override Task<StudentObjectResponse> GetStudent(GetStudentRequest request, ServerCallContext context)
    {
        if(_students.TryGetValue(request.StudentId, out var student))
        {
            return Task.FromResult(student);
        }

        throw new RpcException(new Status(StatusCode.NotFound, $"Student with ID {request.StudentId} not found"));
    }

    public override async Task GetAllStudents(GetAllStudentRequest request, IServerStreamWriter<StudentObjectResponse> responseStream, ServerCallContext context)
    {

        var limit = request.Limit > 0 ? request.Limit : int.MaxValue;
        var count = 0;

        foreach (var student in _students.Values)
        {
            if (count++ >= limit) break;

            await responseStream.WriteAsync(student);
            await Task.Delay(500); // Имитация задержки для наглядности
        }

        //return base.GetAllStudents(request, responseStream, context);
    }
}
