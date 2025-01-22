using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using PlannerApp.Views;

namespace PlannerApp.ViewModels;

public partial class LoginWindowViewModel : ViewModelBase
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ICommand LoginCommand { get; }

    public LoginWindowViewModel()
    {
        LoginCommand = new RelayCommand(ExecuteLogin);
    }

    private void ExecuteLogin()
    {
        if (Username == "admin" && Password == "password")
        {
            // Логика перехода к главному окну
            // Создаём главное окно
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Закрываем текущее окно
            var currentWindow = this; // если вы находитесь в коде окна
            currentWindow.Close();
        }
    }
}
