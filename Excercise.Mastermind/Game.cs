using System;

namespace Excercise.Mastermind
{
    internal class Game
    {
        //Use events to abstract the fact this is a console game. The exception is the debug message, but that is OK.
        public event EventHandler DisplayWelcome;
        public event EventHandler PrintWinner;
        public event EventHandler PrintReminder;
        public event EventHandler PrintLoser;
        public event EventHandler PrintHint;

        public int answerLength;
        public int numberOfAttempts;
        public readonly int lowestPossibleValue;
        public readonly int highestPossibleValue;
        private  Combination _answer;
        internal int attempt;

        private bool _playerHasWon = false;
        public bool PlayerHasWon { get { return _playerHasWon; } } 

        /// <summary>
        /// Creates a new Game with the parameters specified.
        /// </summary>
        /// <param name="answerLength">The number of digits the answer will have.</param>
        /// <param name="lowestPossibleValue">The lowest value for any one digit.</param>
        /// <param name="highestPossibleValue">This highest value for any one digit.</param>
        /// <param name="numberOfAttempts">The number of guesses the user will have.</param>
        public Game(int answerLength, int lowestPossibleValue, int highestPossibleValue, int numberOfAttempts)
        {
            this.answerLength = answerLength;
            this.lowestPossibleValue = lowestPossibleValue;
            this.highestPossibleValue = highestPossibleValue;
            this.numberOfAttempts = numberOfAttempts;
        }

        public void NewGame()
        {
            int[] answer = CreateRandomAnswer(lowestPossibleValue, highestPossibleValue, answerLength);
            _answer = new Combination(answer);
            attempt = 0;
            _playerHasWon = false;

            DebugPrintAnswer();
            DisplayWelcome(this, null);
        }

        //This method is for unit testing only.
        internal void SetAnswer(int[] answer, int numberOfAttempts)
        {
            _answer = new Combination(answer);
            answerLength = answer.Length;
            this.numberOfAttempts = numberOfAttempts;
        }

        private int[] CreateRandomAnswer(int lowestPossibleValue, int highestPossibleValue, int length)
        {
            int[] answer = new int[length];
            var rand = new Random();
            for (int i = 0; i < length; i++)
            {
                answer[i] = rand.Next(lowestPossibleValue, highestPossibleValue + 1);
            }

            return answer;
        }
        
        public void Guess(string userInput)
        {
            bool userIsCorrect = false;
            if (attempt < numberOfAttempts)
            {
                if (UserInputIsValid(userInput))
                {
                    attempt++; //Only count valid guesses.
                    Combination userGuess = new Combination(userInput); //create a new Combination object. We'll use that to compare to the answer.
                    userGuess.CompareToAnswer(_answer, out int correctDigitsInTheCorrectPlace, out int correctDigitsInTheWrongPlace, out userIsCorrect);

                    if (userIsCorrect)
                    {
                        _playerHasWon = true;
                        PrintWinner(this, null);
                    }
                    else
                    {
                        string hint = GenerateHint(correctDigitsInTheCorrectPlace, correctDigitsInTheWrongPlace);
                        PrintHint(hint, null);
                    }
                }
                else
                {
                    PrintReminder(this, null); //Tell the user they entered an invalid guess.
                }
            }

            if (!_playerHasWon && !ContinueGame())
            {
                PrintLoser(this, null); //The player didn't guess correctly and the game is over. Give them the bad news.
            }
        }

        public bool ContinueGame()
        {
            if (attempt >= numberOfAttempts) return false;

            if (PlayerHasWon) return false;

            return true;
        }

        private string GenerateHint(int correctDigitsInTheCorrectPlace, int correctDigitsInTheWrongPlace)
        {
            string hint = string.Empty;
            int counter = 0;

            while (counter < correctDigitsInTheCorrectPlace)
            {
                hint = hint + "+";
                counter++;
            }

            counter = 0;

            while (counter < correctDigitsInTheWrongPlace)
            {
                hint = hint + "-";
                counter++;
            }

            return hint;
        }

        private bool UserInputIsValid(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput)) return false; //the user needs to enter a value

            int intValue;
            if (!int.TryParse(userInput, out intValue)) return false; //the value must be an integer

            if (intValue < 0) return false; //the value must be zero or greater

            //at this point we know intValue is an int and is >=0
            if (intValue.ToString().Length != answerLength) return false;

            return true;
        }

        [System.Diagnostics.Conditional("DEBUG")] //Use the Debug conditional so this method is only called in debug mode.
        private void DebugPrintAnswer()
        {
            Console.WriteLine();
            Console.Write($"You are build the application in debug mode. The answer is: {_answer.ToString()}");
            Console.WriteLine();
        }

    }
}
