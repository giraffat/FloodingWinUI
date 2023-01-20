using CommunityToolkit.Mvvm.ComponentModel;
using Drastic.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System.ComponentModel;
using System.Diagnostics;
using WinUIEx;

namespace Flooding
{
    internal sealed partial class MainWindow: INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        public bool BoolNegation(bool value) => !value;
    }
    
    
}