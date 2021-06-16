using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContactManager.Services
{
    public class CsvReaderService
    {
        public List<string[]> ReadRows(string filePath, string separator = ",", int skipRows = 0)
        {
            var result = new List<string[]>();
            string csvData = File.ReadAllText(filePath);
            csvData = csvData.Replace("\r", "");

            foreach (string row in csvData.Split('\n').Skip(skipRows))
            {
                if (string.IsNullOrEmpty(row))
                {
                    continue;
                }
                result.Add(row.Split(separator));
            }

            return result;
        }
    }
}
