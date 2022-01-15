using Android.App;
using Android.Content;
using Android.Media;
using Android.Views;
using System;

namespace DailyMusicalNote
{
    /// <summary>
    /// Here are keyboard press handling.
    /// </summary>
    class KeyMechanism : Activity
    {
        private string elementId = "temp", AudioFileName = "temp";
        private readonly Activity myActivity;
        private readonly Context myContext;
        private bool disableKeyFlag = false;
        private MyEnums.ClefList bassOrTreble;
        private View pianoKey;
        private MediaPlayer player;
        private bool isItBassKey = false;

        /// <summary>
        /// Constructor, initializes necesary variables and sets the click handler.
        /// </summary>
        /// <param name="elementId">View's element ID</param>
        /// <param name="myActivity">Variable of Activity type to get activity from GameAvtivity.cs (main game activity).</param>
        /// <param name="myContext">Variable of Context type to get context from GameAvtivity.cs (main game activity).</param>
        /// <param name="bassOrTreble">Current chosen clef.</param>
        /// <param name="audioName">Optional, audio file name (sound which will be playing after click an object).</param>
        public KeyMechanism(string elementId, Activity myActivity, Context myContext, MyEnums.ClefList bassOrTreble, string audioName = "")
        {
            this.elementId = elementId;
            this.myActivity = myActivity;
            this.myContext = myContext;
            this.bassOrTreble = bassOrTreble;
            this.AudioFileName = audioName;
            pianoKey = myActivity.FindViewById(myActivity.Resources.GetIdentifier(elementId, "id", myContext.PackageName));

            //Big mistake???
            ClickHander();
        }

        /// <summary>
        /// Stops playing sound.
        /// </summary>
        public void stopPlay()
        {
            player.Stop();
        }

        /// <summary>
        /// When current clef is bass then transforms keyboard view (transform -24 keys).
        /// </summary>
        private void setBassKeys()
        {
            //a3 = 27, last of treble clef
            int tempKey = (int)MyEnums.clickedKey;
            tempKey += 24;
            MyEnums.clickedKey = (MyEnums.pianoKey)tempKey;
        }

        /// <summary>
        /// Handlers when key is down and when is up (including playing sounds).
        /// </summary>
        /// <param name="AudioName">Name of audio file to play.</param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void touched(string AudioName, object s, View.TouchEventArgs e)
        {
            if (!disableKeyFlag)
            {
                bool handled = false;

                //Key down
                if (e.Event.Action == MotionEventActions.Down && MyEnums.isKeyUp == true)
                {
                    MyEnums.isKeyUp = false;

                    var fileDescriptor = myActivity.Assets.OpenFd("Sounds/" + AudioName + ".mp3");
                    player = new MediaPlayer();
                    player.Reset();
                    player.SetDataSource(fileDescriptor.FileDescriptor, fileDescriptor.StartOffset, fileDescriptor.Length);

                    //TUTAJ JEST PRZYPISANIE DO ENUMA CO KLIKNIETO
                    MyEnums.clickedKey = (MyEnums.pianoKey)Enum.Parse(typeof(MyEnums.pianoKey), elementId);
                    if(isItBassKey)
                    {
                        setBassKeys();
                    }

                    //Add click visual effect
                    if (elementId.Contains("sh"))
                    {
                        //black keys
                        pianoKey.SetBackgroundColor(color: Android.Graphics.Color.Rgb(40, 40, 40));
                    }
                    else
                    {
                        //white keys
                        pianoKey.SetBackgroundColor(color: Android.Graphics.Color.Rgb(195, 195, 195));
                    }

                    //Start play note
                    player.Prepare();
                    player.Start();
                    handled = true;

                    if (MyEnums.currentGameMode == MyEnums.gameMode.notePractice)
                    {
                        GameActivity.myGame.NextNote();
                    }
                    else if (MyEnums.currentGameMode == MyEnums.gameMode.hearingPractice)
                    {
                        GameActivity.myHearingGame.chceckNote();
                    }
                    
                }
                //Key up
                else if (e.Event.Action == MotionEventActions.Up)
                {
                    //Remove click visual effect
                    if (elementId.Contains("sh"))
                    {
                        //black keys
                        pianoKey.SetBackgroundColor(color: Android.Graphics.Color.Rgb(0, 0, 0));
                    }
                    else
                    {
                        //white keys
                        pianoKey.Background = myActivity.GetDrawable(Resource.Drawable.whiteButton);
                    }

                    //Stop play note
                    player.Stop();
                    player.Release();
                    handled = true;

                    MyEnums.isKeyUp = true;

                    if (MyEnums.currentGameMode == MyEnums.gameMode.hearingPractice && GameActivity.myHearingGame.isCorrect == true)
                    { 
                        GameActivity.myHearingGame.playNote();
                        GameActivity.myHearingGame.isCorrect = false;
                    }
                }

                e.Handled = handled;
            }
            else
            {
                bool handled = false;

                //Key down
                if (e.Event.Action == MotionEventActions.Down)
                {
                    //do nothing
                }
                //Key up
                else if (e.Event.Action == MotionEventActions.Up)
                {
                    //Remove click visual effect
                    if (elementId.Contains("sh"))
                    {
                        //black keys
                        pianoKey.SetBackgroundColor(color: Android.Graphics.Color.Rgb(0, 0, 0));
                    }
                    else
                    {
                        //white keys
                        pianoKey.Background = myActivity.GetDrawable(Resource.Drawable.whiteButton);
                    }

                    //Stop play note
                    player.Stop();
                    player.Release();
                    handled = true;
                }

                e.Handled = handled;
            }
        }

        /// <summary>
        /// Handlers key click.
        /// </summary>
        private void ClickHander()
        {
            player = new MediaPlayer();

            pianoKey.Touch += (s, e) =>
            {
                if (bassOrTreble == MyEnums.currentClef && bassOrTreble == MyEnums.ClefList.treble)
                {
                    //Treble clef
                    isItBassKey = false;
                    AudioFileName = elementId.Substring(0, elementId.LastIndexOf("_"));
                    touched(AudioFileName, s, e);
                }
                else if(bassOrTreble == MyEnums.currentClef && bassOrTreble == MyEnums.ClefList.bass)
                {
                    //Bass clef
                    isItBassKey = true;
                    touched(AudioFileName, s, e);
                }
            };

        }

        /// <summary>
        /// Disables key.
        /// </summary>
        public void DisableKey()
        {
            disableKeyFlag = true;
        }
    }
}