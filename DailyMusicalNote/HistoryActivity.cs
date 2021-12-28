using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DailyMusicalNote
{
    [Activity(Label = "HistoryActivity")]
    public class HistoryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.history);

            //Full screen
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            //Definition od variables
            List<string> myList = new List<string>();
            ListView lv = FindViewById<ListView>(Resource.Id.listView1);

            var directory = this.FilesDir + MyEnums.StorageFolderName;
            var filePath = directory + "/" + MyEnums.HistoryFileName;
            int counter = 0;
            using (var streamReader = new System.IO.StreamReader(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    counter++;
                    //10 spaces
                    string myString = counter.ToString() + ".          " + streamReader.ReadLine() + " pkt          " + streamReader.ReadLine()+"%";
                    myList.Insert(0, myString);
                }
            }

            //Create list
            ArrayAdapter<string> myAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myList);
            lv.Adapter = myAdapter;
        }
    }
}