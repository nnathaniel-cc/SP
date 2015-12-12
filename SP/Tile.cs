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
