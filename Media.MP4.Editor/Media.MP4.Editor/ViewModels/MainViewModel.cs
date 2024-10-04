using CommunityToolkit.Mvvm.ComponentModel;
using Media.MP4.Editor.ViewModels;

namespace Vipl.Media.MP4.Editor.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
}