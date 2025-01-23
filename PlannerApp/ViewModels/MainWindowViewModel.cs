using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlannerApp.Models;
using PlannerApp.Services;


namespace PlannerApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly MainContentViewModel _mainContentViewModel;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private bool _isLoggedIn;

    [ObservableProperty]
    private string _errorMessage;
    
    public MainContentViewModel ContentViewModel { get; } = new MainContentViewModel();


    public MainWindowViewModel()
    {
        _mainContentViewModel = new MainContentViewModel();
        LoginCommand = new RelayCommand(ExecuteLogin);
    }
    
    // Прокси-свойство для SelectedTodo
    public Todo SelectedTodo
    {
        get => ContentViewModel.SelectedTodo;
        set => ContentViewModel.SelectedTodo = value;
    }

    public IRelayCommand LoginCommand { get; }
    
    [ObservableProperty]
    private string greeting = "И КАКИЕ У НАС СЕГОДНЯ ДЕЛА?";



    private async void ExecuteLogin()
    {
        try
        {
            //var apiService = new ApiService("", "");
            var apiService = new ApiService(Username, Password);

            var isAuthenticated = await apiService.AuthenticateAsync(Username, Password);

            if (isAuthenticated)
            {
                IsLoggedIn = true;
                ErrorMessage = string.Empty;
                ContentViewModel.InitializeApiService(Username, Password);
            }
            else
            {
                IsLoggedIn = false;
                ErrorMessage = "Неверный логин или пароль! Попробуйте снова.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка входа: {ex.Message}";
        }
    }
}