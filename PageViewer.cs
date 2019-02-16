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
using Android.Support.V4.View;
using Android.Support.V4.App;
using Englishbook;
using Android.Graphics.Drawables;
using Android.Preferences;

namespace webview
{
    [Activity(Label = "تعلم الانجليزية",MainLauncher =true)]
    public class PageViewer : FragmentActivity
    {
        ViewPager vp;
        FragmentPagerAdapter adapter;
        Android.Support.V4.App.FragmentManager fm;
        JavaList<Android.Support.V4.App.Fragment> fragment;
        Button butChild;
        Button butprophet;
        Button butmiracles;
        Button butabout;
        string Activity;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.pageviewer);
            ActionBar.Title = "Learn English";
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Blue));
           
            //DEFINE API
            vp = FindViewById<ViewPager>(Resource.Id.pager);
            butChild = FindViewById<Button>(Resource.Id.butChild);
            butprophet = FindViewById<Button>(Resource.Id.butProhet);
            butmiracles = FindViewById<Button>(Resource.Id.butMiracles);
            butabout = FindViewById<Button>(Resource.Id.butAbout);
            //END API

            fm = this.SupportFragmentManager;
            adapter = new MyAdapter(fm, Getfragment());
            vp.Adapter = adapter;

            //EVENT API
            vp.PageSelected += Vp_PageSelected;
            butChild.Click += ButChild_Click;
            butprophet.Click += Butprophet_Click;
            butmiracles.Click += Butmiracles_Click;
            butabout.Click += Butabout_Click;
            //END EVENT
            Activity = Intent.GetStringExtra("Activity");
            if (Activity == "WordFragment")
            {
                vp.CurrentItem = 0;
                
            }
            if (Activity == "GrammerFragment")
            {
                vp.CurrentItem = 1;
                
            }
            if (Activity == "storiesConversationItems")
            {
                vp.CurrentItem = 2;

            }
            if (Activity == "testFragment")
            {
                vp.CurrentItem = 3;

            }


        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Rate:
                    //do something
                    var ur = Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=com.mohameedsalah.LearnEnglish");
                    var intent2 = new Intent(Intent.ActionView, ur);
                    StartActivity(intent2);
                    return true;

            }
            switch (item.ItemId)
            {
                case Resource.Id.Another:
                    //do something
                    var ur = Android.Net.Uri.Parse("https://play.google.com/store/apps/developer?id=mohameed+salah");
                    var intent2 = new Intent(Intent.ActionView, ur);
                    StartActivity(intent2);
                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }
        //TO Rate Applications

        void saveData(int Rateing)
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = pref.Edit();
            editor.PutInt("Rateing", Rateing);
            editor.Apply();
        }
        void CheckData()
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            int rate = pref.GetInt("Rateing", 0);
            if (rate == 0)
            {
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetTitle("تقييم التطبيق");
                callDialog.SetMessage("(هل تريد تقييم التطبيق؟(رجاء تقييم التطبيق رايك يهمنالتطوير التطبيق لافضل");
                callDialog.SetNeutralButton("نعم", delegate {
                    saveData(1);
                    var ur = Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=com.mohameedsalah.LearnEnglish");
                    var intent2 = new Intent(Intent.ActionView, ur);
                    StartActivity(intent2);
                });
                callDialog.SetNegativeButton("لا شكرا", delegate { this.FinishAffinity(); });

                callDialog.Show();
            }

            if (rate == 1)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);

                alert.SetTitle("تاكيد الخروج");
                alert.SetMessage("الخروج من البرنامج");
                alert.SetPositiveButton("نعم", (senderAlert, args) =>
                {
                    this.FinishAffinity();

                });
                alert.SetNegativeButton("لا", (senderAlert, args) =>
                {

                });
                Dialog dialog = alert.Create();
                dialog.Show();
            }
        }
        public override void OnBackPressed()
        {
            CheckData();




        }

        private void Butabout_Click(object sender, EventArgs e)
        {
            vp.CurrentItem = 3;
        }

        private void Butmiracles_Click(object sender, EventArgs e)
        {
            vp.CurrentItem = 2;
        }

        private void Butprophet_Click(object sender, EventArgs e)
        {
            vp.CurrentItem = 1;
        }

        private void ButChild_Click(object sender, EventArgs e)
        {
            vp.CurrentItem = 0;
        }

        private void Vp_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if (vp.CurrentItem == 0)
            {
                butChild.SetBackgroundResource(Resource.Drawable.backline);
                butprophet.SetBackgroundResource(Resource.Drawable.backlinenone);
                butmiracles.SetBackgroundResource(Resource.Drawable.backlinenone);
                butabout.SetBackgroundResource(Resource.Drawable.backlinenone);
            }
            if (vp.CurrentItem == 1)
            {
                butChild.SetBackgroundResource(Resource.Drawable.backlinenone);
                butprophet.SetBackgroundResource(Resource.Drawable.backline);
                butmiracles.SetBackgroundResource(Resource.Drawable.backlinenone);
                butabout.SetBackgroundResource(Resource.Drawable.backlinenone);
            }
            if (vp.CurrentItem == 2)
            {
                butChild.SetBackgroundResource(Resource.Drawable.backlinenone);
                butprophet.SetBackgroundResource(Resource.Drawable.backlinenone);
                butmiracles.SetBackgroundResource(Resource.Drawable.backline);
                butabout.SetBackgroundResource(Resource.Drawable.backlinenone);
            }
            if (vp.CurrentItem == 3)
            {
                butChild.SetBackgroundResource(Resource.Drawable.backlinenone);
                butprophet.SetBackgroundResource(Resource.Drawable.backlinenone);
                butmiracles.SetBackgroundResource(Resource.Drawable.backlinenone);
                butabout.SetBackgroundResource(Resource.Drawable.backline);
            }


        }

        private JavaList<Android.Support.V4.App.Fragment> Getfragment()
        {
            fragment = new JavaList<Android.Support.V4.App.Fragment>();
            fragment.Add(new WordFragment());
            fragment.Add(new GrammarFragment());
            fragment.Add(new StoriesFragment());
            fragment.Add(new TestFragment());
            return fragment;
        }
    }
}