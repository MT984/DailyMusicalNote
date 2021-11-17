using Android.App;
using Android.Content;
using Android.Content.PM;
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
    //Landscape screen orientation 
    [Activity(Label = "GameActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Landscape)]
    public class GameActivity : Activity 
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);

            //Full screen
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            ////Demo below
            //TextView tekst = FindViewById<TextView>(Resource.Id.textView1);

            //string testChoosing;

            //switch(MyEnums.ChoseDifficulty)
            //{
            //    case MyEnums.DifficultyMode.Easy:
            //        testChoosing = "Easy";
            //        break;
    
            //    case MyEnums.DifficultyMode.Medium:
            //        testChoosing = "Medium";
            //        break;

            //    case MyEnums.DifficultyMode.Hard:
            //        testChoosing = "Hard";
            //        break;

            //    default:
            //        testChoosing = "There is an error";
            //        break;
            //}

            //tekst.Text = testChoosing;
        }
    }
}