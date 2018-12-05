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
        }

        public override void LoadAnimations()
        {
            idleAnimation = new Animations().WereWIdle;
            CurrentAnimation = new Animations().WereWRun;
            previousAnimation = new Animations().WereWRun;
            runAnimation = new Animations().WereWRun;
            attackAnimation = new Animations().WereWAtk;
        }

        public override void setSpeed()
        {
            speed = 175;
        }

    }
}
