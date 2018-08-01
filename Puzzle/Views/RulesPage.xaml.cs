using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Puzzle
{
    /// <summary>
    /// Rules page class.
    /// </summary>
    public partial class RulesPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:Puzzle.RulesPage"/> class.
        /// </summary>
        public RulesPage()
        {

            InitializeComponent();

            rulesContent.Text = "The purpose of this game is to move the " +
                "cells of the board so that they are aranged in increasing " +
                "order from the top-left corner, and such that the bottom-" +
                "right cell is empty. For example, a solved 4X4 board looks " +
                "like this:";
        }

        /// <summary>
        /// Opens an alert dialog when the info icon is clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        async void AboutAppAsync(object sender, System.EventArgs e)
        {
            var response = await DisplayActionSheet("About This App", "Close",
                                                null, "Learn More");

            if (response == null)
                return;
            if (response.Equals("Learn More"))
                await Navigation.PushAsync(new InfoPage());
            return;
        }
    }
}
