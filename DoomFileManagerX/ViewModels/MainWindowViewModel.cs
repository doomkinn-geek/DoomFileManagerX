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
        public string WindowHeader => "Cross-platform version of File Manager";
        RelayCommand selectedPathFromTreeCommand;
        public ICommand SelectedPathFromTreeCommand => selectedPathFromTreeCommand ??
                       (selectedPathFromTreeCommand =
                              new RelayCommand(x => SelectedPath = x as string));

        // Selected path set by command call when item is clicked
        private string selectedPath;
        public string SelectedPath
        {
            get => selectedPath; 
            set => selectedPath = value; 
        }
        public ObservableCollection<ITreeItem> RootChildren
        {
            get => rootChildren;
            set => rootChildren = value;
        }
        public MainWindowViewModel(ITreeItem root)
        {
            _root = root;
            //Type selectedType = typeof(DriveRootItem);
            //TreeItem rootItem = (TreeItem)Activator.CreateInstance(selectedType);
            DriveRootItem rootItem = new DriveRootItem();
            this.rootChildren = new ObservableCollection<ITreeItem>(
                new ITreeItem[]
                {
                    rootItem,
                });
        }
    }
}
