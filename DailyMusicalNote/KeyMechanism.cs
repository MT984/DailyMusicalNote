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
        public KeyMechanism(string elementId, Activity myActivity, Context myContext)
        {
            this.elementId = elementId;
            this.myActivity = myActivity;
            this.myContext = myContext;

            this.AudioFileName = elementId.Substring(0, elementId.LastIndexOf("_"));

            //Big mistake???
            ClickHander();
        }

        private void ClickHander()
        {
            //int foo = myActivity.Resources.GetIdentifier(elementId, "id", myContext.PackageName);
            var pianoKey = myActivity.FindViewById( myActivity.Resources.GetIdentifier(elementId, "id", myContext.PackageName) );

            MediaPlayer player;
            player = new MediaPlayer();
            player.Reset();

            var fileDescriptor = myActivity.Assets.OpenFd("Sounds/"+AudioFileName+".mp3");

            //Wywala jakis dziwny blad po randomowym czasie
            player.SetDataSource(fileDescriptor.FileDescriptor, fileDescriptor.StartOffset, fileDescriptor.Length);

                pianoKey.Touch += (s, e) =>
                {
                    if (!disableKeyFlag)
                    {
                        bool handled = false;

                        //Key down
                        if (e.Event.Action == MotionEventActions.Down)
                        {
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
                            handled = true;
                        }

                        e.Handled = handled;
                    }
                };           
        }

        public void DisableKey()
        {
            disableKeyFlag = true;
        }
    }
}