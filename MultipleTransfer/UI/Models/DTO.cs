using System;
using System.Collections.Generic;

namespace MultipleTransfer.UI.Models
{
    public class DTO
    {
        public DTO()
        {
        }
    }

    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginResponseModel
    {
        public string acctNumber { get; set; }
        public string bvn { get; set; }
        public string email { get; set; }
        public string accountName { get; set; }
        public decimal accountBalance { get; set; }
        public string bank { get; set; }
        public string accountType { get; set; }
        public string accountNumber { get; set; }
        public string firstName { get; set; }
    }

    public class SummaryPasser
    {
        public string groupName { get; set; }
        public List<Beneficiary> beneficiaries { get; set; }
    }


    public class RecipientDTO
    {
        public double TransactionAmount { get; set; }
        public string ReceiverAccount { get; set; }
        public string Bank { get; set; }

        public RecipientDTO(double transactionAmount, string receiverAccount, string bank)
        {
            this.TransactionAmount = transactionAmount;
            this.ReceiverAccount = receiverAccount;
            this.Bank = bank;
        }
    }

    public class VerifyBeneficiary
    {

        public string bank { get; set; }
        public string accountNumber { get; set; }
        public string customerName { get; set; }
    }

    /*
     * {"href":"https://xmtapi.azurewebsites.net/Customers/890f9675-4591-471b-a7a7-610156c7c9fe","method":"GET",
     * "accountId":"890f9675-4591-471b-a7a7-610156c7c9fe","bvn":"4243457462","accountNumber":"1863863162",
     * "accountName":"Tosin Ayoola","accountType":"Current","accountBalance":20000,"bank":"Zenith","firstName":"Tosin","lastName":"Ayoola","email":"peterstosin556@gmail.com"}
     */
}
