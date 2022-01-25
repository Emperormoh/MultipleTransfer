using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using System.Text;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Widget;
using Google.Android.Material.TextField;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Android.Content;
using MultipleTransfer.Activities;


//using System.Collections.Generic;
//using System.Linq;
//using Android.Content;
//using Android.Views;



namespace MultipleTransfer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btn_login;

       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


         btn_login = FindViewById<Button>(Resource.Id.btn_login1);
         btn_login.Click += Button_Click;

        }

        private void Button_Click(object sender, EventArgs e)
        {
            Intent login = new Intent(this, typeof(LoginActivity));
            this.StartActivity(login);
            this.Finish();
            this.OverridePendingTransition(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_out_bottom);
        }


    }
}
