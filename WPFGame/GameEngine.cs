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
        int UserXposition;
        Player user;

        public GameEngine()
        {

        }

        public void StartGame()
        {
            user = new Player();
            gameWorld = new MenuWorld();
            gameWorld.AddUser (user);
            gameWorld.StartTimer();
        }

        public void FrameEvents(WriteableBitmap surface)
        {
            surface.Clear();
            gameWorld.DrawStage(surface);
            gameWorld.GameTick();

            foreach (GameEntity entity in gameWorld.GameEntities)
            {
                entity.Draw(surface);
            }
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
        }

    }
}
