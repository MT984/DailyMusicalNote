using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMusicalNote
{
    class Controller
    {
        private readonly MainPage _mainPage;
        private readonly Model _model;
        public Page GetMainPage => _mainPage;

        /// <summary>
        /// Constructor of the Controller class. In the constructor
        /// the model and the _mainPage fields are initialized.
        /// </summary>
        /// <param name="model">The model of the app that
        /// is used by the controller.</param>
        public Controller(Model model)
        {
            Debug.WriteLine("Controller() constructor");
            _model = model;
            _mainPage = new MainPage();
        }
    }
}
