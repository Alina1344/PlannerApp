using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace PlannerApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private bool _isLoggedIn;
    
    [ObservableProperty]
    private string _errorMessage;


    public ICommand LoginCommand { get; }

    public MainWindowViewModel()
    {
        LoginCommand = new RelayCommand(ExecuteLogin);
    }

    private void ExecuteLogin()
    {
        if (Username == "admin" && Password == "password")
        {
            IsLoggedIn = true;
            ErrorMessage = string.Empty;  // Очистить сообщение об ошибке
        }
        else
        {
            IsLoggedIn = false;
            ErrorMessage = "Неверный пароль! Попробуйте снова.";  // Установить сообщение об ошибке
        }
    }


    public string Greeting => "Добро пожаловать!";
}