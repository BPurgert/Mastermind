# Mastermind

This is a console based version of the game Mastermind. You'll have 10 changes to guess the four digit combination. The digits of the combination are between 1 and 6. Invalid guesses are not counted against your 10 changes. 

A + sign is printed for each correct digit in the correct place. A - sign is printed for every digit that is part of the combination but in the wrong place. If a digit in your guess is not part of the combination nothing will be printed.

## Example
The secret combination is 1234.

If you guess 1155 you will see

+-

You'll see a + because the first 1 is in the correct place. You'll see a - because the second 1 is a correct digit but it is in the wrong place.

## Notes
If you build in debug mode the answer is shown at the start of the game. Building in release mode will not show the answer.
