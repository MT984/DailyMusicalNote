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
        private Activity mActivity;
        private Context context;
        public KeyMechanism(string elementId, string AudioFileName, Activity mActivity, Context context)
        {
            this.elementId = elementId;
            this.AudioFileName = AudioFileName;
            this.mActivity = mActivity;
            this.context = context;
        }

        public void ClickHander()
        {
            int foo = mActivity.Resources.GetIdentifier(elementId, "id", context.PackageName);
            var pianoKey = mActivity.FindViewById( foo );

            MediaPlayer player;
            player = new MediaPlayer();
            player.Reset();

            //var fileDescriptor = Assets.OpenFd("Sounds/"+AudioFileName+".mp3");
            var fileDescriptor = mActivity.Assets.OpenFd("Sounds/c5.mp3");

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