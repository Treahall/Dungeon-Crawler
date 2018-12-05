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
        private int UserXposition;
        Player user;
        Enemy enemy;

        public GameEngine()
        {

        }

        public void StartGame()
        {
            user = new Player();
            gameWorld = new FightingWorld();
            //enemy = new WereWolf();

            gameWorld.AddUser(user);
            //gameWorld.AddEntity(enemy);
            gameWorld.StartTimer();
        }

        public void FrameEvents(WriteableBitmap surface)
        {
            surface.Clear();
            gameWorld.DrawStage(surface);
            gameWorld.GameTick();

            user.Draw(surface);

            foreach (GameEntity entity in gameWorld.GameEntities)
            {
                entity.Draw(surface);
            }

            sharePositions();
        }

        public void sharePositions()
        { 
            user.enemyPositions.Clear();

            foreach (GameEntity entity in gameWorld.GameEntities)
            {
                user.enemyPositions.Add((int)entity.Position.X);
                entity.PlayerXpos = (int)user.Position.X;
            }

            gameWorld.PlayerXpos = (int)user.Position.X;
            UserXposition = (int)user.Position.X;
        }

    }
}
