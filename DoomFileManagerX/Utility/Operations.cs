using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomFileManagerX.Utility
{
    public static class Operations
    {
        static void CPCommand(List<string> arguments)
        {
            if (arguments.Count == 0)
            {
                Console.WriteLine("Неверные параметры...");
                return;
            }
            else if (arguments.Count == 1)
            {
                if (arguments[0].Trim() == "/?")
                {
                    Console.WriteLine("cp <путь - откуда копируем> <путь куда копируем> - копирование файла/каталога");
                }
                else
                {
                    Console.WriteLine("Неправильный вызов команды cp");
                }
            }
            else if (arguments.Count == 2)
            {
                string source = arguments[0];
                string dest = arguments[1];
                if (source.Trim() == dest.Trim())
                {
                    Console.WriteLine("Нельзя скопировать файл/папку саму в себя");
                }
                else if (source.Trim() == "" || dest.Trim() == "")
                {
                    Console.WriteLine("Неверные аргументы");
                }
                else
                {
                    Copy(source, dest);
                }
            }
            else
            {
                string resSourcePath = arguments[0];
                string resDestPath = "";
                int indexOfStartDest = 0;//индекс начала второго параметра
                for (int i = 1; i < arguments.Count; i++)
                {
                    resSourcePath += " ";
                    if (arguments[i].Length > 1)
                    {
                        if (arguments[i].Substring(1, 1) != ":")
                        {
                            resSourcePath += arguments[i];
                        }
                        else
                        {
                            indexOfStartDest = i;
                            break;
                        }
                    }
                    else
                    {
                        resSourcePath += arguments[i];
                    }
                }
                resSourcePath = resSourcePath.Trim();
                for (int i = indexOfStartDest; i < arguments.Count; i++)
                {
                    resDestPath += arguments[i];
                    resDestPath += " ";
                }
                resDestPath = resDestPath.Trim();
                Copy(resSourcePath, resDestPath);
            }
        }

        //Если в параметрах существует путь с пробелом, то нужно сначала разобрать пути, потому запускать копирование
        //поэтому вынес непосредственно копирование в отдельную процедуру
        static void Copy(string source, string dest)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(source);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    if (CopyDirectory(source, dest))
                        Console.WriteLine("Копирование завершено...");
                }
                else
                {
                    try
                    {
                        File.Copy(source, dest, true);
                        Console.WriteLine("Копирование завершено...");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Ошибка копирования: {e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static bool CopyDirectory(string sourceDirName, string destDirName)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);
                DirectoryInfo[] dirs = dir.GetDirectories();

                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                    Console.WriteLine($"Копируем {file.Name}");
                }

                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка копирования: {e.Message}");
                return false;
            }
        }

        static void RMCommand(List<string> arguments)
        {
            if (arguments.Count == 0)
            {
                Console.WriteLine("Неверные параметры...");
                return;
            }
            else if (arguments.Count == 1)
            {
                if (arguments[0].Trim() == "/?")
                {
                    Console.WriteLine("rm <путь к папке/файлу, которую/который необходимо удалить>");
                }
                else
                {
                    Delete(arguments[0], "");
                }
            }
            else
            {
                string resPath = arguments[0];
                for (int i = 1; i < arguments.Count; i++)
                {
                    resPath += " ";
                    resPath += arguments[i];
                }
                resPath = resPath.Trim();
                Delete(resPath, "");
            }
        }

        static void Delete(string path, string CurrentPath)
        {
            FileInfo file;
            DirectoryInfo directories;
            try
            {
                FileAttributes attr = File.GetAttributes(path);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    directories = new DirectoryInfo(path);
                    foreach (DirectoryInfo dir in directories.GetDirectories())
                    {
                        Console.WriteLine($"Удаляем {dir.Name}");
                        dir.Delete(true);
                    }
                    if (directories.FullName == CurrentPath)
                    {
                        CurrentPath = directories.Parent.FullName;//присваиваем, чтобы сработала процедура, которая обновит список
                        //ConsoleDrawings.PrintFoldersTree(CurrentPath, CurrentFoldersFiles, 1);
                    }
                    directories.Delete(true);
                }
                else
                {
                    file = new FileInfo(path);
                    file.Delete();
                    if (file.Directory.FullName == CurrentPath)
                    {
                        CurrentPath = file.Directory.FullName;//присваиваем, чтобы сработала процедура, которая обновит список
                        //ConsoleDrawings.PrintFoldersTree(CurrentPath, CurrentFoldersFiles, 1);
                    }
                }
                Console.WriteLine("Удаление завершено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        static void FILECommand(List<string> arguments)
        {
            if (arguments.Count == 0)
            {
                Console.WriteLine("Неверные параметры...");
                return;
            }
            else if (arguments.Count == 1)
            {
                if (arguments[0].Trim() == "/?")
                {
                    Console.WriteLine("file <путь к файлу, который необходимо вывести>");
                }
                else
                {
                    ReadFile(arguments[0]);
                }
            }
            else
            {
                string resPath = arguments[0];
                for (int i = 1; i < arguments.Count; i++)
                {
                    resPath += " ";
                    resPath += arguments[i];
                }
                resPath = resPath.Trim();
                ReadFile(resPath);
            }
        }
        static void ReadFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Файл {path} не существует/не доступен");
                }
                else
                {
                    //ConsoleDrawings.PrintFileContent(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка чтения файла: {e.Message}");
            }
        }
    }
}
