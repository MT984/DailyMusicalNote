using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace DailyMusicalNote
{
    /// <summary>
    /// Game avtivity, here are generated all game views and init mechanisms.
    /// </summary>
    //Landscape screen orientation 
    [Activity(Label = "GameActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Landscape)]
    public class GameActivity : Activity 
    {
        public static GameMechanism myGame;
        public static HearingMechanism myHearingGame;
        /// <summary>
        /// On create method, here are code for run game.
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Full screen
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            Activity myActivity = this;
            Context myContext = Android.App.Application.Context;

            
            if (MyEnums.currentGameMode == MyEnums.gameMode.notePractice)
            {
                //Note practice
                SetContentView(Resource.Layout.game);

                myGame = null;
                myGame = new GameMechanism(myActivity, myContext);
                myGame.CreateMechanism();

                //Initialize game mechanism
                myGame.NextNote();
                myGame.StartTimer();
            }else if(MyEnums.currentGameMode == MyEnums.gameMode.hearingPractice)
            {
                //Hearing practice
                SetContentView(Resource.Layout.hearing);

                myHearingGame = null;
                myHearingGame = new HearingMechanism(myActivity, myContext);
                myHearingGame.CreateMechanism();

                //Initialize game mechanism
                myHearingGame.nextHearingNote();
                myHearingGame.playNote();
                myHearingGame.StartTimer();
            }
        }
    }
}