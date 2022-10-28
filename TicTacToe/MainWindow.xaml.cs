using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members 

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;
        
        /// <summary>
        /// True if it is player 1's turn (X) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True i´f the game ended 
        /// </summary>
        private bool mGameEnded;  

        #endregion

        #region Constructor   
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();

        }
        
        #endregion 

        /// <summary>
        /// Starts a new game and clears all values back to the start 
        /// </summary>
        private void NewGame()
        {
            //create a new blank array of free cells 
            mResults = new MarkType[9]; 

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Make sure Player 1 starts the game 
            mPlayer1Turn = true;
            
            //Interate every button on the grid... 
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //change background, foreground, and content to default values 
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Make sure the game hasn't finished 
            mGameEnded = false; 
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender"> The button that was clicked </param> 
        /// <param name="e"> The events of the click </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            
            //Cast a sender to the button
            var button = (Button)sender; 
            
            //Find the position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if the cell already has a value in it 
            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value based on which players turn it is 
            /*if (mPlayer1Turn)
                mResults[index] = MarkType.Cross; 
            else
                mResults[index] = MarkType.Nought; //one way of doing it*/
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Set button text to the result 
            button.Content = mPlayer1Turn ? "X" : "O";

            /*if (mPlayer1Turn)              // this is another way to write the statement
                mPlayer1Turn = false;
            else
                mPlayer1Turn = true; */

            //Change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Green;  //can change to other color too

            //Toggle the players turns 
            mPlayer1Turn ^= true;

            //check for a winner 
            CheckForWinner();                       

        }
        /// <summary>
        /// Check if there is a winner of 3 line straight 
        /// </summary>
        /// <exception cref=""></exception>

        private void CheckForWinner()
        {
            #region Horizontal Wins 

            //check for horizontal wins  
            //
            // - Row 0
            //
            //All these three are the same value 
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in color
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Goldenrod; 
            }

            //
            // - Row 1 
            //
            //All these three are the same value 
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in color
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Goldenrod;
            }

            //
            // - Row 2
            //
            //All these three are the same value 
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in color
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Goldenrod;
            }

            #endregion

            #region Vertical wins 

            //check for vertical wins  
            //
            // - Column 0
            //
            //All these three are the same value 
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in color
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Goldenrod;
            }

            //check for vertical wins  
            //
            // - Column 1
            //
            //All these three are the same value 
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in color
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Goldenrod;
            }

            //check for vertical wins  
            //
            // - Column 2
            //
            //All these three are the same value 
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in color
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Goldenrod;
            }

            #endregion

            #region Diaganal Wins 

            //check for diaganal wins  
            //
            // - Top left to bottom right 
            //
            //All these three are the same value 
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in color
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Goldenrod;
            }

            //
            // - Top right to bottom left 
            //
            //All these three are the same value 
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game ends  
                mGameEnded = true;

                //Highlight winning cells in color
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Goldenrod;
            }
            #endregion

            #region No Winners 
            //Check for no winner and full board 
            if (!mResults.Any(f => f == MarkType.Free ))
            {
                //Game ended 
                mGameEnded = true;

                //Turn all cells other color  
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Coral;
                });
            }
            #endregion 
        }
    }
}
