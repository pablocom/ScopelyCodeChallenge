using System;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords
{
    public class Reel
    {
        private readonly char[] _letters;
        private int _position;

        public char CurrentLetter => _letters[_position - 1];

        public Reel(IEnumerable<char> letters, int initialPosition)
        {
            if (initialPosition < 1 || initialPosition > letters.Count())
                throw new ArgumentOutOfRangeException(nameof(initialPosition));

            _letters = letters.ToArray();
            _position = initialPosition;
        }

        public void MoveToNext()
        {
            if (_letters.Length == _position)
                _position = 1;
            else
                _position++;
        }
    }
}
