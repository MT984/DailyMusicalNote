using System.Collections.ObjectModel;

namespace DailyMusicalNote.Views;

public partial class HistoryView : ContentPage
{
    public ObservableCollection<Save> Results { get; set; } = new();
    public EventHandler? ButtonReturnClickedEvent;
    public EventHandler? ButtonClearClickedEvent;

    /// <summary>
    /// Constructor of the HistoryView class.
    /// </summary>
    public HistoryView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Sets a history from save file into GUI.
    /// </summary>
    /// <param name="saveList">A list of saves.</param>
    public void SetHistoryList(List<Save> saveList)
    {
        Results.Clear();
        saveList = saveList.OrderByDescending(s => s.dateTime).ToList();

        foreach (var s in saveList)
        {
            Results.Add(s);
        }

        BindingContext = this;
    }

    /// <summary>
    /// Invokes the ButtonReturnClickedEvent event. 
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Event arguments.</param>
    private void ButtonReturnClicked(object sender, EventArgs e) =>
        ButtonReturnClickedEvent?.Invoke(sender, e);

    /// <summary>
    /// Displays an alert then invoke the ButtonClearClickedEvent when chosen option is true.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Event arguments.</param>
    private async void ButtonClearClicked(object sender, EventArgs e)
    {
        bool answer = await Application.Current.MainPage.DisplayAlert(
            DailyMusicalNote.Resources.Lang.langResources.historyAlertTitle,
            DailyMusicalNote.Resources.Lang.langResources.historyAlertContent,
            DailyMusicalNote.Resources.Lang.langResources.historyAlertYes,
            DailyMusicalNote.Resources.Lang.langResources.historyAlertNo
        );

        if (answer)
        {
            Results.Clear();
            ButtonClearClickedEvent?.Invoke(sender, e);
        }
    }
}