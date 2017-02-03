using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebRole1.interfaces;

namespace WebRole1.models
{
    public class Trie : ITrie
    {
        private TrieNode root = new TrieNode();

        public void AddTitle(string prefix)
        {
            AddTitleHelper(prefix, this.root);
        }

        private void AddTitleHelper(string prefix, TrieNode node)
        {
            if (prefix.Length == 0) // handle base case
            {
                return;
            }

            char firstLetter = prefix[0];
            TrieNode nextNode = node.containsChildWith(firstLetter);

            if (nextNode == null) // char not there yet, so add it and recurse
            {
                nextNode = new TrieNode(); // create new node
                nextNode.setData(firstLetter); // set its data
                node.addChild(nextNode); // add new node to current node's collection
                
                if (prefix.Length == 1)
                {
                    nextNode.markAsWordEnding();
                }

            }
            AddTitleHelper(prefix.Substring(1), nextNode); // recurse
        }

        public List<string> SearchForPrefix(string prefix)
        {
            TrieNode prefixRoot = GetPrefixRoot(prefix);
            List<string> results = SearchFromPrefixRoot(prefixRoot);

            return results;
        }

        private TrieNode GetPrefixRoot(string prefix)
        {
            return new TrieNode();
        }

        private List<string> SearchFromPrefixRoot(TrieNode prefixRoot)
        {
            List<string> results = new List<string>();
            return results;
        }

    }
}