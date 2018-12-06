using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGame.Data;

namespace WPFGame.Entities
{
    class Mandrake : Enemy
    {
        public Mandrake() : base()
        {
            CurrentHealth = 30;
            damage = (int)((double)2 / attackingFpa);
        }

        public override void LoadAnimations()
        {
            idleAnimation = new Animations().MandrakeRun;
            CurrentAnimation = new Animations().MandrakeSpawn;
            previousAnimation = new Animations().MandrakeSpawn;
            runAnimation = new Animations().MandrakeRun;
            attackAnimation = new Animations().MandrakeAtk;
            damageindex = attackAnimation.Count / 2;
        }

        public override void setSpeed()
        {
            speed = 100;
        }

        public override int getAttackDistance()
        {
            return 30;
        }
    }
}
