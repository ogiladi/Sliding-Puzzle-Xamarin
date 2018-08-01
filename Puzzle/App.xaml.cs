using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Puzzle
{
    /// <summary>
    /// App class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Puzzle.App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
            Application.Current.MainPage = new NavigationPage(new WelcomePage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["toolbarBlue"],
                BarTextColor = Color.White,
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
