using Android.App;
using Android.Content;
using Android.Media;
using Android.Views;
using System;

namespace DailyMusicalNote
{
    class KeyMechanism : Activity
    {
        private string elementId = "temp", AudioFileName = "temp";
        private readonly Activity myActivity;
        private readonly Context myContext;
        private bool disableKeyFlag = false;
        private MyEnums.ClefList bassOrTreble;
        private View pianoKey;
        private MediaPlayer player;

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

        private void touched(string AudioName, object s, View.TouchEventArgs e)
        {
            if (!disableKeyFlag)
            {
                bool handled = false;

                //Key down
                if (e.Event.Action == MotionEventActions.Down)
                {
                    var fileDescriptor = myActivity.Assets.OpenFd("Sounds/" + AudioName + ".mp3");
                    player = new MediaPlayer();
                    player.Reset();
                    player.SetDataSource(fileDescriptor.FileDescriptor, fileDescriptor.StartOffset, fileDescriptor.Length);

                    //TUTAJ JEST PRZYPISANIE DO ENUMA CO KLIKNIETO
                    MyEnums.clickedKey = (MyEnums.pianoKey)Enum.Parse(typeof(MyEnums.pianoKey), elementId);

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

                    GameActivity.myGame.NextNote();
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
        private void ClickHander()
        {
            player = new MediaPlayer();

            pianoKey.Touch += (s, e) =>
            {
                if (bassOrTreble == MyEnums.currentClef && bassOrTreble == MyEnums.ClefList.treble)
                {
                    AudioFileName = elementId.Substring(0, elementId.LastIndexOf("_"));
                    touched(AudioFileName, s, e);
                }
                else if(bassOrTreble == MyEnums.currentClef && bassOrTreble == MyEnums.ClefList.bass)
                {
                    touched(AudioFileName, s, e);
                }
            };

        }

        public void DisableKey()
        {
            disableKeyFlag = true;
        }
    }
}