/*
 * FILE : MainPage.xaml.cs
 * PROJECT : Option 1 - Puzzle Game
 * PROGRAMMER : Brodie Paterson, Nicholas Nathaniel
 * FIRST VERSION : 2015-12-17
 * DESCRIPTION : This program will generate a picture puzzle
 *              that the player will have to solve. The user 
 *              uploads an image through the device camera or a local file 
 *              which gets randomized into tiles as it is loaded.
 *              The user will attempt to solve the puzzle by 
 *              sliding the picture tiles to the correct order. Once a winning 
 *              combination is found, the user is alerted and prompted to play again or quit.
 * 
 * 
 * 
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void newGameButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			this.Frame.Navigate(typeof(PuzzlePage));
        }

        private void exitButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			Application.Current.Exit();
        }
    }
}
