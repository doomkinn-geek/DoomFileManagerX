using DoomFileManagerX.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
        public string FullPathName { get => fullPathName; }
        public string CreationTime { get => creationTime.ToString("dd.MM.yyyy"); }        
        public string LastModifiedTime { get => lastModifiedTime.ToString("dd.MM.yyyy"); }
        public string Attributes
        {
            get 
            {
                string attrib;
                attrib = "Атрибуты: ";
                if (attributes.HasFlag(FileAttributes.ReadOnly))
                    attrib += "Только для чтения | ";
                if (attributes.HasFlag(FileAttributes.Archive))
                    attrib += "Архивный | ";
                if (attributes.HasFlag(FileAttributes.System))
                    attrib += "Системный | ";
                if (attributes.HasFlag(FileAttributes.Hidden))
                    attrib += "Скрытый | ";
                if (attrib != "Атрибуты: ")
                {
                    attrib = attrib.Substring(0, attrib.Length - 3);                    
                }
                return attrib;
            }
        }
        public long FullSize { get => fullSize; }

        public event PropertyChangedEventHandler PropertyChanged;
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
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
