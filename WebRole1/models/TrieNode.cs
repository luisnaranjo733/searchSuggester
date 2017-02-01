using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebRole1.interfaces;

namespace WebRole1.models
{
    public class TrieNode : ITrie
    {
        private char data { get; set; }
        private bool isWordEnding = false;
        private List<TrieNode> children = new List<TrieNode>();

        public override string ToString()
        {
            return "< " + data + "/>";
        }
        
        /// <summary>
        /// Add a word to the trie
        /// </summary>
        /// <param name="title">The name of the word to be added to the trie. Must be non-empty string</param>
        public void AddTitle(string title)
        {
            if (title.Length == 0) // do nothing if illegal string passed in
            {
                return;
            }

            char firstLetter = title[0];
            bool firstLetterFound = false;
            foreach(TrieNode child in children)
            {
                if (child.data == firstLetter) // character is present, so get next node and recurse
                {
                    firstLetterFound = true;
                    child.AddTitle(title.Substring(1));
                    break; // character found, all recursion needs to be based off this character, so stop looping through adjacent irrelevant branches
                }
            }

            if (!firstLetterFound) // character is not present yet, so add it and then recurse
            {
                TrieNode childNode = new TrieNode();
                childNode.data = firstLetter;
                children.Add(childNode);

                if (title.Length == 1) // childNode is the last character of a title, so mark it and stop recursing
                {
                    childNode.isWordEnding = true;
                }

                if (title.Length > 1) // childNode is not the last character of a title yet, so keep recursing
                {
                    childNode.AddTitle(title.Substring(1));
                } 
            }
        }

        /// <summary>
        /// Fetch a list of search sugguestions for a prefix
        /// </summary>
        /// <param name="title">Title is the prefix string search query</param>
        /// <returns>List which represents a collection of the complete search results found. May be empty but will always be defined</returns>
        public List<string> SearchForPrefix(string title)
        {
            TrieNode trie = GetWordTree(title);
            List<string> suggestions = new List<string>();
            if (trie != null)
            {
                suggestions = GetSuggestions(title, new StringBuilder(title), trie);
            }
            return suggestions;
        }

        /// <summary>
        /// This recursive method searches a trie for search suggestions
        /// </summary>
        /// <param name="title">Search prefix string</param>
        /// <param name="buffer">StringBugger of search prefix string</param>
        /// <param name="node">Node of Trie to begin search from (doesn't have to be root - use GetWordTree() for faster)</param>
        /// <returns></returns>
        private List<string> GetSuggestions(string title, StringBuilder buffer, TrieNode node)
        {
            List<string> suggestions = new List<string>();

            foreach(TrieNode child in node.children)
            {
                string bufferString = buffer.ToString();
                StringBuilder newBuffer = new StringBuilder(bufferString);
                newBuffer.Append(child.data);

                if (child.isWordEnding)
                {
                    suggestions.Add(newBuffer.ToString());
                }
                if (child.children.Count > 0)
                {
                    suggestions.AddRange(GetSuggestions(title, newBuffer, child));
                }
            }

            return suggestions;
        }

        /// <summary>
        /// This method returns a reference to the TrieNode which represents the last character of the search prefix.
        /// This is a private helper method for SearchForPrefix.
        /// e.g: GetWordTree("dog") -> returns a reference to the TrieNode "g" (last letter of "dog") so that you can begin
        /// searching from that node with GetSuggestions()
        /// </summary>
        /// <param name="title">search prefix</param>
        /// <returns>Reference to TrieNode</returns>
        private TrieNode GetWordTree(string title)
        {
            TrieNode wordTree = null;

            // if user searches for an empty string + also this is the recursive base case
            if (title.Length == 0)
            {
                return null;
            }

            char firstLetter = title[0];
            foreach (TrieNode child in children)
            {
                if (child.data == firstLetter)
                {
                    title = title.Substring(1);
                    wordTree = child.GetWordTree(title);

                    if (wordTree == null)
                    {
                        return child;
                    } else
                    {
                        return wordTree;
                    }
                    break;
                }
            }

            return wordTree;
        }
    }
}