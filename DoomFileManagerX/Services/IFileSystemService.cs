using DoomFileManagerX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomFileManagerX.Services
{
    public interface IFileSystemService
    {       
        Task Action(List<string> parameters, MainWindowViewModel viewModel);
    }
}
