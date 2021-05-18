using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the input string");
            string strPalindrome = Console.ReadLine();
            strPalindrome = strPalindrome.ToLower();
            char[] strRevPalindrome = strPalindrome.ToCharArray();
            bool isPalindrome=true;

            //the below region is one more way of finding whether a given string is palindrome or not
            #region Reversing a string
            //Array.Reverse(strRevPalindrome);
            //if (strPalindrome == new string(strRevPalindrome))
            //{
            //    Console.WriteLine("Given string is a Palindrome");
            //}
            //else
            //{
            //    Console.WriteLine("Given string is not a Palindrome");
            //}
            #endregion Reversing a string

            for (int i = 0; i < strRevPalindrome.Length; i++)
            {
                if (strRevPalindrome[i] != strRevPalindrome[strRevPalindrome.Length - i - 1])
                {
                    isPalindrome=false;
                    Console.WriteLine("Not a palindrome");
                    Console.ReadKey();
                    break;
                }
            }
            if(isPalindrome){
                Console.WriteLine("Given string is a palindrome");
            }
            Console.ReadKey();
        }
    }
}
