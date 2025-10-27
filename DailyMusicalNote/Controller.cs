using DailyMusicalNote.Views;
using System.Diagnostics;

namespace DailyMusicalNote
{
    class Controller
    {
        private readonly MainPage _mainPage = new();
        private readonly DifficultyView _difficultyView = new();
        private readonly HistoryView _historyView = new();
        private readonly GameView _gameView = new();
        private readonly Model _model;
        private IDispatcherTimer _timer = Application.Current!.Dispatcher.CreateTimer();
        private int _correctAnswerCounter;
        private int _overallAnswerCounter;

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
            _gameView.GameOverResult += OnGameOverResult;
            _historyView.Loaded += OnHistoryViewLoaded;

            //TODO protect when timer.Enabled in model == false
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                _gameView.UpdateTimerLabel(_model.Elapsed);
            };
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
        /// <param name="notesNumer">Event arguments.</param>
        private async void OnButtonStartGameClicked(object? sender, int notesNumer)
        {
            if (_mainPage.Navigation.NavigationStack.Contains(_difficultyView))
                _mainPage.Navigation.RemovePage(_difficultyView);

            bool isGameViewOpen = _mainPage.Navigation.NavigationStack
                .Any(p => p == _gameView);

            if (!isGameViewOpen)
                await _mainPage.Navigation.PushAsync(_gameView);

            //Wait for view initialization.
            await Task.Delay(250);

            _timer.Start();

            //First start game then show note.
            _model.StartGame(notesNumer);
            _gameView.ShowNote(_model.NextNote);

            _correctAnswerCounter = 0;
            _overallAnswerCounter = 0;
            _gameView.UpdateNotesCounter(_correctAnswerCounter, notesNumer);
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

            _overallAnswerCounter++;

            Debug.WriteLine($"OnKeyClicked handler." +
                $" Clicked on: {key.Note}, {key.Octave}." +
                $" Current note: {_model.CurrentlyDisplayingNote.Note}, {_model.CurrentlyDisplayingNote.Octave}.");

            if (_model.CheckResult(key))
            {
                ProcessCorrectAnswer();
            }
            else
            {
                _gameView.ShowIncorrectImageAsync();
                Debug.WriteLine("Incorrect");
            }

        }

        /// <summary>
        /// Processes the correct answer. Updates the GUI and model.
        /// </summary>
        private void ProcessCorrectAnswer()
        {
            _correctAnswerCounter++;
            _gameView.ShowCorrectImageAsync();
            _gameView.UpdateNotesCounter(_correctAnswerCounter);

            RandomNote nextNote = _model.NextNote;

            //If it's the end of the notes
            //(they are set to LAST_ELEMENT), game over occurs.
            if (nextNote.Note == Notes.LAST_ELEMENT &&
               nextNote.Octave == Octaves.LAST_ELEMENT)
            {
                ProcessGameOver();
            }
            else
            {
                _gameView.ShowNote(nextNote);
            }
        }

        /// <summary>
        /// Processes the game over.
        /// </summary>
        private void ProcessGameOver()
        {
            Debug.WriteLine("Game over");

            _timer.Stop();

            int misclicks = _overallAnswerCounter - _correctAnswerCounter;
            int percent = (int)Math.Round((double)_correctAnswerCounter /
                                                  _overallAnswerCounter * 100);

            int score = _model.GameOver(_correctAnswerCounter, misclicks, percent);

            _gameView.GameOver(score, percent);
        }

        /// <summary>
        /// Restarts game or returns to main menu based on chosen option.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="result">Event arguments - chosen option.</param>
        private void OnGameOverResult(object? sender, GameOverPopup.PopupResult result)
        {
            switch (result)
            {
                case GameOverPopup.PopupResult.RESTART_GAME:
                    OnButtonStartGameClicked(this, _correctAnswerCounter);
                    break;

                default:
                case GameOverPopup.PopupResult.RETURN_TO_MAIN_MENU:
                    if (_mainPage.Navigation.NavigationStack.Contains(_gameView))
                        _mainPage.Navigation.RemovePage(_gameView);
                    break;
            }
        }

        private void OnHistoryViewLoaded(object? sender, EventArgs e)
        {
            List<Save> saveList = _model.GetSavedHistory();
            _historyView.SetHistoryList(saveList);
        }

        #endregion
    }
}
