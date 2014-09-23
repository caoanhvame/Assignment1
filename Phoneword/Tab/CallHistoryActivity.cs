
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

namespace Anh
{
	[Activity (Label = "CallHistoryActivity")]			
	public class CallHistoryActivity : ListActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			var phoneNumbers=Intent.Extras.GetStringArrayList("phone_numbers") ?? new String[0];
			this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);
		}
	}
}

