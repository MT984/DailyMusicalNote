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

            _mainPage.ButtonStartClicked += async (s, e) => {
                //Application.Current.MainPage = new DifficultyView();
                await _mainPage.Navigation.PushAsync(_difficultyView);
            };
        }
    }
}
