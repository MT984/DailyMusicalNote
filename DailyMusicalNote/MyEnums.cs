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
    }
}