using DoomFileManagerX.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DoomFileManagerX.Models.TreeItems
{
    public class FolderItem : TreeItem
    {
        public override BitmapSource GetMyIcon()
        {
            return myIcon = GetIcon.GetIconDll(this.FullPathName);
        }
        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            ITreeItem item1;

            try
            {
                DirectoryInfo di = new DirectoryInfo(this.FullPathName);
                if (!di.Exists) return childrenList;
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    item1 = new FolderItem();
                    item1.FullPathName = FullPathName + "\\" + dir.Name;
                    item1.VisibleName = dir.Name;
                    item1.IncludeFileChildren = this.IncludeFileChildren;
                    childrenList.Add(item1);
                }

                if (this.IncludeFileChildren) foreach (FileInfo file in di.GetFiles())
                    {
                        item1 = new FileItem();
                        item1.FullPathName = FullPathName + "\\" + file.Name;
                        item1.VisibleName = file.Name;
                        childrenList.Add(item1);
                    }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            return childrenList;
        }
    }
}
