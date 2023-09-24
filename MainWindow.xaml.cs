using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SLAP_Assignment1_FilterCards_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CardDeck cardDeck;
        private Dictionary<RadioButton, Predicate<Card>> rbDictionary = new();

        public MainWindow()
        {
            InitializeComponent();

            cardDeck = new();
            CreateDeck();
                     
            SetupDictionary();

            RbAll.IsChecked = true;
        }

        /// <summary>
        /// Method for setting up the Dictionary of RadioButtons and Predicates.
        /// </summary>
        private void SetupDictionary()
        {
            rbDictionary.Add(RbAll, card => true);
            rbDictionary.Add(RbClubs, card => card.Suit == CardSuit.Clubs);
            rbDictionary.Add(RbDiamonds, card => card.Suit == CardSuit.Diamonds);
            rbDictionary.Add(RbHearts, card => card.Suit == CardSuit.Hearts);
            rbDictionary.Add(RbSpades, card => card.Suit == CardSuit.Spades);
            rbDictionary.Add(RbPicture, card => (int)card.Value > 9);
            rbDictionary.Add(RbNonpicture, card => (int)card.Value < 10);
            rbDictionary.Add(RbBlack, card => card.Suit == CardSuit.Clubs || card.Suit == CardSuit.Spades);
            rbDictionary.Add(RbRed, card => card.Suit == CardSuit.Diamonds || card.Suit == CardSuit.Hearts);
        }

        /// <summary>
        /// Method for creating the different cards and putting them in the deck.
        /// </summary>
        private void CreateDeck()
        {
            foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue cardValue in Enum.GetValues(typeof(CardValue)))
                {
                    cardDeck.AddCard(cardSuit, cardValue);
                }
            }
        }

        /// <summary>
        /// Event handler for radiobuttons that calls the filter method in CardDeck
        /// and shows a filtered list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            DgvCards.ItemsSource = cardDeck.FilterCardGame(rbDictionary[rb]);
        }

        /// <summary>
        /// Eventhandler for when selection changes in the DataGrid and displays
        /// the selected Card in a textbox. Mostly to use the required
        /// ToString() method in Card.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgvCards.SelectedItem != null)
            {
                // Get the selected item (which corresponds to the clicked row)
                Card selectedCard = (Card)DgvCards.SelectedItem;
                TbSelected.Text = selectedCard.ToString();
            }
        }
    }
}
