using AsyncCommands.Commands;
using DoomFileManagerX.Commands;
using DoomFileManagerX.Models;
using DoomFileManagerX.Models.DetailsItems;
using DoomFileManagerX.Models.TreeItems;
using DoomFileManagerX.Services;
using DoomFileManagerX.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoomFileManagerX.ViewModels
{
    enum OperationType
    {
        NotDefined,
        Copy,
        Cut
    };
    public class MainWindowViewModel : ViewModelBase
    {
        readonly ITreeItem _root;
        private IDetailsItem _detail;
        private ObservableCollection<ITreeItem> rootChildren = new ObservableCollection<ITreeItem> { };
        private string selectedPath;
        private RelayCommand treeClickEventCommand;
        private RelayCommand copyClickCommand;
        private RelayCommand cutClickCommand;

        private OperationType operationType;        
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
        public RelayCommand CopyClickCommand
        {
            get { return copyClickCommand; }
        }
        public RelayCommand CutClickCommand
        {
            get { return cutClickCommand; }
        }
        private void SetStartPathCommandCopy(object parameter)
        {
            try
            {
                string param = ((FolderTreeView)parameter).SelectedItemPath;
                StartPath = param;
                operationType = OperationType.Copy;
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                StartPath = null;
                operationType=OperationType.NotDefined;
            }
        }
        private void SetStartPathCommandCut(object parameter)
        {
            try
            {
                string param = ((FolderTreeView)parameter).SelectedItemPath;
                StartPath = param;
                operationType = OperationType.Cut;
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                StartPath = null;
                operationType = OperationType.NotDefined;
            }
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
        private string _statusMessage;
        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                Notify();
            }
        }
        private string _startPath;
        public string StartPath
        {
            get
            {
                return _startPath;
            }
            set
            {
                _startPath = value;
                Notify();
                Notify("PossibleToPaste");
            }
        }
        private string _endPath;
        public string EndPath
        {
            get
            {
                return _endPath;
            }
            set
            {
                _endPath = value;
                Notify();
            }
        }
        public bool PossibleToPaste => !string.IsNullOrEmpty(StartPath);
        public ICommand CopyCommand { get; }
        private async Task Copy()
        {
            List<string> p = new List<string>();
            p.Add(StartPath);
            p.Add(EndPath);
            await new CopyService().Action(p, this);
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
            copyClickCommand = new RelayCommand(SetStartPathCommandCopy);
            cutClickCommand = new RelayCommand(SetStartPathCommandCut);
            Detail = new DetailsItem(_root.FullPathName);
            CopyCommand = new AsyncRelayCommand(Copy, (ex) => StatusMessage = ex.Message);
            operationType = OperationType.NotDefined;
        }
    }
}
