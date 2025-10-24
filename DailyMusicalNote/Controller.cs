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
            _gameView.KeyClicked += OnKeyClicked;
        }

        #region MainMenuClickHandlers
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
        #endregion

        #region GameMechanismUtils
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
            _model.GenerateRandomNotes(30);

            _mainPage.Navigation.RemovePage(_difficultyView);
            await _mainPage.Navigation.PushAsync(_gameView);

            await Task.Delay(100);
            _gameView.ShowNote(_model.NextNote);
        }

        /// <summary>
        /// Event handler for the OnKeyClicked event.
        /// In this handler, the displayed note
        /// is compared with the clicked note.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnKeyClicked(object? sender, EventArgs e)
        {
            if (sender is not Key key)
                return;

            Debug.WriteLine($"OnKeyClicked handler." +
                $" Clicked on: {key.Note}, {key.Octave}." +
                $" Current note: {_model.CurrentlyDisplayingNote.Note}, {_model.CurrentlyDisplayingNote.Octave}.");
            
            if(_model.CheckResult(key))
            {
                _gameView.ShowCorrectImageAsync();
                _gameView.ShowNote(_model.NextNote);
            }
            else
            {
                _gameView.ShowInvalidImageAsync();
                Debug.WriteLine("Incorrect");
            }

        }
        #endregion
    }
}
