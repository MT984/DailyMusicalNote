using System.Diagnostics;
#if ANDROID
using Android.Content.PM;
using Android.App;
#endif

namespace DailyMusicalNote.Views;

using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;

public partial class GameView : ContentPage
{
    public event EventHandler? KeyClicked;

    private List<(Notes, Octaves, double)> _ratios = new();
    private const double RATIO = 8.7 / (19440.0 / 99.0); //Ratio of y-position.
    private const int KEYBOARD_SIZE = 28; //Size in keys (white and black).

    /// <summary>
    /// GameView() constructor. Initializes a keyboard layout.
    /// </summary>
    public GameView()
    {
        InitializeComponent();
        GenerateKeyboard();
    }

    /// <summary>
    /// The note is displayed at the correct vertical position.
    /// It is rotated if necessary.
    /// </summary>
    /// <param name="note">Note that should be displayed in the GUI.</param>
    public void ShowNote(RandomNote note)
    {
        Debug.WriteLine($"Displaying note: {note.Note}, {note.Octave}");

        //Run on UI thread.
        Staff.Dispatcher.Dispatch(() =>
        {
            //Get ratio.
            var result = _ratios.FirstOrDefault(x =>
                                             x.Item1 == note.Note &&
                                             x.Item2 == note.Octave);

            if (result != default)
            {
                //If the note is outside the main staff,
                //display an additional staff above or below.
                double ratio = (double)result.Item3;
                TopStaff.IsVisible = ratio >= 7 * RATIO ? true : false;
                DownStaff.IsVisible = ratio <= -5 * RATIO ? true : false;

                //If note is above A3 (A3 is the default (ratio=0). Above it
                //the ratio is positive, below it the ratio is negative).
                if (ratio > 0)
                {
                    //Rotate a note and compensate a ratio (because
                    //image rotates around its center).
                    NoteImage.Rotation = 180;
                    ratio -= 2 * RATIO;
                }
                else
                {
                    NoteImage.Rotation = 0;
                }

                //Move note up or down.
                NoteImage.TranslationY = (double)Staff.Height * -ratio;
            }

            //On start NoteImage.IsVisible = false.
            //It prevents a "jump" of the note when the GUI starts.
            NoteImage.IsVisible = true;
        });
    }

    /// <summary>
    /// Temporarily displays a "v" image to
    /// indicate that the answer is correct.
    /// Image fades in, then fades out after
    /// a few hundred milliseconds.
    /// </summary>
    public async void ShowCorrectImageAsync()
    {
        InvalidImage.IsVisible = false;

        CorrectImage.IsVisible = true;
        await CorrectImage.FadeTo(1, 100);
        await Task.Delay(700);
        await CorrectImage.FadeTo(0, 200);
        CorrectImage.IsVisible = false;
    }

    /// <summary>
    /// Temporarily displays a "x" image to
    /// indicate that the answer is incorrect.
    /// Image fades in, then fades out after
    /// a few hundred milliseconds.
    /// </summary>
    public async void ShowIncorrectImageAsync()
    {
        CorrectImage.IsVisible = false;

        InvalidImage.IsVisible = true;
        await InvalidImage.FadeTo(1, 100);
        await Task.Delay(700);
        await InvalidImage.FadeTo(0, 200);
        InvalidImage.IsVisible = false;
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
        //Now works for easy mode only.
        _ratios.Clear();

        CalculateUpperStaffRatios();
        CalculateLowerStaffRatios();
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
    /// Calcualtes a ratios for the upper side of the staff.
    /// </summary>
    private void CalculateUpperStaffRatios()
    {
        //Start from A4 (include A4).
        //TODO add bass clef case (changing  octave).
        //Same in CalculateLowerStaffRatios()
        int noteCounter = (int)Notes.NOTE_A - 1;
        int octaveCounter = (int)Octaves.OCTAVE_4;

        //From note A4 to upmost.
        for (int i = 0; i < KEYBOARD_SIZE / 2; i++)
        {
            Notes note;

            //Do not calculate ratios for black keys.
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

            //Add ratio to list.
            _ratios.Add(((Notes)noteCounter, (Octaves)octaveCounter, RATIO * i));
        }
    }

    /// <summary>
    /// Calcualtes a ratios for the lower side of the staff.
    /// </summary>
    private void CalculateLowerStaffRatios()
    {
        //Start from A4 (exclude A4).
        int noteCounter = (int)Notes.NOTE_A;
        int octaveCounter = (int)Octaves.OCTAVE_4;

        //From lower notes up to A4 (without A4).
        for (int i = -1; i >= -KEYBOARD_SIZE / 2; i--)
        {
            Notes note;

            //Do not calculate ratios for black keys.
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

            //Add ratio to the list.
            _ratios.Add(((Notes)noteCounter, (Octaves)octaveCounter, RATIO * i));
        }
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

        KeyClicked?.Invoke(key, e);
    }

    /// <summary>
    /// Not implemented yet.
    /// </summary>
    public async void GameOver()
    {
        //TODO implement game over. An alert is now shown for testing.
        await DisplayAlert("End", "Game over", "OK");
    }
}