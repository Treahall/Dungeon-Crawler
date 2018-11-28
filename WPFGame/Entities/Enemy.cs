using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGame.stageGraphics;
using System.Resources;

namespace WPFGame.Entities
{
    public abstract class Enemy : GameEntity
    {
        public int PlayerX;

        public Enemy() : base()
        {
            setSpeed();
            setInitialPosition();
        }

        //Randomly sets first position to left or right of screen.
        void setInitialPosition()
        {
            Random random = new Random();
            List<int> bounds = new List<int> { 0, (int)graphics.WindowHeight };
            Position = new System.Numerics.Vector2((float)bounds[random.Next(bounds.Count)], (float)graphics.FloorPos.Y);
        }

        public void UpdatePlayerXResource()
        {
            
        }

        public override void CalculateDirection()
        {
            UpdatePlayerXResource();
            PreviousDirection = Currentdirection;

            //if difference in distance between player and enemy < 50
            if (Math.Abs(Position.X - int.Parse(savedData.GetString("PositionX"))) <= 50)
            {
                Currentdirection = Direction.idle;
            }
            else if ((Position.X - int.Parse(savedData.GetString("PositionX"))) < 0)
            {
                Currentdirection = Direction.left;
                FlipEntity = true;
            }
            else
            {
                Currentdirection = Direction.right;
                FlipEntity = false;
            }
        }
        public override abstract void SetVelocity();
    }
}
