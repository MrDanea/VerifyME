﻿using System;
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
        public static string Labels = Path.Combine(Storage, "Labels");
        public static string Images = Path.Combine(Storage, "Images");
        public static string ListofValidFileNames = Path.Combine(Storage, "ListofValidFileNames");
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
            if (Directory.Exists(Storage))
            {
                int CASE = Merge(Exist(ListofValidFileNames), Exist(Labels), Exist(Images));
                switch (CASE)
                {
                    case 0: 
                        Directory.CreateDirectory(Labels);
                        Directory.CreateDirectory(Images);
                        Directory.CreateDirectory(ListofValidFileNames);
                        break;

                    case 1: 
                        Directory.CreateDirectory(Images);
                        Directory.CreateDirectory(ListofValidFileNames);
                        break;

                    case 2: 
                        Directory.CreateDirectory(Labels);
                        Directory.CreateDirectory(ListofValidFileNames);
                        break;

                    case 3:
                        Directory.CreateDirectory(ListofValidFileNames);
                        break;
                    default: 
                        break;
                }
                return;
            }
            Directory.CreateDirectory(Storage);
            Directory.CreateDirectory(Labels);
            Directory.CreateDirectory(Images);
            Directory.CreateDirectory(ListofValidFileNames);
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
        public static bool TryReadTextFromFile(string filePath, out string? fileContent)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    fileContent = File.ReadAllText(filePath);
                    return true;
                }
                else
                {
                    fileContent = null;
                    return false;
                }
            }
            catch
            {
                fileContent = null;
                return false;
            }
        }
        public static bool DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
