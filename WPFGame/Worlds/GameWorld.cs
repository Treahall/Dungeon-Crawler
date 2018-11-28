using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.Entities;
using WPFGame.stageGraphics;



namespace WPFGame.Worlds
{
    public class GameWorld : World
    {
        
        public GameWorld() : base()
        {
        }

        public override void calculateNumMaps()
        {
            numMaps = graphics.Backgrounds.Count;
        }

        public override void DrawStage(WriteableBitmap surface)
        {
            //loads the background
            BitmapImage bg = new BitmapImage(new Uri(graphics.Backgrounds[backgroundIndex], UriKind.Relative));
            WriteableBitmap bgWBM = new WriteableBitmap(source: bg);
            //loads the floor
            BitmapImage fl = new BitmapImage(new Uri(graphics.Floors[groundIndex], UriKind.Relative));
            WriteableBitmap flWBM = new WriteableBitmap(fl);

            //Merges them and writes to the Window
            bgWBM.Blit(graphics.FloorPos, flWBM, new Rect(graphics.FloorSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            surface.Blit(graphics.BackgroundPos, bgWBM, new Rect(graphics.BackgroundSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }
    }
}
