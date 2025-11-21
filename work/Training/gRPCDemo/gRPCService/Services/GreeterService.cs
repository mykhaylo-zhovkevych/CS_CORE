using Grpc.Core;
using gRPCService;

namespace gRPCService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + "Address is " + request.Address + "Age is: " + request.Age,
                Response = "Successfully read the gRPC Server"
            });
        }
        public override Task<CountParameters> ReturnParameters(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CountParameters
            {
                Count = request.CalculateSize(),
            });
        }
    }
}
