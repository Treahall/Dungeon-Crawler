using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGame.Data;
using WPFGame.Entities;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace WPFGame.Worlds
{
    public abstract class World
    {
        public int backgroundIndex, numMaps, PlayerXpos;
        public TimeSpan previousGameTick;
        public List<GameEntity> GameEntities { get; set; }
        public GameEntity User;
        public Stopwatch GameTimer { get; }
        
        public World()
        {
            calculateNumMaps();
            GameTimer = new Stopwatch();
            GameEntities = new List<GameEntity>();
            CreateStageIndexes();
        }

        public abstract void calculateNumMaps();
        public void AddEntity(GameEntity entity) { GameEntities.Add(entity); }
        public void AddUser(GameEntity user) { User = user; }
        public void StartTimer() { GameTimer.Start(); }

        public float MillisecondsPassedSinceLastTick
        {
            get { return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds; }
        }

        //gets index of random stage bg and floor.
        public virtual void CreateStageIndexes()
        {
            Random random = new Random();
            backgroundIndex = random.Next(numMaps);
        }

        public abstract void DrawStage(WriteableBitmap surface);

        public void GameTick()
        {
            foreach (var entity in GameEntities)
                entity.GameTick(MillisecondsPassedSinceLastTick);

            previousGameTick = GameTimer.Elapsed;
        }

    }
}
