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
using System.Timers;

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

            int secs = 0;
            int mins = 0;
            TextView czas = FindViewById<TextView>(Resource.Id.topBar_timer_value);
            Timer myTimer = new Timer(1000);
            myTimer.AutoReset = true;
            myTimer.Enabled = true;
            myTimer.Elapsed += (s, e) =>
            {
                secs++;
                RunOnUiThread(() => {

                    string timeString = "00:00", minsString="00", secString = "00";
                    if(secs>=60)
                    {
                        secs = 0;
                        mins++;
                    }

                    secString = secs < 10 ? "0" + secs.ToString() : secs.ToString();
                    minsString = mins < 10 ? "0" + mins.ToString() : mins.ToString();

                    timeString = minsString + ":" + secString;

                    czas.Text = timeString;
                
                });
            };
            myTimer.Start();

            //Initialize game mechanism
            myGame.NextNote();

            testButton.Click += (s, e) => myGame.NextNote();
        }
    }
}