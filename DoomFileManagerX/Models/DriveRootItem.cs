using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomFileManagerX.Models
{
    public class DriveRootItem : TreeItem
    {
        public DriveRootItem()
        {
            //Constructor sets some properties
            VisibleName = "DriveRoot";
            IsExpanded = true;
            FullPathName = "$xxDriveRoot$";
        }
        public override ObservableCollection<ITreeItem> GetMyChildren()
        {
            ObservableCollection<ITreeItem> childrenList = new ObservableCollection<ITreeItem>() { };
            ITreeItem item1;
            string fn = "";

            //string[] allDrives = System.Environment.GetLogicalDrives();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
                if (drive.IsReady)
                {
                    item1 = new DriveItem();

                    // Some processing for the FriendlyName
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
