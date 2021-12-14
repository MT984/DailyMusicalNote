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
    class MyEnums
    {
        //Enum for choosing difficulty
        public enum DifficultyMode : byte
        {
            Easy,
            Medium,
            Hard
        }

        //Initialization of global enum variable
        static public DifficultyMode ChoseDifficulty = DifficultyMode.Easy;

        //Enums describe music keys
        public enum MusicKey : byte
        {
            //Keys description in major
            //Csh -> C sharp
            //Cb - C flat
            //etc.
            //treble: 0 -> 14
            //bass: 15 -> 29
            trebleC,
            trebleG,
            trebleD,
            trebleA,
            trebleE,
            trebleH,
            trebleFsh,
            trebleCsh,
            trebleCb,
            trebleGb,
            trebleDb,
            trebleAb,
            trebleEb,
            trebleB,
            trebleF,
            bassC,
            bassG,
            bassD,
            bassA,
            bassE,
            bassH,
            bassFsh,
            bassCsh,
            bassCb,
            bassGb,
            bassDb,
            bassAb,
            bassEb,
            bassB,
            bassF
        }

        //Initializaton of MusicKey enum variable. Default (easy mode) is key C
        static public MusicKey currentKey = MusicKey.trebleC;

        //Enum describe current clef - bass or treble
        public enum ClefList : byte
        {
            treble,
            bass
        }

        //Initialization of current clef. Default treble
        static public ClefList currentClef = ClefList.treble;
    }
}