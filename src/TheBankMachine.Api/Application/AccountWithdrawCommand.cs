using MediatR;
using System.Runtime.Serialization;
using TheBankMachine.Api.Models;

namespace TheBankMachine.Api.Application
{
    public class AccountWithdrawCommand : IRequest<AccountBalanceViewModel>
    {
        [DataMember]
        public string AccountNumber {  get; set; }

        [DataMember]
        public string PinCode { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
    }
}
