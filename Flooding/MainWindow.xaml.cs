using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WinUIEx;

namespace Flooding;

[ObservableObject]
internal sealed partial class MainWindow
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
                    break;
                case nameof(_viewmodel.IsFloodingTimesLimited):
                    OnPropertyChanged(nameof(IsFloodingTimesNumberBoxEnabled));
                    break;
            }
        };
    }

    private bool IsFloodingTimesNumberBoxEnabled => !_viewmodel.IsFloodingTimesLimited;

    private string MasterButtonText => _viewmodel.IsFlooding ? "È¡Ïû" : "Æô¶¯";

    private ICommand MasterButtonCommand =>
        _viewmodel.IsFlooding ? _viewmodel.StopFloodingCommand : _viewmodel.BeginFloodCommand;
}