using System.Diagnostics;
namespace DailyMusicalNote.Views;

public partial class DifficultyView : ContentPage
{
    private string _chosenDifficulty;
    private const string DEFAULT_NOTES_NUMBER = "15";
    public event EventHandler<int>? ButtonStartGameClicked;
    
    /// <summary>
    /// Initializes a new instance of the DifficultyView class.
    /// Also initializes a variables. 
    /// </summary>
	public DifficultyView()
	{
		InitializeComponent();
        _chosenDifficulty = string.Empty;
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
                ButtonStartGameClicked?.Invoke(this, number);
            }
            else
            {
                //TODO use lang
                DisplayAlert("Info",
                    "Incorect input data. You can play from 5 to 50 notes.", "OK");
            }
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