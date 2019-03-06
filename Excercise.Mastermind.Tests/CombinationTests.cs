using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Mastermind.Tests
{
    [TestFixture]
    public class CombinationTests
    {
        [TestCase("4321", 4, 3, 2, 1)]
        [TestCase("1234", 1, 2, 3, 4)]
        [TestCase("1144", 1, 1, 4, 4)]
        [TestCase("  1144  ", 1, 1, 4, 4)]
        [TestCase("  1X44  ", 1, 0, 4, 4)]
        public void TestCreationFromString(string inputString, int digit1, int digit2, int digit3, int digit4)
        {
            //Arrange

            //Act
            var combo = new Combination(inputString);

            //Assert
            Assert.AreEqual(digit1, combo.CombinationDigits.FirstOrDefault(x => x.Sequence == 0)?.Value);
            Assert.AreEqual(digit2, combo.CombinationDigits.FirstOrDefault(x => x.Sequence == 1)?.Value);
            Assert.AreEqual(digit3, combo.CombinationDigits.FirstOrDefault(x => x.Sequence == 2)?.Value);
            Assert.AreEqual(digit4, combo.CombinationDigits.FirstOrDefault(x => x.Sequence == 3)?.Value);
        }

        [TestCase(4, 3, 2, 1)]
        [TestCase(1, 2, 3, 4)]
        [TestCase(1, 1, 4, 4)]
        [TestCase(1, 0, 4, 4)]
        public void TestCreationFromArray(int digit1, int digit2, int digit3, int digit4)
        {
            //Arrange
            int[] inputArray = { digit1, digit2, digit3, digit4 };

            //Act
            var combo = new Combination(inputArray);

            //Assert
            Assert.AreEqual(digit1, combo.CombinationDigits.FirstOrDefault(x => x.Sequence == 0)?.Value);
            Assert.AreEqual(digit2, combo.CombinationDigits.FirstOrDefault(x => x.Sequence == 1)?.Value);
            Assert.AreEqual(digit3, combo.CombinationDigits.FirstOrDefault(x => x.Sequence == 2)?.Value);
            Assert.AreEqual(digit4, combo.CombinationDigits.FirstOrDefault(x => x.Sequence == 3)?.Value);
        }

        [TestCase("1234", "1234", 4, 0, true)]
        [TestCase("1234", "1255", 2, 0, false)]
        [TestCase("1234", "2155", 0, 2, false)]
        [TestCase("1234", "5678", 0, 0, false)]
        [TestCase("2134", "1234", 2, 2, false)]
        public void TestCompare(string guessString, string answerString, int expectedCorrectInCorrectPlace, int expectedCorreceInTheWrongPlace, bool expectedUserIsCorrect)
        {
            //Arrange
            var guess = new Combination(guessString);
            var answer = new Combination(answerString);

            //Act
            guess.CompareToAnswer(answer, out int correctDigitInCorrectPlace, out int correctDigitsInTheWrongPlace, out bool userIsCorrect);

            //Assert
            Assert.AreEqual(expectedCorrectInCorrectPlace, correctDigitInCorrectPlace);
            Assert.AreEqual(expectedCorreceInTheWrongPlace, correctDigitsInTheWrongPlace);
            Assert.AreEqual(expectedUserIsCorrect, userIsCorrect);
        }
    }
}
