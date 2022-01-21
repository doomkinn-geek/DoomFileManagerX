using DoomFileManagerX.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomFileManagerX.Models
{
    public abstract class TreeItem : ViewModelBase, ITreeItem
    {
        public string? VisibleName { get; set; }
        public string FullPathName { get; set; }

        protected ObservableCollection<ITreeItem> children;
        public ObservableCollection<ITreeItem> Children
        {
            get { return children ?? (children = GetMyChildren()); }
            set => children = value;
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set => isExpanded = value;
        }
        public bool IncludeFileChildren { get; set; }
        public abstract ObservableCollection<ITreeItem> GetMyChildren();

        public void DeleteChildren()
        {
            if (children != null)
            {
                for (int i = children.Count - 1; i >= 0; i--)
                {
                    children[i].DeleteChildren();
                    children[i] = null;
                    children.RemoveAt(i);
                }
                children = null;
            }
        }
    }
}
