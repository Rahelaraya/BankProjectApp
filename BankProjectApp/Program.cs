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

            int currentSelection = 1; // Start with the first menu item
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
                                Console.WriteLine("Enter account ID:");
                                if (!int.TryParse(Console.ReadLine(), out int accountId))
                                {
                                    Console.WriteLine("Invalid account ID.");
                                    return;
                                }

                                var account = bankdata.AllAccountsJson.FirstOrDefault(a => a.Id == accountId);
                                if (account == null)
                                {
                                    Console.WriteLine("Account not found.");
                                    return;
                                }

                                Console.WriteLine("Enter amount to deposit:");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                                {
                                    Console.WriteLine("Invalid deposit amount. Please enter a positive number.");
                                    return;
                                }

                                account.Balance += amount;

                                account.Transactions.Add(new Transaction
                                {
                                    TransactionId = account.Transactions.Count + 1,
                                    Amount = amount,
                                    Date = DateTime.Now,
                                    Type = "Deposit"
                                });

                                Console.WriteLine($"Successfully deposited {amount:C}. New balance: {account.Balance:C}");
                                break;
                            case 3:

                                Console.WriteLine("Enter account ID:");
                                if (!int.TryParse(Console.ReadLine(), out int id))
                                {
                                    Console.WriteLine("Invalid account ID.");
                                    return;
                                }

                                Console.WriteLine("Enter amount to withdraw:");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                                {
                                    Console.WriteLine("Invalid withdrawal amount.");
                                    return;
                                }

                                if (withdrawAmount <= 0)
                                {
                                    Console.WriteLine("Withdrawal amount must be greater than zero.");
                                    return;
                                }

                                var accounts = bankdata.AllAccountsJson.FirstOrDefault(a => a.Id == id);

                                if (accounts is null)
                                {
                                    Console.WriteLine("Account not found.");
                                    return;
                                }

                                if (accounts.Balance < withdrawAmount)
                                {
                                    Console.WriteLine("Insufficient funds.");
                                    return;
                                }

                                accounts.Balance -= withdrawAmount;

                                accounts.Transactions ??= new List<Transaction>();
                                accounts.Transactions.Add(new Transaction
                                {
                                    TransactionId = accounts.Transactions.Count + 1,
                                    Amount = -withdrawAmount,
                                    Date = DateTime.Now,
                                    Type = "Withdrawal"
                                });

                                Console.WriteLine($"Successfully withdrew {withdrawAmount:C}. New balance: {accounts.Balance:C}");

                                break;
                            case 4:

                                    Console.WriteLine("Transfer Money");
                                    Console.WriteLine("----------------");

                                    Console.Write("Enter sender account ID: ");
                                    if (!int.TryParse(Console.ReadLine(), out int senderId))
                                    {
                                        Console.WriteLine("Invalid sender account ID.");
                                        return;
                                    }

                                    Console.Write("Enter receiver account ID: ");
                                    if (!int.TryParse(Console.ReadLine(), out int receiverId))
                                    {
                                        Console.WriteLine("Invalid receiver account ID.");
                                        return;
                                    }

                                    Console.Write("Enter amount to transfer: ");
                                    if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
                                    {
                                        Console.WriteLine("Invalid transfer amount.");
                                        return;
                                    }

                                    if (transferAmount <= 0)
                                    {
                                        Console.WriteLine("Transfer amount must be greater than zero.");
                                        return;
                                    }

                                    var senderAccount = bankdata.AllAccountsJson.FirstOrDefault(a => a.Id == senderId);
                                    var receiverAccount = bankdata.AllAccountsJson.FirstOrDefault(a => a.Id == receiverId);

                                    if (senderAccount is null)
                                    {
                                        Console.WriteLine("Sender account not found.");
                                        return;
                                    }

                                    if (receiverAccount is null)
                                    {
                                        Console.WriteLine("Receiver account not found.");
                                        return;
                                    }

                                    if (senderAccount.Balance < transferAmount)
                                    {
                                        Console.WriteLine("Insufficient funds in sender account.");
                                        return;
                                    }

                                    // Update balances
                                    senderAccount.Balance -= transferAmount;
                                    receiverAccount.Balance += transferAmount;

                                    // Add transactions
                                    senderAccount.Transactions ??= new List<Transaction>();
                                    senderAccount.Transactions.Add(new Transaction
                                    {
                                        TransactionId = senderAccount.Transactions.Count + 1,
                                        Amount = -transferAmount,
                                        Date = DateTime.Now,
                                        Type = "Transfer Out"
                                    });

                                    receiverAccount.Transactions ??= new List<Transaction>();
                                    receiverAccount.Transactions.Add(new Transaction
                                    {
                                        TransactionId = receiverAccount.Transactions.Count + 1,
                                        Amount = transferAmount,
                                        Date = DateTime.Now,
                                        Type = "Transfer In"
                                    });

                                    Console.WriteLine($"Successfully transferred {transferAmount:C}. New balances:");
                                    Console.WriteLine($"Sender: {senderAccount.Balance:C}");
                                    Console.WriteLine($"Receiver: {receiverAccount.Balance:C}");
                                

                                break;
                            case 5:
                                Console.WriteLine("Adding a new account...");
                                break;
                            case 6:
                                Console.WriteLine("Displaying transaction history...");
                                break;
                            case 7:
                              



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
