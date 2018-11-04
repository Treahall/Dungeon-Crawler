using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPFGame.GameEntities
{
    //Is the abstract game entity and all entities should inherit from it.
    public abstract class GameEntity
    {
        //Location of game entity.
        public Vector2 Position { get; set; }

        //Velocity of game entity in pixels per second.
        public Vector2 Velocity { get; set; }

        //Contains game logic; milliseconds passed is how much time has passed since last execution.
        public virtual void GameTick(float millisecondsPassed)
        {
             
        }

        public virtual void move()
        {

        }

        //Entities draw themselves.
        public virtual void Draw(WriteableBitmap surface)
        {

        }
    }
}
