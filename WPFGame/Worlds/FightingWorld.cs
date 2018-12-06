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
using System.Windows.Input;

namespace WPFGame.Worlds
{
    public class FightingWorld : World
    {

        private Point Doorpos;
        public FightingWorld() : base()
        {
        }

        public override void calculateNumMaps()
        {
            numMaps = new StageGraphics().Backgrounds.Count;
        }

        public void createExit(WriteableBitmap surface)
        {
            //draw Door
            BitmapImage DoorImage = new BitmapImage(new Uri(new StageGraphics().DoorGraphic, UriKind.Relative));
            WriteableBitmap Door = new WriteableBitmap(DoorImage);
            Doorpos = new Point(Door.PixelWidth/2, new StageGraphics().FloorPos.Y - (Door.PixelHeight + 1));
            surface.Blit(Doorpos, Door, new Rect(new Size(Door.PixelWidth, Door.PixelHeight)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }

        public void tryEnteringDoor()
        {
            if (Keyboard.IsKeyDown(Key.Enter) && !Leave && DrawExitDoor)
            {
                if ((WorldUser.Position.X - Doorpos.X + Doorpos.X / 2) <= 50)
                {
                    Leave = true;
                }
            }
        }
        public override void DrawStage(WriteableBitmap surface)
        {
            //loads the background
            BitmapImage bg = new BitmapImage(new Uri(new StageGraphics().Backgrounds[backgroundIndex], UriKind.Relative));
            WriteableBitmap bgWBM = new WriteableBitmap(source: bg);



            //writes to the Window
            surface.Blit(new StageGraphics().BackgroundPos, bgWBM, new Rect(new StageGraphics().BackgroundSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            DisplayHearts(surface);
            DisplayCoins(surface);
            if (DrawExitDoor) { createExit(surface); tryEnteringDoor();}
        }
    }
}
