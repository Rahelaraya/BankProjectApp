using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectApp
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public required string Type { get; set; }
        public decimal Amount { get; set; }

    }
}
