using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net;
using Java.IO;
using Java.Util;
namespace Tab
{	[Activity (Label = "Tab", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{   
		List<string> callHistory = new List<string>();
		Button addGroupButton;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
				
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			AddTab ("Tab 1", Resource.Drawable.Icon, new SampleTabFragment ());
			AddTab ("Tab 2", Resource.Drawable.Icon, new SampleTabFragment2 ());
			if (bundle != null)
				this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));
			addGroupButton = FindViewById<Button> (Resource.Id.add_tab_Button);
			addGroupButton.LongClick += delegate {
				AddTab ("Tab 3", Resource.Drawable.Icon, new SampleTabFragment ());
				Java.IO.File sdCard = Android.OS.Environment.ExternalStorageDirectory;
				Java.IO.File directory = new Java.IO.File (sdCard.AbsolutePath + "/downloads");

				if (!directory.Exists ()) {
					directory.Mkdirs ();
				} 

				Java.IO.File file = new Java.IO.File (directory, "text.txt");
					FileWriter writer = new FileWriter (file,true); 
					// Writes the content to the file
					writer.Append ("Press\n Add\n Button\n in\n Main\n"); 
					writer.Flush ();
					writer.Close (); 
			};
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			outState.PutInt("tab", this.ActionBar.SelectedNavigationIndex);

			base.OnSaveInstanceState(outState);
		}

		void AddTab (string tabText, int iconResourceId, Fragment view)
		{
			var tab = this.ActionBar.NewTab ();            
			tab.SetText (tabText);
			//tab.SetIcon (Resource.Drawable.ic_tab_white);

			// must set event handler before adding tab
			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
			{
				var  fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
				if (fragment != null)
					e.FragmentTransaction.Remove(fragment);    
				Bundle bundle = new Bundle();
				bundle.PutInt("detail",3);
				view.Arguments=bundle;
				e.FragmentTransaction.Add (Resource.Id.fragmentContainer, view);
			};
			tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Remove(view);  
			};

			this.ActionBar.AddTab (tab);
		}
	}
}



