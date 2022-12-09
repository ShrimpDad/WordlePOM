using System;
using System.Collections.Generic;
using System.Text;

namespace WordlePOM
{
    public class LetterState
    {
        public string Letter;
        public string State;
        public int Position;

        public LetterState(string letter, string state, int position)
        {
            Letter = letter;
            State = state;
            Position = position;
        }
    }
}
