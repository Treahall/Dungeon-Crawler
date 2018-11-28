using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WPFGame.stageGraphics
{
    public class StageGraphic
    {
        public Double WindowHeight;
        public Double WindowWidth;
        public Size FloorSize;
        public Point FloorPos;
        public Size BackgroundSize;
        public Point BackgroundPos;

        public StageGraphic()
        {
            WindowWidth = (double)Application.Current.Resources["WindowWidth"];
            WindowHeight = (double)Application.Current.Resources["WindowHeight"];
            FloorSize = new Size(WindowWidth, 100);
            BackgroundSize = new Size(WindowWidth, WindowHeight);
            FloorPos = new Point(0, (float)(WindowHeight - FloorSize.Height));
            BackgroundPos = new Point(0,0);
        }
        //Menu graphics stored as {background, floor}
        public List<string> MainMenuGraphics = new List<string> { "../../VisualAssets/Backgrounds/Main_Menu_BG.png", "../../VisualAssets/Floors/floor_0.png" };

        public List<string> Floors = new List<string> { "../../VisualAssets/Floors/floor_0.png" };
        public List<string> Backgrounds = new List<string> { "../../VisualAssets/Backgrounds/BG_0.png" };
    }


}
        

