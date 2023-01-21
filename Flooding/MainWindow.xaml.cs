using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WinUIEx;

namespace Flooding;

[ObservableObject]
#pragma warning disable MVVMTK0033
public sealed partial class MainWindow
#pragma warning restore MVVMTK0033
{
    private readonly MainWindowViewmodel _viewmodel = new();

    public MainWindow()
    {
        Title = "ˢ��";

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
            }
        };
    }

    private bool IsFloodTimesNumberBoxEnabled => !(_viewmodel.IsFloodingUnlimited || _viewmodel.IsFlooding);

    private bool IsFloodingIntervalNumberBoxEnabled => !_viewmodel.IsFlooding;

    private bool IsFloodTextTextBoxEnabled => !_viewmodel.IsFlooding;

    private bool IsFloodingUnlimitedCheckBoxEnabled => !_viewmodel.IsFlooding;

    private bool IsProgressBarIndeterminate => _viewmodel.IsFloodingUnlimited && _viewmodel.IsFlooding;

    private string MasterButtonText => _viewmodel.IsFlooding ? "ȡ��" : "����";

    private ICommand MasterButtonCommand =>
        _viewmodel.IsFlooding ? _viewmodel.StopFloodingCommand : _viewmodel.BeginFloodCommand;
}