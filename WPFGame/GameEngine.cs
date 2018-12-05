using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WPFGame.Entities;
using WPFGame.Worlds;

namespace WPFGame
{
    public class GameEngine
    {
        

        public World CurrentWorld;

        private WorldCreator menuCreator;
        private WorldCreator dungeonCreator;
        private RandomEnemyCreator RandomEnemy;
        Player user;
        Enemy enemy;

        public GameEngine()
        {
            user = new Player();
            menuCreator = new MenuCreator(user);
            dungeonCreator = new DungeonCreator(user);
            RandomEnemy = new RandomEnemyCreator();
        }

        public void StartGame()
        {

            enemy = new Hound() { theUser = user };
            CurrentWorld = menuCreator.getWorld();
            CurrentWorld.AddEnemy(enemy);
            CurrentWorld.StartTimer();
        }

        public void FrameEvents(WriteableBitmap surface)
        {
            surface.Clear();
            CurrentWorld.DrawStage(surface);
            CurrentWorld.GameTick();

            foreach (Enemy enemy in CurrentWorld.GameEnemies)
            {
                enemy.Draw(surface);
            }
            user.Draw(surface);
            shareEnemiesWithUser();
        }

        public void shareEnemiesWithUser()
        {
            user.enemies = CurrentWorld.GameEnemies;
        }

    }
}
