using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.Data;

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
            groundIndex = 0;
        }

        public override void calculateNumMaps()
        {
            //not needed. Never changes maps for menu
            return;
        }

        public override void DrawStage(WriteableBitmap surface)
        {
            //loads the background
            BitmapImage bg = new BitmapImage(new Uri(new StageGraphics().MainMenuGraphics[backgroundIndex], UriKind.Relative));
            WriteableBitmap bgWBM = new WriteableBitmap(source: bg);

            //loads the floor
            BitmapImage fl = new BitmapImage(new Uri(new StageGraphics().Floors[groundIndex], UriKind.Relative));
            WriteableBitmap flWBM = new WriteableBitmap(fl);

            //Merges them and writes to the Window
            surface.Blit(new Point(0, -(new StageGraphics().FloorSize.Height + 50)), bgWBM, new Rect(new Size(800, 600)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            surface.Blit(new StageGraphics().FloorPos, flWBM, new Rect(new StageGraphics().FloorSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }
    }
}
