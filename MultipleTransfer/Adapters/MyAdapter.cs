using System;
using Android.Views;
using Android.Widget;
using Android.Runtime;
using AndroidX.RecyclerView.Widget;
using MultipleTransfer.UI.Models;
using System.Collections.Generic;

namespace MultipleTransfer.Adapters
{
    public class MyAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        private List<Beneficiary> spaceCrafts;

        

        public MyAdapter(List<Beneficiary> spaceCrafts)
        {
            this.spaceCrafts = spaceCrafts;
        }



        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Beneficiary beneficiary = spaceCrafts[position];
            MyViewHolder h = holder as MyViewHolder;
            if (h != null && h.accountNumberTxt != null)
            h.accountNameTxt.Text = spaceCrafts[position].accountName;
            h.accountNumberTxt.Text = spaceCrafts[position].accountNumber;
            h.bankNameTxt.Text = spaceCrafts[position].accountId;
            h.amountTxt.Text = spaceCrafts[position].Amount;
        }



        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.recycler_model, parent, false);
            return new MyViewHolder(v, OnClick);
        }

        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }

        public override int ItemCount => spaceCrafts.Count;

        internal void swapData(List<Beneficiary> space)
        {
            this.spaceCrafts = space;
            NotifyDataSetChanged();
        }
    }



    public class MyViewHolder : RecyclerView.ViewHolder
    {
        public TextView accountNameTxt;
        public TextView accountNumberTxt;
        public TextView bankNameTxt;
        public TextView amountTxt;

        private readonly Action<int> listener;

        public MyViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            accountNameTxt = itemView.FindViewById<TextView>(Resource.Id.txt_acctName);
            accountNumberTxt = itemView.FindViewById<TextView>(Resource.Id.txt_acctNumber);
            bankNameTxt = itemView.FindViewById<TextView>(Resource.Id.txt_bank);
            amountTxt = itemView.FindViewById<TextView>(Resource.Id.txt_amount);
            this.listener = listener;

            itemView.Click += ItemView_Click;

        }

        private void ItemView_Click(object sender, EventArgs e)
        {
            listener(LayoutPosition);
        }
    }
}
