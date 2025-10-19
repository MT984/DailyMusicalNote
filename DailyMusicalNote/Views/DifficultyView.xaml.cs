using System.Diagnostics;
namespace DailyMusicalNote.Views;

public partial class DifficultyView : ContentPage
{
    private string _chosenDifficulty;
    public event EventHandler ButtonStartGameClicked;

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
        //If there is an error then return.
        if (sender is not Button button)
        {
            return;
        }

        //If the start game button is clicked, then GameView should open.
        if (button.ClassId == "bStart")
        {
            ButtonStartGameClicked?.Invoke(this, EventArgs.Empty);
        }

        //Set chosen option.
        _chosenDifficulty = button.ClassId;
        var buttons = new[] { buttonEasy, buttonMedium, buttonHard };

        //Foreach used to colorize buttons
        foreach (var btn in buttons)
        {
            //"Reset" all button colors.
            btn.BackgroundColor = (Color)Application.Current.Resources["ButtonColor1"];

            //Set the color of the clicked button.
            if (btn.ClassId == button.ClassId)
                btn.BackgroundColor = (Color)Application.Current.Resources["ChosenOption"];
        }
    }
}