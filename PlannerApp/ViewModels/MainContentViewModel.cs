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

    // Новые свойства для данных задачи
    private string _newTodoTitle;
    public string NewTodoTitle
    {
        get => _newTodoTitle;
        set => SetProperty(ref _newTodoTitle, value);
    }

    private string _newTodoDescription;
    public string NewTodoDescription
    {
        get => _newTodoDescription;
        set => SetProperty(ref _newTodoDescription, value);
    }
    
    // Команды
    public ICommand AddTodoCommand { get; }
    public ICommand RemoveTodoCommand { get; }

    public MainContentViewModel()
    {
        _apiService = new ApiService("admin", "password");
        AddTodoCommand = new RelayCommand(async () => await AddTodo());
        RemoveTodoCommand = new RelayCommand(async () => await RemoveTodo());


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
    
    private DateTimeOffset? _newTodoDeadline = DateTimeOffset.Now.AddDays(1); // Значение по умолчанию
    public DateTimeOffset? NewTodoDeadline
    {
        get => _newTodoDeadline;
        set => SetProperty(ref _newTodoDeadline, value);
    }


    protected virtual async Task AddTodo()
    {
        if (string.IsNullOrWhiteSpace(NewTodoTitle) || string.IsNullOrWhiteSpace(NewTodoDescription))
        {
            Console.WriteLine("Введите заголовок и описание задачи.");
            return;
        }

        if (NewTodoDeadline == null)
        {
            Console.WriteLine("Не выбран дедлайн задачи.");
            return;
        }
        
        // Преобразуем DateTimeOffset в DateTime перед отправкой в API
        var deadline = NewTodoDeadline?.DateTime ?? DateTime.Now.AddDays(1);

        // Передаем значение DateTime (не Nullable)
        await _apiService.AddTodoAsync(NewTodoTitle, NewTodoDescription, deadline);
        await LoadTodosAsync();

        // Очистка полей ввода после добавления задачи
        NewTodoTitle = string.Empty;
        NewTodoDescription = string.Empty;
        NewTodoDeadline = DateTime.Now.AddDays(1); // Сбрасываем на значение по умолчанию
    }
    
    private async Task RemoveTodo()
    {
        if (SelectedTodo == null)
        {
            Console.WriteLine("Не выбрана задача для удаления.");
            return;
        }
        await _apiService.RemoveTodoAsync(SelectedTodo.Id);
        await LoadTodosAsync();
    }

}

}