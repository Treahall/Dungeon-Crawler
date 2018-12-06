using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPFGame.Entities;
using WPFGame.Worlds;

namespace WPFGame
{
    public class GameEngine
    {
        public enum WorldType { menu, dungeon};
        public WorldType currentworldtype;
        public World CurrentWorld;

        public int level = 0, numEnemies = 0;
        private bool pause = false;
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
            RandomEnemy = new RandomEnemyCreator(user);
        }

        public void StartGame()
        {
            CurrentWorld = menuCreator.getWorld();
            currentworldtype = WorldType.menu;
            CurrentWorld.StartTimer();
        }

        public void tryTogglePause()
        {
            
            if (Keyboard.IsKeyDown(Key.P))
            {
                pause = true;
                CurrentWorld.GameTimer.Reset();
                CurrentWorld.previousGameTick = CurrentWorld.GameTimer.Elapsed;

            }
            else if (Keyboard.IsKeyDown(Key.U))
            {
                pause = false;
                CurrentWorld.GameTimer.Start();
            }
        }

        public void ChangeWorldIfNeeded()
        {
            if (CurrentWorld.Leave)
            {
                if (currentworldtype == WorldType.menu)
                {
                    CurrentWorld = dungeonCreator.getWorld();
                    currentworldtype = WorldType.dungeon;
                    level += 1;
                    numEnemies = level;
                }
                else
                {
                    CurrentWorld = menuCreator.getWorld();
                    currentworldtype = WorldType.menu;
                }
                user.refreshStats();
                CurrentWorld.StartTimer();
            }

        }

        public void dungeonWorldOperations()
        {
            if (numEnemies <= 0 && CurrentWorld.GameEnemies.Count <= 0)
            {
                CurrentWorld.DrawExitDoor = true;
            }
            else if(numEnemies > 0 && user.enemies.Count < 1)
            {
                CurrentWorld.AddEnemy(RandomEnemy.getEnemy());
                numEnemies -= 1;
            }
        }

        public void gameEngineTick()
        {
            ChangeWorldIfNeeded();
            tryTogglePause();

            if(currentworldtype == WorldType.dungeon)
                dungeonWorldOperations();
        }

        public void FrameEvents(WriteableBitmap surface)
        {
            

            if (!pause)
            {
                gameEngineTick();
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
            else
                tryTogglePause();
        }

        public void shareEnemiesWithUser()
        {
            user.enemies = CurrentWorld.GameEnemies;
        }

    }
}
