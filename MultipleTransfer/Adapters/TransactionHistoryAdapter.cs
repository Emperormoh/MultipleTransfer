using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MultipleTransfer.UI.Models;

namespace MultipleTransfer.Adapters
{
    public class TransactionHistoryAdapter : RecyclerView.Adapter
    {
        public List<Transactions> TransactionHistory;
        public Context _context;
        public event EventHandler<int> ItemClick;

        public override int ItemCount => TransactionHistory.Count;

        public TransactionHistoryAdapter(List<Transactions> TransactionHistory)
        {
            this.TransactionHistory = TransactionHistory;
        }

       

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Transactions transactions = TransactionHistory[position];
            TransactionViewHolder tvh = holder as TransactionViewHolder;
            if (tvh != null && tvh.TransactionAmountTxt != null)
            tvh.TransactionAmountTxt.Text = TransactionHistory[position].totalAmount.ToString();
            tvh.RecipientsTxt.Text = TransactionHistory[position].NumberOfRecipients.ToString();
            tvh.SenderAccountTxt.Text = TransactionHistory[position].senderAccount;
            tvh.TransactionTypeTxt.Text = TransactionHistory[position].transactionType;
            tvh.TransactionGroupNameTxt.Text = TransactionHistory[position].transactionGroupName;
            //tvh.Ref_IDTxt.Text = TransactionHistory[position].transactionId;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View trans = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.trans_history_model, parent, false);
            return new TransactionViewHolder(trans, OnClick);
        }

        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }

        public class TransactionViewHolder : RecyclerView.ViewHolder
        {

            public TextView TransactionAmountTxt { get; set; }
            public TextView RecipientsTxt { get; set; }
            public TextView SenderAccountTxt { get; set; }
            public TextView TransactionTypeTxt { get; set; }
            public TextView TransactionGroupNameTxt { get; set; }
            //public TextView Ref_IDTxt { get; set; }

            private readonly Action<int> listener;
            private Context context;
            private List<Transactions> transactionHistory;

            public TransactionViewHolder(View itemView, Action<int>listener) : base(itemView)
            {
                TransactionAmountTxt = ItemView.FindViewById<TextView>(Resource.Id.txt_trans_amount);
                RecipientsTxt = ItemView.FindViewById<TextView>(Resource.Id.txt_trans_recipients);
                SenderAccountTxt = ItemView.FindViewById<TextView>(Resource.Id.txt_trans_senderAcct);
                TransactionTypeTxt = ItemView.FindViewById<TextView>(Resource.Id.txt_trans_type);
                TransactionGroupNameTxt = ItemView.FindViewById<TextView>(Resource.Id.txt_trans_tag);
                //Ref_IDTxt = ItemView.FindViewById<TextView>(Resource.Id.txt_trans_id);

                this.listener = listener;
                itemView.Click += ItemView_Click;
            }

            private void ItemView_Click(object sender, EventArgs e)
            {
                listener(LayoutPosition);
            }

           
        }



        internal void swapdata(List<Transactions> mm)
        {
            this.TransactionHistory = mm;
            NotifyDataSetChanged();
        }


    }
}
