using DoomFileManagerX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DoomFileManagerX.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string WindowHeader => "Cross-platform version of File Manager";
        private ObservableCollection<ITreeItem> rootChildren = new ObservableCollection<ITreeItem> { };
        public ObservableCollection<ITreeItem> RootChildren
        {
            get => rootChildren;
            set => rootChildren = value;
        }
    }
}
