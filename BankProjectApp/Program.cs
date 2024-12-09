using System.Security.Principal;
using System.Text.Json;

namespace BankProjectApp
{

    class Program
    {
        static void Main(string[] args)
        {
            string BankDataJSONfilePath = "BankInfo.json";
            string AllBankDataJSONfilePathTyp = File.ReadAllText(BankDataJSONfilePath);
            Bankdata bankdata = JsonSerializer.Deserialize<Bankdata>(AllBankDataJSONfilePathTyp)!;
            Bank bank = new Bank();
            string[] menuOptions = {
              "Show All Account",
             "Deposit Money",
             "Withdraw Money",
             "Transfer Money",
             "Add Account",
             "Show Transactions",
             "Save & Exit"
            };

            int currentSelection = 1; 
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
                for (int i = 0; i < menuOptions.Length; i++) // Start from 0
                {
                    if (i + 1 == currentSelection) // Compare with 1-based index
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
                        if (currentSelection > 1) currentSelection--; 
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentSelection < menuOptions.Length) currentSelection++; 
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You selected: {menuOptions[currentSelection - 1]}\n"); 
                        Console.ResetColor();

                        switch (currentSelection) 
                        {
                            case 1:
                                bank.viewallaccount(bankdata); 
                                break;
                            case 2:
                               bank.Deposit(bankdata);
                                break;
                            case 3:
                                bank.withdrawal(bankdata);

                                break;
                            case 4:
                               bank.TransferMoney(bankdata);

                                break;
                            case 5:
                               bank.AddAccount(bankdata);
                                break;
                            case 6:
                               bank.ShowTransactions(bankdata);

                                break;
                            case 7:
                                bank.SaveandExit(bankdata);
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
                    
                }
            }
        }

       

       
        
        

       


    }
}
