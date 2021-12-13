﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMusicalNote
{
    //Landscape screen orientation 
    [Activity(Label = "GameActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Landscape)]
    public class GameActivity : Activity 
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);

            //Full screen
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            //Finding elements by id
            TextView topBarNotesLeftValue = FindViewById<TextView>(Resource.Id.topBar_notesLeft_value);

            ImageView trebleClef = FindViewById<ImageView>(Resource.Id.trebleClef);
            ImageView bassClef = FindViewById<ImageView>(Resource.Id.bassClef);
            ImageView[] musicKeys =
            {
                trebleClef,
                FindViewById<ImageView>(Resource.Id.trebleG),
                FindViewById<ImageView>(Resource.Id.trebleD),
                FindViewById<ImageView>(Resource.Id.trebleA),
                FindViewById<ImageView>(Resource.Id.trebleE),
                FindViewById<ImageView>(Resource.Id.trebleH),
                FindViewById<ImageView>(Resource.Id.trebleFsh),
                FindViewById<ImageView>(Resource.Id.trebleCsh),
                FindViewById<ImageView>(Resource.Id.trebleCb),
                FindViewById<ImageView>(Resource.Id.trebleGb),
                FindViewById<ImageView>(Resource.Id.trebleDb),
                FindViewById<ImageView>(Resource.Id.trebleAb),
                FindViewById<ImageView>(Resource.Id.trebleEb),
                FindViewById<ImageView>(Resource.Id.trebleB),
                FindViewById<ImageView>(Resource.Id.trebleF),

                bassClef,
                FindViewById<ImageView>(Resource.Id.bassG),
                FindViewById<ImageView>(Resource.Id.bassD),
                FindViewById<ImageView>(Resource.Id.bassA),
                FindViewById<ImageView>(Resource.Id.bassE),
                FindViewById<ImageView>(Resource.Id.bassH),
                FindViewById<ImageView>(Resource.Id.bassFsh),
                FindViewById<ImageView>(Resource.Id.bassCsh),
                FindViewById<ImageView>(Resource.Id.bassCb),
                FindViewById<ImageView>(Resource.Id.bassGb),
                FindViewById<ImageView>(Resource.Id.bassDb),
                FindViewById<ImageView>(Resource.Id.bassAb),
                FindViewById<ImageView>(Resource.Id.bassEb),
                FindViewById<ImageView>(Resource.Id.bassB),
                FindViewById<ImageView>(Resource.Id.bassF),
            };
            ImageView[] keys =
            {
                 FindViewById<ImageView>(Resource.Id.a2),
                 FindViewById<ImageView>(Resource.Id.b2),
                 FindViewById<ImageView>(Resource.Id.c3),
                 FindViewById<ImageView>(Resource.Id.d3),
                 FindViewById<ImageView>(Resource.Id.e3),
                 FindViewById<ImageView>(Resource.Id.f3),
                 FindViewById<ImageView>(Resource.Id.g3),
                 FindViewById<ImageView>(Resource.Id.a3),
                 FindViewById<ImageView>(Resource.Id.b3),
                 FindViewById<ImageView>(Resource.Id.c4),
                 FindViewById<ImageView>(Resource.Id.d4),
                 FindViewById<ImageView>(Resource.Id.e4),
                 FindViewById<ImageView>(Resource.Id.f4),
                 FindViewById<ImageView>(Resource.Id.g4),
                 FindViewById<ImageView>(Resource.Id.a4),
                 FindViewById<ImageView>(Resource.Id.b4),
                 FindViewById<ImageView>(Resource.Id.c5)
            };

            //KeyMechanism myKey1 = new KeyMechanism("c4_key", "c4");
            //myKey1.ClickHander();
            int foo = Resources.GetIdentifier("c5_key", "id", PackageName);

            KeyMechanism myKey2 = new KeyMechanism("c5_key", "c5");
            myKey2.ClickHander();

            //Button testButton = FindViewById<Button>(Resource.Id.button1);
            Button a0 = FindViewById<Button>(Resource.Id.a4_key);
                ImageButton a0sh = FindViewById<ImageButton>(Resource.Id.a4sh_key);
                Button b0 = FindViewById<Button>(Resource.Id.b4_key);

                a0.Click += (s, e) => topBarNotesLeftValue.Text = "a0";
                a0sh.Click += (s, e) => topBarNotesLeftValue.Text = "a0sh";
                b0.Click += (s, e) => topBarNotesLeftValue.Text = "b0";

            //Variable to count how many notes on the sheet has been shown
            byte noteCounter = 1;

            //Initialize game mechanism
            nextNote();

            void nextNote()
            {
                Random randomNumbers = new Random();

                if(noteCounter <= 50)
                {
                    //Hide all unnecessary elements
                    foreach (ImageView im in musicKeys)
                    {
                        im.Visibility = Android.Views.ViewStates.Gone;
                    }

                    foreach (ImageView im in keys)
                    {
                        im.Visibility = Android.Views.ViewStates.Gone;
                    }

                    //Show random elements. It is ependence of chosen dificulty
                    switch (MyEnums.ChoseDifficulty)
                    {
                        //Easy - Only treble clef
                        case MyEnums.DifficultyMode.Easy:
                            trebleClef.Visibility = Android.Views.ViewStates.Visible;
                            break;

                        //Medium - Treble and bass clef
                        case MyEnums.DifficultyMode.Medium:

                            switch (randomNumbers.Next(0, 2))
                            {
                                case 0:
                                    trebleClef.Visibility = Android.Views.ViewStates.Visible;
                                    break;

                                case 1:
                                    bassClef.Visibility = Android.Views.ViewStates.Visible;
                                    break;
                            }

                            break;

                        //Hard - All clefs and all music keys
                        case MyEnums.DifficultyMode.Hard:

                            musicKeys[randomNumbers.Next(0, musicKeys.Length)].Visibility = Android.Views.ViewStates.Visible;
                            break;
                    }

                    //enuma dodac
                    keys[randomNumbers.Next(0,keys.Length)].Visibility = Android.Views.ViewStates.Visible;

                    //ifa dodac na wyglad
                    topBarNotesLeftValue.Text = noteCounter + "/50";
                    noteCounter++;
                }
                else
                {
                    topBarNotesLeftValue.Text = "KONIEC";
                    //testButton.Click += (s, e) => Finish();
                }
            }
        }
    }
}