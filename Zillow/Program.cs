namespace Zillow
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static IList<int> randomIntegers;
        
        static void Main(string[] args)
        {
            testTernaryTree();
            testStringToLong();
            Console.ReadLine();
        }

        private static void testStringToLong() {
            var helper = new StringHelper();
            var testStrings = new String[] {"123", "-234", "0" ,"12312453523", "abcde"};
            foreach (var str in testStrings) {
                long i = helper.stringToLong(str);
                Console.WriteLine("Input {0} - output {1}", str, i);
            }
        }

        /// <summary>
        /// Driver for testing the ternary tree.
        /// </summary>
        private static void testTernaryTree() {
            initializeInputs();
            var tree = new TernaryTree<int>();
            foreach (var n in randomIntegers) {
                tree.Add(n);
            }

            // pick the 1st element and delete
            var first = randomIntegers[0];
            tree.Delete(first);
            // pick the 5th element and delete
            var fifth = randomIntegers[4];
            tree.Delete(fifth);
            // pick the last element and delete
            var last = randomIntegers[randomIntegers.Count - 1];
            tree.Delete(last);
        }

        /// <summary>
        /// Use a statically defined input to initialize input values for the tree.
        /// </summary>
        private static void initializeInputs() {
            randomIntegers = new List<int>() { 85, 81, 47, 24, 83, 2, 12, 95, 16, 74, 27, 83, 71, 99, 13, 73, 26, 80, 95, 0 };
        }
    }
}
