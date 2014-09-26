
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

namespace Tab
{
	[Activity (Label = "PersonProfileActivity")]			
	public class PersonProfileActivity : Activity
	{
		private ImageView ava;
		private TextView extra;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Title = "Anything";
			SetContentView (Resource.Layout.PersonProfile);
			ava = FindViewById<ImageView> (Resource.Id.userProfileAva);
			extra = FindViewById<TextView> (Resource.Id.userProfileExtra);
			extra.Text = "Like ...";

		}
	}
}

