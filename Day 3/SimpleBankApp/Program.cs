using System;

class BankAccount
{
  public string AccountHolder { get; set; }
  public string AccountNumber { get; private set; }
  private decimal balance;

  public BankAccount(string accountHolder, string accountNumber, decimal initialBalance)
  {
    AccountHolder = accountHolder;
    AccountNumber = accountNumber;
    balance = initialBalance;
  }

  public void Deposit(decimal amount)
  {
    if (amount > 0)
    {
      balance += amount;
      Console.WriteLine($"Deposited: {amount:C}. New Balance: {balance:C}");
    }
    else
    {
      Console.WriteLine("Deposit amount must be positive");
    }
  }

  public void Withdraw(decimal amount)
  {
    if (amount > 0 && amount <= balance)
    {
      balance -= amount;
      Console.WriteLine($"Withdrawn: {amount:C}. New Balance: {balance:C}");
    }
    else
    {
      Console.WriteLine("Insufficient funds or invalid amount.");
    }
  }

  public void CheckBalance()
  {
    Console.WriteLine($"Account Balance: {balance:C}");
  }
}

class Program
{
  static void Main()
  {
    Console.Clear();

    string? name;
    do
    {
      Console.Write("Enter Account Holder Name: ");
      name = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(name));

    string accountNumber = "ACC" + new Random().Next(100, 999);

    BankAccount account = new BankAccount(name, accountNumber, 999);
    int choice = 0;

    do
    {
      Console.WriteLine("\nBanking System Menu:");
      Console.WriteLine("1. Deposit");
      Console.WriteLine("2. Withdraw");
      Console.WriteLine("3. Check Balance");
      Console.WriteLine("4. Exit");
      Console.Write("Choose an option: ");

      if (int.TryParse(Console.ReadLine(), out choice))
      {
        switch (choice)
        {
          case 1:
            Console.Write("Enter deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
            {
              account.Deposit(depositAmount);
            }
            else
            {
              Console.WriteLine("Invalid amount.");
            }
            break;

          case 2:
            Console.Write("Enter withdrawal amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
            {
              account.Withdraw(withdrawAmount);
            }
            else
            {
              Console.WriteLine("Invalid amount.");
            }
            break;

          case 3:
            account.CheckBalance();
            break;
          case 4:
            Console.WriteLine("Thank you for using our banking system.");
            break;

          default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
        }
      }
      else
      {
        Console.WriteLine("Please enter a valid option.");
      }

    } while (choice != 4);
  }
}