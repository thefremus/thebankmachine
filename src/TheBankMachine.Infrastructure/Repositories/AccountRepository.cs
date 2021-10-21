using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBankMachine.Domain.AggregatesModel.AccountAggregate;
using TheBankMachine.Domain.SeedWork;

namespace TheBankMachine.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TheBankMachineContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public AccountRepository(TheBankMachineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public AccountAggregate Add(AccountAggregate account)
        {
            return _context.Add(account).Entity;
        }

        public AccountAggregate Update(AccountAggregate account)
        {
            return _context.Update(account).Entity;
        }

        public AccountAggregate Get(string accountNumber)
        {
            return _context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        }
    }
}
