using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReelWords.FileReaders
{
    internal class ReelsReader : IDisposable
    {
        private readonly Stream _stream;

        public ReelsReader(Stream stream)
        {
            _stream = stream;
        }

        public async Task<IEnumerable<Reel>> ReadAsync()
        {
            using var streamReader = new StreamReader(_stream);
            var reels = new List<Reel>();

            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();

                var letters = line.Split(' ').Select(char.Parse).ToArray();

                var reel = new Reel(letters, new Random().Next(1, letters.Length));
                reels.Add(reel);
            }

            return reels.ToArray();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
