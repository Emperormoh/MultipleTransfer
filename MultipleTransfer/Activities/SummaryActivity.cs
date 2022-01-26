
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
using Google.Android.Material.TextField;
using MultipleTransfer.UI.Models;
using MultipleTransfer.UI.Repository;
using MultipleTransfer.UI.Utils;
using Newtonsoft.Json;

namespace MultipleTransfer.Activities
{
    [Activity(Label = "SummaryActivity")]
    public class SummaryActivity : Activity
    {
        public Button BackToBenList;
        private SummaryPasser summaryPasser;

        private LoginResponseModel loginResponse;
        private TextView txtNumber;
        private TextView txtAcctType;
        private TextView txtAcctBalance;
        private string pinInput;

        //summarry
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_summary);

            string mData = Intent.GetStringExtra("summarry");
            if (!string.IsNullOrEmpty(mData))
            {
                summaryPasser = JsonConvert.DeserializeObject<SummaryPasser>(mData);
            }

            // Create your application here

            Switch add2Group = FindViewById<Switch>(Resource.Id.swt_addToGroup);
            add2Group.CheckedChange += add2Group_CheckedChange;
            BackToBenList = FindViewById<Button>(Resource.Id.btn_backToBenList);
            BackToBenList.Click += BackToBen_Click;
            Button toConfirmTransfer = FindViewById<Button>(Resource.Id.btn_confirm);
            toConfirmTransfer.Click += delegate { askToInputPin(); };

            TextView summaryAmount = FindViewById<TextView>(Resource.Id.txt_amount_summary);
            TextView summaryGroupName = FindViewById<TextView>(Resource.Id.txt_groupname_summary);
            TextView summaryRecipients = FindViewById<TextView>(Resource.Id.txt_summary_recipients);

            summaryAmount.Text = Util.TotalAmount(summaryPasser.beneficiaries).ToString();
            summaryGroupName.Text = summaryPasser.groupName;
            summaryRecipients.Text = summaryPasser.beneficiaries.Count().ToString();

            txtNumber = FindViewById<TextView>(Resource.Id.dash_acct_number);
            txtAcctType = FindViewById<TextView>(Resource.Id.acct_type);
            txtAcctBalance = FindViewById<TextView>(Resource.Id.acct_balance);

            var wData = Intent.GetStringExtra("LoginResponseModelK");
            if (!string.IsNullOrEmpty(mData))
            {
                var userDetails = MemoryManager.Instance(this).getLoginUser("LoginResponseModelK");
                loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(mData);
                txtNumber.Text = userDetails.accountNumber;
                txtAcctType.Text = userDetails.accountType;
                decimal ab = userDetails.accountBalance;
                txtAcctBalance.Text = ab.ToString();


            }
        }

        private void add2Group_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            Intent toCreateGroup = new Intent(this, typeof(CreateGroupActivity));
            this.StartActivity(toCreateGroup);
        }

        private void BackToBen_Click(object sender, EventArgs e)
        {
            Intent _toBen = new Intent(this, typeof(NoBenActivity));
            this.StartActivity(_toBen);
        }
        public void askToInputPin()
        {
            var dialogView = LayoutInflater.Inflate(Resource.Layout.dialog_enter_pin, null);

            AlertDialog alertDiaog;
            using (var dialog2 = new AlertDialog.Builder(this))
            {
                dialog2.SetView(dialogView);
                alertDiaog = dialog2.Create();

                ImageView img_cancel_pin = dialogView.FindViewById<ImageView>(Resource.Id.cancel_pin);
                TextInputEditText edt_enter_pin = dialogView.FindViewById<TextInputEditText>(Resource.Id.edt_pin);
                Button pin_okay = dialogView.FindViewById<Button>(Resource.Id.btn_Okay);

                pin_okay.Click += delegate
                {

                    proceedToVerifyPin(edt_enter_pin, alertDiaog);
                };
                img_cancel_pin.Click += delegate { alertDiaog.Dismiss(); };
                alertDiaog.Show();

            }

        }

        public void proceedToVerifyPin(TextInputEditText edt_enter_pin, AlertDialog alertDiaog,
          string groupName = "")
        {
            pinInput = edt_enter_pin.Text.ToString();

            if (LoginActivity.checkEmpty(pinInput) && pinInput.Length == 4)
            {
                proceedToTransfer(pinInput, alertDiaog, groupName );
            }
            else
            {
                Toast.MakeText(this, "Error Pin!\nTry again", ToastLength.Short).Show();
            }
            //proceedToTransfer(bank, receiverAccount, transactionAmount, senderAccount);
        }

        public class MakeTransfer
        {
            public string senderAccount { get; set; }
            public List<MakeTransferList> transfers { get; set; }
            public string accountPin { get; set; }
            public string groupName { get; set; }

       

            public MakeTransfer()
            {

            }
        }

        public class MakeTransferList
        {
            public decimal transactionAmount { get; set; }
            public string receiverAccount { get; set; }
            public string bank { get; set; }
            public string narration { get; set; }

        }
        public class TransferR
        {
            public string message { get; set; }
        }


        private async void proceedToTransfer(string accountPin, AlertDialog alertDiaog, string groupName = "Transfer")
        {
            var userDetails = MemoryManager.Instance(this).getLoginUser("LoginResponseModelK");
            var   spacecrafts = MemoryManager.Instance(this).getUserList("ben");
            if(spacecrafts != null)
            {
                var makeTransferL = new List<MakeTransferList>();
                foreach(var user in spacecrafts)
                {
                    makeTransferL.Add(new MakeTransferList() { receiverAccount = user.accountNumber, transactionAmount = Convert.ToDecimal(user.Amount), bank = user.accountId });
                }

            
                MakeTransfer make_Transfer = new MakeTransfer() { accountPin = accountPin, transfers = makeTransferL, senderAccount = userDetails.accountNumber, groupName = "Transfer" };
                string mRawData = JsonConvert.SerializeObject(make_Transfer);
                string response = await NetworkUtil.PostUSSDAsycT("Transaction/make_transfer", mRawData);
                if (!string.IsNullOrEmpty(response))
                {
                 
                    var mm = JsonConvert.DeserializeObject<TransferR>(response);
                    if(mm != null)
                    {
                        Intent toSuccess = new Intent(this, typeof(SuccessfulActivity));
                        toSuccess.PutExtra("goToSuccessful", response);
                        StartActivity(toSuccess);
                    }
                }
                else
                {
                    Toast.MakeText(this, "Transaction not successful", ToastLength.Long).Show();
                }
            }
            alertDiaog.Dismiss();
           

        }

    }
}
