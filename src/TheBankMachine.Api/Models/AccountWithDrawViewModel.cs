namespace TheBankMachine.Api.Models
{
    public class AccountWithDrawViewModel
    {
        public string AccountNumber { get; set; }
        public string PinCode { get; set; }
        public decimal Amount { get; set; }
    }
}
