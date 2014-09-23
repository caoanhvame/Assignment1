﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Tab
{	[Activity (Label = "Tab", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{   
		List<string> callHistory = new List<string>();
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
				bundle.PutInt("aaa",3);
				fragment.setArguments(bundle);
				//fragment.setArguments(bundle);
				e.FragmentTransaction.Add (Resource.Id.fragmentContainer, view);
			};
			tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Remove(view);
			};

			this.ActionBar.AddTab (tab);
		}

		class SampleTabFragment: ListFragment
		{            
			public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				base.OnCreateView (inflater, container, savedInstanceState);

				var view = inflater.Inflate (Resource.Layout.Tab, container, false);
				var sampleTextView = view.FindViewById<TextView> (Resource.Id.sampleTextView);             
				sampleTextView.Text = "sample fragment text";
				//var phoneNumbers=Bundle.GetStringArrayList("phone_numbers") ?? new String[0];
				//this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);
				return view;
			}
		}

		class SampleTabFragment2 : Fragment
		{
			public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				base.OnCreateView(inflater, container, savedInstanceState);

				var view = inflater.Inflate(Resource.Layout.Tab, container, false);
				var sampleTextView = view.FindViewById<TextView>(Resource.Id.sampleTextView);
				sampleTextView.Text = "sample fragment text 2";

				return view;
			}
		}
	}
}


