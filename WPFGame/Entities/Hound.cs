using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGame.Data;

namespace WPFGame.Entities
{
    class Hound : Enemy
    {
        public Hound() : base()
        {
            CurrentHealth = 40;
            damage = (int)((double)5 / attackingFpa);
        }

        public override void LoadAnimations()
        {
            idleAnimation = Animations.HoundIdle;
            CurrentAnimation = Animations.HoundRun;
            previousAnimation = Animations.HoundRun;
            runAnimation = Animations.HoundRun;
            attackAnimation = Animations.HoundAtk;
            damageindex = attackAnimation.Count / 2;
        }

        public override void setSpeed()
        {
            speed = 150;
        }
        public override int getAttackDistance()
        {
            return 50;
        }
    }
}
