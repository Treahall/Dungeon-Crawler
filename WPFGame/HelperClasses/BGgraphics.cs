using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFGame
{
    class Graphics
    {
        public Size WindowSize;
        public Size FloorSize;
        public Size BackgroundSize;
        public List<string> Floors = new List<string> { "../../VisualAssets/Floors/floor_0.png" };
        public List<string> Backgrounds = new List<string> { "../../VisualAssets/Backgrounds/BG_0.png" };
    }
}
