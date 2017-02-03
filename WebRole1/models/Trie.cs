using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebRole1.interfaces;

namespace WebRole1.models
{
    public class Trie : ITrie
    {
        private TrieNode root = new TrieNode();

        public void AddTitle(string title)
        {
            AddTitleHelper(title, this.root);
        }

        private void AddTitleHelper(string title, TrieNode node)
        {
            if (title.Length == 0) // handle base case
            {
                return;
            }

            char firstLetter = title[0];
            TrieNode nextNode = node.containsChildWith(firstLetter);

            if (nextNode == null) // char not there yet, so add it and recurse
            {
                nextNode = new TrieNode(); // create new node
                nextNode.setData(firstLetter); // set its data
                node.addChild(nextNode); // add new node to current node's collection
                
                if (title.Length == 1)
                {
                    nextNode.markAsWordEnding();
                }

            }
            AddTitleHelper(title.Substring(1), nextNode); // recurse
        }

        public List<string> SearchForPrefix(string prefix)
        {
            TrieNode prefixRoot = GetPrefixRoot(prefix, this.root);
            List<string> results = new List<string>();
            if (prefixRoot != null)
            {
                results = SearchFromPrefixRoot(prefix, prefixRoot);
            }
            
            return results;
        }

        private TrieNode GetPrefixRoot(string prefix, TrieNode node)
        {
            if (prefix.Length == 0)
            {
                return node;
            }

            char firstLetter = prefix[0];
            TrieNode nextNode = node.containsChildWith(firstLetter);

            if (nextNode != null)
            {
                return GetPrefixRoot(prefix.Substring(1), nextNode);

            } else
            {
                return null;
            }
        }

        private List<string> SearchFromPrefixRoot(string prefix, TrieNode prefixRoot)
        {
            List<string> results = new List<string>();

            foreach(TrieNode childNode in prefixRoot.getChildren())
            {
                StringBuilder suggestionCandidate = new StringBuilder(prefix);
                suggestionCandidate.Append(childNode.getData());

                if (childNode.isWordEnding)
                {
                    results.Add(suggestionCandidate.ToString());
                }

                results.AddRange(SearchFromPrefixRoot(suggestionCandidate.ToString(), childNode));
            }

            return results;
        }

    }
}