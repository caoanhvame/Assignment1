
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Tab
{
	class SampleTabFragment2 : Fragment
	{		List<int> allPictures = new List<int> ();
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{	
//			allPictures.Add (Resource.Drawable.sample_0);
			base.OnCreateView(inflater, container, savedInstanceState);
			var view = inflater.Inflate(Resource.Layout.Tab2, container, false);
			var sampleTextView = view.FindViewById<TextView>(Resource.Id.sampleTextView);
			sampleTextView.Text = "sample fragment text 2";
			var gridview = view.FindViewById<GridView> (Resource.Id.gridview);
			//var tab_layout = view.FindViewById<LinearLayout> (Resource.Id.tab_layout);
			gridview.Adapter = new ImageAdapter (Activity.ApplicationContext,allPictures);
			Button btn = view.FindViewById<Button> (Resource.Id.translate_Button);
			int piccount=0;
			btn.Click += delegate {
				switch (piccount){
				case 0:
					allPictures.Add(Resource.Drawable.sample_0);
					break;
				case 1:
					allPictures.Add(Resource.Drawable.sample_1);
					break;
				case 2:
					allPictures.Add(Resource.Drawable.sample_2);
					break;
				case 3:
					allPictures.Add(Resource.Drawable.sample_3);
					break;
				case 4:
					allPictures.Add(Resource.Drawable.sample_4);
					break;
				};
				if(piccount<4){
					piccount++;
				}else{
					piccount=0;
				}
				gridview.Adapter = new ImageAdapter (Activity.ApplicationContext,allPictures);

			};
			gridview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
				Toast.MakeText (Activity.ApplicationContext, args.Position.ToString (), ToastLength.Short).Show ();
			};
			return view;
		}
	}
}


