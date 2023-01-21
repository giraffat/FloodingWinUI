using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Flooding;

public partial class MainWindowViewmodel : ObservableObject
{
    private CancellationTokenSource _cancellationTokenSource = new();
    [ObservableProperty] private int _floodingInterval = 1000;
    [ObservableProperty] private string _floodText;
    [ObservableProperty] private double _floodTimes = 10;

    private bool _isFlooding;
    [ObservableProperty] private bool _isFloodingUnlimited;

    private double _progress;

    public double Progress
    {
        get => _progress;
        private set => SetProperty(ref _progress, value);
    }

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

    [RelayCommand(CanExecute = nameof(CanBeginFlood))]
    private async void BeginFlood()
    {
        IsFlooding = true;
        var token = _cancellationTokenSource.Token;

        try
        {
            await Task.Delay(2000, token);

            if (IsFloodingUnlimited)
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    SendKeys.SendKeys.SendWait(FloodText + "%S");
                    await Task.Delay(FloodingInterval, token);
                }

            for (var i = 0; i < FloodTimes; i++)
            {
                token.ThrowIfCancellationRequested();
                SendKeys.SendKeys.SendWait(FloodText + "%S");
                Progress = i / FloodTimes * 100;
                await Task.Delay(FloodingInterval, token);
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            IsFlooding = false;
            Progress = 0;
        }
    }

    private bool CanBeginFlood()
    {
        return !IsFlooding;
    }

    [RelayCommand(CanExecute = nameof(CanStopFlooding))]
    private void StopFlooding()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private bool CanStopFlooding()
    {
        return IsFlooding;
    }
}