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
        public List<Enemy> GameEnemies { get; set; }
        public GameEntity User;
        public Stopwatch GameTimer { get; }
        
        public World()
        {
            calculateNumMaps();
            GameTimer = new Stopwatch();
            GameEnemies = new List<Enemy>();
            CreateStageIndexes();
        }

        public abstract void calculateNumMaps();
        public void AddEnemy(Enemy entity) { GameEnemies.Add(entity); }
        public void AddUser(GameEntity user) { User = user; }
        public void StartTimer() { GameTimer.Start(); }

        public void removeDeadEnemies()
        {
            List<Enemy> enemiesToRemove = new List<Enemy>();
            foreach(Enemy entity in GameEnemies)
            {
                if (entity.health <= 0) enemiesToRemove.Add(entity);
            }
            foreach(Enemy enemy in enemiesToRemove)
            {
                GameEnemies.Remove(enemy);
            }
            
        }

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
            User.GameTick(MillisecondsPassedSinceLastTick);

            foreach (var entity in GameEnemies)
                entity.GameTick(MillisecondsPassedSinceLastTick);

            removeDeadEnemies();

            previousGameTick = GameTimer.Elapsed;
        }

    }
}
