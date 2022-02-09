using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomFileManagerX.Models.DetailsItems
{
    public interface IDetailsItem : INotifyPropertyChanged
    {
        string FullPathName { get;}
        string CreationTime { get; }
        string LastModifiedTime { get; }
        string Attributes { get; }
        string FullSize { get; }
    }
}
