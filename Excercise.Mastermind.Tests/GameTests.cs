using NUnit.Framework;
using System;
using System.Text;

namespace Excercise.Mastermind.Tests
{
    [TestFixture]
    public class GameTests
    {
        private Game _game;
        private int _reminderCount;
        private string _hint;
        private bool _userWon;
        private bool _userLost;

        [SetUp]
        public void InitGame()
        {
            _game = new Game(4, 1, 6, 10);
            _game.DisplayWelcome += PrintWelcome;
            _game.PrintReminder += PrintReminder;
            _game.PrintWinner += PrintWinner;
            _game.PrintHint += PrintHint;
            _game.PrintLoser += PrintLoser;
            _game.NewGame();

            _reminderCount = 0;
            _userLost = false;
            _userWon = false;
        }

        [TearDown]
        public void Cleanup()
        {
            _game.DisplayWelcome -= PrintWelcome;
            _game.PrintReminder -= PrintReminder;
            _game.PrintWinner -= PrintWinner;
            _game.PrintHint -= PrintHint;
            _game.PrintLoser -= PrintLoser;
        }

        [Test] //There is a limited number of valid inputs. Test them all.
        public void ValidInputsCountAsAnAttempt(
            [Range(1, 6)] int digit1,
            [Range(1, 6)] int digit2,
            [Range(1, 6)] int digit3,
            [Range(1, 6)] int digit4)
        {
            //Arrange
            int[] answer = { 7, 7, 7, 7 }; //7777 is outside the range of an normal answer, so all our guesses will be wrong. We want that.
            _game.SetAnswer(answer, 10);
            string guess = $"{digit1}{digit2}{digit3}{digit4}";

            //Act
            Assert.AreEqual(0, _game.attempt); //make sure the state is what we expect before we start.
            _game.Guess(guess);

            //Assert
            Assert.AreEqual(1, _game.attempt); //verify we used one attempt
            Assert.AreEqual(true, _game.ContinueGame()); //verify the game is still going.
            Assert.AreEqual(false, _game.PlayerHasWon); //verify the player did not win
        }

        [Test]
        public void RandomStringsShouldTriggerAReminder(
            [Random(0, 100, 100)] int numberOfCharacters)
        {
            //Arrange
            string userInput = RandomString(numberOfCharacters);

            //Act
            _game.Guess(userInput);

            //Assert
            Assert.AreEqual(1, _reminderCount); //We should be reminding the user of the rules.
        }

        [TestCase(1, 1, 5, 6, "+-")]  //1 is a correct digit in the correct place. It is also a correct digit in the incorrect place. Quote from the assignment: "a minus (-) sign should be printed for *every* digit that is correct but in the wrong position,"
        [TestCase(2, 1, 5, 6, "--")]
        [TestCase(5, 5, 5, 5, "")]
        [TestCase(1, 2, 3, 4, "")] //the player wins. No need to give a hint.
        [TestCase(4, 3, 2, 1, "----")]
        [TestCase(5, 5, 3, 4, "++")]
        [TestCase(5, 1, 3, 5, "+-")]
        [TestCase(5, 5, 3, 4, "++")]
        public void TestHint(int digit1, int digit2, int digit3, int digit4, string expectedHint)
        {
            //Arrange
            int[] answer = { 1, 2, 3, 4 };
            _game.SetAnswer(answer, 10);
            string guess = $"{digit1}{digit2}{digit3}{digit4}";

            //Act
            _game.Guess(guess);

            //Assert
            Assert.AreEqual(expectedHint, _hint); //We should be reminding the user of the rules.
        }

        [Test]
        public void CorrectAnswersShouldWin()
        {
            //Arrange
            int[] answer = { 1, 2, 3, 4 };
            _game.SetAnswer(answer, 10);
            string guess = "1234";

            //Act
            Assert.AreEqual(false, _userWon); //Make sure we are in the state we expect.
            _game.Guess(guess);

            //Assert
            Assert.AreEqual(true, _userWon); //We should be reminding the user of the rules.
        }

        [Test]
        public void IncorrectAnswersWillCauseYouToLose()
        {
            //Arrange
            int[] answer = { 1, 2, 3, 4 };
            _game.SetAnswer(answer, 3);
            string guess = "5555";

            //Act
            Assert.AreEqual(false, _userLost); //Make sure we are in the state we expect.
            _game.Guess(guess);
            _game.Guess(guess);
            Assert.AreEqual(false, _userLost); //Test again. We didn't lose yet.
            _game.Guess(guess);

            //Assert
            Assert.AreEqual(true, _userLost); //We should be reminding the user of the rules.
        }

        [Test]
        public void TestGenerateHint()
        {
            //Arrange
            int[] answer = { 1, 2, 3, 4 };
            _game.SetAnswer(answer, 3);
            string guess = "5555";

            //Act
            Assert.AreEqual(false, _userLost); //Make sure we are in the state we expect.
            _game.Guess(guess);
            _game.Guess(guess);
            Assert.AreEqual(false, _userLost); //Test again. We didn't lose yet.
            _game.Guess(guess);

            //Assert
            Assert.AreEqual(true, _userLost); //We should be reminding the user of the rules.
        }

        //source: https://stackoverflow.com/a/730352
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        #region " Event handlers "

        private void PrintWelcome(object sender, EventArgs e)
        {
            Game game = (Game)sender;
        }

        private void PrintReminder(object sender, EventArgs e)
        {
            Game game = (Game)sender;
            _reminderCount++;
        }

        private void PrintWinner(object sender, EventArgs e)
        {
            _userWon = true;
        }

        private void PrintLoser(object sender, EventArgs e)
        {
            _userLost = true;
        }

        private void PrintHint(object hint, EventArgs e)
        {
            _hint = hint.ToString();
        }

        #endregion " Event handlers "
    }
}