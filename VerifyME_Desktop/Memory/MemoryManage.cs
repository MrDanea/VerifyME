using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerifyME_Desktop.Memory
{
    static class MemoryManage
    {
        public static string Project = Environment.CurrentDirectory;
        public static string Storage = Path.Combine(Project, "Storage");
        public static string Labels = Path.Combine(Project, Storage, "Labels");
        public static string Images = Path.Combine(Project, Storage, "Images");
        private static int Exist(string LINK) => Directory.Exists(LINK) ? 1 : 0;
        /// <summary>
        /// Array [1,0,1] => 101
        /// </summary>
        /// <param name="INPUT">The type of Array is int</param>
        /// <returns>The number merged</returns>
        private static int Merge(params int[] conditions)
        {
            int result = 0;
            for (int i = 0; i < conditions.Length; i++)
            {
                result |= conditions[i] << (conditions.Length - 1 - i);
            }
            return result;
        }
        public static void Create() 
        {
            if(Path.Exists(Storage))
            {
                int CASE = Merge(Exist(Labels), Exist(Images));
                switch (CASE)
                {
                    case 0: Directory.CreateDirectory(Labels); Directory.CreateDirectory(Images); break; //00
                    case 1: Directory.CreateDirectory(Images); break; //01
                    case 2: Directory.CreateDirectory(Labels); break;//10
                    default: break; //11
                }
                return;
            }
            Directory.CreateDirectory(Labels); 
            Directory.CreateDirectory(Images);
        }
        public static int CountLabelFiles()
        {
            string[] txtFiles = Directory.GetFiles(Labels, "*.txt", SearchOption.AllDirectories);
            return txtFiles.Length;
        }
        public static int CountImageFiles(string folderPath)
        {
            string[] patterns = { "*.png", "*.jpg" };
            return patterns.Sum(pattern => Directory.GetFiles(folderPath, pattern, SearchOption.AllDirectories).Length);
        }
        public static long GetFolderSize(string folderPath)
        {
            long totalSize = 0;
            string[] allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            foreach (string file in allFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                totalSize += fileInfo.Length;
            }
            return totalSize;
        }
        static void ClearFolder(string folderPath)
        {
            string[] allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            foreach (string file in allFiles)
            {
                File.Delete(file);
            }
            string[] allDirectories = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories);
            foreach (string dir in allDirectories)
            {
                Directory.Delete(dir, true);
            }
        }
    }
}
