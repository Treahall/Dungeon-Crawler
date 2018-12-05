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
        public Player theUser;
        public int damageindex;

        public Enemy() : base()
        {
            LoadAnimations();
            setInitialPosition();
        }

        //Randomly sets first position to off left or right of screen.
        public void setInitialPosition()
        {
            Random random = new Random();
            List<int> bounds = new List<int> { (int)leftbound,(int)( new StageGraphics().WindowWidth - GetSpriteSize().Width)}; //left/right
            int index = random.Next(2);
            Position = new System.Numerics.Vector2(bounds[index], floor -= (int)GetSpriteSize().Height);

        }
        public abstract int getAttackDistance();
        public abstract bool inAttackRange();

        public override void TakeDamage()
        {
            if (inAttackRange())
            {
                //if in range and the user is exactly mid attack then take damage and parry enemy attack
                if (theUser.attacking && theUser.AnimationIndex == damageindex)
                {
                    CurrentHealth -= theUser.damage;
                    attacking = false;
                }
            }
        }

        public override void CalculateDirection()
        {
            if (inAttackRange())
            {
                MyDirection = Direction.idle;
            }
            else if ((Position.X - theUser.Position.X) < 0)
            {
                MyDirection = Direction.right;
                FlipEntity = false; 
            }
            else
            {
                MyDirection = Direction.left;
                FlipEntity = true;
            }

            if (MyDirection == Direction.idle && !attacking)
            {
                attacking = true;
                Fpa = attackingFpa;
            }
        }

        public override void SetVelocity()
        {
            CalculateDirection();

            switch (MyDirection)
            {
                case Direction.idle:
                    Velocity = new System.Numerics.Vector2(0, 0);
                    CurrentAnimation = idleAnimation;
                    break;
                case Direction.left:
                    Velocity = new System.Numerics.Vector2(-speed, 0);
                    CurrentAnimation = runAnimation;
                    break;
                case Direction.right:
                    Velocity = new System.Numerics.Vector2(speed, 0);
                    CurrentAnimation = runAnimation;
                    break;
            }

            if (attacking)
            {
                if (AnimationIndex >= attackAnimation.Count - 1) { attacking = false; Fpa = 10; AnimationIndex = 0; }
            }

            setAnimation();
        }

        public override void setAnimation()
        {
            previousAnimation = CurrentAnimation;

            switch (MyDirection)
            {
                case Direction.idle:
                    CurrentAnimation = idleAnimation;
                    break;
                case Direction.left:
                    CurrentAnimation = runAnimation;
                    FlipEntity = false;
                    break;
                case Direction.right:
                    CurrentAnimation = runAnimation;
                    FlipEntity = true;
                    break;
            }

            if (attacking)
                CurrentAnimation = attackAnimation;

           if (previousAnimation != CurrentAnimation && !attacking)
                AnimationIndex = 0;
        }

        public override void GameTick(float millisecondsPassed)
        {
            base.GameTick(millisecondsPassed);
        }
    }
}
