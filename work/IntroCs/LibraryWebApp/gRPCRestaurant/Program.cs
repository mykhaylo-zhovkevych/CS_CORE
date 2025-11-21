using gRPCRestaurant.Services;

namespace gRPCRestaurant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // app.MapGrpcService<GreeterService>();
            app.MapGet("/", () => "gRPC is working");

            app.Run();
        }
    }
}