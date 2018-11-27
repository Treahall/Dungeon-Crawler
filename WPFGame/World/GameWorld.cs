using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.GameEntities;

namespace WPFGame.World
{
    class GameWorld
    {
        public GameWorld()
        {
            GameEntities = new List<GameEntity>();
            GameTimer = new Stopwatch();
        }

        public List<GameEntity> GameEntities { get; set; }
        public Stopwatch GameTimer { get;}

        public void AddEntity(GameEntity entity)
        {
            GameEntities.Add(entity);
        }

        private TimeSpan previousGameTick;

        public float MillisecondsPassedSinceLastTick
        {
            get
            {
                return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds;
            }
        }

        public void StartTimer()
        {
            GameTimer.Start();
        }

        public void DrawStage(string stageURI, WriteableBitmap surface)
        {   //Draws the floor
            BitmapImage imga = new BitmapImage(new Uri("../../VisualAssets/Tiles/floor_light.png", UriKind.Relative));
            WriteableBitmap bma = new WriteableBitmap(imga);
            surface.Blit(new Point(0, 472), bma, new Rect(new Size(800, 100)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }

        public void GameTick()
        {

            foreach (var entity in GameEntities)
                entity.GameTick(MillisecondsPassedSinceLastTick);

            previousGameTick = GameTimer.Elapsed;
            
        }

    }
}
