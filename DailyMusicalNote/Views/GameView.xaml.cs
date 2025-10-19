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
    /// <summary>
    /// GameView() constructor. Initializes a keyboard layout.
    /// </summary>
	public GameView()
	{
		InitializeComponent();
        GenerateKeyboard();
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
        int startNote = (int)Notes.NOTE_A;
        int startOctave = (int)Octaves.OCTAVE_0;
        double xIndex = 0;

        for (int i = 0; i < keys.Length; i++)
        {
            //Key initialize.
            keys[i] = new Key((Notes)startNote++, (Octaves)startOctave);

            //If the last note is NOTE_B, increment the
            //octave and start from NOTE_C.
            if(startNote == (int)Notes.LAST_ELEMENT)
            {
                startNote = 0;
                startOctave++;
            }

            //Color button based on note sharpness.
            if ( new[] {Notes.NOTE_CSH, Notes.NOTE_DSH,
                        Notes.NOTE_FSH, Notes.NOTE_GSH,
                        Notes.NOTE_ASH }.Contains(keys[i].Note))
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
}