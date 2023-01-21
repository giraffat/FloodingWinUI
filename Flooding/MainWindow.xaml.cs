using System;
using System.Windows.Input;
using Windows.System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinUIEx;

namespace Flooding;

[ObservableObject]
#pragma warning disable MVVMTK0033
public sealed partial class MainWindow
{
    private readonly MainWindowViewmodel _viewmodel = new();

    public MainWindow()
    {
        Title = "Ë¢ÆÁ";

        this.SetWindowSize(300, 500);
        this.SetIsAlwaysOnTop(true);
        this.SetIsResizable(false);
        this.SetIsMaximizable(false);
        this.SetIsMinimizable(false);

        InitializeComponent();

        _viewmodel.PropertyChanged += (_, args) =>
        {
            switch (args.PropertyName)
            {
                case nameof(_viewmodel.IsFlooding):
                    OnPropertyChanged(nameof(MasterButtonText));
                    OnPropertyChanged(nameof(MasterButtonCommand));

                    OnPropertyChanged(nameof(IsFloodTimesNumberBoxEnabled));
                    OnPropertyChanged(nameof(IsFloodingIntervalNumberBoxEnabled));
                    OnPropertyChanged(nameof(IsFloodTextTextBoxEnabled));
                    OnPropertyChanged(nameof(IsFloodingUnlimitedCheckBoxEnabled));
                    OnPropertyChanged(nameof(IsProgressBarIndeterminate));
                    break;
                case nameof(_viewmodel.IsFloodingUnlimited):
                    OnPropertyChanged(nameof(IsFloodTimesNumberBoxEnabled));
                    OnPropertyChanged(nameof(IsProgressBarIndeterminate));
                    break;
                case nameof(_viewmodel.FloodText):
                    OnPropertyChanged(nameof(IsMasterButtonEnabled));
                    break;
            }
        };
    }

    private bool IsFloodTimesNumberBoxEnabled => !(_viewmodel.IsFloodingUnlimited || _viewmodel.IsFlooding);

    private bool IsFloodingIntervalNumberBoxEnabled => !_viewmodel.IsFlooding;

    private bool IsFloodTextTextBoxEnabled => !_viewmodel.IsFlooding;

    private bool IsFloodingUnlimitedCheckBoxEnabled => !_viewmodel.IsFlooding;

    private bool IsProgressBarIndeterminate => _viewmodel.IsFloodingUnlimited && _viewmodel.IsFlooding;

    private string MasterButtonText => _viewmodel.IsFlooding ? "È¡Ïû" : "Æô¶¯";

    private ICommand MasterButtonCommand =>
        _viewmodel.IsFlooding ? _viewmodel.StopFloodingCommand : _viewmodel.BeginFloodCommand;

    private bool IsMasterButtonEnabled => _viewmodel.FloodText is not null && _viewmodel.FloodText != string.Empty;

    [RelayCommand]
    private async void OpenGithub()
    {
        await Launcher.LaunchUriAsync(new Uri("https://github.com/giraffat/FloodingWinUI"));
    }
}