using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Storage;
using ConsoleApp5._4Remastered.Enum;
using LibraryAPI.Filters;
using LibraryAPI.Service;
using System.Text.Json.Serialization;

namespace LibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddControllers(o =>
                {
                    o.Filters.Add<HttpExceptionFilter>();
                })
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            builder.Services.AddProblemDetails();

            var library = new Library("Main Library", "123 Main St");
            library.AddShelf(new Shelf(1000));
            var InMemoryRepository = new InMemoryRepository(library);
            PolicyService.AddPolicy(UserType.ExternalUser, ItemType.BoardGame, new Policy("ExternalUser-BoardGame", extensions: 3, loanFees: 1.50m, loanPeriodInDays: 21));
            PolicyService.AddPolicy(UserType.Teacher, ItemType.Magazine, new Policy("Teacher-Magazine", extensions: 1, loanFees: 3.00m, loanPeriodInDays: 7));
            PolicyService.AddPolicy(UserType.Student, ItemType.Book, new Policy("Student-Book", extensions: 2, loanFees: 0.00m, loanPeriodInDays: 14));

            builder.Services.AddSingleton(InMemoryRepository);
            builder.Services.AddSingleton<LibraryService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
