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

public partial class GameView : ContentPage
{
    public event EventHandler KeyClicked;
    private RandomNote _currentlyShowingNote;

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
        //TODO display RandomNote in GUI.
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
    }

    /// <summary>
    /// GenerateKeyboard() generates a piano keyboard in the GUI.
    /// </summary>
    private void GenerateKeyboard()
    {
        //Keys to show in GUI.
        Key[] keys = new Key[25];
        AbsoluteLayout KeyboardLayout = new();

        //Start values.
        //TODO implement difficulty dependency.
        int startNote = (int)Notes.NOTE_A;
        int startOctave = (int)Octaves.OCTAVE_4;
        double xIndex = 0;

        for (int i = 0; i < keys.Length; i++)
        {
            //Key initialize.
            keys[i] = new Key((Notes)startNote++, (Octaves)startOctave);
            keys[i].Clicked += OnKeyClicked;

            //If the last note is NOTE_B, increment the
            //octave and start from NOTE_C.
            if(startNote == (int)Notes.LAST_ELEMENT)
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