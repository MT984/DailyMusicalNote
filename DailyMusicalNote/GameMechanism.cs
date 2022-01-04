using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DailyMusicalNote
{
    public class GameMechanism : GameActivity
    {
        private readonly Activity myActivity;
        private readonly Context myContext;
        private ImageView trebleClef, bassClef, x, v;
        private ImageView[] musicKeys, tempKeys;
        private TextView topBarNotesLeftValue, topBarWrongAnswersLeftValue, scoreValue, correctValue;
        private NoteMechanism[] notes;
        private KeyMechanism[] pianoKeys;
        private RelativeLayout endGameLayout;
        private int wrongAnsers = 0, keyIndex = 0;
        private Timer myTimer = new Timer(1000);
        private byte seconds = 0, minutes = 0;
        private const byte NOTES = 5;

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
            topBarWrongAnswersLeftValue = myActivity.FindViewById<TextView>(Resource.Id.topBar_wrongAnswers_value);
            endGameLayout = myActivity.FindViewById<RelativeLayout>(Resource.Id.endGameLayout);
            scoreValue = myActivity.FindViewById<TextView>(Resource.Id.scoreValue);
            correctValue = myActivity.FindViewById<TextView>(Resource.Id.correctValue);
            x = myActivity.FindViewById<ImageView>(Resource.Id.x);
            v = myActivity.FindViewById<ImageView>(Resource.Id.v);

            endGameLayout.Visibility = Android.Views.ViewStates.Gone;

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

            NoteMechanism[] tempNotes =
            {
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.c6), "c6_key"),

                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.c5), "c5_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.d5), "d5_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.e5), "e5_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.f5), "f5_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.g5), "g5_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.a5), "a5_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.b5), "b5_key"),

                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.c4), "c4_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.d4), "d4_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.e4), "e4_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.f4), "f4_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.g4), "g4_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.a4), "a4_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.b4), "b4_key"),

                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.a3), "a3_key"),
                 new NoteMechanism(myActivity.FindViewById<ImageView>(Resource.Id.b3), "b3_key")
            };
            notes = tempNotes;

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
            pianoKeys = tempPianoKeys;
        }

        private async Task incorrectAnimationAsync()
        {
            x.Visibility = Android.Views.ViewStates.Visible;
            await Task.Delay(750);
            x.Visibility = Android.Views.ViewStates.Gone;
        }
        private async Task correctAnimationAsync()
        {
            v.Visibility = Android.Views.ViewStates.Visible;
            await Task.Delay(750);
            v.Visibility = Android.Views.ViewStates.Gone;
        }
        public void NextNote()
        {
            Random randomNumbers = new Random();

            if (MyEnums.showedKey.ToString() != MyEnums.clickedKey.ToString())
            {
                //If values are differnt
                wrongAnsers++;
                topBarWrongAnswersLeftValue.Text = wrongAnsers.ToString();
                _ = incorrectAnimationAsync();
            }
            else if (noteCounter <= NOTES)
            {
                _ = correctAnimationAsync();

                //Hide all unnecessary elements
                foreach (ImageView im in musicKeys)
                {
                    im.Visibility = Android.Views.ViewStates.Gone;
                }

                foreach (NoteMechanism im in notes)
                {
                    im.DelVisibility();
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

                keyIndex = randomNumbers.Next(0, notes.Length);
                notes[keyIndex].SetVisibility();

                //ifa dodac na wyglad
                topBarNotesLeftValue.Text = noteCounter + "/" + NOTES.ToString();

                noteCounter++;
            }
            else
            {
                endGame();
            }
        }

        public void StartTimer()
        {
            seconds = 0;
            minutes = 0;
            TextView timeValueView = myActivity.FindViewById<TextView>(Resource.Id.topBar_timer_value);
            myTimer.AutoReset = true;
            myTimer.Enabled = true;

            myTimer.Elapsed += (s, e) =>
            {
                seconds++;
                RunOnUiThread(() =>
                {
                    string timeString = "00:00", minsString = "00", secString = "00";
                    if (seconds >= 60)
                    {
                        seconds = 0;
                        minutes++;
                    }

                    secString = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
                    minsString = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();

                    timeString = minsString + ":" + secString;

                    timeValueView.Text = timeString;

                });
            };

            myTimer.Start();
        }

        private void endGame()
        {
            myTimer.Stop();

            double score = ((wrongAnsers + NOTES) / NOTES) * ((minutes * 60) + seconds);
            score *= 10;

            double clickAccuracy = (double)NOTES / (wrongAnsers + NOTES);
            clickAccuracy *= 100;
            
            scoreValue.Text = score.ToString();
            correctValue.Text = Math.Round(clickAccuracy).ToString() + "%";

            endGameLayout.Visibility = Android.Views.ViewStates.Visible;

            foreach (KeyMechanism km in pianoKeys)
            {
                km.DisableKey();
            }

            
            var directory = myActivity.FilesDir + MyEnums.StorageFolderName;
            var filePath = directory + "/" + MyEnums.HistoryFileName;

            //Create file to storage history or modify it
            if (!System.IO.File.Exists(filePath))
            {
                var newFile = new Java.IO.File(directory, MyEnums.HistoryFileName);
                using (FileOutputStream outFile = new FileOutputStream(newFile))
                {
                    string dataToWrite = score.ToString() + "\n" + Math.Round(clickAccuracy).ToString();
                    outFile.Write(System.Text.Encoding.ASCII.GetBytes(dataToWrite));
                    outFile.Close();
                }
            }
            else
            {
                var newFile = new Java.IO.File(directory, MyEnums.HistoryFileName);
                using (FileOutputStream outFile = new FileOutputStream(newFile, true))
                {
                    string dataToWrite = "\n" + score.ToString() + "\n" + Math.Round(clickAccuracy).ToString();
                    outFile.Write(System.Text.Encoding.ASCII.GetBytes(dataToWrite));
                    outFile.Close();
                }
            }

            endGameLayout.Click += (s, e) =>
            {
                Finish();
                myActivity.StartActivity(typeof(MainActivity));
            };
        }
    }
}