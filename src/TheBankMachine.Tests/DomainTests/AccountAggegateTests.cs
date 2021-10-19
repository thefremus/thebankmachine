using TheBankMachine.Domain.AggregatesModel.AccountAggregate;
using TheBankMachine.Domain.Exceptions;
using Xunit;

namespace TheBankMachine.Tests;

public class AccountAggegateTests
{
    [Fact]
    public void Add_Transactions_Check_Balance_With_OverDraft()
    {
        AccountAggregate account = SetupAccount();

        Assert.Equal(account.Balance, 600);
    }

    [Fact]
    public void WithDraw_InsufficientFunds_Test()
    {
        AccountAggregate account = SetupAccount();

        var ex = Assert.Throws<AccountException>(() => account.WithDrawFromAccount(700));
        Assert.Equal("You cannot withdraw the amount specified - insufficient funds", ex.Message);
    }

    [Fact]
    public void WithDraw_ValidAmount_From_Main_And_Overdraft()
    {
        AccountAggregate account = SetupAccount();
        account.WithDrawFromAccount(550);

        Assert.Equal(account.Balance, 50);
    }

    private AccountAggregate SetupAccount()
    {
        var account = new AccountAggregate(1, "12345678", "1234");
        account.AddAccountTransaction(1, 500, "Updated main account");
        account.AddAccountTransaction(2, 100, "Updated overdraft");
        return account;
    }
}