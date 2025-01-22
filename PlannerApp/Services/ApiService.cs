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

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5062/")
            };
        }

        public async Task<List<Todo>> GetAllTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/todos");
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
                Title = title,
                Description = description,
                Deadline = deadline
            };

            var content = new StringContent(JsonSerializer.Serialize(todo), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/todos", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveTodoAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/todos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}