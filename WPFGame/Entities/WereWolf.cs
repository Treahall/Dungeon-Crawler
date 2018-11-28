using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace WPFGame.Entities
{
    class WereWolf : Enemy
    {
        public WereWolf() : base() { }

        public override void setSpeed()
        {
            speed = int.Parse(enemydata.GetString("WereWolfSpeed"));
        }

        public override void SetVelocity()
        {
            CalculateDirection();
            switch (Currentdirection)
            {
                case Direction.idle:
                    Velocity = new System.Numerics.Vector2(0, 0);
                    animation = animationLists.WereWRun;
                    //animation = animationLists.WereWIdle;  change to idle.
                    break;
                case Direction.left:
                    Velocity = new System.Numerics.Vector2(-speed, 0);
                    animation = animationLists.WereWRun;
                    break;
                case Direction.right:
                    Velocity = new System.Numerics.Vector2(speed, 0);
                    animation = animationLists.WereWRun;
                    break;
            }
        }
    }
}
