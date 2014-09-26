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
		Java.IO.File sdCard ;
		Java.IO.File directory;
		Java.IO.File file;
		protected override void OnCreate (Bundle bundle)
		{	
			sdCard= Android.OS.Environment.ExternalStorageDirectory;
			directory= new Java.IO.File (sdCard.AbsolutePath + "/downloads");
			if (!directory.Exists ()) {
				directory.Mkdirs ();
			} 
			file = new Java.IO.File (directory, "text.txt");
		
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			if (bundle != null)
				this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));
			AddTab ("All people", Resource.Drawable.Icon, new AllPeopleFragement ());

			//file function may be used later
//			file.Delete();
//			Fake_data ();
//			Read_file ();
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
		void Read_group(String s){		
			String[] tabs = s.Split ('|'); 
			for (int i = 0; i < tabs.Length; i++) {
				AddTab (tabs[i], Resource.Drawable.Icon, new AllPeopleFragement ());
			}
		}
		void Fake_data(){
			FileWriter writer = new FileWriter (file); 
			// Writes the content to the file
			writer.Append ("Group:Group 1|Group 2|Group3|"); 
			writer.Flush ();
			writer.Close ();
		}
		void Read_file(){

			FileReader fr = new FileReader(file); 
			BufferedReader br = new BufferedReader(fr); 
			String line; 
			String data="";
			while((line = br.ReadLine()) != null) { 
				data += line;
			} 
			fr.Close ();
			String[] tabs = data.Split (':');
			for (int i = 0; i < tabs.Length; i += 2) {
				switch (tabs [i]) {
				case "Group":
					Read_group(tabs [i + 1]);
						break;
				}
			}
		}
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			menu.Add(0,0,0,"Item 0");
			menu.Add(0,1,1,"Item 1");
			return true;
		}
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case 0: //Do stuff for button 0
				return true;
			case 1: //Do stuff for button 1
				return true;
			default:
				return base.OnOptionsItemSelected(item);
			}
		}
	}
}



