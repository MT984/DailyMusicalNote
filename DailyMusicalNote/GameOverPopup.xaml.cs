using CommunityToolkit.Maui.Views;
using System.Net.Sockets;

namespace DailyMusicalNote;

public partial class GameOverPopup : Popup
{
    public enum PopupResult
    {
        RESTART_GAME,
        RETURN_TO_MAIN_MENU
    };

    /// <summary>
    /// Constructor of the GameOverPopup class.
    /// </summary>
    /// <param name="score">The score to be displayed in the popup.</param>
    /// <param name="accuracy">The accuracy to be displayed in the popup.</param>
    public GameOverPopup(int score, int accuracy)
    {
        InitializeComponent();

        //Show results on the screen.
        ScoreLabel.Text += score.ToString();
        AccuracyaLabel.Text += accuracy.ToString();
    }

    /// <summary>
    /// Handles the event when the restart button is clicked.
    /// Then the popup is closed with the result argument.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnRestartClicked(object sender, EventArgs e)
    {
        Close(PopupResult.RESTART_GAME);
    }

    /// <summary>
    /// Handles the event when the return to main menu button is clicked.
    /// Then the popup is closed with the result argument.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnReturnClicked(object sender, EventArgs e)
    {
        Close(PopupResult.RETURN_TO_MAIN_MENU);
    }
}
