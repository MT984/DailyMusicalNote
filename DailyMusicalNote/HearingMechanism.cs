using Android.App;
using Android.Content;
using Android.Media;
using Android.Widget;
using Java.IO;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace DailyMusicalNote
{
    public class HearingMechanism : GameActivity
    {
        private readonly Activity myActivity;
        private ImageView x, v;
        private TextView topBarNotesLeftValue, topBarWrongAnswersLeftValue, scoreValue, correctValue;
        private RelativeLayout endGameLayout;
        private Button c4, c5, c6, StartHearingButton;
        private int wrongAnsers = 0, keyIndex = 0;
        private Timer myTimer = new Timer(1000);
        private byte seconds = 0, minutes = 0;
        private const byte NOTES = 2;
        private MediaPlayer player;
        private string AudioName = "";
        public bool isCorrect = false;

        //Variable to count how many notes on the sheet has been shown
        private byte noteCounter;
        public HearingMechanism(Activity myActivity)
        {
            this.myActivity = myActivity;
            player = new MediaPlayer();

            noteCounter = 1;
        }
        public void CreateMechanism()
        {
            //Finding elements by id
            topBarNotesLeftValue = myActivity.FindViewById<TextView>(Resource.Id.topBar_notesLeft_value);
            topBarWrongAnswersLeftValue = myActivity.FindViewById<TextView>(Resource.Id.topBar_wrongAnswers_value);
            endGameLayout = myActivity.FindViewById<RelativeLayout>(Resource.Id.endGameLayout);
            scoreValue = myActivity.FindViewById<TextView>(Resource.Id.scoreValue);
            correctValue = myActivity.FindViewById<TextView>(Resource.Id.correctValue);
            x = myActivity.FindViewById<ImageView>(Resource.Id.x);
            v = myActivity.FindViewById<ImageView>(Resource.Id.v);
            c4 = myActivity.FindViewById<Button>(Resource.Id.c4_key);
            c5 = myActivity.FindViewById<Button>(Resource.Id.c5_key);
            c6 = myActivity.FindViewById<Button>(Resource.Id.c6_key);
            StartHearingButton = myActivity.FindViewById<Button>(Resource.Id.StartHearingButton);

            endGameLayout.Visibility = Android.Views.ViewStates.Gone;
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
        private async Task playingNoteAsync()
        {
            StartHearingButton.Text = myActivity.Resources.GetString(Resource.String.playingHearNote_string);
            StartHearingButton.Clickable = false;
            await Task.Delay(4000);
            StartHearingButton.Text = myActivity.Resources.GetString(Resource.String.hearNote_string);
            StartHearingButton.Clickable = true;
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

            var directory = myActivity.FilesDir + MyEnums.StorageFolderName;
            var filePath = directory + "/" + MyEnums.HistoryFileName;

            //Create file to storage history or modify it
            if (!System.IO.File.Exists(filePath))
            {
                var newFile = new Java.IO.File(directory, MyEnums.HistoryFileName);
                using (FileOutputStream outFile = new FileOutputStream(newFile))
                {
                    string dataToWrite = score.ToString() + "\n" +
                           Math.Round(clickAccuracy).ToString() + "\n" +
                           myActivity.Resources.GetString(Resource.String.history_hearing_string);

                    outFile.Write(System.Text.Encoding.ASCII.GetBytes(dataToWrite));
                    outFile.Close();
                }
            }
            else
            {
                var newFile = new Java.IO.File(directory, MyEnums.HistoryFileName);
                using (FileOutputStream outFile = new FileOutputStream(newFile, true))
                {
                    string dataToWrite = "\n" + score.ToString() + "\n" +
                           Math.Round(clickAccuracy).ToString() + "\n" +
                           myActivity.Resources.GetString(Resource.String.history_hearing_string);

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
        public void nextHearingNote()
        {         
            //Wait until key will be up
            //while (MyEnums.isKeyUp) ;

            Random randomNumbers = new Random();
            keyIndex = randomNumbers.Next(0, 1+(int)MyEnums.pianoKey.c2_key);

            MyEnums.currentClef = keyIndex >= 28 ? MyEnums.ClefList.bass : MyEnums.ClefList.treble;
            MyEnums.currentHearingNote = (MyEnums.pianoKey)keyIndex;

            AudioName = MyEnums.currentHearingNote.ToString().Substring(0, MyEnums.currentHearingNote.ToString().LastIndexOf("_"));

            player.Release();
            player = new MediaPlayer();
            var fileDescriptor = myActivity.Assets.OpenFd("Sounds/" + AudioName + ".mp3");
            player.Reset();
            player.SetDataSource(fileDescriptor.FileDescriptor, fileDescriptor.StartOffset, fileDescriptor.Length);
            player.Prepare();

            if (MyEnums.currentClef == MyEnums.ClefList.treble)
            {
                c4.Text = "C4";
                c5.Text = "C5";
                c6.Text = "C6";
            }
            else if (MyEnums.currentClef == MyEnums.ClefList.bass)
            {
                //setBassNotes();
                c4.Text = "C2";
                c5.Text = "C3";
                c6.Text = "C4";
            }

            //playNote();

            StartHearingButton.Click += (s, e) => playNote();
        }
        public void playNote()
        {
            //Start play note
            player.Start();
            _ = playingNoteAsync();
        }

        public void chceckNote()
        {
            if (MyEnums.clickedKey != MyEnums.currentHearingNote)
            {   
                _ = incorrectAnimationAsync();
                wrongAnsers++;
                topBarWrongAnswersLeftValue.Text = wrongAnsers.ToString();
            }
            else if (noteCounter <= NOTES)
            {
                _ = correctAnimationAsync();

                topBarNotesLeftValue.Text = noteCounter + "/" + NOTES.ToString();
                noteCounter++;

                
                isCorrect = true;
                nextHearingNote();
            }
            else
            {
                endGame();
            }
        }
    }
}