using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBankMachine.Api.Application;
using TheBankMachine.Domain.AggregatesModel.AccountAggregate;
using Moq;
using System.Threading;
using Xunit;

namespace TheBankMachine.Tests.Application
{
    public class AccountCreateCommandHandlerTests
    {
        Mock<IAccountRepository> _accountRepositoryMock;

        public AccountCreateCommandHandlerTests()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
        }

        [Fact]
        public async Task Account_Create_Successfully()
        {
            string accountNumber = "12345678";
            string pinCode = "1234";
            decimal balance = 0;
            var accountCreateCommand = new AccountCreateCommand();
            accountCreateCommand.Balance = balance;
            accountCreateCommand.AccountNumber = accountNumber;
            accountCreateCommand.PinCode = pinCode;

            _accountRepositoryMock.Setup(accountRepo => accountRepo.Add(new AccountAggregate(0, accountCreateCommand.AccountNumber, accountCreateCommand.PinCode))).Returns(new AccountAggregate(0, accountNumber, pinCode));
            _accountRepositoryMock.Setup(accountRepo => accountRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));
            var accountCreateCommandHandler = new AccountCreateCommandHandler(_accountRepositoryMock.Object);
            var cltToken = new CancellationToken();
            var result = await accountCreateCommandHandler.Handle(accountCreateCommand, cltToken);

            Assert.True(result);
        }
    }
}
