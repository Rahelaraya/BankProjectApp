using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

   }
}
