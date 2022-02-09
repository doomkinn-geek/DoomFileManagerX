using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomFileManagerX.Models
{
    public class FileItem : TreeItem
    {
        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            return childrenList;
        }
    }
}
