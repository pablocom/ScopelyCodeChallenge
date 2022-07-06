using System.Collections.Generic;
using System.Linq;

namespace ReelWords
{
    public class TrieNode
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
            return _childrenByCharacter.Any();
        }
    }
}
