using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Puzzle
{
    /// <summary>
    /// Game page class.
    /// </summary>

    public partial class GamePage : ContentPage
    {        
        int currBoardSize;
        bool gameOn = false;
        Board board;
        Button[,] buttons;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:Puzzle.GamePage"/> class.
        /// </summary>
        /// <param name="size">Board size.</param>
        public GamePage(int size)
        {
            InitializeComponent();
            currBoardSize = size;
            //PlayGameAsync(currBoardSize);
        }

        /// <summary>
        /// Start a game when the page appears for the first time. Then 
        /// keep the current game when the user navigates to another page. 
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!gameOn)
            {
                await PlayGameAsync(currBoardSize);
                gameOn = true;
            }
        }

        /// <summary>
        /// Generates a game on a different thread. Shows an activity indicator
        /// while loading the game.
        /// </summary>
        /// <param name="size">Board Size.</param>
        async Task PlayGameAsync(int size)
        {
            playGrid.IsVisible = false;
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            await Task.Delay(1500);
            await Task.Run(() =>
            {
                currBoardSize = size;
                board = new Board(currBoardSize);
                board.CreateGame();
            });
            BuildBoardGrid();
            UpdateBoardValues();
            playGrid.IsVisible = true;
            indicator.IsRunning = false;
            indicator.IsVisible = false;
        }

        /// <summary>
        /// When the game is over, show an alert dialog.
        /// </summary>
        async Task GameOverAsync()
        {
            var response = await DisplayActionSheet("You Did It!", "Close", null, "Start a new game");
            if (response == null)
                return;
            if (response.Equals("Start a new game"))
            {
                var validChoice = false;
                int choice = 0;
                var chooseSize = await DisplayActionSheet("Choose Board Size", 
                                                          "Close", 
                                                          null, 
                                                          "3X3", 
                                                          "4X4", 
                                                          "5X5", 
                                                          "6X6");
                if (chooseSize == null)
                    return;
                if (chooseSize.Equals("3X3"))
                {
                    choice = 3;
                    validChoice = true;
                }
                else if (chooseSize.Equals("4X4"))
                {
                    choice = 4;
                    validChoice = true;
                }
                else if (chooseSize.Equals("5X5"))
                {
                    choice = 5;
                    validChoice = true;
                }
                else if (chooseSize.Equals("6X6"))
                {
                    choice = 6;
                    validChoice = true;
                }

                if (validChoice)
                {
                    await PlayGameAsync(choice);
                }
            }
           return;
        }

        /// <summary>
        /// Builds the board grid.
        /// </summary>
        void BuildBoardGrid()
        {
            int size = currBoardSize;
            double boardFontSize = BoardFontSize(size);
            buttons = new Button[size, size];
            boardGrid.RowDefinitions.Clear();
            boardGrid.ColumnDefinitions.Clear();
            boardGrid.Children.Clear();
            for (int i = 0; i < size; i++)
            {
                boardGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
                boardGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
            }

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    buttons[row, col] = new Button
                    {
                        Style = 
                            (Style)Application.Current.Resources["GridCellStyle"],
                        FontSize = boardFontSize,
                    };
                    buttons[row, col].Clicked += GridButtonClickedAsync;
                    boardGrid.Children.Add(buttons[row, col], col, row);
                }
            }
        }

        /// <summary>
        /// Updates the board values.
        /// </summary>
        void UpdateBoardValues()
        {
            int size = currBoardSize;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var text = board.CellValue(row, col);
                    buttons[row, col].Text = text;
                    if (!text.Equals(""))
                        buttons[row, col].BackgroundColor =
                            (Color)Application.Current.Resources["backLight"];
                    else
                        buttons[row, col].BackgroundColor =
                                             Color.Transparent;
                }
            }
        }

        /// <summary>
        /// In case the cell clicked is adjacent to the empty cell, move 
        /// the clicked cell to the empty cell.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        async void GridButtonClickedAsync(object sender, System.EventArgs e)
        {
            Button theButton = (Button)sender;
            int buttonRow = Grid.GetRow(theButton);
            int buttonCol = Grid.GetColumn(theButton);
            var pressed = board.ChooseCell(buttonRow, buttonCol);
            if (pressed)
                UpdateBoardValues();            
            if (board.IsSolved())
                await GameOverAsync();   
        }

        /// <summary>
        /// Sets the board font size dynamically according to the device type.
        /// </summary>
        /// <returns>The font size.</returns>
        /// <param name="size">Board Size.</param>
        double BoardFontSize(int size)
        {
            if (size == 3)
            {
                if (Device.Idiom == TargetIdiom.Phone)
                    return 45;
                return 70;  
            }
            if (size == 4)
            {
                if (Device.Idiom == TargetIdiom.Phone)
                    return 38;
                return 60;
            }
            if (size == 5)
            {
                if (Device.Idiom == TargetIdiom.Phone)
                    return 30;
                return 48;  
            }
            if (size == 6)
            {
                if (Device.Idiom == TargetIdiom.Phone)
                    return 22;
               return 36;
            }
            return 10;
        }

        /// <summary>
        /// Open an alert dialog when the menu button is clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event.</param>
        async void MenuButtonClickedAsync(object sender, System.EventArgs e)
        {
            var response = await DisplayActionSheet("Options Menu",
                                                    "Back To Game",
                                                    null,
                                                    "Recall Game Rules",
                                                    "New Game");
            if (response == null)
                return;

            if (response.Equals("Recall Game Rules"))
                await Navigation.PushAsync(new RulesPage());

            if (response.Equals("New Game"))
            {
                var chooseSize = await DisplayActionSheet("Choose Board Size",
                                                         "Back To Game",
                                                         null,
                                                         "3X3",
                                                         "4X4",
                                                         "5X5",
                                                         "6X6");
                if (chooseSize == null)
                    return;

                int choice = 0;
                var validChoice = false;

                if (chooseSize.Equals("3X3"))
                {
                    choice = 3;
                    validChoice = true;
                }
                else if (chooseSize.Equals("4X4"))
                {
                    choice = 4;
                    validChoice = true;
                }
                else if (chooseSize.Equals("5X5"))
                {
                    choice = 5;
                    validChoice = true;
                }
                else if (chooseSize.Equals("6X6"))
                {
                    choice = 6;
                    validChoice = true;
                }

                if (validChoice)
                {
                    await PlayGameAsync(choice);
                }
            }
            return;
        }

        /// <summary>
        /// Open an alert dialog when the app info icon is clicked.
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
