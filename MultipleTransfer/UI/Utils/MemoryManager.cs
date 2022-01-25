using System;
using System.Collections.Generic;
using Android.Content;
using MultipleTransfer.UI.Models;
using Newtonsoft.Json;

namespace MultipleTransfer.UI.Utils
{

    public class MemoryManager
    {
        private static MemoryManager instance = null;
        private static string DEFAULT_VALUE = "";
        private static readonly object padlock = new object();
        private static ISharedPreferences preferences;
        private static ISharedPreferencesEditor editor;
        MemoryManager()
        {
        }

        public static MemoryManager Instance(Context context)
        {
            lock (padlock)
            {
                if (preferences == null)
                {
                    instance = new MemoryManager();
                    preferences = context.GetSharedPreferences(context.PackageName, FileCreationMode.Private);
                    editor = preferences.Edit();

                }
                return instance;
            }
        }

        public void savePreference(string key, string value)
        {
            editor.PutString(key, value);
            editor.Commit();
        }
        public void savePreference(string key, bool value)
        {
            editor.PutBoolean(key, value);
            editor.Commit();
        }

        public void removePreference(string key)
        {
            editor.Remove(key);
            editor.Commit();
        }


        public void setUserAccount(string key, Beneficiary user)     
        {
            editor.PutString(key, JsonConvert.SerializeObject(user));
            editor.Commit();
        }

        //for Groups
        public void setGroups(string key, Groups groups)
        {
            editor.PutString(key, JsonConvert.SerializeObject(groups));
            editor.Commit();
        }


        public void setUserAccountList(string key, List<Beneficiary> user)
        {
            editor.PutString(key, JsonConvert.SerializeObject(user));
            editor.Commit();
        }

        //for Groups
        public void setGroupsList(string key, List<Groups> groups)
        {
            editor.PutString(key, JsonConvert.SerializeObject(groups));
            editor.Commit();
        }


        public void setUserAccountTest(string key, LoginResponseModel user)
        {
            editor.PutString(key, JsonConvert.SerializeObject(user));
            editor.Commit();
        }


        public LoginResponseModel getLoginUser(string key)
        {
            string user = preferences.GetString(key, DEFAULT_VALUE);
            if (user != null)
            {
                return JsonConvert.DeserializeObject<LoginResponseModel>(user);
            }
            else return null;

        }


        public Beneficiary getUser(string key)
        {
            string user = preferences.GetString(key, DEFAULT_VALUE);
            if (user != null)
            {
                return JsonConvert.DeserializeObject<Beneficiary>(user);
            }
            else return null;

        }
        //for Groups
        public Groups getGroups(string key)
        {
            string groups = preferences.GetString(key, DEFAULT_VALUE);
            if (groups != null)
            {
                return JsonConvert.DeserializeObject<Groups>(groups);
            }
            else return null;

        }



        public List<Beneficiary> getUserList(string key)
        {
            string user = preferences.GetString(key, DEFAULT_VALUE);
            if (user != null)
            {
                return JsonConvert.DeserializeObject<List<Beneficiary>>(user);
            }
            else return null;

        }

        //for Groups
        public List<Groups> getGroupsList(string key)
        {
            string group = preferences.GetString(key, DEFAULT_VALUE);
            if (group != null)
            {
                return JsonConvert.DeserializeObject<List<Groups>>(group);
            }
            else return null;

        }

        public bool clearPreference()
        {
            return editor.Clear().Commit();
        }


        public string retreivePreference(string key)
        {
            return preferences.GetString(key, DEFAULT_VALUE);
        }

    }

}
