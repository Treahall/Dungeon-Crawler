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
            CurrentHealth = 50;
            damage = (int)((double)10 / attackingFpa);
        }

        public override void LoadAnimations()
        {
            idleAnimation = Animations.WereWIdle;
            CurrentAnimation = Animations.WereWRun;
            previousAnimation = Animations.WereWRun;
            runAnimation = Animations.WereWRun;
            attackAnimation = Animations.WereWAtk;
            damageindex = attackAnimation.Count / 2;
        }

        public override int getAttackDistance()
        {
            return 50;
        }


        public override void setSpeed()
        {
            speed = 175;
        }

    }
}
