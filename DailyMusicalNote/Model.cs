using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMusicalNote
{
    internal enum Notes
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

    internal enum Octaves
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

    enum Clefs
    {
        CLEF_TREBLE,
        CLEF_BASS,
        LAST_ELEMENT
    }

    enum Difficulty
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

    struct RandomNote
    {
        public Notes Note;
        public Octaves Octave;
        public Clefs Clef;
    }

    class Model
    {
        private PriorityQueue<RandomNote, int> _randomNotes = new();
        public RandomNote NextNote => _randomNotes.Dequeue();

        //At this moment changing difficulty isn't implemented.
        private Difficulty _difficulty = Difficulty.EASY;

        /// <summary>
        /// Model class constructor.
        /// </summary>
        public Model()
        {
            Debug.WriteLine("Model() constructor");
        }

        /// <summary>
        /// Generates a PriorityQueue containing notes that can be displayed in the GUI.
        /// </summary>
        /// <param name="notesNumber">Number of notes to generate.</param>
        public void GenerateRandomNotes(int notesNumber)
        {
            Random random = new Random();

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

                        octave = random.Next((int)Octaves.OCTAVE_4, (int)Octaves.OCTAVE_7);
                        clef = (int)Clefs.CLEF_TREBLE;
                        break;

                    case Difficulty.MEDIUM:
                        do
                        {
                            note = random.Next(0, (int)Notes.LAST_ELEMENT);
                        } while (IsSharp((Notes)note));

                        octave = random.Next((int)Octaves.OCTAVE_4, (int)Octaves.OCTAVE_7);
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
    }
}
