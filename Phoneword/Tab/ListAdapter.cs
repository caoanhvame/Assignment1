
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
using Android.Text;// for Spannable
using Android.Graphics;
using Android.Text.Style;
namespace Tab
{
	[Activity (Label = "ListAdapter")]			
	public class CustomList: BaseAdapter{
		private Activity context;
		private List<string> web;
		private List<int> imageId;
		private String search_str="";
		public CustomList(Activity context,List<string>  web, List<int> imageId) {
			this.context = context;
			this.web = web;
			this.imageId = imageId;
		}
		public CustomList(Activity context,List<string>  web, List<int> imageId,String str) {
			this.context = context;
			this.web = web;
			this.imageId = imageId;
			this.search_str = str;
		}

		public override int Count {
			get { return web.Count; }
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
			int found_pos = web [position].IndexOfAny (search_str.ToCharArray ());
			if (found_pos!= -1) {
				SpannableString spannable = new SpannableString (web [position]);
				spannable.SetSpan (new ForegroundColorSpan (Color.Red), found_pos, 
					found_pos+search_str.Length, Android.Text.SpanTypes.ExclusiveExclusive);

				txtTitle.SetText (spannable, TextView.BufferType.Spannable);
			} else {
				txtTitle.Text = web [position];
			}
			imageView.SetImageResource(imageId[position]);
			imageView.Tag =imageId [position];
//			txtTitle.Touch += delegate(object sender, View.TouchEventArgs touchEventArgs) {
//								string message;
//								switch (touchEventArgs.Event.Action )
//								{
//								case MotionEventActions.Down:
//								case MotionEventActions.Move:
//									message = "Touch Begins";
//									System.Console.WriteLine("X  "+touchEventArgs.Event.GetX());
//									break;
//								case MotionEventActions.Up:
//									message = "Touch Ends";
//									System.Console.WriteLine("After:  "+touchEventArgs.Event.GetX());
//									break;
//				
//								default:
//									message = string.Empty;
//									break;
//								}
//				
//								System.Console.WriteLine(message);
//			
//							};
			return rowView;
		}
	}
}



