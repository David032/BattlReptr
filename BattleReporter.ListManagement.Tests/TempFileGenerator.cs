using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleReporter.ListManagement.Tests
{
    public static class TempFileGenerator
    {
        public static List<string> GeneratedFiles = new List<string>();

        public static string GenerateTempFile(string extension) 
        {
            var file = Path.GetTempFileName() + '.' +extension;
            GeneratedFiles.Add(file);
            return file;
        }

        public static void DeleteAllTempFiles() 
        {
            var listOfFiles = GeneratedFiles;
            foreach (var file in listOfFiles) 
            {
                File.Delete(file);
            }
        }
    }
}
