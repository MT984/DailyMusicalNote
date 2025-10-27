using System.Collections.ObjectModel;

namespace DailyMusicalNote.Views;

public partial class HistoryView : ContentPage
{
    public ObservableCollection<Save> Results { get; set; } = new();
    public HistoryView()
	{
		InitializeComponent();
    }

	public void SetHistoryList(List<Save> saveList)
	{
        saveList = saveList.OrderByDescending(s => s.dateTime).ToList();

        foreach (var s in saveList)
		{
            Results.Add(s);
        }

        BindingContext = this;
    }
}