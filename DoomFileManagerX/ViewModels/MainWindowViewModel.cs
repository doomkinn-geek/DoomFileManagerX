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
        public string WindowHeader => "Windows ������ ��������� ���������";
        public string MainTreeHeader => "������ �����";
        public string DetailsHeader => "���������� ������� �����";
        public string Greeting => "�����������";
        RelayCommand selectedPathFromTreeCommand;
        public ICommand SelectedPathFromTreeCommand => selectedPathFromTreeCommand ??
                       (selectedPathFromTreeCommand =
                              new RelayCommand(x => SelectedPath = x as string));
        
        private string selectedPath;
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
        }
    }
}
