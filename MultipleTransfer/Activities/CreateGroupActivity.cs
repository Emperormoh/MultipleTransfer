
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
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MultipleTransfer.Adapters;
using MultipleTransfer.UI.Models;
using MultipleTransfer.UI.Repository;
using MultipleTransfer.UI.Utils;
using Newtonsoft.Json;

namespace MultipleTransfer.Activities
{
    [Activity(Label = "CreateGroupActivity")]
    public class CreateGroupActivity : Activity
    {

        private RecyclerView rvGroup;
        private GroupsAdapter groupAdapter;
        private List<Groups> groupwise;
        private Button btn_addgroups;

        //TextInputEditText edt_groupname;

        private LinearLayout noGroupLinear;

        private Button backToDashboard;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.create_group);

            // Create your application here
            // RecyclerView
            rvGroup = FindViewById<RecyclerView>(Resource.Id.groupRecycler);
            btn_addgroups = FindViewById<Button>(Resource.Id.add_group);
            noGroupLinear = FindViewById<LinearLayout>(Resource.Id.linearLayout_creategroup);

            backToDashboard = FindViewById<Button>(Resource.Id.back2Dash);
            backToDashboard.Click += toDash_Click;


            // Setting it's Layout
            rvGroup.SetLayoutManager(new LinearLayoutManager(this));
            rvGroup.SetItemAnimator(new DefaultItemAnimator());
            groupwise = new List<Groups>();
            groupwise = MemoryManager.Instance(this).getGroupsList("group");
            if (groupwise == null)
            {
                groupwise = new List<Groups>();
            }
            else
            {
                noGroupLinear.Visibility = ViewStates.Gone;
                rvGroup.Visibility = ViewStates.Visible;
            }
            // Adapter
            groupAdapter = new GroupsAdapter(groupwise, this);
            rvGroup.SetAdapter(groupAdapter);


            btn_addgroups.Click += delegate { AskToAddGroup(); };

            
        }

        private void toDash_Click(object sender, EventArgs e)
        {
            Intent _toDash = new Intent(this, typeof(DashboardActivity));
            this.StartActivity(_toDash);
        }


        public void AskToAddGroup()
        {
            /* Displaying a Dialog using an AlertDialog Fragment   */
            var dialogView = LayoutInflater.Inflate(Resource.Layout.dialog4_creategroup, null);
            AlertDialog alertDiaog;
            using (var dialog2 = new AlertDialog.Builder(this))
            {
                dialog2.SetView(dialogView);
                alertDiaog = dialog2.Create();
            }


            ImageView img_cancelAddGroup = dialogView.FindViewById<ImageView>(Resource.Id.cancel_addGroup);
            TextInputEditText edt_groupName = dialogView.FindViewById<TextInputEditText>(Resource.Id.edt_groupName);

       
            img_cancelAddGroup.Click += delegate { alertDiaog.Dismiss(); };
            Button btn_CreateGroup = dialogView.FindViewById<Button>(Resource.Id.btn_CreateGroup);
            btn_CreateGroup.Click += delegate { ProceedToAddGroup(edt_groupName,  alertDiaog); };
            alertDiaog.Show();
        }

        private  void ProceedToAddGroup(TextInputEditText edt_groupName, AlertDialog alertDiaog)

        {
            string groupName = edt_groupName.Text.ToString();


            if (LoginActivity.checkEmpty(groupName))
            {
                Groups newGroup = new Groups() {  GroupName = groupName, GroupId = DateTime.Now.ToString("yyyyMMddHHmmss")
                };

                //AddGroup addGroup = new AddGroup() {  groupName = groupName };
                //string mRawData = JsonConvert.SerializeObject(addGroup);
                //string response = await NetworkUtil.PostUSSDAsyc("addGroup", mRawData);
                //if (!string.IsNullOrEmpty(response))
                //{

                //}
                groupwise.Add(newGroup);
                MemoryManager.Instance(this).setGroupsList("group", groupwise);
                alertDiaog.Dismiss();
                groupAdapter.swapData(groupwise);
                groupAdapter.NotifyDataSetChanged();
                noGroupLinear.Visibility = ViewStates.Gone;
                rvGroup.Visibility = ViewStates.Visible;
            }

        }

       
    }

    public class AddGroup
    {
        public string groupName { get; set; }
    }
}
