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
            var usedReelPositions = new List<int>();
            var lettersInReels = AvailableLetters.ToList();
            foreach (var letter in word)
            {
                usedReelPositions.Add(lettersInReels.IndexOf(letter));
            }

            foreach (var position in usedReelPositions)
            {
                var reelToMove = _reels.ElementAt(position);
                reelToMove.MoveLetterToTop();
            }
        }
    }
}
