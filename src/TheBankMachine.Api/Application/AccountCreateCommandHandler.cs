using MediatR;
using TheBankMachine.Domain.AggregatesModel.AccountAggregate;

namespace TheBankMachine.Api.Application
{
    public class AccountCreateCommandHandler : IRequestHandler<AccountCreateCommand, bool>
    {
        //TODO: Add logging
        private readonly IAccountRepository _accountRepository;
        public AccountCreateCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
        {
            var accountEntity = new AccountAggregate(0, request.AccountNumber, request.PinCode);
            _accountRepository.Add(accountEntity);
            await _accountRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
