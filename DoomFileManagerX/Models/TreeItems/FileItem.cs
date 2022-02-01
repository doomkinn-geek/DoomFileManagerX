using DoomFileManagerX.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DoomFileManagerX.Models.TreeItems
{
    public class FileItem : TreeItem
    {
        public override BitmapSource GetMyIcon()
        {
            // to do, use a cache for .ext != "" or ".exe" or ".lnk"
            return myIcon = GetIcon.GetIconDll(this.FullPathName);
        }
        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            return childrenList;
        }
        string FullSize 
        { 
            get
            {
                long fullSize;
                fullSize = SizeCalculation.GetFileSize(this.FullPathName);
                return SizeCalculation.ToPrettySize(fullSize);
            }
        }
    }
}
