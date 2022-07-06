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
    }
}