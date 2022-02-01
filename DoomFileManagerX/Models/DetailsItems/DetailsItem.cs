using DoomFileManagerX.Utility;
using DoomFileManagerX.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoomFileManagerX.Models.DetailsItems
{
    public class DetailsItem : ViewModelBase, IDetailsItem
    {
        private string fullPathName { get; set; }
        private DateTime creationTime { get; set; }
        private DateTime lastModifiedTime { get; set; }
        private FileAttributes attributes { get; set; }
        private long fullSize { get; set; }   
        private string prettySize { get; set; }
        public string FullPathName { get => fullPathName; }
        public string CreationTime { get => creationTime.ToString("dd.MM.yyyy"); }        
        public string LastModifiedTime { get => lastModifiedTime.ToString("dd.MM.yyyy"); }
        public string Attributes
        {
            get 
            {
                string attrib = "";                
                if (attributes.HasFlag(FileAttributes.ReadOnly))
                    attrib += "Только для чтения | ";
                if (attributes.HasFlag(FileAttributes.Archive))
                    attrib += "Архивный | ";
                if (attributes.HasFlag(FileAttributes.System))
                    attrib += "Системный | ";
                if (attributes.HasFlag(FileAttributes.Hidden))
                    attrib += "Скрытый";
                if (attrib.Substring(attrib.Length - 2, 2) == "| ")
                {
                    attrib = attrib.Substring(0, attrib.Length - 2);                    
                }
                return attrib;
            }
        }
        public string FullSize
        {
            get => prettySize;
            set
            {
                prettySize = value;
                Notify();
            }
        }
        public async void FullSizeTask()
        {
            FullSize = "<Расчитывается...>";
            await Task.Run(() =>
            {
                long fullDirSize = 0;
                string size = "-1";
                fullDirSize = SizeCalculation.GetTotalSize(this.FullPathName);                    
                try
                {
                    size = SizeCalculation.ToPrettySize(fullDirSize);
                    FullSize = size;
                }
                catch (Exception)
                {
                    size = "0";
                }                
            });
        }
        public DetailsItem(string path)
        {
            try
            {
                fullPathName = path;
                FileAttributes attr = File.GetAttributes(FullPathName);
                attributes = attr;
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    DirectoryInfo di = new DirectoryInfo(FullPathName);
                    creationTime = di.CreationTime;
                    lastModifiedTime = di.LastWriteTime;
                }
                else
                {
                    FileInfo fi = new FileInfo(FullPathName);
                    creationTime = fi.CreationTime;
                    lastModifiedTime = fi.LastWriteTime;
                }
            }
            catch (Exception ex)
            {
                return;
            }
            FullSizeTask();
        }        
    }
}
