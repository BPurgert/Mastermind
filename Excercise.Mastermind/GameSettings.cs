namespace Excercise.Mastermind
{
    internal class GameSettings
    {
        //Answers should be 4 digits long. Each digit ranging from 1 to 6.
        private const int ANSWER_LENGTH = 4;

        private const int LOWEST_POSSIBLE_DIGIT_VALUE = 1;
        private const int HIGHEST_POSSIBLE_DIGIT_VALUE = 6;
        private const int NUMBER_OF_ATTEMPTS = 10;

        public int AnswerLength { get { return ANSWER_LENGTH; } }
        public int LowestPossibleDigitValue { get { return LOWEST_POSSIBLE_DIGIT_VALUE; } }
        public int HighestPossibleDigitValue { get { return HIGHEST_POSSIBLE_DIGIT_VALUE; } }
        public int NumberOfAttempts { get { return NUMBER_OF_ATTEMPTS; } }
    }
}