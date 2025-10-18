namespace DailyMusicalNote.Views
{
    public partial class MainPage : ContentPage
    {
        public event EventHandler ButtonStartClicked;
        public event EventHandler ButtonHistoryClicked;

        /// <summary>
        /// Initializes a new instance of the MainPage class.
        /// </summary>
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
                    ButtonStartClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "bHistory":
                    ButtonHistoryClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "bExit":
                    Environment.Exit(0);
                    break;
            }
        }
    }

}
