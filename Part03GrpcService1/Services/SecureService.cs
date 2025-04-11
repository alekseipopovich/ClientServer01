using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Part03GrpcService1;

namespace Part03GrpcService1.Services
{
    [Authorize]
    public class SecureService : Part03GrpcService1.SecureService.SecureServiceBase // Fix: Explicitly qualify the base class with the namespace
    {
        private readonly ILogger<SecureService> _logger;

        public SecureService(ILogger<SecureService> logger)
        {
            _logger = logger;
        }

        public override Task<SecretResponse> GetSecretMessage(SecretRequest request, ServerCallContext context)
        {
            // Получаем имя пользователя из токена
            var userName = context.GetHttpContext().User.Identity?.Name ?? "Unknown";

            return Task.FromResult(new SecretResponse
            {
                Message = $"Secret message for {userName}, request ID: {request.RequestId}"
            });
        }
    }
}
