using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PlannerApp.Models;
using PlannerApp.Services;

namespace PlannerApp.ViewModels
{
    public class MainContentViewModel : ViewModelBase
    {
        private readonly ApiService _apiService;

        public ObservableCollection<Todo> Todos { get; set; } = new();

        // Команды
        public ICommand AddTodoCommand { get; }
        public ICommand RemoveTodoCommand { get; }

        public MainContentViewModel()
        {
            _apiService = new ApiService("admin", "password");
            AddTodoCommand = new RelayCommand(async () => await AddTodo());
            RemoveTodoCommand = new RelayCommand<Guid>(async id => await RemoveTodoAsync(id));

            LoadTodosAsync();
        }
        
        private Todo _selectedTodo;
        public Todo SelectedTodo
        {
            get => _selectedTodo;
            set => SetProperty(ref _selectedTodo, value);
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

        protected virtual async Task AddTodo()
        {
            // Здесь можно использовать статические данные для тестирования
            var newTodo = new Todo
            {
                Title = "Новая задача",
                Description = "Описание задачи",
                Deadline = DateTime.Now.AddDays(1)
            };

            await _apiService.AddTodoAsync(newTodo.Title, newTodo.Description, newTodo.Deadline);
            await LoadTodosAsync();
        }

        private async Task RemoveTodoAsync(Guid id)
        {
            await _apiService.RemoveTodoAsync(id);
            await LoadTodosAsync();
        }
    }
}