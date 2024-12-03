
namespace BankProjectApp
{
    public class Account : BankAccounts
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Account(int id, string name, string accountType, int accountNumber, decimal balance)
            : base(id, name, accountType, accountNumber, balance)
        {
        }


    }
}