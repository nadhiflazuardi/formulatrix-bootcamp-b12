Random numberGenerator = new();

int randomNumber = numberGenerator.Next(101);
string input;
string message = "";
int userGuess = 0;

while (true)
{
  Console.Clear();
  Console.WriteLine("Choose a number between 1-100");

  if (userGuess != 0)
  {
    Console.WriteLine($"Your last guess: {userGuess}. {message}");
  }

  Console.Write("Input: ");
  input = Console.ReadLine() ?? "0";
  userGuess = Convert.ToInt32(input);

  if (userGuess == randomNumber)
  {
    Console.WriteLine("You won!");
    break;
  }
  else if (userGuess > randomNumber)
  {
    message = "Try lower!";
  }
  else
  {
    message = "Try higher!";
  }
}