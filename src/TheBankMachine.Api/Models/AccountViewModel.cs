namespace TheBankMachine.Api.Models
{
    public class AccountViewModel
    {
        //not sure this is the best class name
        public int Id {  get; set; }
        public string AccountNumber { get; set; }
        public string PinCode { get; set; }
        public decimal Balance { get; set; }
    }
}
