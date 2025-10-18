using System.Diagnostics;

namespace DailyMusicalNote
{
    public partial class App : Application
    {
        private readonly Model _model;
        private readonly Controller _controller;
        public App()
        {
            Debug.WriteLine("App() constructor");

            InitializeComponent();

            _model = new();
            _controller = new(_model);

            MainPage = new NavigationPage(_controller.GetMainPage);
        }
    }
}
