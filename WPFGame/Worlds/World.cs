using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGame.stageGraphics;
using WPFGame.Entities;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace WPFGame.Worlds
{
    abstract class World
    {

        public int backgroundIndex, groundIndex, numMaps;
        public TimeSpan previousGameTick;
        public List<GameEntity> GameEntities { get; set; }
        public StageGraphic graphics;
        public Stopwatch GameTimer { get; }

        public World()
        {
            GameTimer = new Stopwatch();
            GameEntities = new List<GameEntity>();
            graphics = new StageGraphic();
            CreateStageIndexes();
        }

        public void AddEntity(GameEntity entity) { GameEntities.Add(entity); }
        public void StartTimer() { GameTimer.Start(); }

        public float MillisecondsPassedSinceLastTick
        {
            get
            {
                return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds;
            }
        }

        public void CreateStageIndexes()
        {
            Random random = new Random();
            backgroundIndex = random.Next(numMaps);
            groundIndex = random.Next(numMaps);
        }

        public abstract void DrawStage(WriteableBitmap surface);



    }
}
