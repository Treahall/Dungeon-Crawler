using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using WPFGame.Data;

namespace WPFGame.Entities
{
    class WereWolf : Enemy
    {
        public WereWolf() : base() { animation = new Animations().WereWRun; }

        public override void setSpeed()
        {
            speed = 60;
            //setanimation to idle when I get it
            setInitialPosition();
        }

        public override void SetVelocity()
        {
            CalculateDirection();
            switch (Currentdirection)
            {
                case Direction.idle:
                    Velocity = new System.Numerics.Vector2(0, 0);
                    animation = new Animations().WereWRun;
                    //animation = animationLists.WereWIdle;  change to idle.
                    break;
                case Direction.left:
                    Velocity = new System.Numerics.Vector2(-speed, 0);
                    animation = new Animations().WereWRun;
                    break;
                case Direction.right:
                    Velocity = new System.Numerics.Vector2(speed, 0);
                    animation = new Animations().WereWRun;
                    break;
            }
        }

    }
}
