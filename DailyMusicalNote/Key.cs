using System.Diagnostics;

namespace DailyMusicalNote
{
    internal class Key : Button
    {
        private readonly Octaves _octave;
        private readonly Notes _note;
        private Color _colorBuffer = Color.FromArgb("000");
        public Notes Note => _note;
        public Octaves Octave => _octave;

        /// <summary>
        /// Initializes a new instance of the Key class.
        /// Inherits from the Button class.
        /// </summary>
        /// <param name="note">The note value of the key (e.g., C, D, etc.).</param>
        /// <param name="octave">The octave of the key (e.g., 1, 2, etc.).</param>
        public Key(Notes note, Octaves octave) : base()
        {
            _octave = octave;
            _note = note;

            Debug.WriteLine($"Key() constructor. Note: {note.ToString()}, " +
                            $"octave: {octave.ToString()}");

            SetButtonStyle();

            Pressed += OnPressed;
            Released += OnReleased;
        }

        /// <summary>
        /// Handles the OnPressed event.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPressed(object? sender, EventArgs e)
        {
            _colorBuffer = BackgroundColor;
            BackgroundColor = IsSharpedKey() ?
                (Color)Application.Current.Resources["KeySharpedPressed"] :
                (Color)Application.Current.Resources["KeyPressed"];
        }

        /// <summary>
        /// Handles the OnReleased event.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnReleased(object? sender, EventArgs e)
        {
            BackgroundColor = _colorBuffer;
        }

        /// <summary>
        /// Sets style of the button.
        /// </summary>
        private void SetButtonStyle()
        {
            Padding = new Thickness(0, 130, 0, 0);
            CornerRadius = 0;

            //Set text on C notes to inform about octeaves.
            if (_note == Notes.NOTE_C)
            {
                Text = "C" + (int)_octave;
            }
            else
            {
                Text = "";
            }
        }

        /// <summary>
        /// Checks the key's sharpness. Returns true if the key is sharp.
        /// </summary>
        /// <returns>True if the key (assigned note) is sharp. Otherwise, false.</returns>
        public bool IsSharpedKey()
        {
            return (new[] {
                Notes.NOTE_CSH,
                Notes.NOTE_DSH,
                Notes.NOTE_FSH,
                Notes.NOTE_GSH,
                Notes.NOTE_ASH
            }.Contains(_note));
        }
    }
}
