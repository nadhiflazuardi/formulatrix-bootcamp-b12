Random random = new();

int flip = random.Next(2);

Console.WriteLine(flip == 0 ? "Heads" : "Tails");