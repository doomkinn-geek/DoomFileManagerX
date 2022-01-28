using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DoomFileManagerX.Models.TreeItems
{
    public interface ITreeItem : INotifyPropertyChanged
    {
        // Короткое имя отображаемое в дереве
        string VisibleName { get; set; }
        string FullPathName { get; set; }
        BitmapSource MyIcon { get; set; }

        ObservableCollection<ITreeItem> Children { get; }

        bool IsExpanded { get; set; }

        bool IncludeFileChildren { get; set; }

        // Для сброса дерева
        void DeleteChildren();
    }
}
