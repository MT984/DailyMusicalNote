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

        //Enums describes music keys
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

        //Enum describes current clef - bass or treble
        public enum ClefList : byte
        {
            treble,
            bass
        }

        //Initialization of current clef. Default treble
        static public ClefList currentClef = ClefList.treble;
        //Helpful variable to handle appropriate piano key. Default treble
        static public ClefList bassOrTreble = ClefList.treble;

        //Enum describes value of piano keys and piano notes
        public enum pianoKey: byte
        {
            c6_key,

            b5_key,
            a5sh_key,
            a5_key,
            g5sh_key,
            g5_key,
            f5sh_key,
            f5_key,
            e5_key,
            d5sh_key,
            d5_key,
            c5sh_key,
            c5_key,

            b4_key,
            a4sh_key,
            a4_key,
            g4sh_key,
            g4_key,
            f4sh_key,
            f4_key,
            e4_key,
            d4sh_key,
            d4_key,
            c4sh_key,
            c4_key,

            b3_key,
            a3sh_key,
            a3_key,

            //Below bass
            g3sh_key, //28
            g3_key,
            f3sh_key,
            f3_key,
            e3_key,
            d3sh_key,
            d3_key,
            c3sh_key,
            c3_key,

            b2_key,
            a2sh_key,
            a2_key,
            g2sh_key,
            g2_key,
            f2sh_key,
            f2_key,
            e2_key,
            d2sh_key,
            d2_key,
            c2sh_key,
            c2_key
        }

        //Initialization of variable which stores clicked key
        public static pianoKey clickedKey = pianoKey.c6_key;

        //Initialization of variable which stores which key is displayed
        public static pianoKey showedKey = pianoKey.c6_key;

        //Initialization of variable which stores which note to hearing is displayed
        public static pianoKey hearingNote = pianoKey.c6_key;

        //Initialization of variable which stores draw by lot note
        public static pianoKey currentRandomNote = pianoKey.c6_key;

        //Initialization of variable which stores draw by lot note
        public static pianoKey currentHearingNote = pianoKey.c6_key;

        //Enum which store which game mode in selected by user
        public enum gameMode : byte
        {
            notePractice,
            hearingPractice
        }
        //Var init
        public static gameMode currentGameMode = gameMode.notePractice;

        //Maybe not enum but static var available in all files <- code neatness

        //Global bools variable
        public static bool isKeyUp = true;

        //History file name
        public static string HistoryFileName
        {
            get { return "history.data"; }
        }

        //Folder name to storage data
        public static string StorageFolderName
        {
            get { return "/appData"; }
        }
    }
}