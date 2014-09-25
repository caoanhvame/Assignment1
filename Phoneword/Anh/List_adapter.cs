
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
	[Activity (Label = "List_adapter")]			
	public class CustomList: BaseAdapter{
		private Activity context;
		private string[] web;
		private int[] imageId;
		public CustomList(Activity context,string[] web, int [] imageId) {
			this.context = context;
			this.web = web;
			this.imageId = imageId;
		}

		public override int Count {
			get { return web.Length; }
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return 0;
		}


		public override View GetView(int position, View view, ViewGroup parent) {
			LayoutInflater inflater = context.LayoutInflater;
			View rowView=inflater.Inflate(Resource.Layout.list_row, null, true);
			TextView txtTitle = (TextView) rowView.FindViewById(Resource.Id.txt);
			ImageView imageView = (ImageView) rowView.FindViewById(Resource.Id.img);
			txtTitle.Text=web[position];
			imageView.SetImageResource(imageId[position]);

			return rowView;
		}
	}
}

