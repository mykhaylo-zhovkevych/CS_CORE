using ConsoleApp5._4Remastered.Enum;
using LibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client
{
    record UserResponseDto(Guid Id, string Name, UserType UserType);
    record ItemResponseDto(Guid Id, string Name, ItemType ItemType, bool IsBorrowed);
    record BorrowingResponseDto(Guid UserId, Guid ItemId, string Message);

    class Program
    {
        private static string BaseUrl = "http://localhost:5108";
        private static readonly HttpClient http = new HttpClient();
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        static async Task Main(string[] args)
        {
            Console.WriteLine("Client App");
            Console.WriteLine($"API base: {BaseUrl}\n");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Choose Option:");
                Console.WriteLine("1. Create new User");
                Console.WriteLine("2. Create new Item");
                Console.WriteLine("3. Borrow Item");
                Console.WriteLine("4. Show active borrowings");
                Console.WriteLine("0. Exit");

                var key = Console.ReadLine();

                try
                {
                    switch (key)
                    {
                        case "1":
                            await CreateUserAsync();
                            break;
                        case "2":
                            await CreateItemAsync();
                            break;
                        case "3":
                            await BorrowItemAsync();
                            break;
                        case "4":
                            await GetBorrowingsAsync();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        static async Task CreateUserAsync()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Choose UserType: ");
            foreach (var v in Enum.GetValues(typeof(UserType)))
            {
                Console.WriteLine($" - {v}");
            }
            Console.Write("UserType: ");

            var utInput = Console.ReadLine();

            if (!Enum.TryParse<UserType>(utInput, true, out var userType))
            {
                Console.WriteLine("Invalid UserType");
                return;
            }

            var dto = new CreateUserDto(name, userType);
            var result = await PostJsonAsync<UserResponseDto>($"{BaseUrl}/api/library/newuser", dto);

            Console.WriteLine($"Status: {result.StatusCode}");
            if (result.IsSuccess && result.Data is not null)
            {
                Console.WriteLine($"User created: Id={result.Data.Id}, Name={result.Data.Name}, Type={result.Data.UserType}");
            }
            else
            {
                Console.WriteLine("No answer from Server");
                Console.WriteLine(result.RawBody);
            }
        }
        static async Task CreateItemAsync()
        {
            Console.WriteLine("Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Choose ItemType: ");

            foreach (var i in Enum.GetValues(typeof(ItemType)))
            {
                Console.WriteLine($" - {i}");
            }
            Console.Write("ItemType: ");

            var itInput = Console.ReadLine();

            if (!Enum.TryParse<ItemType>(itInput, true, out var itemType))
            {
                Console.WriteLine("Invalid ItemType");
                return;
            }

            var dto = new CreateItemDto(name, itemType);
            var result = await PostJsonAsync<ItemResponseDto>($"{BaseUrl}/api/library/newitem", dto);

            Console.WriteLine($"Status: {result.StatusCode}");
            if (result.IsSuccess && result.Data is not null)
            {
                Console.WriteLine($"Item created: Id={result.Data.Id}, Name={result.Data.Name}, Type={result.Data.ItemType}");
            }
            else
            {
                Console.WriteLine("No answer from Server");
                Console.WriteLine(result.RawBody);
            }
        }

        static async Task BorrowItemAsync()
        {
            Console.Write("UserId: ");
            if (!Guid.TryParse(Console.ReadLine(), out var userId))
            {
                Console.WriteLine("Invalid ID");
                return;
            }

            Console.Write("ItemId: ");
            if (!Guid.TryParse(Console.ReadLine(), out var itemId))
            {
                Console.WriteLine("Invalid ID");
                return;
            }

            var dto = new BorrowItemDto(userId, itemId);
            var result = await PostJsonAsync<BorrowingResponseDto>($"{BaseUrl}/api/library/newborrowing", dto);

            Console.WriteLine($"Status: {result.StatusCode}");

            if (result.IsSuccess && result.Data is not null)
            {
                Console.WriteLine($"Message: {result.Data.Message}");
            }
            else
            {
                Console.WriteLine("No answer from Server");
                Console.WriteLine(result.RawBody);
            }
        }

        static async Task GetBorrowingsAsync()
        {
            Console.Write("UserId ID: ");
            if (!Guid.TryParse(Console.ReadLine(), out var userId))
            {
                Console.WriteLine("Invalid ID");
                return;
            }

            var url = $"{BaseUrl}/api/library/{userId}";

            var result = await GetAsync<List<BorrowingResponseDto>>($"{BaseUrl}/api/library/{userId}");

            Console.WriteLine($"Status: {result.StatusCode}");
            if (result.IsSuccess && result.Data is { Count: > 0 })
            {
                Console.WriteLine($"Active Borrowings ({result.Data.Count}):");
                foreach (var b in result.Data)
                {
                    Console.WriteLine($" - ItemId={b.ItemId}, Message={b.Message}");
                }
            }
            else
            {
                Console.WriteLine("No borrowings found:");
                Console.WriteLine(result.RawBody);
            }

        }


        static async Task<HttpResult<T>> PostJsonAsync<T>(string url, object dto)
        {
            var json = JsonSerializer.Serialize(dto);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await http.PostAsync(url, content);
            // Returning a new String Objectm, from JSON to Object
            var body = await response.Content.ReadAsStringAsync();
            T? data = default;
            if (response.IsSuccessStatusCode)
            {
                data = JsonSerializer.Deserialize<T>(body, jsonOptions);
            }
            return new HttpResult<T>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode,
                Data = data,
                RawBody = body
            };
        }

        static async Task<HttpResult<T>> GetAsync<T>(string url)
        {
            using var resp = await http.GetAsync(url);
            var body = await resp.Content.ReadAsStringAsync();
            T? data = default;
            try 
            { 
                data = JsonSerializer.Deserialize<T>(body, jsonOptions); 
            } 
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialize failed: {ex.Message}");
            }
            return new HttpResult<T>
            {
                IsSuccess = resp.IsSuccessStatusCode,
                StatusCode = (int)resp.StatusCode,
                Data = data,
                RawBody = body
            };
        }
    }
}