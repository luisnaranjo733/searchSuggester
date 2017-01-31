using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebRole1.interfaces;

namespace WebRole1.models
{
    public class Trie : ITrie
    {
        private ITrieNode root;
        public Trie()
        {
            root = new TrieNode();
        }

        public void AddTitle(string title)
        {

        }

        public string[] SearchForPrefix(string query)
        {
            List<string> suggestions = new List<string>();
            suggestions.Add("microwave");
            return suggestions.ToArray();
        }
    }
}