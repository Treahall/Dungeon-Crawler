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
        int numMaps = new Graphics().Floors.Count;
        private TimeSpan previousGameTick;
        public List<GameEntity> GameEntities { get; set; }
        public Stopwatch GameTimer { get; }

        public GameWorld()
        {
            GameEntities = new List<GameEntity>();
            GameTimer = new Stopwatch();
        }

        public void AddEntity(GameEntity entity)
        {
            GameEntities.Add(entity);
        }

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

        public void DrawStage(WriteableBitmap surface)
        {
            Random random = new Random();

            //Draw the background
            BitmapImage img = new BitmapImage(new Uri(new Graphics().Backgrounds[random.Next(numMaps)], UriKind.Relative));
            WriteableBitmap bma = new WriteableBitmap(img);
            surface.Blit(new Point(0,0), bma, new Rect(new Size(800, 600)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            //Draws the floor
            img = new BitmapImage(new Uri(new Graphics().Floors[random.Next(numMaps)], UriKind.Relative));
            bma = new WriteableBitmap(img);
            surface.Blit(new Point(0, 472), bma, new Rect(new Size(800, 100)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }

        public void GameTick()
        {
            //draw Background
            foreach (var entity in GameEntities)
                entity.GameTick(MillisecondsPassedSinceLastTick);

            previousGameTick = GameTimer.Elapsed;
            
        }

    }
}
