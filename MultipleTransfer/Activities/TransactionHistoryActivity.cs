
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MultipleTransfer.Adapters;
using MultipleTransfer.UI.Models;
using MultipleTransfer.UI.Repository;
using Newtonsoft.Json;

namespace MultipleTransfer.Activities
{
    [Activity(Label = "TransactionHistoryActivity")]
    public class TransactionHistoryActivity : Activity
    {
        private RecyclerView TransRv;
        private TransactionHistoryAdapter transactionHistoryAdapter;
        private List<Transactions> transactions;
        private ImageView ToDashwise;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_transaction_history);

            // Create your application here
            ToDashwise = FindViewById<ImageView>(Resource.Id.img_toDash);
            ToDashwise.Click += ToDashwise_Click;

            TransRv = FindViewById<RecyclerView>(Resource.Id.transaction_history_recycler);
            TransRv.SetLayoutManager(new LinearLayoutManager(this));
            TransRv.SetItemAnimator(new DefaultItemAnimator());


            transactions = new List<Transactions>();
            transactionHistoryAdapter = new TransactionHistoryAdapter(transactions);
            TransRv.SetAdapter(transactionHistoryAdapter);
        }

        private void ToDashwise_Click(object sender, EventArgs e)
        {
            Intent gotoDash = new Intent(this, typeof(DashboardActivity));
            this.StartActivity(gotoDash);
        }

        private async void transactionHistory(string transactionGroupName, string senderAccount, string transactionType,
            string numberOfRecipients, decimal totalAmount, string transactionId, string transactionDate,
            decimal transactionAmount, string narration, string receiverAccount)
        {
            var mtransactions = new List<Transactions>();
            var newTransaction = new Transactions()
            {
                transactionId = transactionId,
                transactionDate = transactionDate,
                transactionAmount = transactionAmount,
                receiverAccount = receiverAccount,
                transactionType = transactionType,
                senderAccount = senderAccount,
                narration = narration
            };

            mtransactions.Add(newTransaction);

            TranscationGroup transactionGroup = new TranscationGroup()
            {
                transactionGroupName = transactionGroupName,
                senderAccount = senderAccount,
                transactionType = transactionType,
                numberOfRecipients = numberOfRecipients,
                totalAmount = totalAmount,
                transfers = mtransactions,
                transactionDate = transactionDate
            };

            string mRawData = JsonConvert.SerializeObject(transactionGroup);
            string receipt = await NetworkUtil.PostUSSDAsyc("Transaction/transaction_receipts", mRawData);

            if (!string.IsNullOrEmpty(receipt))
            {
                var mm = JsonConvert.DeserializeObject<List<Transactions>>(receipt);

                transactionHistoryAdapter.swapdata(mm);
                transactionHistoryAdapter.NotifyDataSetChanged();

            }
            else
            {
                Toast.MakeText(this, "Error fetching history", ToastLength.Long).Show();
            }


        }


    }
}
