
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
	[Activity (Label = "ListViewActivity")]			
	public class ListViewActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ListView);
			String[] web = {
				"Google Plus",
				"Twitter",
				"Windows",
				"Bing",
				"Itunes",
				"Wordpress",
				"Drupal"
			} ;
			int[] imageId = {
				Resource.Drawable.sample_0,
				Resource.Drawable.sample_1,
				Resource.Drawable.sample_2,
				Resource.Drawable.sample_3,
				Resource.Drawable.sample_4,
				Resource.Drawable.sample_5,
				Resource.Drawable.sample_6,
			};
			CustomList adapter = new CustomList( this, web, imageId);
			ListView list=(ListView)FindViewById(Resource.Id.list);
			list.Adapter = adapter;
			//			list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			//				@Override
			//				public void onItemClick(AdapterView<?> parent, View view,
			//					int position, long id) {
			//					Toast.makeText(MainActivity.this, "You Clicked at " +web[+ position], Toast.LENGTH_SHORT).show();
			//				}
			//			});
		}
	}
}

