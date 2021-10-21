using MediatR;
using TheBankMachine.Api.Models;
using TheBankMachine.Domain.AggregatesModel.AccountAggregate;
using TheBankMachine.Domain.Exceptions;

namespace TheBankMachine.Api.Application
{
    public class AccountWithDrawCommandHandler : IRequestHandler<AccountWithdrawCommand, AccountBalanceViewModel>
    {
        private readonly IAccountRepository _accountRepository;
        public AccountWithDrawCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountBalanceViewModel> Handle(AccountWithdrawCommand request, CancellationToken cancellationToken)
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

            account.WithDrawFromAccount(request.Amount);
            await _accountRepository.UnitOfWork.SaveChangesAsync();
            return new AccountBalanceViewModel { AccountNumber = request.AccountNumber, Balance = account.Balance };
        }
    }
}
