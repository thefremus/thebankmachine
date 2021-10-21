using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBankMachine.Domain.AggregatesModel.AccountAggregate;
using TheBankMachine.Domain.SeedWork;

namespace TheBankMachine.Infrastructure
{
    public class TheBankMachineContext : DbContext, IUnitOfWork
    {
        public DbSet<AccountAggregate> Accounts { get; set; }
        public TheBankMachineContext(DbContextOptions<TheBankMachineContext> options) : base(options) { }
    }
}
