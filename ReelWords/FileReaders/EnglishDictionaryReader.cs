using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReelWords.FileReaders
{
    public class EnglishDictionaryReader : IDisposable
    {
        private readonly Stream _stream;

        public EnglishDictionaryReader(Stream stream)
        {
            _stream = stream;
        }

        public async Task<Trie> ReadAsync()
        {
            var words = await ReadAllWords();
            return BuildTrie(words);
        }

        private async Task<IEnumerable<string>> ReadAllWords()
        {
            using var streamReader = new StreamReader(_stream);
            var words = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var word = await streamReader.ReadLineAsync();
                words.Add(word);
            }

            return words.ToArray();
        }

        private Trie BuildTrie(IEnumerable<string> words)
        {
            var trie = new Trie();
            foreach (var word in words)
            {
                trie.Insert(word);
            }

            return trie;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
