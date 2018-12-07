using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private bool healthBought = false, damageBought = false;

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
                BitmapImage adviceImage = new BitmapImage(new Uri(StageGraphics.EnterDoorMessage, UriKind.Relative));
                WriteableBitmap advice = new WriteableBitmap(adviceImage);

                surface.Blit(new Point(StageGraphics.WindowWidth/2 - advice.PixelWidth/2, StageGraphics.WindowHeight - advice.PixelHeight), advice, 
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

        public void inChurchOperations()
        {
            if (damageBought && Keyboard.IsKeyUp(Key.D)) damageBought = false;
            if (healthBought && Keyboard.IsKeyUp(Key.H)) healthBought = false;

            if (Keyboard.IsKeyDown(Key.H) && !healthBought)
            {
                if (WorldUser.coins > 0)
                {
                    WorldUser.MaxHealth += 1;
                    WorldUser.coins -= 1;
                    healthBought = true;
                }
            }
            else if (Keyboard.IsKeyDown(Key.D) && !damageBought)
            {
                if (WorldUser.coins > 0)
                {
                    WorldUser.MaxDamage += 1;
                    WorldUser.coins -= 1;
                    damageBought = true;
                }
            }
        }

        public void tryDrawChurchMenu(WriteableBitmap surface)
        {
            if (InChurch)
            {
                Bitmap menu = new Bitmap(StageGraphics.ChurchMenu);

                using (Graphics g = Graphics.FromImage(menu))
                {
                    g.DrawString(WorldUser.MaxHealth.ToString(), new Font("Times New Roman", 34), System.Drawing.Brushes.Indigo, new System.Drawing.Point(75, 190));
                    g.DrawString(WorldUser.MaxDamage.ToString(), new Font("Times New Roman", 34), System.Drawing.Brushes.Indigo, new System.Drawing.Point(270, 190));
                    IntPtr hBitmap = menu.GetHbitmap();
                    try
                    {
                        BitmapSource Imagesource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero,
                            Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        WriteableBitmap ImageBitmap = new WriteableBitmap(Imagesource);

                        surface.Blit(new Point(StageGraphics.WindowWidth/2 - menu.Width/2, 50), ImageBitmap, new Rect(new Size((double)ImageBitmap.PixelWidth,
                            (double)ImageBitmap.PixelHeight)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
                    }
                    finally
                    {
                        DeleteObject(hBitmap);
                    }
                }
            }
        }

        public override void DrawStage(WriteableBitmap surface)
        {
            //loads the background
            BitmapImage bg = new BitmapImage(new Uri(StageGraphics.MainMenuGraphics[0], UriKind.Relative));
            WriteableBitmap bgWBM = new WriteableBitmap(source: bg);

            //Writes to the Window
            surface.Blit(StageGraphics.BackgroundPos, bgWBM, new Rect(StageGraphics.BackgroundSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            DisplayHearts(surface);
            DisplayCoins(surface);
            tryWritingAdvice(surface);
            tryDrawChurchMenu(surface);

        }

        public override void GameTick()
        {
            base.GameTick();
            tryEnteringBuilding();
            if(InChurch) inChurchOperations();
        }
    }
}
