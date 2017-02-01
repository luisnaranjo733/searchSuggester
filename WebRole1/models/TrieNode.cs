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
        /// <param name="title">The name of the word to be added to the trie</param>
        public void AddTitle(string title)
        {
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
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
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