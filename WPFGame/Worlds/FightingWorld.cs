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
using WPFGame.Data;



namespace WPFGame.Worlds
{
    public class FightingWorld : World
    {
        public FightingWorld() : base()
        {
        }

        public override void calculateNumMaps()
        {
            numMaps = new StageGraphics().Backgrounds.Count;
        }

        public override void DrawStage(WriteableBitmap surface)
        {
            //loads the background
            BitmapImage bg = new BitmapImage(new Uri(new StageGraphics().Backgrounds[backgroundIndex], UriKind.Relative));
            WriteableBitmap bgWBM = new WriteableBitmap(source: bg);
            //loads the floor
            BitmapImage fl = new BitmapImage(new Uri(new StageGraphics().Floors[groundIndex], UriKind.Relative));
            WriteableBitmap flWBM = new WriteableBitmap(fl);

            //Merges them and writes to the Window
            bgWBM.Blit(new StageGraphics().FloorPos, flWBM, new Rect(new StageGraphics().FloorSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            surface.Blit(new StageGraphics().BackgroundPos, bgWBM, new Rect(new StageGraphics().BackgroundSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }
    }
}
