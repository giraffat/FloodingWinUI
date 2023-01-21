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

    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly CancellationToken _cancellationToken;

    public MainWindowViewmodel()
    {
        _cancellationToken = _cancellationTokenSource.Token;
    }

    [RelayCommand(CanExecute = nameof(CanBeginFlood))]
    private void BeginFlood()
    {
        IsFlooding = true;

        Task.Run(() =>
        {
            Thread.Sleep(2000);
            
            for (var i = 0; i < FloodTimes; i++)
            {
                FloodHelper.InjectMessage(FloodText, new[] {VirtualKey.Menu, VirtualKey.S});
                Progress = i / FloodTimes * 100;
                _cancellationToken.ThrowIfCancellationRequested();
                Thread.Sleep(FloodingInterval);
            }
        }, _cancellationToken);
    }

    private bool CanBeginFlood() => !IsFlooding;

    [RelayCommand(CanExecute = nameof(CanStopFlooding))]
    private void StopFlooding()
    {
        _cancellationTokenSource.Cancel();
        IsFlooding = false;
    }

    private bool CanStopFlooding() => IsFlooding;
}