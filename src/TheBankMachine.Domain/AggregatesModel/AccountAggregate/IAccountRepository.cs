using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBankMachine.Domain.SeedWork;

namespace TheBankMachine.Domain.AggregatesModel.AccountAggregate
{
    public interface IAccountRepository : IRepository<AccountAggregate>
    {
        AccountAggregate Add(AccountAggregate account);
        AccountAggregate Update(AccountAggregate account);
    }
}
