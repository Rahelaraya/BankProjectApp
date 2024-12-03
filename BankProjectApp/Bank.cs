using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankProjectApp
{
   public class Bank
   {
        public void viewallaccount(Bankdata bankdata)
        {
            Console.WriteLine("------ Account Details ------");
            foreach (var accounts in bankdata.AllAccountsJson)

            {
                Console.WriteLine($"Account Type: {accounts.AccountType}");
                Console.WriteLine($"Account Number: {accounts.AccountNumber}");
                Console.WriteLine($"Account Balance: {accounts.Balance}");
            }
        }
        public void Deposit(Bankdata bankdata)
        {
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
        }
        public void withdrawal(Bankdata bankdata)
        {
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
        }
        public void TransferMoney(Bankdata bankdata)
        {
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
        }
        public void ShowTransactions(Bankdata bankdata)
        {
            Console.WriteLine("Enter the account ID to view the transaction history:");
            if (!int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine("Invalid account ID.");
                return;
            }

            // Find the account
            var Account = bankdata.AllAccountsJson.FirstOrDefault(a => a.Id == Id);
            if (Account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            if (Account.Transactions == null || !Account.Transactions.Any())
            {
                Console.WriteLine("No transactions found for this account.");
                return;
            }

            Console.WriteLine($"Transaction History for Account ID: {Id}");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-10}", "Txn ID", "Date", "Type", "Amount");
            Console.WriteLine("-------------------------------------------------");

            foreach (var txn in Account.Transactions)
            {
                Console.WriteLine("{0,-10} {1,-20:yyyy-MM-dd HH:mm} {2,-15} {3,-10:C}",
                    txn.TransactionId, txn.Date, txn.Type, txn.Amount);
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Current Balance: {Account.Balance:C}");
        }

        public void AddAccount(Bankdata bankdata)
        {
            Console.WriteLine("Choose the type of account you want to create:");
            Console.WriteLine("1. Saving Account");
            Console.WriteLine("2. Personal Account");
            Console.WriteLine("3. Investment Account");

            if (!int.TryParse(Console.ReadLine(), out int accountTypeChoice) || accountTypeChoice < 1 || accountTypeChoice > 3)
            {
                Console.WriteLine("Invalid choice. Please select a valid account type.");
                return;
            }


            string accountType = accountTypeChoice switch
            {
                1 => "Saving Account",
                2 => "Personal Account",
                3 => "Investment Account",
            };

            Console.WriteLine("Enter the new account holder's name:");
            string accountHolderName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountHolderName))
            {
                Console.WriteLine("Account holder's name cannot be empty.");
                return;
            }

            Console.WriteLine("Enter an initial deposit amount:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialDeposit) || initialDeposit < 0)
            {
                Console.WriteLine("Invalid deposit amount. Please enter a positive number or zero.");
                return;
            }


            var newAccount = new Account(4, "", "", 44, 2)
            {
                Id = bankdata.AllAccountsJson.Any() ? bankdata.AllAccountsJson.Max(a => a.Id) + 1 : 1,
                Name = accountHolderName,
                AccountType = accountType,
                Balance = initialDeposit,
                Transactions = new List<Transaction>
                                    {
                                        new Transaction
                                        {
                                            TransactionId = 1,
                                            Amount = initialDeposit,
                                            Date = DateTime.Now,
                                            Type = "Initial Deposit"
                                        }
                                    }
            };

            // Add the new account to the collection


            Console.WriteLine($"Account successfully created for {accountHolderName}.");
            Console.WriteLine($"Account Type: {accountType}");
            Console.WriteLine($"Account ID: {newAccount.Id}");
            Console.WriteLine($"Initial Balance: {newAccount.Balance:C}");
        }
        public void SaveandExit(Bankdata bankdata)
        {
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You selected: Save & Exit\n");
            Console.ResetColor();

            Console.WriteLine("Saving your data...");
            try
            {
                string updatedBankDataJson = JsonSerializer.Serialize(bankdata, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("BankInfo.json", updatedBankDataJson);
                Console.WriteLine("Data saved successfully. Exiting the application.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);

            } 
        }


    }
}
