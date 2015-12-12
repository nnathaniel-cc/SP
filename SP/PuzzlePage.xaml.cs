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


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PuzzlePage : Page
    {
        static bool mouseDown = false;
        List<Tile> Tiles = new List<Tile>();
        String ImagePath;
        Point down = new Point();
        Point up = new Point();

        public PuzzlePage()
        {
            this.InitializeComponent();

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

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

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

            // Check to make sure user picked a file
            if (file != null)
            {
                uint counter = 0;

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

                image1.Source = Tiles[0].image;
                image2.Source = Tiles[1].image;
                image3.Source = Tiles[2].image;
                image4.Source = Tiles[3].image;
                image5.Source = Tiles[4].image;
                image6.Source = Tiles[5].image;
                image7.Source = Tiles[6].image;
                image8.Source = Tiles[7].image;
                image9.Source = Tiles[8].image;
                image10.Source = Tiles[9].image;
                image11.Source = Tiles[10].image;
                image12.Source = Tiles[11].image;
                image13.Source = Tiles[12].image;
                image14.Source = Tiles[13].image;
                image15.Source = Tiles[14].image;
                Tiles[15].image = new BitmapImage();
                Tiles[15].blank = true;

            }
        }


        /////////////////////////////////// Pointer Pressed method events for each image 1-16///////////////////////////////////////////////

        private void Image1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image1);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image2);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image3_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image3);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image4_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image4);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image5_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image5);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image6_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image6);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image7_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image7);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image8_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image8);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image9_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image9);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image10_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image10);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image11_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image11);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image12_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image12);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image13_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image13);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image14_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image14);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image15_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image15);
            down = ptrPt.Position;
            mouseDown = true;
        }
        private void Image16_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(image16);
            down = ptrPt.Position;
            mouseDown = true;
        }


        ////////////////////// Pointer Released for all images 1-16//////////////////////////////////////////////////////////////

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

                image1.Source = Tiles[0].image;
                image2.Source = Tiles[1].image;

            }
            //Down Movement
            else if (up.Y > down.Y && Tiles[4].blank == true && mouseDown == true)
            {
                temp = Tiles[4];
                Tiles[4] = Tiles[0];
                Tiles[0] = temp;

                image1.Source = Tiles[0].image;
                image5.Source = Tiles[4].image;
            }

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
                image3.Source = Tiles[2].image;

            }

            //Left Move
            else if (up.X < down.X && Tiles[0].blank == true && mouseDown == true)
            {
                temp = Tiles[0];
                Tiles[0] = Tiles[1];
                Tiles[1] = temp;

                image2.Source = Tiles[1].image;
                image1.Source = Tiles[0].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[5].blank == true && mouseDown == true)
            {
                temp = Tiles[5];
                Tiles[5] = Tiles[1];
                Tiles[1] = temp;

                image2.Source = Tiles[1].image;
                image6.Source = Tiles[5].image;
            }

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
                image2.Source = Tiles[1].image;

            }

            //Right Move
            if (up.X > down.X && Tiles[3].blank == true && mouseDown == true)
            {
                temp = Tiles[3];
                Tiles[3] = Tiles[2];
                Tiles[2] = temp;

                image3.Source = Tiles[2].image;
                image4.Source = Tiles[3].image;

            }

            //Down Move
            else if (up.Y > down.Y && Tiles[6].blank == true && mouseDown == true)
            {
                temp = Tiles[6];
                Tiles[6] = Tiles[2];
                Tiles[2] = temp;

                image3.Source = Tiles[2].image;
                image7.Source = Tiles[6].image;
            }

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
                image3.Source = Tiles[2].image;

            }

            //Down Move
            else if (up.Y > down.Y && Tiles[7].blank == true && mouseDown == true)
            {
                temp = Tiles[7];
                Tiles[7] = Tiles[3];
                Tiles[3] = temp;

                image4.Source = Tiles[3].image;
                image8.Source = Tiles[7].image;
            }

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
                image6.Source = Tiles[5].image;

            }


            //Up Move
            else if (up.Y < down.Y && Tiles[0].blank == true && mouseDown == true)
            {
                temp = Tiles[0];
                Tiles[0] = Tiles[4];
                Tiles[4] = temp;

                image5.Source = Tiles[4].image;
                image1.Source = Tiles[0].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[8].blank == true && mouseDown == true)
            {
                temp = Tiles[8];
                Tiles[8] = Tiles[4];
                Tiles[4] = temp;

                image5.Source = Tiles[4].image;
                image9.Source = Tiles[8].image;
            }

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
                image7.Source = Tiles[6].image;

            }

            //Left Move
            if (up.X < down.X && Tiles[4].blank == true && mouseDown == true)
            {
                temp = Tiles[4];
                Tiles[4] = Tiles[5];
                Tiles[5] = temp;

                image6.Source = Tiles[5].image;
                image5.Source = Tiles[4].image;

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[1].blank == true && mouseDown == true)
            {
                temp = Tiles[1];
                Tiles[1] = Tiles[5];
                Tiles[5] = temp;

                image6.Source = Tiles[5].image;
                image2.Source = Tiles[1].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[9].blank == true && mouseDown == true)
            {
                temp = Tiles[9];
                Tiles[9] = Tiles[5];
                Tiles[5] = temp;

                image6.Source = Tiles[5].image;
                image10.Source = Tiles[9].image;
            }

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
                image8.Source = Tiles[7].image;

            }

            //Left Move
            if (up.X < down.X && Tiles[5].blank == true && mouseDown == true)
            {
                temp = Tiles[5];
                Tiles[5] = Tiles[6];
                Tiles[6] = temp;

                image7.Source = Tiles[6].image;
                image6.Source = Tiles[5].image;

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[2].blank == true && mouseDown == true)
            {
                temp = Tiles[2];
                Tiles[2] = Tiles[6];
                Tiles[6] = temp;

                image7.Source = Tiles[6].image;
                image3.Source = Tiles[2].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[10].blank == true && mouseDown == true)
            {
                temp = Tiles[10];
                Tiles[10] = Tiles[6];
                Tiles[6] = temp;

                image7.Source = Tiles[6].image;
                image11.Source = Tiles[10].image;
            }

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
                image7.Source = Tiles[6].image;

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[3].blank == true && mouseDown == true)
            {
                temp = Tiles[3];
                Tiles[3] = Tiles[7];
                Tiles[7] = temp;

                image8.Source = Tiles[7].image;
                image4.Source = Tiles[3].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[11].blank == true && mouseDown == true)
            {
                temp = Tiles[11];
                Tiles[11] = Tiles[7];
                Tiles[7] = temp;

                image8.Source = Tiles[7].image;
                image12.Source = Tiles[11].image;
            }

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
                image10.Source = Tiles[9].image;

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[4].blank == true && mouseDown == true)
            {
                temp = Tiles[4];
                Tiles[4] = Tiles[8];
                Tiles[8] = temp;

                image9.Source = Tiles[8].image;
                image5.Source = Tiles[4].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[12].blank == true && mouseDown == true)
            {
                temp = Tiles[12];
                Tiles[12] = Tiles[8];
                Tiles[8] = temp;

                image9.Source = Tiles[8].image;
                image13.Source = Tiles[12].image;
            }

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
                image9.Source = Tiles[8].image;

            }

            //right Move
            if (up.X > down.X && Tiles[10].blank == true && mouseDown == true)
            {
                temp = Tiles[10];
                Tiles[10] = Tiles[9];
                Tiles[9] = temp;

                image10.Source = Tiles[9].image;
                image11.Source = Tiles[10].image;

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[5].blank == true && mouseDown == true)
            {
                temp = Tiles[5];
                Tiles[5] = Tiles[9];
                Tiles[9] = temp;

                image10.Source = Tiles[9].image;
                image6.Source = Tiles[5].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[13].blank == true && mouseDown == true)
            {
                temp = Tiles[13];
                Tiles[13] = Tiles[9];
                Tiles[9] = temp;

                image10.Source = Tiles[9].image;
                image14.Source = Tiles[13].image;
            }

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
                image10.Source = Tiles[9].image;

            }

            //right Move
            if (up.X > down.X && Tiles[11].blank == true && mouseDown == true)
            {
                temp = Tiles[11];
                Tiles[11] = Tiles[10];
                Tiles[10] = temp;

                image11.Source = Tiles[10].image;
                image12.Source = Tiles[11].image;

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[6].blank == true && mouseDown == true)
            {
                temp = Tiles[6];
                Tiles[6] = Tiles[10];
                Tiles[10] = temp;

                image11.Source = Tiles[10].image;
                image7.Source = Tiles[6].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[14].blank == true && mouseDown == true)
            {
                temp = Tiles[14];
                Tiles[14] = Tiles[10];
                Tiles[10] = temp;

                image11.Source = Tiles[10].image;
                image15.Source = Tiles[14].image;
            }

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
                image11.Source = Tiles[10].image;

            }

            //Up Move
            else if (up.Y < down.Y && Tiles[7].blank == true && mouseDown == true)
            {
                temp = Tiles[7];
                Tiles[7] = Tiles[11];
                Tiles[11] = temp;

                image12.Source = Tiles[11].image;
                image8.Source = Tiles[7].image;
            }

            //Down Move
            else if (up.Y > down.Y && Tiles[15].blank == true && mouseDown == true)
            {
                temp = Tiles[15];
                Tiles[15] = Tiles[11];
                Tiles[11] = temp;

                image12.Source = Tiles[11].image;
                image16.Source = Tiles[15].image;
            }

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
                image14.Source = Tiles[13].image;

            }


            //Up Move
            else if (up.Y < down.Y && Tiles[8].blank == true && mouseDown == true)
            {
                temp = Tiles[8];
                Tiles[8] = Tiles[12];
                Tiles[12] = temp;

                image13.Source = Tiles[12].image;
                image9.Source = Tiles[8].image;
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
                image15.Source = Tiles[14].image;

            }

            //Left Move
            else if (up.X < down.X && Tiles[12].blank == true && mouseDown == true)
            {
                temp = Tiles[12];
                Tiles[12] = Tiles[13];
                Tiles[13] = temp;

                image14.Source = Tiles[13].image;
                image13.Source = Tiles[12].image;
            }

            //Up Move
            else if (up.Y < down.Y && Tiles[9].blank == true && mouseDown == true)
            {
                temp = Tiles[9];
                Tiles[9] = Tiles[13];
                Tiles[13] = temp;

                image14.Source = Tiles[13].image;
                image10.Source = Tiles[9].image;
            }

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
                image16.Source = Tiles[15].image;

            }

            //Left Move
            else if (up.X < down.X && Tiles[13].blank == true && mouseDown == true)
            {
                temp = Tiles[13];
                Tiles[13] = Tiles[14];
                Tiles[14] = temp;

                image15.Source = Tiles[14].image;
                image14.Source = Tiles[13].image;
            }

            //Up Move
            else if (up.Y < down.Y && Tiles[10].blank == true && mouseDown == true)
            {
                temp = Tiles[10];
                Tiles[10] = Tiles[14];
                Tiles[14] = temp;

                image15.Source = Tiles[14].image;
                image11.Source = Tiles[10].image;
            }

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
                image15.Source = Tiles[14].image;
            }

            //Up Move
            else if (up.Y < down.Y && Tiles[11].blank == true && mouseDown == true)
            {
                temp = Tiles[11];
                Tiles[11] = Tiles[15];
                Tiles[15] = temp;

                image16.Source = Tiles[15].image;
                image12.Source = Tiles[11].image;
            }

            mouseDown = false;
        }

        /////////////////////////
    }

}
