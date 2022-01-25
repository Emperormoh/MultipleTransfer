
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.App;
using MultipleTransfer.UI.Models;
using MultipleTransfer.UI.Repository;
using MultipleTransfer.UI.Utils;
using Newtonsoft.Json;

namespace MultipleTransfer.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : AppCompatActivity
    {
        LinearLayout mLinearLayout;
        private Button btn_login1;
        private TextInputEditText edt_email;
        TextInputEditText edt_password;
        [Obsolete]
        private ProgressDialog progress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity);

            // Create your application here
            progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");
            progress.SetTitle("Loading");

            mLinearLayout = FindViewById<LinearLayout>(Resource.Id.mainView);
           // mLinearLayout.Click += MLinearLayout_Click;

            btn_login1 = FindViewById<Button>(Resource.Id.btn_login2);
            btn_login1.Click += Button_Click1;

            edt_email = FindViewById<TextInputEditText>(Resource.Id.edt_email);
            edt_password = FindViewById<TextInputEditText>(Resource.Id.edt_password);

        }

        public static bool checkEmpty(string value)
        {
            if (!string.IsNullOrEmpty(value)) return true;
            return false;
        }

        private void Button_Click1(object sender, EventArgs e)
        {
            string email = edt_email.Text.ToString();
            string password = edt_password.Text.ToString();

            if (checkEmpty(edt_email.Text.ToString()) && checkEmpty(edt_password.Text.ToString()))
            {
                //Proceeed To Validate From API.
                progress.Show();
                ProceedToValidate(email, password);
            }
            else
            {
                Toast.MakeText(this, "Fields Cannot Be Empty", ToastLength.Short).Show();
            }
           
        }

        private void MLinearLayout_Click(object sender, EventArgs e)
        {
            using InputMethodManager inputManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }


        private async void ProceedToValidate(string email, string password)
        {
            LoginModel loginModel = new LoginModel() { email = email, password = password };
            string mRawData = JsonConvert.SerializeObject(loginModel);
            string response = await NetworkUtil.PostUSSDAsyc("login", mRawData);
            var desModel = JsonConvert.DeserializeObject<LoginResponseModel>(response);
            if(desModel != null)
            {
                MemoryManager.Instance(this).setUserAccountTest("LoginResponseModelK", desModel);
                // var mRaw = JsonConvert.SerializeObject(response);
                var inten = new Intent(this, typeof(DashboardActivity));
                inten.PutExtra("LoginResponseModelK", response);
                StartActivity(inten);
                Finish();

            }
        
            else
            {
                Toast.MakeText(this, "Please try again..", ToastLength.Long).Show();

            }
            progress.Dismiss();
        }


      

    }




}
