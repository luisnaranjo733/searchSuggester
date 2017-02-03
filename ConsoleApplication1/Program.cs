using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRole1.models;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie();

            string seedFilePath = @"C:\code\info344\hwk2\seed_super_short.txt";

            foreach (string title in File.ReadLines(seedFilePath))
            {
                float memoryRemaining = 21;
                if (memoryRemaining > 20)
                {
                    trie.AddTitle(title);
                }

            }

            while (true)
            {
                Console.WriteLine("Query: ");
                string searchQuery = Console.ReadLine();
                List<string> results = trie.SearchForPrefix(searchQuery);
                foreach(string result in results)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
