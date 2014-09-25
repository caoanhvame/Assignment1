﻿
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
	class SampleTabFragment: Fragment
	{  string data="";
		TextView sampleTextView;
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{ 	
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.Tab, container, false);
			sampleTextView = view.FindViewById<TextView> (Resource.Id.sampleTextView); 
			var imageView1 = view.FindViewById<ImageView> (Resource.Id.imageView1);
			String status = Android.OS.Environment.ExternalStorageState;
			if (Android.OS.Environment.MediaMounted.Equals (status)) {
				Toast.MakeText (Activity.ApplicationContext, "External", ToastLength.Short).Show ();
			}
			Java.IO.File sdCard = Android.OS.Environment.ExternalStorageDirectory;
			Java.IO.File directory = new Java.IO.File (sdCard.AbsolutePath + "/downloads");

			if (!directory.Exists ()) {
				System.Console.Write ("bbbbbbbbbbbbbb");
				directory.Mkdirs ();
			} 
				
			Java.IO.File file = new Java.IO.File (directory, "text.txt");
			if (!file.Exists ()) {
				FileWriter writer = new FileWriter (file); 
				// Writes the content to the file
				writer.Append ("This\n is\n not\n an\n animal\n"); 
				writer.Append ("This\n is\n an\n animal\n"); 
				writer.Flush ();
				writer.Close ();
			} 
			FileReader fr = new FileReader(file); 
			BufferedReader br = new BufferedReader(fr); 
			String line; 
			while((line = br.ReadLine()) != null) { 
				data += line; 
			} 
//			char [] a = new char[190];
//			fr.Read(a); // reads the content to the array
//			for (int i = 0; i < a.Length; i++) {
//				data+=a[i];
//			}
			fr.Close();
			sampleTextView.Text=data;


		


			Bundle bundle = Arguments; 
			String s = "aaaa" + bundle.GetInt ("detail", 0);
//			sampleTextView.Text = s;
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

			String xmlString =
				@"<bookstore>
        <book genre='autobiography' publicationdate='1981-03-22' ISBN='1-861003-11-0'>
            <title>The Autobiography of Benjamin Franklin</title>
            <author>
                <first-name>Benjamin</first-name>
                <last-name>Franklin</last-name>
            </author>
            <price>8.99</price>
        </book>
 		<book genre='novel' publicationdate='1981-03-22' ISBN='1-861003-11-0'>
            <title>New novel</title>
            <author>
                <first-name>AAAA</first-name>
                <last-name>Franklin</last-name>
            </author>
            <price>8.99</price>
        </book>
    	</bookstore>";
//			HandleXML a = new HandleXML (data);
//			a.parseXMLAndStoreIt ();
			return view;
		}

	}
}


