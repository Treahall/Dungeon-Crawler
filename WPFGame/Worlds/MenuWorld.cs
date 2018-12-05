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
        public bool startGame = false;

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

        public override void DrawStage(WriteableBitmap surface)
        {
            //loads the background
            BitmapImage bg = new BitmapImage(new Uri(new StageGraphics().MainMenuGraphics[0], UriKind.Relative));
            WriteableBitmap bgWBM = new WriteableBitmap(source: bg);

            //Writes to the Window
            surface.Blit(new StageGraphics().BackgroundPos, bgWBM, new Rect(new StageGraphics().BackgroundSize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }
    }
}
