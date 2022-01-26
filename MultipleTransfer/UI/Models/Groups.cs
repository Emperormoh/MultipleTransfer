using System;

namespace MultipleTransfer.UI.Models
{
    public class Groups
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string Amount { get; set; }
        public string SenderAccount { get; set; }
        public string TransactionType { get; set; }
        public int NumberOfRecipients { get; set; }
        public double TotalAmount { get; set; }
        public Transactions[] Transactions { get; set; }

        public Groups()
        {
        }

    }
}
