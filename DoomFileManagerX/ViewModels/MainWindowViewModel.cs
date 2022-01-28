using DoomFileManagerX.Commands;
using DoomFileManagerX.Models;
using DoomFileManagerX.Models.DetailsItems;
using DoomFileManagerX.Models.TreeItems;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoomFileManagerX.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly ITreeItem _root;
        private IDetailsItem _detail;
        private ObservableCollection<ITreeItem> rootChildren = new ObservableCollection<ITreeItem> { };
        private string selectedPath;
        private RelayCommand treeClickEventCommand;
        
        public string WindowHeader => "Windows версия файлового менеджера";
        public string Greeting => "Приветствие";
        public IDetailsItem Detail
        {
            get => _detail;
            set
            {
                _detail = value;
                Notify();
            }
        }
        private void DoTreeClickEventCommand(object parameter)
        {
            TreeView control = parameter as TreeView;
            ITreeItem selected = control.SelectedItem as ITreeItem;
            Detail = new DetailsItem(selected.FullPathName);
            Notify();
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
