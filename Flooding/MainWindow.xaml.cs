using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinUIEx;

namespace Flooding;

internal sealed partial class MainWindow : INotifyPropertyChanged
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

        _viewmodel.PropertyChanged += (_, _) => ButtonText = _viewmodel.IsFlooding ? "È¡Ïû" : "Æô¶¯";
    }

    private bool BoolNegation(bool value)
    {
        return !value;
    }

    private void ActionButtonClick()
    {
        if (_viewmodel.IsFlooding)
        {
            _viewmodel.Cancel();
        }
        else
        {
            _viewmodel.BeginFlood();
        }
    }

    private string _buttonText;

    public string ButtonText
    {
        get => _buttonText;
        set => SetField(ref _buttonText, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}