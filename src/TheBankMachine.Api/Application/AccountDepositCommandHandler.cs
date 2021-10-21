using MediatR;
using TheBankMachine.Api.Models;
using TheBankMachine.Domain.AggregatesModel.AccountAggregate;
using TheBankMachine.Domain.Exceptions;

namespace TheBankMachine.Api.Application
{
    public class AccountDepositCommandHandler : IRequestHandler<AccountDepositCommand, AccountBalanceViewModel>
    {
        private readonly IAccountRepository _accountRepository;
        public AccountDepositCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountBalanceViewModel> Handle(AccountDepositCommand request, CancellationToken cancellationToken)
        {
            var account = _accountRepository.Get(request.AccountNumber);

            if(account == null)
            {
                throw new AccountException("Invalid account number");
            }

            if(account.PinCode != request.PinCode)
            {
                throw new AccountException("Invalid pin code");
            }

            account.AddAccountTransaction(1, request.Amount, "Deposit");
            await _accountRepository.UnitOfWork.SaveChangesAsync();
            return new AccountBalanceViewModel { AccountNumber = request.AccountNumber, Balance = account.Balance };
        }
    }
}
