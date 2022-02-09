using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;

namespace DoomFileManagerX.Utility
{
    public static class GetIcon
    {

        public static System.Windows.Media.Imaging.BitmapSource GetIconDll(string fileName)
        {
            BitmapSource myIcon = null;

            bool validDrive = false;
            foreach (DriveInfo D in System.IO.DriveInfo.GetDrives())
            {   
                if (fileName == D.Name)
                {
                    validDrive = true;
                }
            }

            if ((System.IO.File.Exists(fileName)) || (System.IO.Directory.Exists(fileName)) || (validDrive))
            {
                using (System.Drawing.Icon sysIcon = ShellIcon.GetLargeIcon(fileName))
                {
                    try
                    {
                        myIcon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                                        sysIcon.Handle,
                                        System.Windows.Int32Rect.Empty,
                                        System.Windows.Media.Imaging.BitmapSizeOptions.FromWidthAndHeight(34, 34));
                    }
                    catch
                    {
                        myIcon = null;
                    }
                }
            }
            return myIcon;
        }
    }
}
