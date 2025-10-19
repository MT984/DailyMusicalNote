using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Diagnostics;
#if ANDROID
using Android.Content.PM;
using Android.App;
#endif

namespace DailyMusicalNote.Views;

public partial class GameView : ContentPage
{
	public GameView()
	{
		InitializeComponent();
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
}