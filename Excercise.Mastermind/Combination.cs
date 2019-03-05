using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excercise.Mastermind
{
    internal class Combination
    {
        public List<CombinationDigit> CombinationDigits { get; set; }

        public Combination(string combo)
        {
            CombinationDigits = new List<CombinationDigit>();
            for (int i = 0; i<combo.Length; i++)
            {
                int result = 0;
                int.TryParse(combo[i].ToString(), out result);
                CombinationDigits.Add(new CombinationDigit(i, result));
            }
        }

        public Combination(int[] combo)
        {
            CombinationDigits = new List<CombinationDigit>();
            for (int i = 0; i < combo.Length; i++)
            {
                CombinationDigits.Add(new CombinationDigit(i, combo[i]));
            }
        }

        public void CompareToAnswer(Combination answer, out int correctDigitInCorrectPlace, out int correctDigitsInTheWrongPlace, out bool userIsCorrect)
        {
            correctDigitInCorrectPlace = 0;
            correctDigitsInTheWrongPlace = 0;
            userIsCorrect = false;
            //this is the guess
            foreach (CombinationDigit digit in CombinationDigits)
            {
                if(answer.CombinationDigits.Any(answerDigit => answerDigit.Sequence == digit.Sequence && answerDigit.Value == digit.Value))
                {
                    correctDigitInCorrectPlace++;
                }
                else if(answer.CombinationDigits.Any(answerDigit => answerDigit.Sequence != digit.Sequence && answerDigit.Value == digit.Value))
                {
                    correctDigitsInTheWrongPlace++;
                }
            }

            //todo: make this better.
            if (correctDigitInCorrectPlace == CombinationDigits.Count)
            {
                userIsCorrect = true;
            }
            
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (CombinationDigit digit in CombinationDigits.OrderBy(d => d.Sequence))
            {
                builder.Append(digit.Value.ToString());
            }

            return builder.ToString();
        }
    }
}
