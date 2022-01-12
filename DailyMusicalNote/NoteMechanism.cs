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

        private void setMusicKey()
        {
            switch(MyEnums.currentKey)
            {
                //////////////////////////////////////////TREBLE
                case MyEnums.MusicKey.trebleC:
                case MyEnums.MusicKey.bassC:
                    break;

                case MyEnums.MusicKey.trebleG:
                case MyEnums.MusicKey.bassG:
                    switch(MyEnums.showedKey)
                    {
                        //fsh
                        case MyEnums.pianoKey.f2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.f3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.f4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.f5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleD:
                case MyEnums.MusicKey.bassD:
                    switch (MyEnums.showedKey)
                    {
                        //fsh
                        case MyEnums.pianoKey.f2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.f3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.f4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.f5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //csh
                        case MyEnums.pianoKey.c2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.c3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.c4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.c5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleA:
                case MyEnums.MusicKey.bassA:
                    switch (MyEnums.showedKey)
                    {
                        //fsh
                        case MyEnums.pianoKey.f2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.f3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.f4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.f5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //csh
                        case MyEnums.pianoKey.c2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.c3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.c4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.c5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //gsh
                        case MyEnums.pianoKey.g2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.g3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.g4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.g5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleE:
                case MyEnums.MusicKey.bassE:
                    switch (MyEnums.showedKey)
                    {
                        //fsh
                        case MyEnums.pianoKey.f2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.f3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.f4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.f5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //csh
                        case MyEnums.pianoKey.c2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.c3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.c4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.c5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //gsh
                        case MyEnums.pianoKey.g2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.g3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.g4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.g5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //dsh
                        case MyEnums.pianoKey.d2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.d3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.d4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.d5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleH:
                case MyEnums.MusicKey.bassH:
                    switch (MyEnums.showedKey)
                    {
                        //fsh
                        case MyEnums.pianoKey.f2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.f3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.f4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.f5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //csh
                        case MyEnums.pianoKey.c2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.c3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.c4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.c5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //gsh
                        case MyEnums.pianoKey.g2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.g3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.g4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.g5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //dsh
                        case MyEnums.pianoKey.d2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.d3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.d4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.d5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //ash
                        case MyEnums.pianoKey.a2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.a3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.a4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.a5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleFsh:
                case MyEnums.MusicKey.bassFsh:
                    switch (MyEnums.showedKey)
                    {
                        //fsh
                        case MyEnums.pianoKey.f2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.f3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.f4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.f5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //csh
                        case MyEnums.pianoKey.c2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.c3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.c4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.c5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //gsh
                        case MyEnums.pianoKey.g2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.g3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.g4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.g5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //dsh
                        case MyEnums.pianoKey.d2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.d3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.d4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.d5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //ash
                        case MyEnums.pianoKey.a2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.a3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.a4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.a5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                        //esh
                        case MyEnums.pianoKey.e2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2_key;
                            break;
                        case MyEnums.pianoKey.e3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3_key;
                            break;
                        case MyEnums.pianoKey.e4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4_key;
                            break;
                        case MyEnums.pianoKey.e5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleCsh:
                case MyEnums.MusicKey.bassCsh:
                    switch (MyEnums.showedKey)
                    {
                        //fsh
                        case MyEnums.pianoKey.f2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.f3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.f4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.f5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //csh
                        case MyEnums.pianoKey.c2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.c3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.c4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.c5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //gsh
                        case MyEnums.pianoKey.g2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.g3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.g4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.g5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //dsh
                        case MyEnums.pianoKey.d2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.d3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.d4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.d5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //ash
                        case MyEnums.pianoKey.a2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.a3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.a4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.a5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                        //esh
                        case MyEnums.pianoKey.e2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2_key;
                            break;
                        case MyEnums.pianoKey.e3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3_key;
                            break;
                        case MyEnums.pianoKey.e4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4_key;
                            break;
                        case MyEnums.pianoKey.e5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5_key;
                            break;
                        //bsh
                        case MyEnums.pianoKey.b2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3_key;
                            break;
                        case MyEnums.pianoKey.b3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4_key;
                            break;
                        case MyEnums.pianoKey.b4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5_key;
                            break;
                        case MyEnums.pianoKey.b5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c6_key;
                            break;
                    }
                    break;
            ////////////////////////////////////////////////////   /\OK
                case MyEnums.MusicKey.trebleCb:
                case MyEnums.MusicKey.bassCb:
                    switch (MyEnums.showedKey)
                    {
                        //fb
                        case MyEnums.pianoKey.f2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.e2_key;
                            break;
                        case MyEnums.pianoKey.f3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.e3_key;
                            break;
                        case MyEnums.pianoKey.f4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.e4_key;
                            break;
                        case MyEnums.pianoKey.f5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.e5_key;
                            break;
                        //cb
                        case MyEnums.pianoKey.c3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.b2_key;
                            break;
                        case MyEnums.pianoKey.c4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.b3_key;
                            break;
                        case MyEnums.pianoKey.c5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.b4_key;
                            break;
                        case MyEnums.pianoKey.c6_key:
                            MyEnums.showedKey = MyEnums.pianoKey.b5_key;
                            break;
                        //gb
                        case MyEnums.pianoKey.g2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.g3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.g4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.g5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //db
                        case MyEnums.pianoKey.d2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.d3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.d4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.d5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //ab
                        case MyEnums.pianoKey.a2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.a3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.a4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.a5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //eb
                        case MyEnums.pianoKey.e2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.e3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.e4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.e5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //bb
                        case MyEnums.pianoKey.b2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.b3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.b4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.b5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleGb:
                case MyEnums.MusicKey.bassGb:
                    switch (MyEnums.showedKey)
                    {
                        //cb
                        case MyEnums.pianoKey.c3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.b2_key;
                            break;
                        case MyEnums.pianoKey.c4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.b3_key;
                            break;
                        case MyEnums.pianoKey.c5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.b4_key;
                            break;
                        case MyEnums.pianoKey.c6_key:
                            MyEnums.showedKey = MyEnums.pianoKey.b5_key;
                            break;
                        //gb
                        case MyEnums.pianoKey.g2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.g3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.g4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.g5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //db
                        case MyEnums.pianoKey.d2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.d3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.d4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.d5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //ab
                        case MyEnums.pianoKey.a2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.a3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.a4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.a5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //eb
                        case MyEnums.pianoKey.e2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.e3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.e4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.e5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //bb
                        case MyEnums.pianoKey.b2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.b3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.b4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.b5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleDb:
                case MyEnums.MusicKey.bassDb:
                    switch (MyEnums.showedKey)
                    {
                        //gb
                        case MyEnums.pianoKey.g2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f2sh_key;
                            break;
                        case MyEnums.pianoKey.g3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f3sh_key;
                            break;
                        case MyEnums.pianoKey.g4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f4sh_key;
                            break;
                        case MyEnums.pianoKey.g5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.f5sh_key;
                            break;
                        //db
                        case MyEnums.pianoKey.d2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.d3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.d4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.d5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //ab
                        case MyEnums.pianoKey.a2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.a3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.a4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.a5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //eb
                        case MyEnums.pianoKey.e2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.e3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.e4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.e5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //bb
                        case MyEnums.pianoKey.b2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.b3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.b4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.b5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleAb:
                case MyEnums.MusicKey.bassAb:
                    switch (MyEnums.showedKey)
                    {
                        //db
                        case MyEnums.pianoKey.d2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c2sh_key;
                            break;
                        case MyEnums.pianoKey.d3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c3sh_key;
                            break;
                        case MyEnums.pianoKey.d4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c4sh_key;
                            break;
                        case MyEnums.pianoKey.d5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.c5sh_key;
                            break;
                        //ab
                        case MyEnums.pianoKey.a2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.a3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.a4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.a5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //eb
                        case MyEnums.pianoKey.e2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.e3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.e4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.e5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //bb
                        case MyEnums.pianoKey.b2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.b3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.b4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.b5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleEb:
                case MyEnums.MusicKey.bassEb:
                    switch (MyEnums.showedKey)
                    {
                        //ab
                        case MyEnums.pianoKey.a2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g2sh_key;
                            break;
                        case MyEnums.pianoKey.a3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g3sh_key;
                            break;
                        case MyEnums.pianoKey.a4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g4sh_key;
                            break;
                        case MyEnums.pianoKey.a5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.g5sh_key;
                            break;
                        //eb
                        case MyEnums.pianoKey.e2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.e3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.e4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.e5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //bb
                        case MyEnums.pianoKey.b2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.b3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.b4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.b5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleB:
                case MyEnums.MusicKey.bassB:
                    switch (MyEnums.showedKey)
                    {
                        //eb
                        case MyEnums.pianoKey.e2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d2sh_key;
                            break;
                        case MyEnums.pianoKey.e3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d3sh_key;
                            break;
                        case MyEnums.pianoKey.e4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d4sh_key;
                            break;
                        case MyEnums.pianoKey.e5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.d5sh_key;
                            break;
                        //bb
                        case MyEnums.pianoKey.b2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.b3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.b4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.b5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                    }
                    break;

                case MyEnums.MusicKey.trebleF:
                case MyEnums.MusicKey.bassF:
                    switch (MyEnums.showedKey)
                    {
                        //bb
                        case MyEnums.pianoKey.b2_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a2sh_key;
                            break;
                        case MyEnums.pianoKey.b3_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a3sh_key;
                            break;
                        case MyEnums.pianoKey.b4_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a4sh_key;
                            break;
                        case MyEnums.pianoKey.b5_key:
                            MyEnums.showedKey = MyEnums.pianoKey.a5sh_key;
                            break;
                    }
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

            if(MyEnums.ChoseDifficulty == MyEnums.DifficultyMode.Hard)
            {
                setMusicKey();
            }
        }
        public void DelVisibility()
        {
            note.Visibility = Android.Views.ViewStates.Gone;
        }
    }
}