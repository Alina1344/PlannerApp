using CommunityToolkit.Mvvm.ComponentModel;

namespace PlannerApp.ViewModels;

using PlannerApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Title => "Планировщик задач";
    public MainContentViewModel ContentViewModel { get; }

    public string Greeting => "Welcome to the Planner!";

    public MainWindowViewModel()
    {
        ContentViewModel = new MainContentViewModel();
    }
}

