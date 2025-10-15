using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;

namespace DailyMusicalNote
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
                    //TODO Implement bStart handler.
                    break;
                case "bHistory":
                    //TODO Implement bHistory handler.
                    break;
                case "bExit":
                    Environment.Exit(0);
                    break;
            }
        }
    }

}
