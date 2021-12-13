using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DailyMusicalNote
{
    class KeyMechanism : Activity
    {
        private string elementId, AudioFileName;
        public KeyMechanism(string elementId, string AudioFileName)
        {
            this.elementId = elementId;
            this.AudioFileName = AudioFileName;
        }

        public void ClickHander()
        {
            int foo = Resources.GetIdentifier("c5_key", "id", PackageName);
            var pianoKey = FindViewById( foo );

            MediaPlayer player;
            player = new MediaPlayer();
            player.Reset();

            var fileDescriptor = Assets.OpenFd("Sounds/"+AudioFileName+".mp3");

            player.SetDataSource(fileDescriptor.FileDescriptor, fileDescriptor.StartOffset, fileDescriptor.Length);

            pianoKey.Touch += (s, e) =>
            {
                bool handled = false;
                if (e.Event.Action == MotionEventActions.Down)
                {
                    player.Prepare();
                    player.Start();
                    handled = true;
                }
                else if (e.Event.Action == MotionEventActions.Up)
                {
                    player.Stop();
                    handled = true;
                }

                e.Handled = handled;
            };
        }
    }
}