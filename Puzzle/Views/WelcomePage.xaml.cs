using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Puzzle
{
    /// <summary>
    /// Welcome page class.
    /// </summary>
    public partial class WelcomePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the
        ///  <see cref="T:Puzzle.WelcomePage"/> class.
        /// </summary>
        public WelcomePage()
        {
            InitializeComponent();

            var sizeIcon = 40;
            if (Device.Idiom == TargetIdiom.Tablet)
                sizeIcon = 80;

            var icon = new Image { Source = "icon_info_large.png" };

            var trigger = new TapGestureRecognizer();
            trigger.Tapped += TriggerTappedAsync;
            icon.GestureRecognizers.Add(trigger);

            upperLayout.Children.Add(icon,
                                     new Rectangle(0.9, 0.15, sizeIcon, sizeIcon),
                                     AbsoluteLayoutFlags.PositionProportional);
        }

        /// <summary>
        /// Open an alert dialog when info icon is clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        async void TriggerTappedAsync(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("About This App", "Close",
                                                null, "Learn More");

            if (response == null)
                return;
            if (response.Equals("Learn More"))
                await Navigation.PushAsync(new InfoPage());
            return;
        }

        /// <summary>
        /// Starts a new game when player chooses board size.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        async void StartGameAsync(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            var title = button.Text;
            int size = 0;
            if (title.Equals("3x3"))
                size = 3;
            else if (title.Equals("4x4"))
                size = 4;
            else if (title.Equals("5x5"))
                size = 5;
            else if (title.Equals("6x6"))
                size = 6;
            if (size > 0)
                await Navigation.PushAsync(new GamePage(size));
        }

    }
}
