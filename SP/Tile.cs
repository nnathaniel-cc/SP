/*
 * FILE : Tile.cs
 * PROJECT : Option 1 - Puzzle Game
 * PROGRAMMER : Brodie Paterson, Nicholas Nathaniel
 * FIRST VERSION : 2015-12-17
 * DESCRIPTION : This file contains the Tile class for holding each image tile position, image and blank 16th tile.
 *           
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace SP
{
    class Tile
    {
        public uint position { get; set; }
        public BitmapImage image { get; set; }
        public bool blank { get; set; }
    }
}
