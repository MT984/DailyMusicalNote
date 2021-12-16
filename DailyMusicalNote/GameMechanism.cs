using Android.App;
using Android.Content;
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
    public class GameMechanism : GameActivity
    {
        private readonly Activity myActivity;
        private readonly Context myContext;
        private ImageView trebleClef, bassClef;
        private ImageView[] musicKeys, keys, pianoKeys;
        private TextView topBarNotesLeftValue;

        //Variable to count how many notes on the sheet has been shown
        private byte noteCounter;

        public GameMechanism(Activity myActivity, Context myContext)
        {
            this.myActivity = myActivity;
            this.myContext = myContext;
            noteCounter = 1;
        }

        public void CreateMechanism()
        {
            //Finding elements by id
            trebleClef = myActivity.FindViewById<ImageView>(Resource.Id.trebleClef);
            bassClef = myActivity.FindViewById<ImageView>(Resource.Id.bassClef);
            topBarNotesLeftValue = myActivity.FindViewById<TextView>(Resource.Id.topBar_notesLeft_value);

            ImageView[] tempMusicKeys =
           {
                trebleClef,
                myActivity.FindViewById<ImageView>(Resource.Id.trebleG),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleD),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleA),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleE),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleH),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleFsh),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleCsh),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleCb),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleGb),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleDb),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleAb),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleEb),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleB),
                myActivity.FindViewById<ImageView>(Resource.Id.trebleF),

                bassClef,
                myActivity.FindViewById<ImageView>(Resource.Id.bassG),
                myActivity.FindViewById<ImageView>(Resource.Id.bassD),
                myActivity.FindViewById<ImageView>(Resource.Id.bassA),
                myActivity.FindViewById<ImageView>(Resource.Id.bassE),
                myActivity.FindViewById<ImageView>(Resource.Id.bassH),
                myActivity.FindViewById<ImageView>(Resource.Id.bassFsh),
                myActivity.FindViewById<ImageView>(Resource.Id.bassCsh),
                myActivity.FindViewById<ImageView>(Resource.Id.bassCb),
                myActivity.FindViewById<ImageView>(Resource.Id.bassGb),
                myActivity.FindViewById<ImageView>(Resource.Id.bassDb),
                myActivity.FindViewById<ImageView>(Resource.Id.bassAb),
                myActivity.FindViewById<ImageView>(Resource.Id.bassEb),
                myActivity.FindViewById<ImageView>(Resource.Id.bassB),
                myActivity.FindViewById<ImageView>(Resource.Id.bassF),
            };
            musicKeys = tempMusicKeys;

            ImageView[] tempKeys =
            {
                 myActivity.FindViewById<ImageView>(Resource.Id.a2),
                 myActivity.FindViewById<ImageView>(Resource.Id.b2),
                 myActivity.FindViewById<ImageView>(Resource.Id.c3),
                 myActivity.FindViewById<ImageView>(Resource.Id.d3),
                 myActivity.FindViewById<ImageView>(Resource.Id.e3),
                 myActivity.FindViewById<ImageView>(Resource.Id.f3),
                 myActivity.FindViewById<ImageView>(Resource.Id.g3),
                 myActivity.FindViewById<ImageView>(Resource.Id.a3),
                 myActivity.FindViewById<ImageView>(Resource.Id.b3),
                 myActivity.FindViewById<ImageView>(Resource.Id.c4),
                 myActivity.FindViewById<ImageView>(Resource.Id.d4),
                 myActivity.FindViewById<ImageView>(Resource.Id.e4),
                 myActivity.FindViewById<ImageView>(Resource.Id.f4),
                 myActivity.FindViewById<ImageView>(Resource.Id.g4),
                 myActivity.FindViewById<ImageView>(Resource.Id.a4),
                 myActivity.FindViewById<ImageView>(Resource.Id.b4),
                 myActivity.FindViewById<ImageView>(Resource.Id.c5)
            };
            keys = tempKeys;

            KeyMechanism[] tempPianoKeys =
            {
                //Order like in real keyboard, not alphabetically
                 new KeyMechanism("c6_key", myActivity, myContext),

                 new KeyMechanism("b5_key", myActivity, myContext),
                 new KeyMechanism("a5sh_key", myActivity, myContext),
                 new KeyMechanism("a5_key", myActivity, myContext),
                 new KeyMechanism("g5sh_key", myActivity, myContext),
                 new KeyMechanism("g5_key", myActivity, myContext),
                 new KeyMechanism("f5sh_key", myActivity, myContext),
                 new KeyMechanism("f5_key", myActivity, myContext),
                 new KeyMechanism("e5_key", myActivity, myContext),
                 new KeyMechanism("d5sh_key", myActivity, myContext),
                 new KeyMechanism("d5_key", myActivity, myContext),
                 new KeyMechanism("c5sh_key", myActivity, myContext),
                 new KeyMechanism("c5_key", myActivity, myContext),

                 new KeyMechanism("b4_key", myActivity, myContext),
                 new KeyMechanism("a4sh_key", myActivity, myContext),
                 new KeyMechanism("a4_key", myActivity, myContext),
                 new KeyMechanism("g4sh_key", myActivity, myContext),
                 new KeyMechanism("g4_key", myActivity, myContext),
                 new KeyMechanism("f4sh_key", myActivity, myContext),
                 new KeyMechanism("f4_key", myActivity, myContext),
                 new KeyMechanism("e4_key", myActivity, myContext),
                 new KeyMechanism("d4sh_key", myActivity, myContext),
                 new KeyMechanism("d4_key", myActivity, myContext),
                 new KeyMechanism("c4sh_key", myActivity, myContext),
                 new KeyMechanism("c4_key", myActivity, myContext),

                 new KeyMechanism("b3_key", myActivity, myContext),
                 new KeyMechanism("a3sh_key", myActivity, myContext),
                 new KeyMechanism("a3_key", myActivity, myContext)

                 //Below bass clef - coming soon
                 //new KeyMechanism("g3sh_key", myActivity, myContext),
                 //new KeyMechanism("gsh_key", myActivity, myContext),
                 //new KeyMechanism("f3sh_key", myActivity, myContext),
                 //new KeyMechanism("f3_key", myActivity, myContext),
                 //new KeyMechanism("e3_key", myActivity, myContext),
                 //new KeyMechanism("d3sh_key", myActivity, myContext),
                 //new KeyMechanism("d3_key", myActivity, myContext),
                 //new KeyMechanism("c3sh_key", myActivity, myContext),
                 //new KeyMechanism("c3_key", myActivity, myContext),

                 //new KeyMechanism("b2_key", myActivity, myContext),
                 //new KeyMechanism("a2sh_key", myActivity, myContext),
                 //new KeyMechanism("a2_key", myActivity, myContext),
                 //new KeyMechanism("g2sh_key", myActivity, myContext),
                 //new KeyMechanism("g2_key", myActivity, myContext),
                 //new KeyMechanism("f2sh_key", myActivity, myContext),
                 //new KeyMechanism("f2_key", myActivity, myContext),
                 //new KeyMechanism("e2_key", myActivity, myContext),
                 //new KeyMechanism("d2sh_key", myActivity, myContext),
                 //new KeyMechanism("d2_key", myActivity, myContext),
                 //new KeyMechanism("c2sh_key", myActivity, myContext),
                 //new KeyMechanism("c2_key", myActivity, myContext)
            };
            pianoKeys = tempKeys;
        }
        public void NextNote()
        {
            Random randomNumbers = new Random();
            if (noteCounter <= 50)
            {
                //Hide all unnecessary elements
                foreach (ImageView im in musicKeys)
                {
                    im.Visibility = Android.Views.ViewStates.Gone;
                }

                foreach (ImageView im in keys)
                {
                    im.Visibility = Android.Views.ViewStates.Gone;
                }

                //Show random elements. It is ependence of chosen dificulty
                switch (MyEnums.ChoseDifficulty)
                {
                    //Easy - Only treble clef
                    case MyEnums.DifficultyMode.Easy:
                        trebleClef.Visibility = Android.Views.ViewStates.Visible;
                        break;

                    //Medium - Treble and bass clef
                    case MyEnums.DifficultyMode.Medium:

                        switch (randomNumbers.Next(0, 2))
                        {
                            case 0:
                                trebleClef.Visibility = Android.Views.ViewStates.Visible;
                                MyEnums.currentClef = MyEnums.ClefList.treble;
                                break;

                            case 1:
                                bassClef.Visibility = Android.Views.ViewStates.Visible;
                                MyEnums.currentClef = MyEnums.ClefList.bass;
                                break;
                        }

                        break;

                    //Hard - All clefs and all music keys
                    case MyEnums.DifficultyMode.Hard:
                        int randomVal = randomNumbers.Next(0, musicKeys.Length);
                        musicKeys[randomVal].Visibility = Android.Views.ViewStates.Visible;
                        MyEnums.currentClef = randomVal > 14 ? MyEnums.ClefList.bass : MyEnums.ClefList.treble;
                        break;
                }

                //enuma dodac
                int keyIndex = randomNumbers.Next(0, keys.Length);
                keys[keyIndex].Visibility = Android.Views.ViewStates.Visible;

                //ifa dodac na wyglad
                topBarNotesLeftValue.Text = noteCounter + "/50";

                noteCounter++;
            }
            else
            {
                topBarNotesLeftValue.Text = "KONIEC";
                //testButton.Click += (s, e) => Finish();
            }
        }
    }
}