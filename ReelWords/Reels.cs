using System;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords
{
    public class Reels
    {
        private readonly IEnumerable<Reel> _reels;
        public IEnumerable<char> AvailableLetters => _reels.Select(r => r.CurrentLetter);

        public Reels(IEnumerable<Reel> reels)
        {
            if (reels is null)
                throw new ArgumentNullException(nameof(reels));

            _reels = reels;
        }

        public bool AvailableLettersCanForm(string word)
        {
            var remainingLetters = AvailableLetters.ToList();
            if (remainingLetters.Count < word.Length)
                return false;

            foreach (var letter in word)
            {
                if (!remainingLetters.Contains(letter))
                    return false;

                remainingLetters.Remove(letter);
            }

            return true;
        }

        public void MoveReelsUsedToForm(string word)
        {
            const char visitedReelIndexMarker = '\0';

            var availableLetters = AvailableLetters.ToArray();
            var usedReelIndexes = new HashSet<int>(_reels.Count());
            foreach (var letter in word)
            {
                var usedIndex = Array.IndexOf(availableLetters, letter);

                usedReelIndexes.Add(usedIndex);
                availableLetters[usedIndex] = visitedReelIndexMarker;
            }

            foreach (var position in usedReelIndexes)
            {
                var reel = _reels.ElementAt(position);
                reel.MoveToNext();
            }
        }
    }
}
