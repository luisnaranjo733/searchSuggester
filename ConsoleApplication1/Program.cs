using System;
using System.Collections.Generic;
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
            TrieNode trie = new TrieNode();
            trie.AddTitle("dog");
            trie.AddTitle("doggy");
            trie.AddTitle("car");
            trie.AddTitle("canada");
            while (true)
            {
                Console.WriteLine("Search query: ");
                string query = Console.ReadLine();
                foreach(string result in trie.SearchForPrefix(query))
                {
                    Console.WriteLine(result);
                }
                
            }
        }
    }
}
