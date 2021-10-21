using MediatR;
using System.Runtime.Serialization;

namespace TheBankMachine.Api.Application
{
    public class AccountCreateCommand : IRequest<bool>
    {
        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public string PinCode { get; set; }

        [DataMember]
        public decimal Balance { get; set; }
    }
}
