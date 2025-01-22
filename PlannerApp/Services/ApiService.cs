using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlannerApp.Models;

namespace PlannerApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(string username, string password)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5062/")
            };

            // Создаем Base64-строку и добавляем ее в заголовок Authorization
            var authString = $"{username}:{password}";
            var base64AuthString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64AuthString);
        }

        public async Task<List<Todo>> GetAllTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Todo/GetAllTodos");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Todo>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Todo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке задач: {ex.Message}");
                return new List<Todo>();
            }
        }

        public async Task AddTodoAsync(string title, string description, DateTime deadline)
        {
            var todo = new
            {
                Id = Guid.NewGuid(),  // Создание уникального идентификатора
                Title = title,
                Description = description,
                Deadline = deadline,
                IsCompleted = false,  // По умолчанию задача не выполнена
                TodoListId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),  // Используйте фиксированный или передаваемый список
                OwnerId = Guid.Parse("0247d914-9421-433c-93d4-e4eebbae4dcd")  // Укажите владельца задачи
            };

            var content = new StringContent(JsonSerializer.Serialize(todo), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Todo/AddTodo", content);
            response.EnsureSuccessStatusCode();
        }


        public async Task RemoveTodoAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Todo/DeleteTodo/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
