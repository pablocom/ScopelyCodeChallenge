using System.Collections.Generic;

namespace ReelWords
{
    public class Trie
    {
        private const char RootSymbol = '^';
        private const char WordEndSymbol = '=';

        private readonly TrieNode _head = new TrieNode(RootSymbol, null);

        public void Insert(string word)
        {
            var currentNode = _head;
            foreach (var letter in word)
            {
                if (currentNode.TryGetChildWithCharacter(letter, out var child))
                {
                    currentNode = child;
                    continue;
                }

                var newChild = new TrieNode(letter, currentNode);
                currentNode.AddChild(newChild);
                currentNode = newChild;
            }

            if (!currentNode.AnyChildWithCharacter(WordEndSymbol))
                currentNode.AddChild(new TrieNode(WordEndSymbol, currentNode));
        }

        public bool Search(string word)
        {
            var currentNode = _head;

            foreach (var letter in word)
            {
                if (!currentNode.TryGetChildWithCharacter(letter, out var child))
                    return false;

                currentNode = child;
            }

            return currentNode.AnyChildWithCharacter(WordEndSymbol);
        }

        public void Delete(string word)
        {
            var currentNode = _head;

            foreach (var letter in word)
            {
                if (!currentNode.TryGetChildWithCharacter(letter, out var child))
                    return;

                currentNode = child;
            }

            var nodeToRemove = currentNode.GetChildWithCharacter(WordEndSymbol);
            while (!nodeToRemove.HasChildren() && nodeToRemove != _head)
            {
                var parent = nodeToRemove.Parent;
                parent.RemoveChildWithCharacter(nodeToRemove.Character);
                nodeToRemove = parent;
            }
        }

        private class TrieNode
        {
            public char Character { get; }
            public TrieNode Parent { get; }

            private readonly IDictionary<char, TrieNode> _childrenByCharacter = new Dictionary<char, TrieNode>();

            public TrieNode(char character, TrieNode parent)
            {
                Character = character;
                Parent = parent;
            }

            public void AddChild(TrieNode child)
            {
                _childrenByCharacter.Add(child.Character, child);
            }

            public TrieNode GetChildWithCharacter(char character)
            {
                return _childrenByCharacter[character];
            }

            public bool TryGetChildWithCharacter(char character, out TrieNode child)
            {
                return _childrenByCharacter.TryGetValue(character, out child);
            }

            public void RemoveChildWithCharacter(char character)
            {
                _childrenByCharacter.Remove(character);
            }

            public bool AnyChildWithCharacter(char character)
            {
                return _childrenByCharacter.ContainsKey(character);
            }

            public bool HasChildren()
            {
                return _childrenByCharacter.Count > 0;
            }
        }
    }
}