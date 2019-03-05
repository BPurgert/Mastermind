using System;

namespace Excercise.Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayGame();
        }

        private static void PlayGame()
        {
            var settings = new GameSettings();
            var userWantsToPlay = true;

            var game = new Game(settings.AnswerLength, settings.LowestPossibleDigitValue, settings.HighestPossibleDigitValue, settings.NumberOfAttempts);

            //wire up the events
            game.DisplayWelcome += PrintWelcome;
            game.PrintReminder += PrintReminder;
            game.PrintWinner += PrintWinner;
            game.PrintHint += PrintHint;
            game.PrintLoser += PrintLoser;

            game.NewGame(); //init the game.

            while (userWantsToPlay && game.ContinueGame())
            {
                string userInput = Console.ReadLine();
                game.Guess(userInput);

                userWantsToPlay = PromptUserToPlayAgainIfGameIsOver(userWantsToPlay, game);
            }
        }

        private static bool PromptUserToPlayAgainIfGameIsOver(bool userWantsToPlay, Game game)
        {
            if (!game.ContinueGame())
            {
                //The current game is over. Ask the user if they want to play again.
                if (PlayerWantsToQuit())
                {
                    //The user is done playing.
                    userWantsToPlay = false;
                }
                else
                {
                    //the user wants to play again.
                    game.NewGame();
                }
            }

            return userWantsToPlay;
        }

        static bool PlayerWantsToQuit()
        {
            Console.WriteLine();
            Console.WriteLine("Do you want to play again? YES/no");

            var userInput = Console.ReadLine();

            if (userInput.Equals("no", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private static void PrintWelcome(object sender, EventArgs e)
        {
            Game game = (Game)sender;

            //Console.WriteLine("Welcome to Mastermind!");
            Console.WriteLine(_mastermind);
            Console.WriteLine();
            Console.WriteLine($"Type {game.answerLength} digits between {game.lowestPossibleValue} and {game.highestPossibleValue} then press enter.");
            Console.WriteLine("A '-' sign means a digit is correct but is in the wrong position.");
            Console.WriteLine("A '+' sign means a digit is correct and in the correct position.");
            Console.WriteLine($"You have {game.numberOfAttempts} attempts. Lets play!");
        }

        private static void PrintReminder(object sender, EventArgs e)
        {
            Game game = (Game)sender;

            Console.WriteLine();
            Console.WriteLine("Your guess is not valid.");
            Console.WriteLine($"Type {game.answerLength} digits between {game.lowestPossibleValue} and {game.highestPossibleValue} then press enter.");
        }

        private static void PrintWinner(object sender, EventArgs e)
        {
            //todo
            Console.WriteLine("You win!");
        }

        private static void PrintLoser(object sender, EventArgs e)
        {
            //todo
            Console.WriteLine("You lost.");
        }

        private static void PrintHint(object hint, EventArgs e)
        {
            Console.WriteLine(hint);
            Console.WriteLine();
        }

        private static string _mastermind = @"  _    _      _                                _        
| |  | |    | |                              | |       
| |  | | ___| | ___ ___  _ __ ___   ___      | |_ ___  
| |/\| |/ _ \ |/ __/ _ \| '_ ` _ \ / _ \     | __/ _ \ 
\  /\  /  __/ | (_| (_) | | | | | |  __/     | || (_) |
 \/  \/ \___|_|\___\___/|_| |_| |_|\___|      \__\___/ 
                                                       
                                                       
___  ___          _                      _           _ 
|  \/  |         | |                    (_)         | |
| .  . | __ _ ___| |_ ___ _ __ _ __ ___  _ _ __   __| |
| |\/| |/ _` / __| __/ _ \ '__| '_ ` _ \| | '_ \ / _` |
| |  | | (_| \__ \ ||  __/ |  | | | | | | | | | | (_| |
\_|  |_/\__,_|___/\__\___|_|  |_| |_| |_|_|_| |_|\__,_|";
    }
}
