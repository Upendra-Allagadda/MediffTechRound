using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            string errorPattern = @"^(Error)";
            List<string> errorList = new List<string>();
            string filePath = ""; //log file path
            string[] lines = System.IO.File.ReadAllLines(filePath);                     

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {

                    var log = Regex.Matches(line,errorPattern);
                    errorList.Add(log.ToString());
                }
            }
            errorList.Add("====Warnings====");
            errorPattern = @"^(Warning)";

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {

                    var log = Regex.Matches(line, errorPattern);
                    errorList.Add(log.ToString());
                }
            }

            string newFilePath = "";//new file path;
            using(StreamWriter sw=new StreamWriter(newFilePath))
            {
                foreach (String s in errorList)
                    sw.WriteLine(s);
            }

            Console.WriteLine("finished parsing file {0} and writing the errors and warnings to file {1}", filePath, newFilePath);
            Console.ReadKey();
        }
    }
}
