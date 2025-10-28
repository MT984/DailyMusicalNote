using System.Diagnostics;
namespace DailyMusicalNote.Views;

public partial class DifficultyView : ContentPage
{
    private Difficulty _chosenDifficulty = Difficulty.EASY; //Easy is default mode.
    private const string DEFAULT_NOTES_NUMBER = "15";
    public event EventHandler<(int, Difficulty)>? ButtonStartGameClicked;
    
    /// <summary>
    /// Initializes a new instance of the DifficultyView class.
    /// Also initializes a variables. 
    /// </summary>
	public DifficultyView()
	{
		InitializeComponent();
        NumberEntry.Text = DEFAULT_NOTES_NUMBER;
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
            //Check that input data is int>0.
            string? text = NumberEntry.Text?.Trim();
            if (int.TryParse(text, out int number) && number>=5 && number <=50)
            {
                ButtonStartGameClicked?.Invoke(this, (number, _chosenDifficulty) );
            }
            else
            {
                DisplayAlert(DailyMusicalNote.Resources.Lang.langResources.difficultyAlertTitle,
                             DailyMusicalNote.Resources.Lang.langResources.difficultyAlertContent,
                             DailyMusicalNote.Resources.Lang.langResources.difficultyAlertOk);
            }
        }

        //TODO clean code

        //Set chosen option.
        switch(button.ClassId)
        {
            case "bHard":
                _chosenDifficulty = Difficulty.HARD;
                break;

            case "bMedium":
                _chosenDifficulty = Difficulty.MEDIUM;
                break;

            case "bEasy":
            default:
                _chosenDifficulty = Difficulty.EASY;
                break;
        }

        SetDifficultyButtonsColors(button.ClassId);
    }

    /// <summary>
    /// Sets the default difficulty level.
    /// </summary>
    /// <param name="difficulty">The difficulty level to set as default.</param>
    public void SetChosenDifficulty(Difficulty difficulty)
    {
        string classId;
        _chosenDifficulty = difficulty;

        switch (difficulty)
        {
            case Difficulty.HARD:
                classId = "bHard";
                break;

            case Difficulty.MEDIUM:
                classId = "bMedium";
                break;

            default:
            case Difficulty.EASY:
                classId = "bEasy";
                break;
        }

        SetDifficultyButtonsColors(classId);
    }

    /// <summary>
    /// Sets a color to the button representing the chosen difficulty.
    /// </summary>
    /// <param name="targetButton">The ClassId of button that should be marked as chosen.</param>
    private void SetDifficultyButtonsColors(string targetButton)
    {
        var buttons = new[] { buttonEasy, buttonMedium, buttonHard };

        //Foreach used to colorize buttons
        foreach (var btn in buttons)
        {
            //"Reset" all button colors.
            btn.BackgroundColor = (Color)Application.Current.Resources["ButtonColor1"];

            //Set the color of the clicked button.
            if (btn.ClassId == targetButton)
                btn.BackgroundColor = (Color)Application.Current.Resources["ChosenOption"];
        }
    }
}