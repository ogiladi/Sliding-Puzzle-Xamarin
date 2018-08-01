using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Puzzle
{
    /// <summary>
    /// Info page class.
    /// </summary>
    public partial class InfoPage : ContentPage
    {
        /// <summary>
        /// Goes to developer's website on a web browser.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        void GoToWebsite(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("http://www.ohadgiladi.com"));
        }

        /// <summary>
        /// Goes to developer's GitHub page on a web browser.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        void GoToGitHub(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://github.com/ogiladi"));
        }


        /// <summary>
        /// Goes to page where the background pattern is taken from.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        void AboutBackground(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.patterncooler.com"));
        }

        /// <summary>
        /// Goes to page where the app icons are taken from.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        void AboutIcons(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://icons8.com"));
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:Puzzle.InfoPage"/> class.
        /// </summary>
        public InfoPage()
        {            
            InitializeComponent();
        }
    }
}
