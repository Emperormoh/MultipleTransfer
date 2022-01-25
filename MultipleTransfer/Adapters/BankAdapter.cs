using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MultipleTransfer.UI.IInterface;
using MultipleTransfer.UI.Models;

namespace MultipleTransfer.Adapters
{
    public class BankAdapter : RecyclerView.Adapter
    {
        public List<string> beneficiaryInstance;
        IBankInterface callBack;
        public Context contxt;

        public override int ItemCount => beneficiaryInstance.Count;

        public BankAdapter(BankInstance bankList)
        {

        }

        public BankAdapter(List<string> beneficiary, IBankInterface beneficairies, Context context)
        {
            beneficiaryInstance = beneficiary;
            callBack = beneficairies;
            contxt = context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BankViewHolder vh = holder as BankViewHolder;
          //  vh.Image.SetImageResource(beneficiaryInstance[position].mPhotoID);
            vh.TxtBankName.Text = beneficiaryInstance[position];
            
        }

    
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.bank_recycler_model, parent, false);
            BankViewHolder vh = new BankViewHolder(itemView, beneficiaryInstance, callBack, contxt);
            return vh;
        }

        public class BankViewHolder : RecyclerView.ViewHolder
        {
            public ImageView Image { get; set; }
            public TextView TxtBankName { get; set; }

        
            public BankViewHolder(View itemView, List<string> beneficiary, IBankInterface callBack, Context contxt) : base(itemView)
            {
                Image = itemView.FindViewById<ImageView>(Resource.Id.imageView1);
                TxtBankName = itemView.FindViewById<TextView>(Resource.Id.textView1);


                itemView.Click += delegate
                {
                    callBack.getBankDetails(beneficiary[AdapterPosition]);
                };

               

            }

        }

        internal void swapData(List<string> bankList)
        {
            this.beneficiaryInstance = bankList;
                NotifyDataSetChanged();
        }
    }
}
