﻿/*
 * FILE : PuzzlePage.xaml.cs
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
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Core;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Xaml.Shapes;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.Media.Capture;
using SP.Shared;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PuzzlePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        static bool mouseDown = false;
        List<Tile> Tiles = new List<Tile>();
        private bool isGameOver = false;
        String ImagePath;
        Point down = new Point();
        Point up = new Point();
        private Point[] imgPosi = new Point[16];

        public PuzzlePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            //image pointer pressed events
                this.image1.PointerPressed += Image1_PointerPressed;
                this.image2.PointerPressed += Image2_PointerPressed;
                this.image3.PointerPressed += Image3_PointerPressed;
                this.image4.PointerPressed += Image4_PointerPressed;
                this.image5.PointerPressed += Image5_PointerPressed;
                this.image6.PointerPressed += Image6_PointerPressed;
                this.image7.PointerPressed += Image7_PointerPressed;
                this.image8.PointerPressed += Image8_PointerPressed;
                this.image9.PointerPressed += Image9_PointerPressed;
                this.image10.PointerPressed += Image10_PointerPressed;
                this.image11.PointerPressed += Image11_PointerPressed;
                this.image12.PointerPressed += Image12_PointerPressed;
                this.image13.PointerPressed += Image13_PointerPressed;
                this.image14.PointerPressed += Image14_PointerPressed;
                this.image15.PointerPressed += Image15_PointerPressed;
                this.image16.PointerPressed += Image16_PointerPressed;

                //image pointer released events
                this.image1.PointerExited += Image1_PointerExited;
                this.image2.PointerExited += Image2_PointerExited;
                this.image3.PointerExited += Image3_PointerExited;
                this.image4.PointerExited += Image4_PointerExited;
                this.image5.PointerExited += Image5_PointerExited;
                this.image6.PointerExited += Image6_PointerExited;
                this.image7.PointerExited += Image7_PointerExited;
                this.image8.PointerExited += Image8_PointerExited;
                this.image9.PointerExited += Image9_PointerExited;
                this.image10.PointerExited += Image10_PointerExited;
                this.image11.PointerExited += Image11_PointerExited;
                this.image12.PointerExited += Image12_PointerExited;
                this.image13.PointerExited += Image13_PointerExited;
                this.image14.PointerExited += Image14_PointerExited;
                this.image15.PointerExited += Image15_PointerExited;
                this.image16.PointerExited += Image16_PointerExited;
                //image pointer exited events
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }

        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// checkWin() is a function that checks each image position to ensure its in the winning format
        /// </summary>
        public void checkWin()
        {
            // Check each image name containing position number
            if (image1.Name == "0")
                if (image2.Name == "1")
                    if (image3.Name == "2")
                        if (image4.Name == "3")
                            if (image5.Name == "4")
                                if (image6.Name == "5")
                                    if (image7.Name == "6")
                                        if (image8.Name == "7")
                                            if (image9.Name == "8")
                                                if (image10.Name == "9")
                                                    if (image11.Name == "10")
                                                        if (image12.Name == "11")
                                                            if (image13.Name == "12")
                                                                if (image14.Name == "13")
                                                                    if (image15.Name == "14")
                                                                        // Tiles in correct order
                                                                        isGameOver = true;
            // WIN STUFF 
            if (isGameOver)
            {		
                wintext.Visibility = Windows.UI.Xaml.Visibility.Visible;
				playAgain.Visibility = Windows.UI.Xaml.Visibility.Visible;
				quitButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

        }
        /// <summary>
        /// ShuffleList randomizes image tiles uploaded by the player
        /// </summary>
        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, 0); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
        /// <summary>
        /// LoadImage loads image from stream for players preview image
        /// </summary>
        private static async Task<BitmapImage> LoadImage(StorageFile file)
        {
            BitmapImage bitmapImage = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read);
            bitmapImage.SetSource(stream);

            return bitmapImage;

        }
        /// <summary>
        /// uploadImage_Click() allows the user to upload a local image file.
        /// </summary>
        async private void uploadImage_Click(object sender, RoutedEventArgs e)
        {
            ImagePath = string.Empty;
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            // Filter to include a sample subset of file types
            filePicker.FileTypeFilter.Clear();
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".jpg");

            // Let user choose one file
            StorageFile file = await filePicker.PickSingleFileAsync();
            
            Tiles.Clear();
            // Check to make sure user picked a file
            if (file != null)
            {
                uint counter = 0;
                // Setup preview image for player
                BitmapImage img = new BitmapImage();
                img = await LoadImage(file);
                previewImage.Source = img;
                // Open a stream for the selected file and bitmap decoder
                Windows.Storage.Streams.IRandomAccessStream fileStream =
                    await file.OpenAsync(FileAccessMode.Read);
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);
                // loop through grid locations to split the picture 16 times
                for (uint y = 0; y <= 3; y++)
                {

                    for (uint x = 0; x <= 3; x++)
                    {
                        // create a new stream and encoder for the new image
                        InMemoryRandomAccessStream accessStream = new InMemoryRandomAccessStream();
                        BitmapEncoder enc = await BitmapEncoder.CreateForTranscodingAsync(accessStream, decoder);

                        // convert the entire bitmap to a 600px by 600px bitmap
                        enc.BitmapTransform.ScaledHeight = 600;
                        enc.BitmapTransform.ScaledWidth = 600;

                        BitmapBounds bounds = new BitmapBounds();
                        bounds.Height = 150;
                        bounds.Width = 150;
                        bounds.X = 150 * x;
                        bounds.Y = 150 * y;
                        enc.BitmapTransform.Bounds = bounds;

                        // write out to the stream
                        try
                        {
                            await enc.FlushAsync();
                        }
                        catch (Exception ex)
                        {
                            string s = ex.ToString();
                        }


                        // Set the image source to the selected bitmap.
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(accessStream);
                        Tile ti = new Tile();
                        ti.image = bitmapImage;
                        ti.position = counter;
                        Tiles.Add(ti);
                        counter++;
                       
                    }

                }

                Tiles = ShuffleList<Tile>(Tiles);
                // transfer image and position number 
                image1.Source = Tiles[0].image;
                image1.Name = Tiles[0].position.ToString();
                image2.Source = Tiles[1].image;
                image2.Name = Tiles[1].position.ToString();
                image3.Source = Tiles[2].image;
                image3.Name = Tiles[2].position.ToString();
                image4.Source = Tiles[3].image;
                image4.Name = Tiles[3].position.ToString();
                image5.Source = Tiles[4].image;
                image5.Name = Tiles[4].position.ToString();
                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();
                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();
                image8.Source = Tiles[7].image;
                image8.Name = Tiles[7].position.ToString();
                image9.Source = Tiles[8].image;
                image9.Name = Tiles[8].position.ToString();
                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();
                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();
                image12.Source = Tiles[11].image;
                image12.Name = Tiles[11].position.ToString();
                image13.Source = Tiles[12].image;
                image13.Name = Tiles[12].position.ToString();
                image14.Source = Tiles[13].image;
                image14.Name = Tiles[13].position.ToString();
                image15.Source = Tiles[14].image;
                image15.Name = Tiles[14].position.ToString();
                Tiles[15].image = new BitmapImage();
                Tiles[15].blank = true;
                
            }
        }


        /////////////////////////////////// Pointer Pressed method events for each image 1-16///////////////////////////////////////////////
        /// <summary>
        /// Image1_PointerPressed to Image16_PointerPressed tracks the clicked image tile.
        /// </summary>
        private void Image1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // If game over no tiles can be moved
            if (isGameOver == false)
            {
                PointerPoint ptrPt = e.GetCurrentPoint(image1);
                down = ptrPt.Position;
                mouseDown = true;
            }
        }
        private void Image2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image2);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image3_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image3);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image4_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image4);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image5_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image5);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image6_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image6);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image7_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image7);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image8_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image8);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image9_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image9);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image10_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image10);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image11_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image11);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image12_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image12);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image13_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image13);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image14_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image14);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image15_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image15);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }
        private void Image16_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isGameOver == false)
            {
            PointerPoint ptrPt = e.GetCurrentPoint(image16);
            down = ptrPt.Position;
            mouseDown = true;
            }
        }


        ////////////////////// Pointer Released for all images 1-16//////////////////////////////////////////////////////////////
        /// <summary>
        /// Image1_PointerExited to Image16_PointerExited allows the moving image tiles to work properly.
        /// </summary>
        //done
        private void Image1_PointerExited(object sender, PointerRoutedEventArgs e)
        {

            PointerPoint ptrPt = e.GetCurrentPoint(image1);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Right movement
            if (up.X > down.X && Tiles[1].blank == true && mouseDown == true)
            {
                temp = Tiles[1];
                Tiles[1] = Tiles[0];
                Tiles[0] = temp;
                // transfer image and position 1-16
                image1.Source = Tiles[0].image;
                image1.Name = Tiles[0].position.ToString();
                image2.Source = Tiles[1].image;
                image2.Name = Tiles[1].position.ToString();
            }
            //Down Movement
            else if (up.Y > down.Y && Tiles[4].blank == true && mouseDown == true)
            {
                temp = Tiles[4];
                Tiles[4] = Tiles[0];
                Tiles[0] = temp;

                image1.Source = Tiles[0].image;
                image1.Name = Tiles[0].position.ToString();
                image5.Source = Tiles[4].image;
                image5.Name = Tiles[4].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image2_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image2);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Right Move
            if (up.X > down.X && Tiles[2].blank == true && mouseDown == true)
            {
                temp = Tiles[2];
                Tiles[2] = Tiles[1];
                Tiles[1] = temp;

                image2.Source = Tiles[1].image;
                image2.Name = Tiles[1].position.ToString();
                image3.Source = Tiles[2].image;
                image3.Name = Tiles[2].position.ToString();

            }

            //Left Move
            else if (up.X < down.X && Tiles[0].blank == true && mouseDown == true)
            {
                temp = Tiles[0];
                Tiles[0] = Tiles[1];
                Tiles[1] = temp;

                image2.Source = Tiles[1].image;
                image2.Name = Tiles[1].position.ToString();
                image1.Source = Tiles[0].image;
                image1.Name = Tiles[0].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[5].blank == true && mouseDown == true)
            {
                temp = Tiles[5];
                Tiles[5] = Tiles[1];
                Tiles[1] = temp;

                image2.Source = Tiles[1].image;
                image2.Name = Tiles[1].position.ToString();
                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image3_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image3);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Left Move
            if (up.X < down.X && Tiles[1].blank == true && mouseDown == true)
            {
                temp = Tiles[1];
                Tiles[1] = Tiles[2];
                Tiles[2] = temp;

                image3.Source = Tiles[2].image;
                image3.Name = Tiles[2].position.ToString();
                image2.Source = Tiles[1].image;
                image2.Name = Tiles[1].position.ToString();

            }

            //Right Move
            if (up.X > down.X && Tiles[3].blank == true && mouseDown == true)
            {
                temp = Tiles[3];
                Tiles[3] = Tiles[2];
                Tiles[2] = temp;

                image3.Source = Tiles[2].image;
                image3.Name = Tiles[2].position.ToString();
                image4.Source = Tiles[3].image;
                image4.Name = Tiles[3].position.ToString();

            }

            //Down Move
            else if (up.Y > down.Y && Tiles[6].blank == true && mouseDown == true)
            {
                temp = Tiles[6];
                Tiles[6] = Tiles[2];
                Tiles[2] = temp;

                image3.Source = Tiles[2].image;
                image3.Name = Tiles[2].position.ToString();
                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image4_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image4);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Left Move
            if (up.X < down.X && Tiles[2].blank == true && mouseDown == true)
            {
                temp = Tiles[2];
                Tiles[2] = Tiles[3];
                Tiles[3] = temp;

                image4.Source = Tiles[3].image;
                image4.Name = Tiles[3].position.ToString();
                image3.Source = Tiles[2].image;
                image3.Name = Tiles[2].position.ToString();

            }

            //Down Move
            else if (up.Y > down.Y && Tiles[7].blank == true && mouseDown == true)
            {
                temp = Tiles[7];
                Tiles[7] = Tiles[3];
                Tiles[3] = temp;

                image4.Source = Tiles[3].image;
                image4.Name = Tiles[3].position.ToString();
                image8.Source = Tiles[7].image;
                image8.Name = Tiles[7].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image5_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image5);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Right Move
            if (up.X > down.X && Tiles[5].blank == true && mouseDown == true)
            {
                temp = Tiles[5];
                Tiles[5] = Tiles[4];
                Tiles[4] = temp;

                image5.Source = Tiles[4].image;
                image5.Name = Tiles[4].position.ToString();
                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();

            }


            //Up Move
            else if (up.Y < down.Y && Tiles[0].blank == true && mouseDown == true)
            {
                temp = Tiles[0];
                Tiles[0] = Tiles[4];
                Tiles[4] = temp;

                image5.Source = Tiles[4].image;
                image5.Name = Tiles[4].position.ToString();
                image1.Source = Tiles[0].image;
                image1.Name = Tiles[0].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[8].blank == true && mouseDown == true)
            {
                temp = Tiles[8];
                Tiles[8] = Tiles[4];
                Tiles[4] = temp;

                image5.Source = Tiles[4].image;
                image5.Name = Tiles[4].position.ToString();
                image9.Source = Tiles[8].image;
                image9.Name = Tiles[8].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image6_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image6);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Right Move
            if (up.X > down.X && Tiles[6].blank == true && mouseDown == true)
            {
                temp = Tiles[6];
                Tiles[6] = Tiles[5];
                Tiles[5] = temp;

                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();
                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();

            }

            //Left Move
            if (up.X < down.X && Tiles[4].blank == true && mouseDown == true)
            {
                temp = Tiles[4];
                Tiles[4] = Tiles[5];
                Tiles[5] = temp;

                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();
                image5.Source = Tiles[4].image;
                image5.Name = Tiles[4].position.ToString();

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[1].blank == true && mouseDown == true)
            {
                temp = Tiles[1];
                Tiles[1] = Tiles[5];
                Tiles[5] = temp;

                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();
                image2.Source = Tiles[1].image;
                image2.Name = Tiles[1].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[9].blank == true && mouseDown == true)
            {
                temp = Tiles[9];
                Tiles[9] = Tiles[5];
                Tiles[5] = temp;

                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();
                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image7_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image7);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Right Move
            if (up.X > down.X && Tiles[7].blank == true && mouseDown == true)
            {
                temp = Tiles[7];
                Tiles[7] = Tiles[6];
                Tiles[6] = temp;

                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();
                image8.Source = Tiles[7].image;
                image8.Name = Tiles[7].position.ToString();

            }

            //Left Move
            if (up.X < down.X && Tiles[5].blank == true && mouseDown == true)
            {
                temp = Tiles[5];
                Tiles[5] = Tiles[6];
                Tiles[6] = temp;

                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();
                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[2].blank == true && mouseDown == true)
            {
                temp = Tiles[2];
                Tiles[2] = Tiles[6];
                Tiles[6] = temp;

                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();
                image3.Source = Tiles[2].image;
                image3.Name = Tiles[2].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[10].blank == true && mouseDown == true)
            {
                temp = Tiles[10];
                Tiles[10] = Tiles[6];
                Tiles[6] = temp;

                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();
                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image8_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image8);
            up = ptrPt.Position;
            Tile temp = new Tile();


            //Left Move
            if (up.X < down.X && Tiles[6].blank == true && mouseDown == true)
            {
                temp = Tiles[6];
                Tiles[6] = Tiles[7];
                Tiles[7] = temp;

                image8.Source = Tiles[7].image;
                image8.Name = Tiles[7].position.ToString();
                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[3].blank == true && mouseDown == true)
            {
                temp = Tiles[3];
                Tiles[3] = Tiles[7];
                Tiles[7] = temp;

                image8.Source = Tiles[7].image;
                image8.Name = Tiles[7].position.ToString();
                image4.Source = Tiles[3].image;
                image4.Name = Tiles[3].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[11].blank == true && mouseDown == true)
            {
                temp = Tiles[11];
                Tiles[11] = Tiles[7];
                Tiles[7] = temp;

                image8.Source = Tiles[7].image;
                image8.Name = Tiles[7].position.ToString();
                image12.Source = Tiles[11].image;
                image12.Name = Tiles[11].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image9_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image9);
            up = ptrPt.Position;
            Tile temp = new Tile();


            //right Move
            if (up.X > down.X && Tiles[9].blank == true && mouseDown == true)
            {
                temp = Tiles[9];
                Tiles[9] = Tiles[8];
                Tiles[8] = temp;

                image9.Source = Tiles[8].image;
                image9.Name = Tiles[8].position.ToString();
                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[4].blank == true && mouseDown == true)
            {
                temp = Tiles[4];
                Tiles[4] = Tiles[8];
                Tiles[8] = temp;

                image9.Source = Tiles[8].image;
                image9.Name = Tiles[8].position.ToString();
                image5.Source = Tiles[4].image;
                image5.Name = Tiles[4].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[12].blank == true && mouseDown == true)
            {
                temp = Tiles[12];
                Tiles[12] = Tiles[8];
                Tiles[8] = temp;

                image9.Source = Tiles[8].image;
                image9.Name = Tiles[8].position.ToString();
                image13.Source = Tiles[12].image;
                image13.Name = Tiles[12].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image10_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image10);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //left Move
            if (up.X < down.X && Tiles[8].blank == true && mouseDown == true)
            {
                temp = Tiles[8];
                Tiles[8] = Tiles[9];
                Tiles[9] = temp;

                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();
                image9.Source = Tiles[8].image;
                image9.Name = Tiles[8].position.ToString();
            }

            //right Move
            if (up.X > down.X && Tiles[10].blank == true && mouseDown == true)
            {
                temp = Tiles[10];
                Tiles[10] = Tiles[9];
                Tiles[9] = temp;

                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();
                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[5].blank == true && mouseDown == true)
            {
                temp = Tiles[5];
                Tiles[5] = Tiles[9];
                Tiles[9] = temp;

                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();
                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[13].blank == true && mouseDown == true)
            {
                temp = Tiles[13];
                Tiles[13] = Tiles[9];
                Tiles[9] = temp;

                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();
                image14.Source = Tiles[13].image;
                image14.Name = Tiles[13].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image11_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image11);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //left Move
            if (up.X < down.X && Tiles[9].blank == true && mouseDown == true)
            {
                temp = Tiles[9];
                Tiles[9] = Tiles[10];
                Tiles[10] = temp;

                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();
                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();

            }

            //right Move
            if (up.X > down.X && Tiles[11].blank == true && mouseDown == true)
            {
                temp = Tiles[11];
                Tiles[11] = Tiles[10];
                Tiles[10] = temp;

                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();
                image12.Source = Tiles[11].image;
                image12.Name = Tiles[11].position.ToString();

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[6].blank == true && mouseDown == true)
            {
                temp = Tiles[6];
                Tiles[6] = Tiles[10];
                Tiles[10] = temp;

                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();
                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[14].blank == true && mouseDown == true)
            {
                temp = Tiles[14];
                Tiles[14] = Tiles[10];
                Tiles[10] = temp;

                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();
                image15.Source = Tiles[14].image;
                image15.Name = Tiles[14].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image12_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image12);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //left Move
            if (up.X < down.X && Tiles[10].blank == true && mouseDown == true)
            {
                temp = Tiles[10];
                Tiles[10] = Tiles[11];
                Tiles[11] = temp;

                image12.Source = Tiles[11].image;
                image12.Name = Tiles[11].position.ToString();
                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[7].blank == true && mouseDown == true)
            {
                temp = Tiles[7];
                Tiles[7] = Tiles[11];
                Tiles[11] = temp;

                image12.Source = Tiles[11].image;
                image12.Name = Tiles[11].position.ToString();
                image8.Source = Tiles[7].image;
                image8.Name = Tiles[7].position.ToString();
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[15].blank == true && mouseDown == true)
            {
                temp = Tiles[15];
                Tiles[15] = Tiles[11];
                Tiles[11] = temp;

                image12.Source = Tiles[11].image;
                image12.Name = Tiles[11].position.ToString();
                image16.Source = Tiles[15].image;
                image16.Name = Tiles[15].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image13_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image13);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Right Move
            if (up.X > down.X && Tiles[13].blank == true && mouseDown == true)
            {
                temp = Tiles[13];
                Tiles[13] = Tiles[12];
                Tiles[12] = temp;

                image13.Source = Tiles[12].image;
                image13.Name = Tiles[12].position.ToString();
                image14.Source = Tiles[13].image;
                image14.Name = Tiles[13].position.ToString();

            }


            //Up Move
            else if (up.Y < down.Y && Tiles[8].blank == true && mouseDown == true)
            {
                temp = Tiles[8];
                Tiles[8] = Tiles[12];
                Tiles[12] = temp;

                image13.Source = Tiles[12].image;
                image13.Name = Tiles[12].position.ToString();
                image9.Source = Tiles[8].image;
                image9.Name = Tiles[8].position.ToString();
            }

            mouseDown = false;
        }

        // done
        private void Image14_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image14);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Right Move
            if (up.X > down.X && Tiles[14].blank == true && mouseDown == true)
            {
                temp = Tiles[14];
                Tiles[14] = Tiles[13];
                Tiles[13] = temp;

                image14.Source = Tiles[13].image;
                image14.Name = Tiles[13].position.ToString();
                image15.Source = Tiles[14].image;
                image15.Name = Tiles[14].position.ToString();

            }

            //Left Move
            else if (up.X < down.X && Tiles[12].blank == true && mouseDown == true)
            {
                temp = Tiles[12];
                Tiles[12] = Tiles[13];
                Tiles[13] = temp;

                image14.Source = Tiles[13].image;
                image14.Name = Tiles[13].position.ToString();
                image13.Source = Tiles[12].image;
                image13.Name = Tiles[12].position.ToString();
            }

            //Up Move
            else if (up.Y < down.Y && Tiles[9].blank == true && mouseDown == true)
            {
                temp = Tiles[9];
                Tiles[9] = Tiles[13];
                Tiles[13] = temp;

                image14.Source = Tiles[13].image;
                image14.Name = Tiles[13].position.ToString();
                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image15_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image15);
            up = ptrPt.Position;
            Tile temp = new Tile();

            //Right Move
            if (up.X > down.X && Tiles[15].blank == true && mouseDown == true)
            {
                temp = Tiles[15];
                Tiles[15] = Tiles[14];
                Tiles[14] = temp;

                image15.Source = Tiles[14].image;
                image15.Name = Tiles[14].position.ToString();
                image16.Source = Tiles[15].image;
                image16.Name = Tiles[15].position.ToString();

            }

            //Left Move
            else if (up.X < down.X && Tiles[13].blank == true && mouseDown == true)
            {
                temp = Tiles[13];
                Tiles[13] = Tiles[14];
                Tiles[14] = temp;

                image15.Source = Tiles[14].image;
                image15.Name = Tiles[14].position.ToString();
                image14.Source = Tiles[13].image;
                image14.Name = Tiles[13].position.ToString();
            }

            //Up Move
            else if (up.Y < down.Y && Tiles[10].blank == true && mouseDown == true)
            {
                temp = Tiles[10];
                Tiles[10] = Tiles[14];
                Tiles[14] = temp;

                image15.Source = Tiles[14].image;
                image15.Name = Tiles[14].position.ToString();
                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }

        //done
        private void Image16_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image16);
            up = ptrPt.Position;
            Tile temp = new Tile();


            //Left Move
            if (up.X < down.X && Tiles[14].blank == true && mouseDown == true)
            {
                temp = Tiles[14];
                Tiles[14] = Tiles[15];
                Tiles[15] = temp;

                image16.Source = Tiles[15].image;
                image16.Name = Tiles[15].position.ToString();
                image15.Source = Tiles[14].image;
                image15.Name = Tiles[14].position.ToString();
            }

            //Up Move
            else if (up.Y < down.Y && Tiles[11].blank == true && mouseDown == true)
            {
                temp = Tiles[11];
                Tiles[11] = Tiles[15];
                Tiles[15] = temp;

                image16.Source = Tiles[15].image;
                image16.Name = Tiles[15].position.ToString();
                image12.Source = Tiles[11].image;
                image12.Name = Tiles[11].position.ToString();
            }
            checkWin();
            mouseDown = false;
        }
        /// <summary>
        /// captureImage_Click allows the user to upload a captured image file from the device camera.
        /// </summary>
        async private void captureImage_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            CameraCaptureUI cameraUI = new CameraCaptureUI();

            cameraUI.PhotoSettings.AllowCropping = false;
            cameraUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;
            //async call to use the camera
            Windows.Storage.StorageFile capturedMedia =
                await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            List<StorageFile> strList = new List<StorageFile>();

            if (capturedMedia != null)
            {
                uint counter = 0;
                BitmapImage img = new BitmapImage();
                // display preview image
                img = await LoadImage(capturedMedia);
                previewImage.Source = img;
                // resize for wide screen camera photo
                previewImage.Stretch = Stretch.Fill;
                using (var streamCamera = await capturedMedia.OpenAsync(FileAccessMode.Read))
                {

                    BitmapImage bitmapCamera = new BitmapImage();
                    bitmapCamera.SetSource(streamCamera);
                    // To display the image in a XAML image object
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(streamCamera);

                    // Convert the camera bitap to a WriteableBitmap object, 
                    // which is often a more useful format.

                    int width = bitmapCamera.PixelWidth;
                    int height = bitmapCamera.PixelHeight;
                    Tiles.Clear();
                    // create bitmap size of camera photo
                    WriteableBitmap wBitmap = new WriteableBitmap(width, height);
                    using (var stream = await capturedMedia.OpenAsync(FileAccessMode.Read))
                    {


                    }
                    for (uint y = 0; y <= 3; y++)
                    {

                        for (uint x = 0; x <= 3; x++)
                        {
                            InMemoryRandomAccessStream accessStream = new InMemoryRandomAccessStream();
                            BitmapEncoder encode = await BitmapEncoder.CreateForTranscodingAsync(accessStream, decoder);

                            // convert the entire bitmap to a 600px by 600px bitmap
                            encode.BitmapTransform.ScaledHeight = 600;
                            encode.BitmapTransform.ScaledWidth = 600;

                            BitmapBounds bounds = new BitmapBounds();
                            bounds.Height = 150;
                            bounds.Width = 150;
                            bounds.X = 150 * x;
                            bounds.Y = 150 * y;
                            encode.BitmapTransform.Bounds = bounds;

                            // write out to the stream
                            try
                            {
                                await encode.FlushAsync();
                            }
                            catch (Exception ex)
                            {
                                string s = ex.ToString();
                            }
                            BitmapImage bitmapImg = new BitmapImage();
                            bitmapImg.SetSource(accessStream);
                            // set each tile to image cut
                            Tile cam = new Tile();
                            cam.image = bitmapImg;
                            cam.position = counter;
                            Tiles.Add(cam);
                            counter++;
                        }
                    }

                }
                // randomize tiles
                Tiles = ShuffleList<Tile>(Tiles);
                // transfer image and position number containing correct order
                image1.Source = Tiles[0].image;
                image1.Name = Tiles[0].position.ToString();
                image2.Source = Tiles[1].image;
                image2.Name = Tiles[1].position.ToString();
                image3.Source = Tiles[2].image;
                image3.Name = Tiles[2].position.ToString();
                image4.Source = Tiles[3].image;
                image4.Name = Tiles[3].position.ToString();
                image5.Source = Tiles[4].image;
                image5.Name = Tiles[4].position.ToString();
                image6.Source = Tiles[5].image;
                image6.Name = Tiles[5].position.ToString();
                image7.Source = Tiles[6].image;
                image7.Name = Tiles[6].position.ToString();
                image8.Source = Tiles[7].image;
                image8.Name = Tiles[7].position.ToString();
                image9.Source = Tiles[8].image;
                image9.Name = Tiles[8].position.ToString();
                image10.Source = Tiles[9].image;
                image10.Name = Tiles[9].position.ToString();
                image11.Source = Tiles[10].image;
                image11.Name = Tiles[10].position.ToString();
                image12.Source = Tiles[11].image;
                image12.Name = Tiles[11].position.ToString();
                image13.Source = Tiles[12].image;
                image13.Name = Tiles[12].position.ToString();
                image14.Source = Tiles[13].image;
                image14.Name = Tiles[13].position.ToString();
                image15.Source = Tiles[14].image;
                image15.Name = Tiles[14].position.ToString();
                Tiles[15].image = new BitmapImage();

                Tiles[15].blank = true;
            }
        }
        /// <summary>
        /// playAgain_Click restarts game if winner clicks
        /// </summary>
        private void playAgain_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            InitializeComponent();
        	this.Frame.Navigate(typeof(PuzzlePage));
        }
        /// <summary>
        /// quitButton_Click quits application if winner clicks
        /// </summary>
        private void quitButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	Application.Current.Exit();
        }
    }

}
