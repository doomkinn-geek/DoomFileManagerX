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
    public class DriveRootItem : TreeItem
    {
        public DriveRootItem()
        {            
            VisibleName = "ROOT";
            IsExpanded = true;
            FullPathName = "DriveRoot";
        }

        public override BitmapSource GetMyIcon()
        {            
            string Param = "pack://application:,,,/" + "MyImages/bullet_blue.png";
            Uri uri1 = new Uri(Param, UriKind.RelativeOrAbsolute);
            return myIcon = BitmapFrame.Create(uri1);
        }
        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            ITreeItem item1;
            string fn = "";
           
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
                if (drive.IsReady)
                {
                    item1 = new DriveItem();                    
                    fn = drive.Name.Replace(@"\", "");
                    item1.FullPathName = fn;
                    if (drive.VolumeLabel == string.Empty)
                    {
                        fn = drive.DriveType.ToString() + " (" + fn + ")";
                    }
                    else if (drive.DriveType == DriveType.CDRom)
                    {
                        fn = drive.DriveType.ToString() + " " + drive.VolumeLabel + " (" + fn + ")";
                    }
                    else
                    {
                        fn = drive.VolumeLabel + " (" + fn + ")";
                    }

                    item1.VisibleName = fn;
                    item1.IncludeFileChildren = this.IncludeFileChildren;
                    childrenList.Add(item1);
                }

            return childrenList;
        }
    }
}
