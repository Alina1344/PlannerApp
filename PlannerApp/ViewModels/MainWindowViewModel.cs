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
        }
        else
        {
            // Логика обработки неверных данных
            IsLoggedIn = false;
        }
    }

    public string Greeting => "Добро пожаловать!";
}