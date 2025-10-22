using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyMusicalNote.Views;
using Microsoft.Maui.Controls;

namespace DailyMusicalNote
{
    class Controller
    {
        private readonly MainPage _mainPage = new();
        private readonly DifficultyView _difficultyView = new();
        private readonly HistoryView _historyView = new();
        private readonly GameView _gameView = new();
        private readonly Model _model;
        public Page GetMainPage => _mainPage;

        /// <summary>
        /// Constructor of the Controller class. In the constructor
        /// the model and the _mainPage fields are initialized.
        /// Moreover, there are subscribers to MainPage events.
        /// </summary>
        /// <param name="model">The model of the app that
        /// is used by the controller.</param>
        public Controller(Model model)
        {
            Debug.WriteLine("Controller() constructor");
            _model = model;

            _mainPage.ButtonStartClicked += OnButtonStartClicked;
            _mainPage.ButtonHistoryClicked += OnButtonHistoryClicked;
            _difficultyView.ButtonStartGameClicked += OnButtonStartGameClicked;
        }

        /// <summary>
        /// Event handler for the ButtonStartClicked event. 
        /// In this handler, the difficultyView is opened.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void OnButtonStartClicked(object? sender, EventArgs e)
        {
            await _mainPage.Navigation.PushAsync(_difficultyView);
        }

        /// <summary>
        /// Event handler for the ButtonHistoryClicked event. 
        /// In this handler, the historyView is opened.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void OnButtonHistoryClicked(object? sender, EventArgs e)
        {
            await _mainPage.Navigation.PushAsync(_historyView);
        }

        /// <summary>
        /// Event handler for the ButtonStartGameClicked event. 
        /// In this handler, the main game view is opened and
        /// the difficultyView is removed.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void OnButtonStartGameClicked(object? sender, EventArgs e)
        {
            //TODO get value from GUI.
            _model.GenerateRandomNotes(15);

            _mainPage.Navigation.RemovePage(_difficultyView);
            await _mainPage.Navigation.PushAsync(_gameView);
        }
    }
}
