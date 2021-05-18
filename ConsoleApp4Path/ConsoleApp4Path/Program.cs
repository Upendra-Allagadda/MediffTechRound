using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4Path
{
    class Path
    {
        public string CurrentPath { get; private set; }

        public Path(string path)
        {
            this.CurrentPath = path;
        }

        public void Cd(string newPath)
        {
            string pattern = @"[^A-Za-z/.]";
            Match match = Regex.Match(newPath, pattern);
            if (match.Success)
            {
                Console.WriteLine("Invalid character found. Only alphabets of upper and lower cases are allowed for directory names.");
                while (match.Success)
                {
                    Console.WriteLine("'{0}' found in the source code at position {1}.",
                                      match.Value, match.Index);
                    match = match.NextMatch();
                }
                return;
            }

            if (newPath == "/")
            {
                CurrentPath = "/";
                return;
            }

            while (newPath.Length > 0)
            {
                if (newPath.Length > 1)
                {
                    if (newPath.Substring(0, 2) == "..")
                    {
                        if (!String.IsNullOrEmpty(CurrentPath))
                        {
                            CurrentPath = CurrentPath.Remove(CurrentPath.LastIndexOf("/", StringComparison.Ordinal));
                            if (String.IsNullOrEmpty(CurrentPath))
                            {
                                CurrentPath = "/";
                            }
                        }

                        newPath = newPath.Remove(0, 2);
                        continue;
                    }
                }

                if (newPath[0] == '/')
                {
                    newPath = newPath.Remove(0, 1);
                    if (newPath[0] == '.')
                    {
                        continue;
                    }
                }

                if (CurrentPath.Last() != '/')
                {
                    CurrentPath += "/";
                }

                var nextPath = newPath.IndexOf("/", StringComparison.Ordinal);
                if (nextPath == -1)
                {
                    CurrentPath += newPath;
                    newPath = "";
                }
                else
                {
                    CurrentPath += newPath.Substring(0, nextPath);
                    newPath = newPath.Remove(0, nextPath);
                }
            }
        }

        public static void Main(string[] args)
        {
            Path path = new Path("/a/b/c/d");
            path.Cd("../@/#/$");
            Console.WriteLine("Current path is {0}", path.CurrentPath);
            Console.ReadLine();

            //for path /a/b/c/d
            //"../x" gives /a/b/c/x
            //"/" gives "/"
            //"../x/../y" gives /a/b/c/y
            //"../@" gives "invalid path" message
        }
    }
}
