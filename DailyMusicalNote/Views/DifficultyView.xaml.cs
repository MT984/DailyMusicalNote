namespace DailyMusicalNote.Views;

public partial class DifficultyView : ContentPage
{
    private string _chosenDifficulty;

    /// <summary>
    /// Initializes a new instance of the DifficultyView class.
    /// Also initializes a variables. 
    /// </summary>
	public DifficultyView()
	{
		InitializeComponent();
        _chosenDifficulty = string.Empty;
    }

    /// <summary>
    /// Event handler for button clicks. One method handles 4 buttons 
    /// (3 difficulty stages and the start button) based on their classId. 
    /// It also changes the button's background depending on the chosen option.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        if(button.ClassId == "bStart")
        {
            //TODO start game
        }

        _chosenDifficulty = button.ClassId;
        var buttons = new[] { buttonEasy, buttonMedium, buttonHard };

        foreach (var btn in buttons)
        {
            btn.BackgroundColor = (Color)Application.Current.Resources["ButtonColor1"];

            if (btn.ClassId == button.ClassId)
                btn.BackgroundColor = (Color)Application.Current.Resources["ChosenOption"];
        }
    }
}