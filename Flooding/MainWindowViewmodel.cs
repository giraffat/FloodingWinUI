using CommunityToolkit.Mvvm.ComponentModel;

namespace Flooding;

public class MainWindowViewmodel : ObservableObject
{
    private double _floodTimes;
    private double _floodingInterval;
    private bool _isFloodingTimesLimited;

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

    public bool IsFloodingTimesLimited
    {
        get => _isFloodingTimesLimited;
        set => SetProperty(ref _isFloodingTimesLimited, value);
    }
}