using DoomFileManagerX.Models.TreeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoomFileManagerX.View
{
    /// <summary>
    /// Логика взаимодействия для FolderTreeView.xaml
    /// </summary>
    public partial class FolderTreeView : UserControl
    {
        public FolderTreeView()
        {
            InitializeComponent();
        }
        public string SelectedItemPath
        {
            get
            {
                ITreeItem selected = treeView.SelectedItem as ITreeItem;
                return selected.FullPathName;
            }
        }
    }
}
