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
    class GameEngine
    {
        World gameWorld;
        Player user;
        Enemy enemy;

        public GameEngine()
        {

        }

        public void StartGame()
        {
            user = new Player();
            gameWorld = new FightingWorld();
            enemy = new WereWolf() { theUser = user };

            gameWorld.AddUser(user);
            gameWorld.AddEnemy(enemy);
            gameWorld.StartTimer();
        }

        public void FrameEvents(WriteableBitmap surface)
        {
            surface.Clear();
            gameWorld.DrawStage(surface);
            gameWorld.GameTick();

            foreach (Enemy enemy in gameWorld.GameEnemies)
            {
                enemy.Draw(surface);
            }
            user.Draw(surface);
            shareEnemiesWithUser();
        }

        public void shareEnemiesWithUser()
        {
            user.enemies = gameWorld.GameEnemies;
        }

    }
}
