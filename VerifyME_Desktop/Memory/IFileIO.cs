using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerifyME_Desktop.Memory
{
    public class IFileIO
    {
        public static bool GetContentLabels(string PATH, out double[]? content)
        {
            content = null;
            if (!File.Exists(PATH)) { return false; }
            try
            {
                string strcontent = File.ReadAllText(PATH);
                content = strcontent
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries) 
                    .Select(double.Parse)
                    .ToArray();

                Console.WriteLine("Mảng double:");
                Console.WriteLine(string.Join(", ", content));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
