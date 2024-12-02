using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectApp
{
   
            public class BankAccounts
            {

                public int Id { get; set; }
                public string Name { get; set; }
                public string AccountType { get; set; }
                public int AccountNumber { get; set; }
                 public decimal Balance { get; set; }
                public List<Transaction> Transactions { get; set; } = new List<Transaction>();
                public BankAccounts(int id,string name, string accountType, int accountNumber, decimal balance)               
                {
                    Id = id;
                    Name = name;
                    AccountType = accountType;
                    AccountNumber = accountNumber;
                    Balance = balance;
                 

                }

            }
    
}
