using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Diagnostics;
#if ANDROID
using Android.Content.PM;
using Android.App;
#endif

namespace DailyMusicalNote.Views;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using static System.Net.Mime.MediaTypeNames;

//TODO clean code
public partial class GameView : ContentPage
{
    public event EventHandler KeyClicked;
    private RandomNote _currentlyShowingNote;

    List<(Notes, Octaves, double)> _ratios = new();
    const double RATIO = 8.7 / (19440.0 / 99.0); //Ratio of y-position.
    const int KEYBOARD_SIZE = 25; //Size in keys (white and black).

    /// <summary>
    /// GameView() constructor. Initializes a keyboard layout.
    /// </summary>
    public GameView()
	{
		InitializeComponent();
        GenerateKeyboard();
    }

    /// <summary>
    /// No description yet. Work in progress.
    /// </summary>
    /// <param name="note">Note that should be displayed in the GUI.</param>
    public void ShowNote(RandomNote note)
    {
        _currentlyShowingNote = note;
        Debug.WriteLine($"Current note: {note.Note}, {note.Octave}");
        //TODO display RandomNote in GUI.

        Staff.Dispatcher.Dispatch(async () =>
        {
            await Task.Delay(100);

            var result = _ratios.FirstOrDefault(x =>
                                             x.Item1 == note.Note &&
                                             x.Item2 == note.Octave);

            if (result != default)
            {
                double ratio = (double)result.Item3;
                TopStaff.IsVisible = ratio >= 7 * RATIO ? true : false;
                DownStaff.IsVisible = ratio <= -5 * RATIO ? true : false;

                if (ratio > 0)
                {
                    NoteImage.Rotation = 180;
                    ratio -= 2 * RATIO;
                }
                else
                {
                    NoteImage.Rotation = 0;
                }
                NoteImage.TranslationY = (double)Staff.Height * -ratio;
            }
        });
    }

    /// <summary>
    /// OnAppearing() is invoked when the GameView becomes visible.
    /// In this case, the screen rotates to landscape
    /// orientation when the page becomes visible.
    /// </summary>
    protected override void OnAppearing()
    {
        Debug.WriteLine("GameView OnAppearing()");
        base.OnAppearing();
#if ANDROID
        var activity = MainActivity.Instance;
        activity.RequestedOrientation = ScreenOrientation.Landscape;
#endif

        CreateNotePositionList();
    }

    /// <summary>
    /// Here the list with a note's y-position values is created.
    /// </summary>
    private void CreateNotePositionList()
    {
        _ratios.Clear();
        int noteCounter = (int)Notes.NOTE_A - 1;
        int octaveCounter = (int)Octaves.OCTAVE_4;

        //From note A4 (with A4) - up
        for (int i = 0; i < 10; i++)
        {
            Notes note;

            do
            {
                noteCounter++;

                if (noteCounter >= (int)Notes.LAST_ELEMENT)
                {
                    noteCounter = 0;
                    if (octaveCounter >= (int)Octaves.LAST_ELEMENT)
                    {
                        octaveCounter = 0;
                    }
                    else
                    {
                        octaveCounter++;
                    }
                }

                note = (Notes)noteCounter;
            } while (note == Notes.NOTE_CSH ||
                    note == Notes.NOTE_DSH ||
                    note == Notes.NOTE_FSH ||
                    note == Notes.NOTE_GSH ||
                    note == Notes.NOTE_ASH);

            _ratios.Add(((Notes)noteCounter, (Octaves)octaveCounter, RATIO * i));
        }

        noteCounter = (int)Notes.NOTE_A;
        octaveCounter = (int)Octaves.OCTAVE_4;

        //To note A4 (without A4) - down
        for (int i = -1; i >= -7; i--)
        {
            Notes note;

            do
            {
                noteCounter--;

                if (noteCounter < 0)
                {
                    noteCounter = (int)Notes.LAST_ELEMENT - 1;
                    octaveCounter--;

                    if (octaveCounter < 0)
                    {
                        octaveCounter = (int)Octaves.LAST_ELEMENT - 1;
                    }
                }

                note = (Notes)noteCounter;
            } while (note == Notes.NOTE_CSH ||
                    note == Notes.NOTE_DSH ||
                    note == Notes.NOTE_FSH ||
                    note == Notes.NOTE_GSH ||
                    note == Notes.NOTE_ASH);

            _ratios.Add(((Notes)noteCounter, (Octaves)octaveCounter, RATIO * i));
        }
    }

    /// <summary>
    /// GenerateKeyboard() generates a piano keyboard in the GUI.
    /// </summary>
    private void GenerateKeyboard()
    {
        //Keys to show in GUI.
        Key[] keys = new Key[KEYBOARD_SIZE];
        AbsoluteLayout KeyboardLayout = new();

        //Start values.
        //TODO implement difficulty dependency.
        int startNote = (int)Notes.NOTE_A;
        int startOctave = (int)Octaves.OCTAVE_3;
        double xIndex = 0;

        for (int i = 0; i < keys.Length; i++)
        {
            //Key initialize.
            keys[i] = new Key((Notes)startNote, (Octaves)startOctave);
            keys[i].Clicked += OnKeyClicked;

            //If the last note is NOTE_B, increment the
            //octave and start from NOTE_C.
            startNote++;
            if (startNote == (int)Notes.LAST_ELEMENT)
            {
                startNote = 0;
                startOctave++;
            }

            //Color button based on note sharpness.
            if (keys[i].IsSharpedKey())
            {
                keys[i].Style = (Style)Microsoft.Maui.Controls.
                                 Application.Current.Resources["KeyboardBlackKey"];
            }
            else
            {
                keys[i].Style = (Style)Microsoft.Maui.Controls.
                                 Application.Current.Resources["KeyboardWhiteKey"];

                //Position and add key to layout.
                AbsoluteLayout.SetLayoutBounds(keys[i], new Rect(xIndex, 0, 45, 170));
                AbsoluteLayout.SetLayoutFlags(keys[i], AbsoluteLayoutFlags.None);
                KeyboardLayout.Children.Add(keys[i]);

                //Set index to next note.
                xIndex += 45;
            }
        }

        //Reset index.
        xIndex = -45;
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].Style == (Style)Microsoft.Maui.Controls.
                                  Application.Current.Resources["KeyboardBlackKey"])
            {
                //Position and add key to layout (black keys).
                double xBuff = xIndex + 45 * 0.7; 
                AbsoluteLayout.SetLayoutBounds(keys[i], new Rect(xBuff, 40, 30, 30));
                AbsoluteLayout.SetLayoutFlags(keys[i], AbsoluteLayoutFlags.None);

                KeyboardLayout.Children.Add(keys[i]);
            }
            else
            {
                //Set index to next note.
                xIndex += 45;
            }
        }

        //Add layout to "Keyboard" section.
        Keyboard.Children.Add(KeyboardLayout);
    }

    /// <summary>
    /// Event handler for the Key.Clicked event. 
    /// In this handler, the currently displayed note
    /// and the selected key are sent to the event.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnKeyClicked(object? sender, EventArgs e)
    {
        if (sender is not Key key)
            return;

        KeyClicked?.Invoke(key, new KeyClickedEventArgs(
            _currentlyShowingNote.Note, _currentlyShowingNote.Octave));
    }

}