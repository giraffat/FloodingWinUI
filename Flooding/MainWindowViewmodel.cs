using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Flooding;

public class MainWindowViewmodel : ObservableObject
{
    private double _floodingInterval;
    private double _floodTimes;
    private bool _isFloodingTimesLimited;
    private double _progress;
    private string _floodText;

    public double FloodTimes
    {
        get => _floodTimes;
        set => SetProperty(ref _floodTimes, value);
    }

    public double FloodingInterval
    {
        get => _floodingInterval;
        set => SetProperty(ref _floodingInterval, value);
    }

    public string FloodText
    {
        get => _floodText;
        set => SetProperty(ref _floodText, value);
    }

    public bool IsFloodingTimesLimited
    {
        get => _isFloodingTimesLimited;
        set => SetProperty(ref _isFloodingTimesLimited, value);
    }

    public double Progress
    {
        get => _progress;
        set => SetProperty(ref _progress, value);
    }

    public bool IsFlooding
    {
        get => _isFlooding;
        set => SetProperty(ref _isFlooding, value);
    }

    private bool _isFlooding;
    
    public Action Cancel;

    public void BeginFlood()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        Task.Run(() =>
        {
            for (var i = 0; i < FloodTimes; i++)
            {
                FloodHelper.InjectMessage(FloodText, new[] {VirtualKey.Menu, VirtualKey.S});
                Progress = i / FloodTimes * 100;
                cancellationToken.ThrowIfCancellationRequested();
            }

            Cancel = () => throw new Exception("已经结束啦");
            IsFlooding = false;
        }, cancellationToken);
        
        Cancel = cancellationTokenSource.Cancel;
        IsFlooding = true;
    }
}