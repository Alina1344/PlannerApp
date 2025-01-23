using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlannerApp.Models;


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



    private void ExecuteLogin()
    {
        if (Username == "admin" && Password == "password")
        {
            IsLoggedIn = true;
            ErrorMessage = string.Empty;
        }
        else
        {
            IsLoggedIn = false;
            ErrorMessage = "Неверный пароль! Попробуйте снова.";
        }
    }
}