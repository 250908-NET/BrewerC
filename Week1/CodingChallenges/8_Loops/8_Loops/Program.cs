

using System;
using System.Collections.Generic;

namespace _8_LoopsChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //int x = 1;
            //List<object> oddsAndEvensDiffTypes = new List<object>() {
            //    -128,127,0,255,'h','e','y',-2147483648,"hey",21474836470,4294967295,
            //    "hey",-9223372036854775808,9223372036854775807,"hey",18446744073709551615,"hey"
            //};
            //Console.WriteLine(Program.UseForEach(oddsAndEvensDiffTypes));

            //List<string>[] strings = new List<string>[5];
            //strings[0] = new List<string> { "Tony", "look", "at" };
            //strings[1] = new List<string> { "me.", "We're", "going to" };
            //strings[2] = new List<string> { "be", "okay...", "I'm" };
            //strings[3] = new List<string> { "sorry.", "You", "can" };
            //strings[4] = new List<string> { "rest", "now.", "\n" };
            //Assert.Equal("Tony look at me. We're going to be okay... I'm sorry. You can rest now. \n ", Program
        }

        /// <summary>
        /// Return the number of elements in the List<int> that are odd.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseFor(List<int> x)
        {
            int count = 0;
            foreach (int item in x)
            {
                if (item % 2 == 1) count++;
            }
            return count;
        }

        /// <summary>
        /// This method counts the even entries from the provided List<object> 
        /// and returns the total number found.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseForEach(List<object> x)
        {
            int count = 0;
            foreach (var item in x)
            {
                if (item is int && (int)item % 2 == 0)
                {
                    count++;
                }
                if (item is long && (long)item % 2 == 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// This method counts the multiples of 4 from the provided List<int>. 
        /// Exit the loop when the integer 1234 is found.
        /// Return the total number of multiples of 4.
        /// </summary>
        /// <param name="x"></param>
        public static int UseWhile(List<int> x)
        {
            int count = 0;
            int i = 0;
            while (x[i] != 1234 && i < x.Count)
            {
                if (x[i] % 4 == 0) count++;
                i++;
            }
            return count;
        }

        /// <summary>
        /// This method will evaluate the Int Array provided and return how many of its 
        /// values are multiples of 3 and 4.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseForThreeFour(int[] x)
        {
            int count = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] % 3 == 0 && x[i] % 4 == 0) count++;
            }
            return count;
        }

        /// <summary>
        /// This method takes an array of List<string>'s. 
        /// It concatenates all the strings, with a space between each, in the Lists and returns the resulting string.
        /// </summary>
        /// <param name="stringListArray"></param>
        /// <returns></returns>
        public static string LoopdyLoop(List<string>[] stringListArray)
        {
            string[] outs = new string[stringListArray.Length];
            for (int i = 0; i < stringListArray.Length; i++) outs[i] = string.Join(' ', stringListArray[i]);
            return string.Join(' ', outs);
        }
    }
}

//using System;
//using System.Collections.Generic;

//namespace _8_LoopsChallenge
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            /* Your code here */

//        }

//        /// <summary>
//        /// Return the number of elements in the List<int> that are odd.
//        /// </summary>
//        /// <param name="x"></param>
//        /// <returns></returns>
//        public static int UseFor(List<int> x)
//        {
//            throw new NotImplementedException("UseFor() is not implemented yet.");
//        }

//        /// <summary>
//        /// This method counts the even entries from the provided List<object> 
//        /// and returns the total number found.
//        /// </summary>
//        /// <param name="x"></param>
//        /// <returns></returns>
//        public static int UseForEach(List<object> x)
//        {
//            throw new NotImplementedException("UseForEach() is not implemented yet.");
//        }

//        /// <summary>
//        /// This method counts the multiples of 4 from the provided List<int>. 
//        /// Exit the loop when the integer 1234 is found.
//        /// Return the total number of multiples of 4.
//        /// </summary>
//        /// <param name="x"></param>
//        public static int UseWhile(List<int> x)
//        {
//            throw new NotImplementedException("UseFor() is not implemented yet.");
//        }

//        /// <summary>
//        /// This method will evaluate the Int Array provided and return how many of its 
//        /// values are multiples of 3 and 4.
//        /// </summary>
//        /// <param name="x"></param>
//        /// <returns></returns>
//        public static int UseForThreeFour(int[] x)
//        {
//            throw new NotImplementedException("UseForThreeFour() is not implemented yet.");
//        }

//        /// <summary>
//        /// This method takes an array of List<string>'s. 
//        /// It concatenates all the strings, with a space between each, in the Lists and returns the resulting string.
//        /// </summary>
//        /// <param name="stringListArray"></param>
//        /// <returns></returns>
//        public static string LoopdyLoop(List<string>[] stringListArray)
//        {
//            throw new NotImplementedException("LoopdyLoop() is not implemented yet.");
//        }
//    }
//}