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
    class NoteMechanism : Activity
    {
        private ImageView note;
        private string noteVal;
        public NoteMechanism(ImageView note, string noteVal)
        {
            this.note = note;
            this.noteVal = noteVal;
        }

        private void SetCurrentNote()
        {
            MyEnums.showedKey = (MyEnums.pianoKey)Enum.Parse(typeof(MyEnums.pianoKey), noteVal);
        }
        public void SetVisibility()
        {
            note.Visibility = Android.Views.ViewStates.Visible;
            SetCurrentNote();
        }

        public void DelVisibility()
        {
            note.Visibility = Android.Views.ViewStates.Gone;
        }
    }
}