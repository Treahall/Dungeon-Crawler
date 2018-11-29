using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WPFGame.Data
{
    public class StageGraphics
    {
        public StageGraphics() { }
        public Double WindowWidth = (double)Application.Current.Resources["WindowWidth"];
        public Double WindowHeight = (double)Application.Current.Resources["WindowHeight"];
        public Size FloorSize = new Size((double)Application.Current.Resources["WindowWidth"], 100);
        public Size BackgroundSize = new Size((double)Application.Current.Resources["WindowWidth"], (double)Application.Current.Resources["WindowHeight"]);
        public Point FloorPos = new Point(0, (float)(double)Application.Current.Resources["WindowHeight"] - 100);
        public Point BackgroundPos = new Point(0, 0);

        //Menu graphics stored as {background, floor}
        public List<string> MainMenuGraphics = new List<string> { "../../VisualAssets/Backgrounds/Main_Menu_BG.png", "../../VisualAssets/Floors/floor_0.png" };
        public List<string> Floors = new List<string> { "../../VisualAssets/Floors/floor_0.png" };
        public List<string> Backgrounds = new List<string> { "../../VisualAssets/Backgrounds/BG_0.png" };
    }

}
        

