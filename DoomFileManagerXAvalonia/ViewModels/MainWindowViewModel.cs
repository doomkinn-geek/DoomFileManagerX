using DoomFileManagerX.Commands;
using DoomFileManagerX.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DoomFileManagerX.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly ITreeItem _root;
        private ObservableCollection<ITreeItem> rootChildren = new ObservableCollection<ITreeItem> { };
        private string selectedPath;
        private RelayCommand treeClickEventCommand;
        private RelayCommand selectedPathFromTreeCommand;
        public string WindowHeader => "Кросс платформенная версия файлового менеджера";
        public string Greeting => "Приветствие";        
        public ICommand SelectedPathFromTreeCommand => selectedPathFromTreeCommand ??
                       (selectedPathFromTreeCommand =
                              new RelayCommand(x => SelectedPath = x as string));
        private void DoTreeClickEventCommand(object parameter)
        {
            int i = 0;
        }
        public RelayCommand TreeClickEventCommand
        {
            get { return treeClickEventCommand; }
        }
        public string SelectedPath
        {
            get => selectedPath;             
            set 
            {                
                selectedPath = value;
                Notify();
            }
        }
        public ObservableCollection<ITreeItem> RootChildren
        {
            get => rootChildren;
            set => rootChildren = value;
        }
        public MainWindowViewModel(ITreeItem root)
        {
            _root = root;                        
            this.rootChildren = new ObservableCollection<ITreeItem>(
                new ITreeItem[]
                {
                    _root,
                });
            treeClickEventCommand = new RelayCommand(DoTreeClickEventCommand);
        }
    }
}
