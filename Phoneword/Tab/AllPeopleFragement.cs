
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;//for thread
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Net;
using Android.Graphics;
using Java.IO;
using Java.Util;
//using Android.Net.Http;

namespace Tab
{
	class AllPeopleFragement: Fragment
	{  string data="All people";
		TextView sampleTextView;
		EditText personNameEditText;
		List<string> web=new List<string>(){"Google ",
			"Twitter",
			"Windows",
			"Bing",
			"Itunes",
			"Wordpress",
			"Drupal"};
		List<int> imageId =new List<int>(){Resource.Drawable.sample_0,
			Resource.Drawable.sample_1,
			Resource.Drawable.sample_2,
			Resource.Drawable.sample_3,
			Resource.Drawable.sample_4,
			Resource.Drawable.sample_5,
			Resource.Drawable.sample_6,};
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{ 	
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.Tab, container, false);
			sampleTextView = view.FindViewById<TextView> (Resource.Id.sampleTextView); 
			personNameEditText = view.FindViewById<EditText> (Resource.Id.peopleName);
			var imageView1 = view.FindViewById<ImageView> (Resource.Id.imageView1);
			String status = Android.OS.Environment.ExternalStorageState;
			if (Android.OS.Environment.MediaMounted.Equals (status)) {
				Toast.MakeText (Activity.ApplicationContext, "External", ToastLength.Short).Show ();
			}
			sampleTextView.Text=data;
			var connectivityManager = (ConnectivityManager)Activity.GetSystemService (Activity.ConnectivityService);
			var activeConnection = connectivityManager.ActiveNetworkInfo;
			if ((activeConnection != null) && activeConnection.IsConnected) {
				string urlAddress = "https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?match_id=27110133&format=xml&key=920D79A1A3DC294290FD3BBB7630B9BE";
				//http://api.openweathermap.org/data/2.5/weather?q=london
				//http://images.nationalgeographic.com/wpf/media-live/photos/000/138/overrides/save-the-ocean-tips_13821_600x450.jpg
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (urlAddress);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
				if (response.StatusCode == HttpStatusCode.OK) {
					Stream receiveStream = response.GetResponseStream ();
					StreamReader readStream = null;
					if (response.CharacterSet == null)
						readStream = new StreamReader (receiveStream);
					else
						readStream = new StreamReader (receiveStream, Encoding.GetEncoding (response.CharacterSet));
//					data = readStream.ReadToEnd ();

					//					byte[] b;
					//					using (BinaryReader br = new BinaryReader(receiveStream))
					//					{
					//						b = br.ReadBytes((int)receiveStream.Length);
					//					}
					//					Bitmap decodedByte = BitmapFactory.DecodeByteArray(b, 0, b.Length); 
					//					imageView1.SetImageBitmap(decodedByte);
					//					Activity
//					sampleTextView.Text = data;
					response.Close ();
					readStream.Close ();
				}
			}
//			HandleXML a = new HandleXML (data);
//			a.parseXMLAndStoreIt ();
		
				

			CustomList adapter = new CustomList( Activity, web, imageId);
			ListView list=(ListView)view.FindViewById(Resource.Id.list);

			list.Adapter = adapter;
			list.ItemClick+= delegate(object sender, AdapterView.ItemClickEventArgs args) {
				list.GetChildAt(args.Position).SetBackgroundColor(Color.Green);
				list.GetChildAt(args.Position).Selected=true;
				Toast.MakeText (Activity.ApplicationContext, args.Position.ToString (), ToastLength.Short).Show ();
				Intent person_profile=new Intent(Activity,typeof(PersonProfileActivity));
				StartActivity(person_profile);
//				list.SetItemChecked(1,true);
			};
			personNameEditText.TextChanged += delegate(object sender, Android.Text.TextChangedEventArgs e) {
				sampleTextView.Text= e.Text+"";
				adapter = new CustomList( Activity, search_function(web,e.Text+""), imageId,e.Text+"");
				list.Adapter=adapter;

			};
			return view;
		}

		private List<String> search_function(List<String> people,String search_str){
			List<String> temp = new List<string>();
			for (int i = 0; i<people.Count; i++) {
				if (people[i].Contains (search_str)) {
					temp.Add (people [i]);
				}
			}
			return temp;
		}

	}
}


