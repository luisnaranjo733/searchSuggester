//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace WebRole1.models
//{
//    public class old
//    {

//        /// <summary>
//        /// Add a word to the trie
//        /// </summary>
//        /// <param name="searchPrefix">The name of the word to be added to the trie. Must be non-empty string</param>
//        public void AddTitle(string searchPrefix)
//        {
//            if (searchPrefix.Length == 0) // do nothing if illegal string passed in
//            {
//                return;
//            }

//            char firstLetter = searchPrefix[0];
//            bool firstLetterFound = false;
//            foreach (TrieNode child in children)
//            {
//                if (child.data == firstLetter) // character is present, so get next node and recurse
//                {
//                    firstLetterFound = true;
//                    child.AddTitle(searchPrefix.Substring(1));
//                    break; // character found, all recursion needs to be based off this character, so stop looping through adjacent irrelevant branches
//                }
//            }

//            if (!firstLetterFound) // character is not present yet, so add it and then recurse
//            {
//                TrieNode childNode = new TrieNode();
//                childNode.data = Char.ToLower(firstLetter);
//                children.Add(childNode);

//                if (searchPrefix.Length == 1) // childNode is the last character of a title, so mark it and stop recursing
//                {
//                    childNode.isWordEnding = true;
//                }

//                if (searchPrefix.Length > 1) // childNode is not the last character of a title yet, so keep recursing
//                {
//                    childNode.AddTitle(searchPrefix.Substring(1));
//                }
//            }
//        }


//        /// <summary>
//        /// Fetch a list of search sugguestions for a prefix
//        /// </summary>
//        /// <param name="searchPrefix">Title is the prefix string search query</param>
//        /// <returns>List which represents a collection of the complete search results found. May be empty but will always be defined</returns>
//        public List<string> SearchForPrefix(string searchPrefix)
//        {
//            searchPrefix = searchPrefix.ToLower();
//            TrieNode trie = GetWordTree(searchPrefix); // reference to last character of prefix string in trie
//            List<string> suggestions = new List<string>();
//            if (trie != null)
//            {
//                suggestions = GetSuggestions(searchPrefix, new StringBuilder(searchPrefix), trie); // start recursive search from end of prefix string
//            }
//            return suggestions;
//        }


//        /// <summary>
//        /// This recursive helper method returns a reference to the TrieNode which represents the last character of the search prefix.
//        /// This is a private helper method for SearchForPrefix.
//        /// e.g: GetWordTree("dog") -> returns a reference to the TrieNode "g" (last letter of "dog") so that you can begin
//        /// searching from that node with GetSuggestions()
//        /// </summary>
//        /// <param name="searchPrefix">search prefix</param>
//        /// <returns>Reference to TrieNode</returns>
//        private TrieNode GetWordTree(string searchPrefix)
//        {
//            TrieNode wordTree = null;

//            // if user searches for an empty string + also this is the recursive base case
//            if (searchPrefix.Length == 0)
//            {
//                return null;
//            }

//            char firstLetter = searchPrefix[0];
//            foreach (TrieNode child in children)
//            {
//                if (child.data == firstLetter)
//                {
//                    searchPrefix = searchPrefix.Substring(1);
//                    wordTree = child.GetWordTree(searchPrefix);

//                    if (wordTree == null)
//                    {
//                        return child;
//                    }
//                    else
//                    {
//                        return wordTree;
//                    }
//                    break; // character found, all recursion needs to be based off this character, so stop looping through adjacent irrelevant branches
//                }
//            }

//            return wordTree;
//        }

//        /// <summary>
//        /// This recursive helper method searches a trie for search suggestions
//        /// </summary>
//        /// <param name="searchPrefix">Search prefix string</param>
//        /// <param name="buffer">StringBugger of search prefix string</param>
//        /// <param name="node">Node of Trie to begin search from (doesn't have to be root - use GetWordTree() for faster)</param>
//        /// <returns></returns>
//        private List<string> GetSuggestions(string searchPrefix, StringBuilder buffer, TrieNode node)
//        {
//            List<string> suggestions = new List<string>();

//            foreach (TrieNode child in node.children)
//            {
//                string bufferString = buffer.ToString();
//                StringBuilder newBuffer = new StringBuilder(bufferString);
//                newBuffer.Append(child.data);

//                if (child.isWordEnding)
//                {
//                    suggestions.Add(newBuffer.ToString());
//                }
//                if (child.children.Count > 0)
//                {
//                    suggestions.AddRange(GetSuggestions(searchPrefix, newBuffer, child));
//                }
//            }

//            return suggestions;
//        }
//    }
//}