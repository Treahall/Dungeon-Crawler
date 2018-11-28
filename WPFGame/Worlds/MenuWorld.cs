using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFGame.Worlds
{
    public class MenuWorld : World
    {
        bool startGame = false;

        public MenuWorld() : base()
        {
        }

        public override void CreateStageIndexes()
        {
            backgroundIndex = 0;
            groundIndex = 1;
        }

        public override void calculateNumMaps()
        {
            //not needed. Never changes maps for menu
            return;
        }

        public override void DrawStage(WriteableBitmap surface)
        {
            //loads the background
            BitmapImage bg = new BitmapImage(new Uri(graphics.MainMenuGraphics[backgroundIndex], UriKind.Relative));
            WriteableBitmap bgWBM = new WriteableBitmap(source: bg);
            //loads the floor
            BitmapImage fl = new BitmapImage(new Uri(graphics.MainMenuGraphics[groundIndex], UriKind.Relative));
            WriteableBitmap flWBM = new WriteableBitmap(fl);

            //Merges them and writes to the Window
            bgWBM.Blit(graphics.FloorPos, flWBM, new Rect(graphics.FloorSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            surface.Blit(graphics.BackgroundPos, bgWBM, new Rect(graphics.BackgroundSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }
    }
}
