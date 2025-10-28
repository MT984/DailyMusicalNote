using System.Diagnostics;
#if ANDROID
using Android.Content.PM;
using Android.App;
#endif

namespace DailyMusicalNote.Views
{
    public partial class MainPage : ContentPage
    {
        public event EventHandler? ButtonStartClicked;
        public event EventHandler? ButtonHistoryClicked;

        /// <summary>
        /// Initializes a new instance of the MainPage class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OnAppearing() is invoked when the MainPage becomes visible.
        /// In this case, the screen rotates to default (portrait)
        /// orientation when the page becomes visible.
        /// </summary>
        protected override void OnAppearing()
        {
            Debug.WriteLine("MainPage OnAppearing()");
            base.OnAppearing();
#if ANDROID
            this.Dispatcher.Dispatch(() => {
                var activity = MainActivity.Instance;
                activity.RequestedOrientation = ScreenOrientation.Portrait;
            });
#endif
        }

        /// <summary>
        /// Button on clicked event handler. One method handles
        /// 3 buttons (start button, history button and exit button)
        /// base on their classId.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            switch (button?.ClassId)
            {
                case "bStart":
                    ButtonStartClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "bHistory":
                    //Set button text to "loading".
                    HistoryButton.Text =
                                DailyMusicalNote.Resources.Lang.langResources.labelLoading;
                    
                    //After 7 seconds set button text to
                    //previous without freeze a program.
                    Task.Run(() =>
                    {
                        Task.Delay(7000);
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            HistoryButton.Text =
                                DailyMusicalNote.Resources.Lang.langResources.buttonMenuHistory;
                        });
                    });

                    ButtonHistoryClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "bExit":
                    Environment.Exit(0);
                    break;
            }
        }
    }

}
