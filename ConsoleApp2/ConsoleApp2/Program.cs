using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> files_and_owners = new Dictionary<string, string>() { 
                { "Input.txt", "Randy" },
                { "Code.py", "Stan" }, 
                {"Output.txt", "Randy"} 
            };

            Dictionary<string, List<string>> RespectiveDict = new Dictionary<string, List<string>>();
            RespectiveDict = group_by_owners(files_and_owners);
            PrintResults(RespectiveDict);
            Console.ReadKey();
        }

        /// <summary>
        /// Prints the resultant dictionary
        /// </summary>
        /// <param name="RespectiveDict">This is a dictionary with owner name as keys and file list as values</param>
        public static void PrintResults(Dictionary<string, List<string>> RespectiveDict)
        {
            foreach(string s in RespectiveDict.Keys.ToList())
            {
                Console.Write(s + " : [ ");
                foreach(string str in RespectiveDict[s])
                {
                    Console.Write(str+" ");
                }
                Console.Write("]");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// This method takes dictionary as input and returns dictionary as output.
        /// Output dictionary contains owners as keys and thier file list as values
        /// </summary>
        /// <param name="FileDict">This is a dicitonary with file names as keys and owner names as value </param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> group_by_owners(Dictionary<string, string> FileDict)
        {
            //if input dictionary is null return null
            if (FileDict.Count == 0)
            {
                return null;
            }

            Dictionary<string, List<string>> RespectiveDict = new Dictionary<string, List<string>>();
            List<string> list;

            foreach (string files in FileDict.Keys.ToList())
            {
                if (RespectiveDict.Keys.Contains(FileDict[files]))
                {
                    list = RespectiveDict[FileDict[files]];
                    list.Add(files);
                    RespectiveDict[FileDict[files]] = list;
                }
                else
                {
                    list= new List<string>();
                    list.Add(files);
                    RespectiveDict[FileDict[files]] = list;
                }
            }
            return RespectiveDict;
        }
    }

}
