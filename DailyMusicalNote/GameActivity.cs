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
        public static GameMechanism myGame;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);

            //Full screen
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            Activity myActivity = this;
            Context myContext = Android.App.Application.Context;

            Button testButton = FindViewById<Button>(Resource.Id.button1);

            myGame = new GameMechanism(myActivity, myContext);
            myGame.CreateMechanism();

            //Initialize game mechanism
            myGame.NextNote();
            myGame.StartTimer();

            testButton.Click += (s, e) => myGame.NextNote();
        }
    }
}