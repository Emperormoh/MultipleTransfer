
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using MultipleTransfer.UI.Utils;
using Newtonsoft.Json;

namespace MultipleTransfer.Activities
{
    [Activity(Label = "TransactionHistoryActivity")]
    public class TransactionHistoryActivity : Activity
    {
        private RecyclerView TransRv;
        private TransactionHistoryAdapter transactionHistoryAdapter;
        private List<Transactions> transactions = new List<Transactions>();
        private ImageView ToDashwise;
        private LoginResponseModel loginResponse;


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

            transactionHistory();
        }

        private void ToDashwise_Click(object sender, EventArgs e)
        {
            Intent gotoDash = new Intent(this, typeof(DashboardActivity));
            this.StartActivity(gotoDash);
        }

        private async void transactionHistory()
        {
            loginResponse = MemoryManager.Instance(this).getLoginUser("LoginResponseModelK");
            if(loginResponse != null)
            {
                string receipt = await NetworkUtil.GetAsycDataTrans("Transaction/get-transaction-groups", loginResponse.accountNumber);

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
            else
            {
                Toast.MakeText(this, "Error fetching history", ToastLength.Long).Show();
            }
            //string mRawData = JsonConvert.SerializeObject(transactionGroup);

        }

    }
    
}
