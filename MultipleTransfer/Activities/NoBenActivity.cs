
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.TextField;
using MultipleTransfer.Adapters;
using MultipleTransfer.UI.IInterface;
using MultipleTransfer.UI.Models;
using MultipleTransfer.UI.Repository;
using MultipleTransfer.UI.Utils;
using Newtonsoft.Json;

namespace MultipleTransfer.Activities
{
    [Activity(Label = "NoBenActivity")]
    public class NoBenActivity : Activity , IBankInterface
    {
        private RecyclerView rv;
        private MyAdapter adapter;
        private List<Beneficiary> spacecrafts;
        private Button btn_addBen;
        private Button btn_Transfer;
        private List<string> bankList;
        BankAdapter bankAdapter;
        private Bank bankClicked;
        private List<Beneficiary> banks1;  
        TextInputEditText edt_bank_name;
        private LinearLayout noItemLinear;
        private Button backToDashboard;
        private TextView numOfBen;
        private TextView amountToTransfer;
        public double totalAmount = 0;
        private SummaryPasser summaryPasser;
        private TextView txtx_AcctName;
        public Spinner spinner;
        private String clicked_spinner;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.no_ben);
            banks1 = new List<Beneficiary>();
            summaryPasser = new SummaryPasser();
            string group_Name = Intent.GetStringExtra("group_position");
            if (!string.IsNullOrEmpty(group_Name))
            {
                summaryPasser.groupName = group_Name;

            }

            backToDashboard = FindViewById<Button>(Resource.Id.backTodash);
            backToDashboard.Click += toDash_Click;

            rv = FindViewById<RecyclerView>(Resource.Id.mRecycler);
            btn_addBen = FindViewById<Button>(Resource.Id.add_beneficiary);
            noItemLinear = FindViewById<LinearLayout>(Resource.Id.linearLayout_noBenAdded);
            numOfBen = FindViewById<TextView>(Resource.Id.count_ben);
            amountToTransfer = FindViewById<TextView>(Resource.Id.total_amount);

            bankList = new List<string>();
            getListBanks();

            // Setting it's Layout
            rv.SetLayoutManager(new LinearLayoutManager(this));
            rv.SetItemAnimator(new DefaultItemAnimator());
            spacecrafts = new List<Beneficiary>();
            spacecrafts = MemoryManager.Instance(this).getUserList("ben");
            if (spacecrafts == null)
            {
                spacecrafts = new List<Beneficiary>();
            }
            else { noItemLinear.Visibility = ViewStates.Gone;
                rv.Visibility = ViewStates.Visible;
            }
            // Adapter
            adapter = new MyAdapter(spacecrafts);
            rv.SetAdapter(adapter);


            btn_addBen.Click += delegate
            {
                if (spacecrafts.Count == 5)
                {
                    Toast.MakeText(this, "Beneficiary Limit reached", ToastLength.Short).Show();
                }
                else
                { AskToAddBenficiries(); }
            };
            BeneficiaryCount();
            setTotalAmount();
          
        }


        private void toDash_Click(object sender, EventArgs e)
        {
            MemoryManager.Instance(this).getLoginUser("LoginResponseModelK");
            Intent _toDash = new Intent(this, typeof(DashboardActivity));
            this.StartActivity(_toDash);
        }

        public void AskToAddBenficiries()
        {
            BeneficiaryCount();
            /* Displaying a Dialog using an AlertDialog Fragment   */
            var dialogView = LayoutInflater.Inflate(Resource.Layout.dialog_addaccount, null);

            AlertDialog alertDiaog;
            using (var dialog2 = new AlertDialog.Builder(this))
            {
                dialog2.SetView(dialogView);
                alertDiaog = dialog2.Create();
            }


            spinner = dialogView.FindViewById<Spinner>(Resource.Id.spinner_bank);
            spinner.Prompt = "Choose bank";
            spinner.ItemSelected += new System.EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, bankList);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            ImageView img_cancel = dialogView.FindViewById<ImageView>(Resource.Id.cancel);
            TextInputEditText edt_acctNumber = dialogView.FindViewById<TextInputEditText>(Resource.Id.edt_account_number);
            TextInputEditText edt_Amount = dialogView.FindViewById<TextInputEditText>(Resource.Id.edt_amount);
            btn_Transfer = FindViewById<Button>(Resource.Id.btn_totransfer);
            btn_Transfer.Click += to_Summary_ClickAsync;



            img_cancel.Click += delegate { alertDiaog.Dismiss(); };
            Button btn_submitAdd = dialogView.FindViewById<Button>(Resource.Id.summit_add);
            btn_submitAdd.Click += delegate
            {
                ProceedToAddBenefeciary(edt_acctNumber, edt_Amount, spinner, alertDiaog);
            };
            alertDiaog.Show();
            
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = sender as Spinner;
            clicked_spinner = spinner.GetItemAtPosition(e.Position).ToString();
            Toast.MakeText(this, "You choose: " + spinner.GetItemAtPosition(e.Position), ToastLength.Short).Show();
        }

        //private async Task to_Summary_ClickAsync(object sender, EventArgs e)
        private void to_Summary_ClickAsync(object sender, EventArgs e)
        {
            summaryPasser.beneficiaries = spacecrafts;
            string mRaw = JsonConvert.SerializeObject(summaryPasser);
            Intent _toSummary = new Intent(this, typeof(SummaryActivity));
            _toSummary.PutExtra("summarry", mRaw);
            this.StartActivity(_toSummary);
        }

        private void ProceedToAddBenefeciary(TextInputEditText edt_acctNumber, TextInputEditText edt_Amount,
           Spinner spinner, AlertDialog alertDiaog)
        {
            string acctNumber = edt_acctNumber.Text.ToString();
            string amount = edt_Amount.Text.ToString();
            string spBank = clicked_spinner;



             amountToTransfer.Text = getCurrentTotalAmount(amount).ToString();

            if (LoginActivity.checkEmpty(acctNumber) && LoginActivity.checkEmpty(amount) && LoginActivity.checkEmpty(clicked_spinner)) {
                VerifyBeneficiary beneficiary = new VerifyBeneficiary() { accountNumber = acctNumber, bank = spBank };
                string mRaw = JsonConvert.SerializeObject(beneficiary);
                validateBeneficiary(mRaw,alertDiaog,amount, beneficiary, spinner);   
                
            }          
        }


        private async void validateBeneficiary(string mData, AlertDialog alertDiaog, string amount, VerifyBeneficiary beneficiary, Spinner spinner)
        {
            var result = await NetworkUtil.PostUSSDAsycT("Transaction/find_account",mData);
            if (!string.IsNullOrEmpty(result)){
                VerifyBeneficiary verifyBeneficiary = JsonConvert.DeserializeObject<VerifyBeneficiary>(result);
                if (!string.IsNullOrEmpty(verifyBeneficiary.customerName))
                {
                   
                    Beneficiary newBank = new Beneficiary() { Amount = amount, accountNumber = beneficiary.accountNumber, accountId = beneficiary.bank, accountName = verifyBeneficiary.customerName};
                    spacecrafts.Add(newBank);
                    MemoryManager.Instance(this).setUserAccountList("ben", spacecrafts);
                    alertDiaog.Dismiss();
                    adapter.swapData(spacecrafts);
                    adapter.NotifyDataSetChanged();
                    noItemLinear.Visibility = ViewStates.Gone;
                    rv.Visibility = ViewStates.Visible;
                    BeneficiaryCount();
                }
            }
            else
            {
                Toast.MakeText(this, "Account Number couldn't be verifield", ToastLength.Long).Show();
            }
        }

        public decimal getCurrentTotalAmount(string vall)
        {
            decimal value = 0;
            var amount = TotalAmount();
            value = Convert.ToDecimal(vall) + Convert.ToDecimal(amount);
            return value;
        }

        public void getBankDetails(string banks)
        {
            //bankClicked = banks;
            edt_bank_name.Text = banks;
            
        }

        public void BeneficiaryCount()
        {
            numOfBen.Text = "Beneficiaries: " + spacecrafts.Count + "/10";
        }

        private async void getListBanks()
        {
            string result = await NetworkUtil.GetAsycData("Bank/get_banks", null);
            if (!string.IsNullOrEmpty(result))
            {
                var bankArray = JsonConvert.DeserializeObject<string[]>(result);
                foreach (string bank in bankArray)
                {
                    bankList.Add(bank);
                    Console.WriteLine(bank);
                }
            }
           
        }

        public double TotalAmount()
        {
            double total = 0;
            foreach (Beneficiary Spacecraft in spacecrafts)
            {
                totalAmount += Convert.ToDouble(Spacecraft.Amount);
            }
            total = totalAmount;
            return totalAmount;
        }


        private void setTotalAmount()
        {
            var totalAmount = TotalAmount();
            amountToTransfer.Text = "Total amount: ₦" + totalAmount.ToString("N0");
        }
    }

}
