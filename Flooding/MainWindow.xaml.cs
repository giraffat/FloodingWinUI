using WinUIEx;

namespace Flooding
{
    internal sealed partial class MainWindow
    {
        public MainWindow()
        {
            this.Title = "ˢ��";
            this.SetWindowSize(300,500);
            this.SetIsAlwaysOnTop(true);
            this.SetIsResizable(false);
            this.SetIsMaximizable(false);
            this.SetIsMinimizable(false);

            InitializeComponent();
        }
    }
}
