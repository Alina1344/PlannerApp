using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PlannerApp.Models;
using PlannerApp.Services;

namespace PlannerApp.ViewModels
{
    public class MainContentViewModel : ViewModelBase
    {
        private readonly ApiService _apiService;

        public ObservableCollection<Todo> Todos { get; set; } = new();

        public MainContentViewModel()
        {
            _apiService = new ApiService();
            LoadTodosAsync();
        }

        private async Task LoadTodosAsync()
        {
            var todos = await _apiService.GetAllTodosAsync();
            Todos.Clear();
            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }
        }

        public async Task AddTodoAsync(string title, string description, DateTime deadline)
        {
            await _apiService.AddTodoAsync(title, description, deadline);
            await LoadTodosAsync();
        }

        public async Task RemoveTodoAsync(Guid id)
        {
            await _apiService.RemoveTodoAsync(id);
            await LoadTodosAsync();
        }
    }
}