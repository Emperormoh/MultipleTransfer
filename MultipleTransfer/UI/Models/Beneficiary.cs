using System;
namespace MultipleTransfer.UI.Models
{
    public class Beneficiary
    {
        public string accountId { get; set; }
        public string bankName { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string Amount { get; set; }
       

        public Beneficiary(string bankName, string accountName, string accountNumber, string Amount)
        {
            this.bankName = bankName;
            this.accountName = accountName;
            this.accountNumber = accountNumber;
            this.Amount = Amount;
        }

        public Beneficiary()
        {
        }
    }
}
