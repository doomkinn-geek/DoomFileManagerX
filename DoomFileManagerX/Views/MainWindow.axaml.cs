using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DoomFileManagerX.Models;
using DoomFileManagerX.ViewModels;

namespace DoomFileManagerX.Views
{
    public partial class MainWindow : Window
    {
        readonly MainWindowViewModel _mainWindowView;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            ITreeItem rootItem = new FolderItem
            {
                VisibleName = "Drives",
                IncludeFileChildren = false,
                FullPathName = "DrivesRoot",
            };            
            _mainWindowView = new MainWindowViewModel(rootItem);
            base.DataContext = _mainWindowView; 
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
