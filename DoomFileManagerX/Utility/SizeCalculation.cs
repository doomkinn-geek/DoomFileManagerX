using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoomFileManagerX.Utility
{
    public static class SizeCalculation
    {
        private const long OneKb = 1024;
        private const long OneMb = OneKb * 1024;
        private const long OneGb = OneMb * 1024;
        private const long OneTb = OneGb * 1024;        
        public static string ToPrettySize(this long value, int decimalPlaces = 0)//функция округляет числа с размерами файлов, чтобы удобнее их было читать
        {
            var asTb = Math.Round((double)value / OneTb, decimalPlaces);
            var asGb = Math.Round((double)value / OneGb, decimalPlaces);
            var asMb = Math.Round((double)value / OneMb, decimalPlaces);
            var asKb = Math.Round((double)value / OneKb, decimalPlaces);
            string chosenValue = asTb > 1 ? string.Format("{0} Tb", asTb)
                : asGb > 1 ? string.Format("{0} Gb", asGb)
                : asMb > 1 ? string.Format("{0} Mb", asMb)
                : asKb > 1 ? string.Format("{0} Kb", asKb)
                : string.Format("{0} B", Math.Round((double)value, decimalPlaces));
            return chosenValue;
        }        
        public static long GetTotalSize(string directory)
        {
            long totalSize = 0;
            string[] files;
            try
            {
                files = System.IO.Directory.GetFiles(directory);
            }
            catch (Exception)
            {
                return -1;
            }
            foreach (string file in files)
            {                
                totalSize += GetFileSize(file);
            }

            string[] subDirs = System.IO.Directory.GetDirectories(directory);
            foreach (string dir in subDirs)
            {
                totalSize += GetTotalSize(dir);
            }
            return totalSize;
        }

        private static long GetFileSize(string path)
        {
            try
            {
                FileInfo fi = new System.IO.FileInfo(path);
                return fi.Length;
            }
            catch
            {
                return 0;
            }
        }
    }
}
