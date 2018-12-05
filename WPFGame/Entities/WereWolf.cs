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
        public WereWolf() : base()
        {
            setAnimations();
        }

        public override void setAnimations()
        {
            idleAnimation = new Animations().WereWIdle;
            CurrentAnimation = new Animations().WereWIdle;
            previousAnimation = new Animations().WereWIdle;
            runAnimation = new Animations().WereWRun;
            attackAnimation = new Animations().WereWAtk;
        }

        public override void setSpeed()
        {
            speed = 175;
            //setanimation to idle when I get it
            setInitialPosition();
        }

        public override void SetVelocity()
        {

        }

    }
}
