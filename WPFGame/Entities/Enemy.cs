using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGame.Data;
using System.Resources;

namespace WPFGame.Entities
{
    public abstract class Enemy : GameEntity
    {
        public Enemy() : base()
        {

        }

        //Randomly sets first position to off left or right of screen.
        public void setInitialPosition()
        {
            Random random = new Random();
            //List<int> bounds = new List<int> { GetSpriteHeight(), (int)new StageGraphics().WindowWidth - 1}; //left/right
            Position = new System.Numerics.Vector2(200, 415);
        }

        public override void CalculateDirection()
        {
            PreviousDirection = Currentdirection;

            //if difference in distance between player and enemy < 50
            if (Math.Abs(Position.X - PlayerXpos) <= 20)
            {
                Currentdirection = Direction.idle;
            }
            else if ((Position.X - PlayerXpos) < 0)
            {
                Currentdirection = Direction.right;
                FlipEntity = true;
            }
            else
            {
                Currentdirection = Direction.left;
                FlipEntity = false;
            }
        }

        public override void SetVelocity()
        {

        }

        public override void GameTick(float millisecondsPassed)
        {
            SetVelocity();
            Position += Velocity * (millisecondsPassed / 1000f);
        }
    }
}
