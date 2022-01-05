using Android.App;
using Android.Widget;
using System;

namespace DailyMusicalNote
{
    class NoteMechanism : Activity
    {
        private ImageView note;
        private Button c4, c5, c6;
        private string noteVal;
        
        public NoteMechanism(ImageView note, string noteVal, Activity myActivity)
        {
            this.note = note;
            this.noteVal = noteVal;
            c4 = myActivity.FindViewById<Button>(Resource.Id.c4_key);
            c5 = myActivity.FindViewById<Button>(Resource.Id.c5_key);
            c6 = myActivity.FindViewById<Button>(Resource.Id.c6_key);
        }

        private void setBassNotes()
        {
            //a3 = 27, last of treble clef
            switch (MyEnums.showedKey)
            {
                //Only white keys
                case MyEnums.pianoKey.c6_key:
                    MyEnums.showedKey = MyEnums.pianoKey.e4_key;
                    break;
                //////////////////////////////////////////////////
                case MyEnums.pianoKey.b5_key:
                    MyEnums.showedKey = MyEnums.pianoKey.d4_key;
                    break;
                case MyEnums.pianoKey.a5_key:
                    MyEnums.showedKey = MyEnums.pianoKey.c4_key;
                    break;
                case MyEnums.pianoKey.g5_key:
                    MyEnums.showedKey = MyEnums.pianoKey.b3_key;
                    break;
                case MyEnums.pianoKey.f5_key:
                    MyEnums.showedKey = MyEnums.pianoKey.a3_key;
                    break;
                case MyEnums.pianoKey.e5_key:
                    MyEnums.showedKey = MyEnums.pianoKey.g3_key;
                    break;
                case MyEnums.pianoKey.d5_key:
                    MyEnums.showedKey = MyEnums.pianoKey.f3_key;
                    break;
                case MyEnums.pianoKey.c5_key:
                    MyEnums.showedKey = MyEnums.pianoKey.e3_key;
                    break;
                //////////////////////////////////////////////////
                case MyEnums.pianoKey.b4_key:
                    MyEnums.showedKey = MyEnums.pianoKey.d3_key;
                    break;
                case MyEnums.pianoKey.a4_key:
                    MyEnums.showedKey = MyEnums.pianoKey.c3_key;
                    break;
                case MyEnums.pianoKey.g4_key:
                    MyEnums.showedKey = MyEnums.pianoKey.b2_key;
                    break;
                case MyEnums.pianoKey.f4_key:
                    MyEnums.showedKey = MyEnums.pianoKey.a2_key;
                    break;
                case MyEnums.pianoKey.e4_key:
                    MyEnums.showedKey = MyEnums.pianoKey.g2_key;
                    break;
                case MyEnums.pianoKey.d4_key:
                    MyEnums.showedKey = MyEnums.pianoKey.f2_key;
                    break;
                case MyEnums.pianoKey.c4_key:
                    MyEnums.showedKey = MyEnums.pianoKey.e2_key;
                    break;
                //////////////////////////////////////////////////
                case MyEnums.pianoKey.b3_key:
                    MyEnums.showedKey = MyEnums.pianoKey.d2_key;
                    break;
                case MyEnums.pianoKey.a3_key:
                    MyEnums.showedKey = MyEnums.pianoKey.c2_key;
                    break;
            }
        }

        public void SetVisibility()
        {
            note.Visibility = Android.Views.ViewStates.Visible;
            MyEnums.showedKey = (MyEnums.pianoKey)Enum.Parse(typeof(MyEnums.pianoKey), noteVal);

            if (MyEnums.currentClef == MyEnums.ClefList.treble)
            {
                c4.Text = "C4";
                c5.Text = "C5";
                c6.Text = "C6";
            }
            else if (MyEnums.currentClef == MyEnums.ClefList.bass)
            {
                setBassNotes();
                c4.Text = "C2";
                c5.Text = "C3";
                c6.Text = "C4";
            }
        }

        public void DelVisibility()
        {
            note.Visibility = Android.Views.ViewStates.Gone;
        }
    }
}