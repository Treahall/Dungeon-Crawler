using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.Data;
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
        public bool pause = false, dead = false, restart = false;
        private WorldCreator menuCreator;
        private WorldCreator dungeonCreator;
        private RandomEnemyCreator RandomEnemy;
        Player user;

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

        public void deathOperations(WriteableBitmap surface)
        {
            pause = true;
            CurrentWorld.GameTimer.Reset();
            CurrentWorld.previousGameTick = CurrentWorld.GameTimer.Elapsed;
            BitmapImage DeathMessageImage = new BitmapImage(new Uri(StageGraphics.YouDied, UriKind.Relative));
            WriteableBitmap DeathMessage = new WriteableBitmap(DeathMessageImage);

            surface.Blit(new Point(StageGraphics.WindowWidth/2 - DeathMessage.PixelWidth/2, StageGraphics.WindowHeight/2 - DeathMessage.PixelHeight/2), DeathMessage,
                new Rect(new Size(DeathMessage.PixelWidth, DeathMessage.PixelHeight)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha );

            if (Keyboard.IsKeyDown(Key.Escape))
            {
                Application.Current.Shutdown();
            }
            else if (Keyboard.IsKeyDown(Key.Enter))
            {
                restart = true;
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

            if (user.CurrentHealth <= 0 && currentworldtype == WorldType.dungeon)
                dead = true;
            if (dead == true)
                deathOperations(surface);

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
