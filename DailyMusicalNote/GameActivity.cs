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
using System.Threading.Tasks;

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

            ImageView trebleClef = FindViewById<ImageView>(Resource.Id.trebleClef);
            ImageView bassClef = FindViewById<ImageView>(Resource.Id.bassClef);

            Button testButton = FindViewById<Button>(Resource.Id.button1);

            testButton.Click += (s, e) => Finish();

            void randomClef()
            {
                Random randomNumbers = new Random();
                int randomTest = randomNumbers.Next(0, 2);

                switch (randomTest)
                {
                    case 0:
                        trebleClef.Visibility = Android.Views.ViewStates.Visible;
                        break;

                    case 1:
                        bassClef.Visibility = Android.Views.ViewStates.Visible;
                        break;
                }
            }

            switch (MyEnums.ChoseDifficulty)
            {
                case MyEnums.DifficultyMode.Easy:
                    trebleClef.Visibility = Android.Views.ViewStates.Visible;
                    break;

                case MyEnums.DifficultyMode.Medium:
                    randomClef();
                    break;

                case MyEnums.DifficultyMode.Hard:
                    randomClef();
                    break;
            }

            //int x = 5;

            //MyEnums.currentKey = (MyEnums.MusicKey)x;

            //if((int)MyEnums.currentKey == 5)
            //{

            //}

            ////Demo below
            //TextView tekst = FindViewById<TextView>(Resource.Id.textView1);

            //string testChoosing;

            //tekst.Text = testChoosing;


        }
    }
}