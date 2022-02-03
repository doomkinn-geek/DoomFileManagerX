using DoomFileManagerX.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomFileManagerX.Services
{
    public class CopyService : IFileSystemService
    {
        private MainWindowViewModel _viewModel;
        public async Task Action(List<string> parameters, MainWindowViewModel viewModel)
        {
            string status = string.Empty;
            _viewModel = viewModel;
            if(parameters.Count != 2)
            {
                throw new Exception("Неправильные параметры команды Copy");
            }
            string StartDirectory = parameters[0];
            string EndDirectory = parameters[1];
            if(StartDirectory.Trim() == EndDirectory.Trim())
            {
                throw new Exception("Невозможно скопировать сам в себя");
            }
            foreach (string dirPath in Directory.GetDirectories(StartDirectory, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(StartDirectory, EndDirectory));
                foreach (string filename in Directory.EnumerateFiles(dirPath))
                {
                    using (FileStream SourceStream = File.Open(filename, FileMode.Open))
                    {
                        using (FileStream DestinationStream = File.Create(filename.Replace(StartDirectory, EndDirectory)))
                        {
                            status = "Копируем " + filename;
                            _viewModel.StatusMessage = status;
                            await SourceStream.CopyToAsync(DestinationStream);
                        }
                    }
                }
            }
            foreach (string filename in Directory.EnumerateFiles(StartDirectory))
            {
                using (FileStream SourceStream = File.Open(filename, FileMode.Open))
                {
                    using (FileStream DestinationStream = File.Create(EndDirectory + filename.Substring(filename.LastIndexOf('\\'))))
                    {
                        status = "Копируем " + filename;
                        _viewModel.StatusMessage = status;
                        await SourceStream.CopyToAsync(DestinationStream);
                    }
                }
            }
        }
    }
}
