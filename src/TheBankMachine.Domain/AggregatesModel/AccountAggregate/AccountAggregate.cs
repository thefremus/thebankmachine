using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBankMachine.Domain.Exceptions;
using TheBankMachine.Domain.SeedWork;

namespace TheBankMachine.Domain.AggregatesModel.AccountAggregate
{
    public class AccountAggregate : Entity, IAggregateRoot
    {
        public string AccountNumber { get; set; }
        public string PinCode { get; set; }
        private decimal _balance;
        public decimal Balance => _balance = _transactions.Sum(d => d.Amount);

        private List<AccountTransaction> _transactions;
        public IEnumerable<AccountTransaction> Transactions => _transactions.AsReadOnly();

        protected AccountAggregate()
        {
            _transactions = new List<AccountTransaction>();
        }

        public AccountAggregate(int id, string accountNumber, string pinCode): this()        {
            Id = id;
            AccountNumber = accountNumber;
            PinCode = pinCode;
        }

        public void AddAccountTransaction(int accountTransactionType, decimal amount, string description)
        {
            _transactions.Add(new AccountTransaction(Id, accountTransactionType, amount, description));
        }

        public void WithDrawFromAccount(decimal amount)
        {
            if (_balance - amount <= 0)
                throw new AccountException("You cannot withdraw the amount specified - insufficient funds");

            var mainAccountBalance = _transactions.Where(x => x.AccountTransactionType == 1).Sum(x => x.Amount);
            var overDraftAccountBalance = _transactions.Where(x => x.AccountTransactionType == 2).Sum(x => x.Amount);
            decimal overDraftAmount = 0;
            //at this stage I used AccountTransactionType = 1 for main account and 2 for overdraft
            //want to check if the amount being withdrawn is more than what is available in the main account. if it is then the extra amount needs to be deducted from the overdraft

            if (amount > mainAccountBalance)
            {
                overDraftAmount = amount - mainAccountBalance;
                _transactions.Add(new AccountTransaction(Id, 2, overDraftAmount * -1, "Withdrawal from overdraft"));
                _transactions.Add(new AccountTransaction(Id, 1, mainAccountBalance * -1, "Withdrawal from main account"));
            }
            else
            {
                _transactions.Add(new AccountTransaction(Id, 1, amount * -1, "Withdrawal from main account"));
            }
        }
    }
}
