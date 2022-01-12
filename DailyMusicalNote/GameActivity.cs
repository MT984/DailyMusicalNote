using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace DailyMusicalNote
{
    //Landscape screen orientation 
    [Activity(Label = "GameActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Landscape)]
    public class GameActivity : Activity 
    {
        public static GameMechanism myGame;
        public static HearingMechanism myHearingGame;
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

                myGame = new GameMechanism(myActivity, myContext);
                myGame.CreateMechanism();

                //Initialize game mechanism
                myGame.NextNote();
                myGame.StartTimer();
            }else if(MyEnums.currentGameMode == MyEnums.gameMode.hearingPractice)
            {
                //Hearing practice
                SetContentView(Resource.Layout.hearing);

                myHearingGame = new HearingMechanism(myActivity);
                myHearingGame.CreateMechanism();

                //Initialize game mechanism
                myHearingGame.nextHearingNote();
                myHearingGame.playNote();
                myHearingGame.StartTimer();
            }
        }
    }
}