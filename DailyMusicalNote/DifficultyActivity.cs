using Android.App;
using Android.OS;
using Android.Views;

namespace DailyMusicalNote
{
    /// <summary>
    /// Choosing difficulty activity (handling and displaying).
    /// </summary>
    [Activity(Label = "DifficultyActivity")]
    public class DifficultyActivity : Activity
    {   
        /// <summary>
        /// On create method.
        /// </summary>
        /// <param name="savedInstanceState"></param>
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

        /// <summary>
        /// Function sets a globabal variable (which stores chosen difficulty mode) then goes to GameAvticity.cs .
        /// </summary>
        /// <param name="setDifficulty">Chosen difficulty mode</param>
        private void goToGamePage(MyEnums.DifficultyMode setDifficulty)
        {
            MyEnums.ChoseDifficulty = setDifficulty;
            StartActivity(typeof(GameActivity));
        }
    }
}