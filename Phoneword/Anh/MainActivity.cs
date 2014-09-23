using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Anh
{
	[Activity (Label = "Anh", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		 List<string> callHistory = new List<string>();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			string translateNumber="";
			EditText phoneNumber = FindViewById<EditText> (Resource.Id.editText1);
			Button translate = FindViewById<Button> (Resource.Id.translate_Button);
			Button call = FindViewById<Button> (Resource.Id.call_Button);
			Button history = FindViewById<Button> (Resource.Id.history_Button);
			call.Enabled = false;
			history.Enabled = false;
			translate.Click += delegate {
				translateNumber=Anh.PhonewordTranslator.ToNumber(phoneNumber.Text);
				call.Text ="Call to:"+  translateNumber;
				call.Enabled=true;
			};
			call.Click += delegate {
				var callConFirmDialog = new AlertDialog.Builder(this);
				callConFirmDialog.SetMessage("Call " + translateNumber + "?");
				callConFirmDialog.SetNeutralButton("Call", delegate{
					var callIntent=new Intent(Intent.ActionCall);
					callIntent.SetData(Android.Net.Uri.Parse("tel:" + translateNumber));
					callHistory.Add(translateNumber);
					history.Enabled=true;
					StartActivity(callIntent);
					}
				);
				callConFirmDialog.SetNegativeButton("Cancel",delegate {});
				callConFirmDialog.Show();
			};

			history.Click += delegate {
				var historyIntent=new Intent(this, typeof(CallHistoryActivity));
				historyIntent.PutStringArrayListExtra("phone_numbers",callHistory);
				StartActivity(historyIntent);
			};

		}
	}
}


