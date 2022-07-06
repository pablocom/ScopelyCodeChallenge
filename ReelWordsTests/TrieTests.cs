using ReelWords;
using Xunit;

namespace ReelWordsTests
{
    public class TrieTests
    {
        private const string AWESOME_CO = "pierplay";

        private readonly Trie _trie;

        public TrieTests()
        {
            _trie = new Trie();
        }

        [Fact]
        public void TrieInsertTest()
        {
            Trie trie = new Trie();
            trie.Insert(AWESOME_CO);
            Assert.True(trie.Search(AWESOME_CO));
        }

        [Fact(Skip = "Pending clarification")]
        public void TrieDeleteTest()
        {
            Trie trie = new Trie();
            trie.Insert(AWESOME_CO);
            Assert.True(trie.Search(AWESOME_CO));
            trie.Delete(AWESOME_CO);
            Assert.True(trie.Search(AWESOME_CO));
        }

        [Fact]
        public void DoNotFindWordIfWasNotAdded()
        {
            var isWordFound = _trie.Search("NonExistingWord");

            Assert.False(isWordFound);
        }

        [Fact]
        public void InsertsOneWord()
        {
            var word = "rare";

            _trie.Insert(word);

            var isWordFound = _trie.Search(word);
            Assert.True(isWordFound);
        }

        [Fact]
        public void InsertsTwoWordsDifferingTheLastLetter()
        {
            var word1 = "cat";
            var word2 = "cats";

            _trie.Insert(word1);
            _trie.Insert(word2);

            var isWord1Found = _trie.Search(word1);
            var isWord2Found = _trie.Search(word2);
            Assert.True(isWord1Found);
            Assert.True(isWord2Found);
        }

        [Fact]
        public void InsertsTwoTimesTheSameWord()
        {
            var word = "cat";

            _trie.Insert(word);
            _trie.Insert(word);

            var isWordFound = _trie.Search(word);
            Assert.True(isWordFound);
        }

        [Fact]
        public void DeletesTheOnlyExistingWord()
        {
            var wordToBeDeleted = "cat";
            _trie.Insert(wordToBeDeleted);

            _trie.Delete(wordToBeDeleted);

            Assert.False(_trie.Search(wordToBeDeleted));
        }

        [Fact]
        public void DeletesASingleWord()
        {
            var wordToBeDeleted = "cat";
            var wordToBePreserved = "bear";
            _trie.Insert(wordToBeDeleted);
            _trie.Insert(wordToBePreserved);

            _trie.Delete(wordToBeDeleted);

            Assert.False(_trie.Search(wordToBeDeleted));
            Assert.True(_trie.Search(wordToBePreserved));
        }

        [Fact]
        public void DeletesWordThatIsASubsequenceOfAnotherPresentWord()
        {
            var wordToBeDeleted = "cat";
            var wordToBePreserved = "cats";
            _trie.Insert(wordToBeDeleted);
            _trie.Insert(wordToBePreserved);

            _trie.Delete(wordToBeDeleted);

            Assert.False(_trie.Search(wordToBeDeleted));
            Assert.True(_trie.Search(wordToBePreserved));
        }

        [Fact]
        public void DeletesWordThatContainsSubsequenceOfAnotherExistingWord()
        {
            var wordToBeDeleted = "cats";
            var wordToBePreserved = "cat";
            _trie.Insert(wordToBeDeleted);
            _trie.Insert(wordToBePreserved);

            _trie.Delete(wordToBeDeleted);

            Assert.False(_trie.Search(wordToBeDeleted));
            Assert.True(_trie.Search(wordToBePreserved));
        }

        [Fact]
        public void DeletesWordWithAnotherPresentSubsequenceInCommon()
        {
            var wordToBeDeleted = "paula";
            var wordToBePreserved = "pablo";
            _trie.Insert(wordToBeDeleted);
            _trie.Insert(wordToBePreserved);

            _trie.Delete(wordToBeDeleted);

            Assert.False(_trie.Search(wordToBeDeleted));
            Assert.True(_trie.Search(wordToBePreserved));
        }
    }
}
