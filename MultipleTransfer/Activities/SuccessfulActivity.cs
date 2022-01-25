
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
using MultipleTransfer.UI.Models;
using MultipleTransfer.UI.Repository;
using Newtonsoft.Json;

namespace MultipleTransfer.Activities
{
    [Activity(Label = "SuccessfulActivity")]
    public class SuccessfulActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.successful_Activity);

            // Create your application here

            var mData = Intent.GetStringExtra("LoginResponseModelK");

            Button history_btn = FindViewById<Button>(Resource.Id.history);
            Button home_btn = FindViewById<Button>(Resource.Id._Home);

           history_btn.Click += toHistory_Click;
           home_btn.Click += to__Home_Click;
        }

        private void toHistory_Click(object sender, EventArgs e)
        {
            Intent toHistory = new Intent(this, typeof(TransactionHistoryActivity));
            this.StartActivity(toHistory);
        }

        private void to__Home_Click(object sender, EventArgs e)
        {
            Intent toHome = new Intent(this, typeof(DashboardActivity));
            this.StartActivity(toHome);
        }

    }
}
