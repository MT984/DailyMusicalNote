using Android.App;
using Android.Content;
using Android.Media;
using Android.Views;
using Android.Widget;
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
        private bool isItBassKey = false;
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
        private void setBassKeys()
        {
            //a3 = 27, last of treble clef
            int tempKey = (int)MyEnums.clickedKey;
            tempKey += 24;
            MyEnums.clickedKey = (MyEnums.pianoKey)tempKey;
            //switch (MyEnums.clickedKey)
            //{
            //    //Only white keys
            //    case MyEnums.pianoKey.c6_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.e4_key;
            //        break;
            //    //////////////////////////////////////////////////
            //    case MyEnums.pianoKey.b5_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.d4_key;
            //        break;
            //    case MyEnums.pianoKey.a5_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.c4_key;
            //        break;
            //    case MyEnums.pianoKey.g5_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.b3_key;
            //        break;
            //    case MyEnums.pianoKey.f5_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.a3_key;
            //        break;
            //    case MyEnums.pianoKey.e5_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.g3_key;
            //        break;
            //    case MyEnums.pianoKey.d5_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.f3_key;
            //        break;
            //    case MyEnums.pianoKey.c5_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.e3_key;
            //        break;
            //    //////////////////////////////////////////////////
            //    case MyEnums.pianoKey.b4_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.d3_key;
            //        break;
            //    case MyEnums.pianoKey.a4_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.c3_key;
            //        break;
            //    case MyEnums.pianoKey.g4_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.b2_key;
            //        break;
            //    case MyEnums.pianoKey.f4_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.a2_key;
            //        break;
            //    case MyEnums.pianoKey.e4_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.g2_key;
            //        break;
            //    case MyEnums.pianoKey.d4_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.f2_key;
            //        break;
            //    case MyEnums.pianoKey.c4_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.e2_key;
            //        break;
            //    //////////////////////////////////////////////////
            //    case MyEnums.pianoKey.b3_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.d2_key;
            //        break;
            //    case MyEnums.pianoKey.a3_key:
            //        MyEnums.clickedKey = MyEnums.pianoKey.c2_key;
            //        break;
            //}

        }
        private void touched(string AudioName, object s, View.TouchEventArgs e)
        {
            if (!disableKeyFlag)
            {
                bool handled = false;

                //Key down
                if (e.Event.Action == MotionEventActions.Down)
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

        public void DisableKey()
        {
            disableKeyFlag = true;
        }
    }
}