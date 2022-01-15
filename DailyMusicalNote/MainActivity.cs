/*
 * Designed and Created by Maciej Trocha
 * Elektronika sem. 5
 * Grupa 1.
 * Jezyki Programowania Wysokiego Poziomu 2021
 */

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using System.IO;

namespace DailyMusicalNote
{
    /// <summary>
    /// Main activity, menu (handling and displaying).
    /// </summary>
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Full screen
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            //Create folder to storage data, only once!
            var directory = this.FilesDir + MyEnums.StorageFolderName;
            var exists = Directory.Exists(directory);
            if (!exists)
            {
                Directory.CreateDirectory(directory);
            }

            //Buttons deffinitions
            View buttonStart = FindViewById<View>(Resource.Id.button_start);
            View buttonHearing = FindViewById<View>(Resource.Id.button_hearing);
            View buttonHistory = FindViewById<View>(Resource.Id.button_history);

            //Buttons events handlers
            buttonStart.Click += (s, e) =>
            {
                MyEnums.currentGameMode = MyEnums.gameMode.notePractice;
                StartActivity(typeof(DifficultyActivity));
            };

            buttonHearing.Click += (s, e) =>
            {
                MyEnums.currentGameMode = MyEnums.gameMode.hearingPractice;
                StartActivity(typeof(GameActivity));
            }; 
            buttonHistory.Click += (s, e) => StartActivity(typeof(HistoryActivity));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}