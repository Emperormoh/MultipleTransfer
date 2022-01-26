
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using MultipleTransfer.Activities;
using MultipleTransfer.UI.Models;
using MultipleTransfer.UI.Utils;
using Newtonsoft.Json;

namespace MultipleTransfer
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : AppCompatActivity
    {

        private ImageView multiple_Transfer;
        private LoginResponseModel loginResponse;
        private TextView txtNumber;
        private TextView txtAcctType;
        private TextView txtAcctBalance;
        private TextView txtUsername;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_dashboard);

            multiple_Transfer = FindViewById<ImageView>(Resource.Id.multi_transfer);
            multiple_Transfer.Click += Save_Click;
            txtNumber = FindViewById<TextView>(Resource.Id.dash_acct_number);
            txtAcctType = FindViewById<TextView>(Resource.Id.txt_acct_type);
            txtAcctBalance = FindViewById<TextView>(Resource.Id.acct_balance);
            txtUsername = FindViewById<TextView>(Resource.Id.user_name);

            Button historia = FindViewById<Button>(Resource.Id.transaction_history);
            historia.Click += historia_Click;


            dashWise();

            
            //MemoryManager.Instance(this).clearPreference();

        }

        private void historia_Click(object sender, EventArgs e)
        {
            Intent toHistory = new Intent(this, typeof(TransactionHistoryActivity));
            this.StartActivity(toHistory);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            //Intent toCreateGroup = new Intent(this, typeof(CreateGroupActivity));
            //this.StartActivity(toCreateGroup);
            Intent toNoben = new Intent(this, typeof(NoBenActivity));
            this.StartActivity(toNoben);
            //this.Finish();
            this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);
        }

        public void dashWise()
        {
            //var mData = Intent.GetStringExtra("LoginResponseModelK");
            //if (!string.IsNullOrEmpty(mData))
            //{
        
                loginResponse = MemoryManager.Instance(this).getLoginUser("LoginResponseModelK");
             if (loginResponse != null)
            {
                //loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(mData);
                txtNumber.Text = loginResponse.accountNumber;
                txtAcctType.Text = loginResponse.accountType;
                txtUsername.Text = "Hi " + loginResponse.firstName + "!";
                decimal ab = loginResponse.accountBalance;
                txtAcctBalance.Text = ab.ToString();
            }
               

           // }

        }
    }
}
