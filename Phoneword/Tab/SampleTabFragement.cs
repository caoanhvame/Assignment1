﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
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
	{       
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			String status = Android.OS.Environment.ExternalStorageState;
			if ( Android.OS.Environment.MediaMounted.Equals(status)) {
				Toast.MakeText (Activity.ApplicationContext, "External", ToastLength.Short).Show ();
			}
			Java.IO.File sdCard = Android.OS.Environment.ExternalStorageDirectory;
			//Create the directory
			Java.IO.File directory = new Java.IO.File(sdCard.AbsolutePath+"/downloads");
			directory.Mkdirs();

			//Create the file
			Java.IO.File file = new Java.IO.File(directory, "text.txt");
			FileWriter writer = new FileWriter(file); 
			// Writes the content to the file
			writer.Write("This\n is\n an\n example\n"); 
			writer.Flush();
			writer.Close();

			var view = inflater.Inflate (Resource.Layout.Tab, container, false);
			var sampleTextView = view.FindViewById<TextView> (Resource.Id.sampleTextView); 
			var imageView1 = view.FindViewById<ImageView> (Resource.Id.imageView1);

			Bundle bundle=Arguments; 
			String s = "aaaa" + bundle.GetInt ("detail", 0);
			sampleTextView.Text = s;

			var connectivityManager = (ConnectivityManager)Activity.GetSystemService(Activity.ConnectivityService);
			var activeConnection = connectivityManager.ActiveNetworkInfo;
			if ((activeConnection != null)  && activeConnection.IsConnected)
			{

				string urlAddress = "http://images.nationalgeographic.com/wpf/media-live/photos/000/138/overrides/save-the-ocean-tips_13821_600x450.jpg";
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				if (response.StatusCode == HttpStatusCode.OK)
				{
					Stream receiveStream = response.GetResponseStream();
					byte[] b;
					using (BinaryReader br = new BinaryReader(receiveStream))
					{
						b = br.ReadBytes((int)receiveStream.Length);
					}
					StreamReader readStream = null;
					if (response.CharacterSet == null)
						readStream = new StreamReader(receiveStream);
					else
						readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
					string data = readStream.ReadToEnd();
					byte[] bytes = readStream.CurrentEncoding.GetBytes(readStream.ReadToEnd());
					Bitmap decodedByte = BitmapFactory.DecodeByteArray(b, 0, b.Length); 

					imageView1.SetImageBitmap(decodedByte);
					response.Close();
					readStream.Close();

				}

			}
			return view;
		}
	}
}
	     private class DownloadWebpageTask extends AsyncTask<String, Void, String> {


