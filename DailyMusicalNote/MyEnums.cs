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
            C,
            G,
            D,
            A,
            E,
            H,
            Fsh,
            Csh,
            Cb,
            Gb,
            Db,
            Ab,
            Eb,
            B,
            F
        }

        //Initializaton of MusicKey enum variable. Default (easy mode) is key C
        static public MusicKey currentKey = MusicKey.C;
    }
}