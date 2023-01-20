using WinUIEx;

namespace Flooding;

internal sealed partial class MainWindow
{
    private readonly MainWindowViewmodel _viewmodel = new();

    public MainWindow()
    {
        Title = "к╒фа";

        this.SetWindowSize(300, 500);
        this.SetIsAlwaysOnTop(true);
        this.SetIsResizable(false);
        this.SetIsMaximizable(false);
        this.SetIsMinimizable(false);

        InitializeComponent();
    }

    private bool BoolNegation(bool value)
    {
        return !value;
    }
}