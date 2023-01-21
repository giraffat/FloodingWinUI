using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Flooding;

public partial class MainWindowViewmodel : ObservableObject
{
    [ObservableProperty] private int _floodingInterval;
    [ObservableProperty] private double _floodTimes;
    [ObservableProperty] private bool _isFloodingTimesLimited;
    [ObservableProperty] private string _floodText;

    private double _progress;

    public double Progress
    {
        get => _progress;
        private set => SetProperty(ref _progress, value);
    }

    private bool _isFlooding;

    public bool IsFlooding
    {
        get => _isFlooding;
        private set
        {
            SetProperty(ref _isFlooding, value);
            BeginFloodCommand.NotifyCanExecuteChanged();
            StopFloodingCommand.NotifyCanExecuteChanged();
        }
    }

    private CancellationTokenSource _cancellationTokenSource = new();

    [RelayCommand(CanExecute = nameof(CanBeginFlood))]
    private async void BeginFlood()
    {
        IsFlooding = true;

        try
        {
            await Task.Run(async () =>
            {
                await Task.Delay(2000);

                for (var i = 0; i < FloodTimes; i++)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    SendKeys.SendKeys.SendWait(FloodText + "%S");
                    Progress = i / FloodTimes * 100;
                    await Task.Delay(FloodingInterval);
                }
            }, _cancellationTokenSource.Token);
        } 
        finally
        {
            IsFlooding = false;
            Progress = 0;
        }
    }

    private bool CanBeginFlood() => !IsFlooding;

    [RelayCommand(CanExecute = nameof(CanStopFlooding))]
    private void StopFlooding()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new();
    }

    private bool CanStopFlooding() => IsFlooding;
}