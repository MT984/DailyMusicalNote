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
    [Activity(Label = "DifficultyActivity")]
    public class DifficultyActivity : Activity
    {   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.difficulty);

            //Full screen
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            //Buttons deffinitions
            View buttonEasy = FindViewById<View>(Resource.Id.button_easy);
            View buttonMedium = FindViewById<View>(Resource.Id.button_medium);
            View buttonHard = FindViewById<View>(Resource.Id.button_hard);

            //Buttons events handlers
            buttonEasy.Click += (s, e) => goToGamePage(MyEnums.DifficultyMode.Easy);
            buttonMedium.Click += (s, e) => goToGamePage(MyEnums.DifficultyMode.Medium);
            buttonHard.Click += (s, e) => goToGamePage(MyEnums.DifficultyMode.Hard);
        }

        //Function goes to GameAvticity.cs and set the 
        private void goToGamePage(MyEnums.DifficultyMode setDifficulty)
        {
            MyEnums.ChoseDifficulty = setDifficulty;
            StartActivity(typeof(GameActivity));
        }
    }
}