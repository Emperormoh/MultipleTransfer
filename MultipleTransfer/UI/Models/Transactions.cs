using System;
using System.Collections.Generic;
using Android.Widget;

namespace MultipleTransfer.UI.Models
{
    public class Transactions
    {
        public string transactionId { get; set; }
        public string transactionDate { get; set; }
        public decimal transactionAmount { get; set; }
        public string transactionType { get; set; }
        public string narration { get; set; }
        public string senderAccount { get; set; }
        public string bank { get; set; }
        public string receiverAccount { get; set; }

        public Transactions(string transId, string transDate, decimal transAmount, string transType, string senderAcct, string receiverAcct, string narration, string bank)
        {
            this.transactionId = transId;
            this.transactionDate = transDate;
            this.transactionAmount = transAmount;
            this.transactionType = transType;
            this.senderAccount = senderAcct;
            this.receiverAccount = receiverAcct;
            this.narration = narration;
            this.bank = bank;
        }

        public Transactions()
        {
        }
    }


    public class TranscationGroup
    {
        public string transactionGroupName { get; set; }
        public string transactionDate { get; set; }
        public string senderAccount { get; set; }
        public string transactionType { get; set; }
        public string numberOfRecipients { get; set; }
        public decimal totalAmount { get; set; }
        public List<Transactions> transfers { get; set; }



        //public TranscationGroup(string transactionGroupName, string senderAccount, string transactionType,
        //    string numberOfRecipients, decimal totalAmount, List<Transactions> transfers)
        //{
        //    this.transactionGroupName = transactionGroupName;
        //    this.senderAccount = senderAccount;
        //    this.transactionType = transactionType;
        //    this.numberOfRecipients = numberOfRecipients;
        //    this.totalAmount = totalAmount;
        //    this.transfers = transfers;
        //}

        public TranscationGroup()
        {
        }
    }
}
