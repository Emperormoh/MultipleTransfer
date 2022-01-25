using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MultipleTransfer.Activities;
using MultipleTransfer.UI.Models;

namespace MultipleTransfer.Adapters
{
    public class GroupsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> GroupItemClick;

        private List<Groups> groupwise;
        public Context contxt;


        public GroupsAdapter(List<Groups> groupwise, Context context)
        {
            this.contxt = context;
            this.groupwise = groupwise;
        }
         
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            GroupViewHolder hg = holder as GroupViewHolder;
            if (hg != null && hg.GroupNameTxt != null) hg.GroupNameTxt.Text = groupwise[position].GroupName;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View vg = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.groups_recycler_model, parent, false);
            return new GroupViewHolder(vg, groupOnClick, contxt, groupwise);
        }

        private void groupOnClick(int obj)
        {
            GroupItemClick?.Invoke(this, obj);
        }

        public override int ItemCount => groupwise.Count;

        internal void swapData(List<Groups> group)
        {
            this.groupwise = group;
            NotifyDataSetChanged();
        }
    }

    public class GroupViewHolder : RecyclerView.ViewHolder
    {
        public TextView GroupNameTxt { get; set; }
        public TextView RecipientsTxt { get; set; }


        private readonly Action<int> listener;

        public GroupViewHolder(View itemView, Action<int> listener, Context context, List<Groups> groups) : base(itemView)
        {
            GroupNameTxt = itemView.FindViewById<TextView>(Resource.Id.txt_groupname);
            RecipientsTxt = itemView.FindViewById<TextView>(Resource.Id.txt_recipients);

            this.listener = listener;

            itemView.Click += delegate {
                Intent toBen = new Intent(context, typeof(NoBenActivity));
                string sendMe = groups[base.AdapterPosition].GroupName;
                toBen.PutExtra("group_position", sendMe);
                context.StartActivity(toBen);


            };;
        }

        private void GroupItemView_Click(object sender, EventArgs e)
        {
            //listener(LayoutPosition);
          
           
        }
    }
}
