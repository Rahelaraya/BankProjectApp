using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectApp
{
   
            public class BankAccounts
            {


                public string Name { get; set; }
                public string AccountType { get; set; }
                public int AccountNumber { get; set; }
                public double AccountBalance { get; set; }
                public BankAccounts(string name, string accountType, int accountNumber, double accountBalance)
               
                {
                    Name = name;
                    AccountType = accountType;
                    AccountNumber = accountNumber;
                    AccountBalance = accountBalance;
                 

                }

            }
    
}
