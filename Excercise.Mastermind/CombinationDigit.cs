namespace Excercise.Mastermind
{
    internal class CombinationDigit
    {
        public CombinationDigit(int sequence, int value)
        {
            Value = value;
            Sequence = sequence;
        }

        public int Value { get; set; }
        public int Sequence { get; set; }
    }
}
