using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.Data;
using System.Windows.Interop;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace WPFGame.Worlds
{
    public class MenuWorld : World
    {
        private int DungeonDoorCenter = 600;
        private int ChurchDoorCenter = 100;

        public MenuWorld() : base()
        {
        }

        public override void CreateStageIndexes()
        {

        }

        public override void calculateNumMaps()
        {
            //not needed. Never changes maps for menu
            return;
        }

        private bool isNearDungeon()
        {
            if (Math.Abs(WorldUser.Position.X - DungeonDoorCenter) <= 50)
            {
                return true;
            }
            else return false;
        }

        private bool isNearChurch()
        {
            if (Math.Abs(WorldUser.Position.X - ChurchDoorCenter) <= 50)
            {
                return true;
            }
            else return false;
        }

        public void tryWritingAdvice(WriteableBitmap surface)
        {
            if (isNearDungeon() || isNearChurch() && !InChurch)
            {
                BitmapImage adviceImage = new BitmapImage(new Uri(new StageGraphics().EnterDoorMessage, UriKind.Relative));
                WriteableBitmap advice = new WriteableBitmap(adviceImage);

                surface.Blit(new Point(new StageGraphics().WindowWidth/2 - advice.PixelWidth/2, new StageGraphics().WindowHeight - advice.PixelHeight), advice, 
                    new Rect(new Size((double) advice.PixelWidth, (double) advice.PixelHeight)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            }
        }

        public void tryEnteringBuilding()
        {
            if (Keyboard.IsKeyDown(Key.Enter) && !Leave && !InChurch)
            {
                if(isNearDungeon())
                    Leave = true;
                if (isNearChurch())
                    InChurch = true;
            }

            if (Keyboard.IsKeyDown(Key.Escape))
            {
                InChurch = false;
            }
        }

        public void tryDrawChurchMenu(WriteableBitmap source)
        {
            if (InChurch)
            {
                //BitmapImage menuImage = new BitmapImage(new Uri(new StageGraphics().ChurchMenu, UriKind.Relative));

                //Graphics g = Graphics.FromImage(Image.FromFile(new StageGraphics().ChurchMenu));
                //Bitmap bm = new Bitmap(menuImage.PixelWidth, menuImage.PixelHeight, g);
                //BitmapSource bmsource = Imaging.CreateBitmapSourceFromHBitmap(bm.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                //WriteableBitmap menuBitmap = new WriteableBitmap(bmsource);


                //Point menuPos = new Point((int)new StageGraphics().WindowWidth/2 - menuBitmap.PixelWidth/2, (int)new StageGraphics().WindowHeight/2 - menuBitmap.PixelHeight/2 );

                //source.Blit(menuPos, menuBitmap, new Rect(0,0,menuBitmap.PixelWidth,menuBitmap.PixelHeight), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);


            }
        }

        public override void DrawStage(WriteableBitmap surface)
        {
            //loads the background
            BitmapImage bg = new BitmapImage(new Uri(new StageGraphics().MainMenuGraphics[0], UriKind.Relative));
            WriteableBitmap bgWBM = new WriteableBitmap(source: bg);

            //Writes to the Window
            surface.Blit(new StageGraphics().BackgroundPos, bgWBM, new Rect(new StageGraphics().BackgroundSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            DisplayHearts(surface);
            DisplayCoins(surface);
            tryWritingAdvice(surface);
            tryDrawChurchMenu(surface);

        }

        public override void GameTick()
        {
            base.GameTick();
            tryEnteringBuilding();
        }
    }
}
