using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBankMachine.Domain.AggregatesModel.AccountAggregate
{
    public class AccountTransaction: Entity
    {
        public AccountTransaction()
        {

        }

        public AccountTransaction(int accountId, int accountTransactionType, decimal amount, string description)
        {
            AccountId = accountId;
            AccountTransactionType = accountTransactionType;
            Amount = amount;
            Description = description;
        }

        public AccountAggregate AccountAggregate { get; set; }
        public int AccountId { get; set; }
        //TODO: Create TransactionType Enumeration
        public int AccountTransactionType { get; set; }
        public string Description { get; set; }
        public decimal Amount {  get; set; }
    }
}
