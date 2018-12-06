using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WPFGame.Data
{
    public static class StageGraphics
    {
        public static Double WindowWidth = 787;
        public static Double WindowHeight = 600;
        public static Size BackgroundSize = new Size(787, 600);
        public static Point FloorPos = new Point(0, 600 - 98);
        public static Point BackgroundPos = new Point(0, 0);

        //Menu graphics stored as {background, floor}
        public static List<string> MainMenuGraphics = new List<string> { "../../VisualAssets/Backgrounds/Main_Menu_BG.png"};

        public static List<string> Backgrounds = new List<string>
        {
            "../../VisualAssets/Backgrounds/BG_0.png",
            "../../VisualAssets/Backgrounds/BG_1.png",
            "../../VisualAssets/Backgrounds/BG_2.png"

        };

        public static string EnterDoorMessage = "../../VisualAssets/Door/Door_text_white.png";
        public static string DoorGraphic = "../../VisualAssets/Door/door.png";
        public static string ChurchMenu = "../../VisualAssets/Churchmenu/upgrade_menu.png";
        public static string YouDied = "../../VisualAssets/Door/You_Died.png";
        public static string StartScreen = "../../VisualAssets/Door/StartMenu.png";

        public static string coin = "../../VisualAssets/Coin/Coin.png";

        public static List<string> Hearts = new List<string>
        {
            "../../VisualAssets/Hearts/dead.png",
            "../../VisualAssets/Hearts/1heart.png",
            "../../VisualAssets/Hearts/2hearts.png",
            "../../VisualAssets/Hearts/3hearts.png",
            "../../VisualAssets/Hearts/4hearts.png",
            "../../VisualAssets/Hearts/5hearts.png"
        };
    }

}
        

