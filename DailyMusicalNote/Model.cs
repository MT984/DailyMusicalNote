using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;

namespace DailyMusicalNote
{
    public enum Notes
    {
        NOTE_C,
        NOTE_CSH,
        NOTE_D,
        NOTE_DSH,
        NOTE_E,
        NOTE_F,
        NOTE_FSH,
        NOTE_G,
        NOTE_GSH,
        NOTE_A,
        NOTE_ASH,
        NOTE_B,
        LAST_ELEMENT
    }

    public enum Octaves
    {
        OCTAVE_0,
        OCTAVE_1,
        OCTAVE_2,
        OCTAVE_3,
        OCTAVE_4,
        OCTAVE_5,
        OCTAVE_6,
        OCTAVE_7,
        OCTAVE_8,
        LAST_ELEMENT
    }

    public enum Clefs
    {
        CLEF_TREBLE,
        CLEF_BASS,
        LAST_ELEMENT
    }

    public enum Difficulty
    {
        /// <summary>
        /// Display only treble clef, no sharped notes.
        /// </summary>
        EASY,
        /// <summary>
        /// Display treble or bass clef, no sharped notes.
        /// </summary>
        MEDIUM,
        /// <summary>
        /// Display treble or bass clefs and musical keys (e.g., A major).
        /// </summary>
        HARD
    }

    public struct RandomNote
    {
        public Notes Note;
        public Octaves Octave;
        public Clefs Clef;
    }

    class Model
    {
        private Difficulty _difficulty = Difficulty.EASY; //Easy is default mode.
        private PriorityQueue<RandomNote, int> _randomNotes = new();
        private RandomNote _currentlyDisplayingNote;
        private Stopwatch _stopwatch = new();
        private string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "save.db");
        public RandomNote NextNote
        {
            get
            {
                if (_randomNotes.Count > 0)
                {
                    _currentlyDisplayingNote = _randomNotes.Dequeue();
                }
                else
                {
                    //If _randomNotes is empty then return note
                    //with LAST_ELEMENTS values.
                    RandomNote buff = new RandomNote();
                    buff.Note = Notes.LAST_ELEMENT;
                    buff.Octave = Octaves.LAST_ELEMENT;
                    buff.Clef = Clefs.LAST_ELEMENT;

                    _currentlyDisplayingNote = buff;
                }

                return _currentlyDisplayingNote;
            }
        }
        public RandomNote CurrentlyDisplayingNote => _currentlyDisplayingNote;
        public TimeSpan Elapsed => _stopwatch.Elapsed;
        public Difficulty Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; }
        }
        /// <summary>
        /// Model class constructor.
        /// </summary>
        public Model()
        {
            Debug.WriteLine("Model() constructor");
        }

        /// <summary>
        /// Generates notes to play and starts the timer.
        /// </summary>
        /// <param name="notesNumber">Number of notes to generate.</param>
        public void StartGame(int notesNumber)
        {
            GenerateRandomNotes(notesNumber);
            StartTimer();
        }

        /// <summary>
        /// Resets and starts the stopwatch.
        /// </summary>
        private void StartTimer()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        private void StopTimer()
        {
            _stopwatch.Stop();
        }

        /// <summary>
        /// Generates a PriorityQueue containing notes that can be displayed in the GUI.
        /// </summary>
        /// <param name="notesNumber">Number of notes to generate.</param>
        private void GenerateRandomNotes(int notesNumber)
        {
            Random random = new Random();
            _randomNotes.Clear();

            for (int i = 0; i < notesNumber; i++)
            {
                int note = 0;
                int octave = 0;
                int clef = 0;

                switch (_difficulty)
                {
                    default:
                    case Difficulty.EASY:
                        do
                        {
                            note = random.Next(0, (int)Notes.LAST_ELEMENT);
                        } while (IsSharp((Notes)note));

                        octave = random.Next((int)Octaves.OCTAVE_4, (int)Octaves.OCTAVE_6);
                        clef = (int)Clefs.CLEF_TREBLE;
                        break;

                    case Difficulty.MEDIUM:
                        do
                        {
                            note = random.Next(0, (int)Notes.LAST_ELEMENT);
                        } while (IsSharp((Notes)note));

                        octave = random.Next((int)Octaves.OCTAVE_4, (int)Octaves.OCTAVE_6);
                        clef = random.Next(0, (int)Clefs.LAST_ELEMENT);
                        break;

                    case Difficulty.HARD:
                        //TODO Implement hard difficulty.
                        break;
                }

                RandomNote buff = new RandomNote();
                buff.Note = (Notes)note;
                buff.Octave = (Octaves)octave;
                buff.Clef = (Clefs)clef;

                Debug.WriteLine($"Picked {i} note:" +
                    $"{buff.Note}, {buff.Octave}, {buff.Clef}");

                //A lower number means a higher priority. Default=100
                _randomNotes.Enqueue(buff, 100);
            }
        }

        /// <summary>
        /// Checks if the picked note is equal to the clicked note.
        /// </summary>
        /// <param name="clickedKey">The key that was clicked.</param>
        /// <returns>
        /// True if the clicked key and the picked
        /// key are equal. Otherwise false.
        /// </returns>
        public bool CheckResult(Key clickedKey)
        {
            return (clickedKey.Note == _currentlyDisplayingNote.Note &&
                    clickedKey.Octave == _currentlyDisplayingNote.Octave);
        }

        /// <summary>
        /// Checks whether the note is sharp.
        /// </summary>
        /// <param name="note">Note to check.</param>
        /// <returns>Returns true if the note is sharp. Otherwise, false.</returns>
        private bool IsSharp(Notes note)
        {
            return (new[] {
                Notes.NOTE_CSH,
                Notes.NOTE_DSH,
                Notes.NOTE_FSH,
                Notes.NOTE_GSH,
                Notes.NOTE_ASH
            }.Contains(note));
        }

        /// <summary>
        /// Stops the timers, calculates a score and saves the score to the file.
        /// </summary>
        /// <param name="correctAnswers">
        /// The number of correct answers
        /// (equals to number of notes in thr game).
        /// </param>
        /// <param name="incorrectAnswers">The number of incorrect answers.</param>
        /// <param name="accuracyPercent">The accuracy result, expressed as a percentage.</param>
        /// <returns>
        /// The number of points a player has earned.
        /// Calculated based on the number of misclicks and gameplay time.
        /// </returns>
        public int GameOver(int correctAnswers, int incorrectAnswers, int accuracyPercent)
        {
            StopTimer();
            double seconds = _stopwatch.Elapsed.TotalSeconds;

            //correctAnswers = numbers of notes in game
            double score = (incorrectAnswers + correctAnswers) / correctAnswers;
            score *= seconds;

            //Run task to avoid GUI freeze.
            Task task = Task.Run(() =>
            {
                var save = new Save
                {
                    dateTime = DateTime.Now,
                    score = (int)score,
                    accuracy = accuracyPercent,
                    gameplayTime = _stopwatch.Elapsed.ToString(@"mm\:ss"),
                    noteCounter = correctAnswers,
                    difficulty = Difficulty.EASY
                };

                _ = SaveResultAsync(save);
            });

            return (int)score;
        }

        /// <summary>
        /// Saves result to a file.
        /// </summary>
        /// <param name="save">The <see cref="Save"/>
        /// object to be written to the file.</param>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        private async Task SaveResultAsync(Save save)
        {
            try
            {
                using var db = new AppDbContext();
                await db.Database.EnsureCreatedAsync();

                db.Save.Add(save);
                _ = db.SaveChangesAsync();

                Debug.WriteLine("The result has been saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("The result is not saved: "+ex.Message);

                await Application.Current.MainPage.DisplayAlert(
                    Resources.Lang.langResources.labelSaveErrorTitle,
                    Resources.Lang.langResources.labelSaveErrorContent+ex.Message, "OK");
            }
        }

        /// <summary>
        /// Gets the list of <see cref="Save"/> objects read from the file.
        /// </summary>
        /// <returns>The list of <see cref="Save"/>
        /// objects that was read from the file.</returns>
        public List<Save> GetSavedHistory()
        {   
            using var db = new AppDbContext();

            try
            {
                var save = db.Save.ToList();
                return save;
            }
            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                return [];
            }
        }

        /// <summary>
        /// Deletes the all save elements (clears the history of results).
        /// </summary>
        public void ClearHistory()
        {
            try
            {
                using var db = new AppDbContext();
                var allSaves = db.Save.ToList();
                db.Save.RemoveRange(allSaves);
                _ = db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
