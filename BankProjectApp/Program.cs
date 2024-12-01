using System.Text.Json;

namespace BankProjectApp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            string BankDataJSONfilePath = "BankInfo.json";
            string AllBankDataJSONfilePathTyp = File.ReadAllText(BankDataJSONfilePath);
            Bankdata bankDB = JsonSerializer.Deserialize<Bankdata>(AllBankDataJSONfilePathTyp)!;


            string[] menuOptions = {
            "Show All Account",
            "Deposit Money",
            "Withdraw Money",
            "Transfer Money",
            "Add Account",
            "Show Transactions",
            "Save & Exit"
            };

            int currentSelection = 0;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========================================");
                Console.WriteLine("         WELCOME TO THE STARBANK        ");
                Console.WriteLine("========================================");
                Console.ResetColor();

                // Display the menu with highlighting for the selected option
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuOptions[i]}");
                    }
                }


                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentSelection > 0) currentSelection--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentSelection < menuOptions.Length - 1) currentSelection++;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You selected: {menuOptions[currentSelection]}\n");
                        Console.ResetColor();


                        switch (currentSelection)
                        {
                            case 0:

                                Console.WriteLine("------ Account Details ------");
                                foreach (var account in bankDB.AllAccountsJson)

                                {
                                    Console.WriteLine($"Account Type: {account.AccountType}");
                                    Console.WriteLine($"Account Number: {account.AccountNumber}");
                                    Console.WriteLine($"Account Balance: {account.AccountBalance}");
                                }

                                break;
                            case 1:


                                break;
                            case 2:
                                Console.WriteLine("Withdraw Money: Enter the amount...");
                                break;
                            case 3:
                                Console.WriteLine("Transfer Money: Enter recipient details...");
                                break;
                            case 4:
                                Console.WriteLine("Adding a new account...");
                                break;
                            case 5:
                                Console.WriteLine("Displaying transaction history...");
                                break;
                            case 6:
                                Console.WriteLine("Saving changes and exiting... Thank you!");
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid Selection. Try again.");
                                break;
                        }


                        if (!exit)
                        {
                            Console.WriteLine("\nPress any key to return to the menu...");
                            Console.ReadKey();
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid key. Use Up/Down arrows or Enter.");
                        break;
                }
            }
        }
    }
}
