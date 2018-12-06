using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            List<int> bounds = new List<int> { (int)leftbound,(int)( StageGraphics.WindowWidth - GetSpriteSize().Width)}; //left/right
            int index = random.Next(2);
            Position = new System.Numerics.Vector2(bounds[index], floor - (int)GetSpriteSize().Height);

        }
        public abstract int getAttackDistance();

        public bool inAttackRange()
        {
            if (Math.Abs(Position.X - theUser.Position.X) <= getAttackDistance())
                return true;
            else
                return false;
        }

        //public bool PlayerFacingMe()
        //{
        //    //if player left and my pos is less then his
        //    if (theUser.FlipEntity && Position.X < theUser.Position.X)
        //    {
        //        return true;
        //    }
        //    //if player is right and my pos is more then his
        //    else if (!theUser.FlipEntity && Position.X >= theUser.Position.X)
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

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

        public bool closestEnemy()
        {
            if (theUser.enemies.Count == 0) return true;

            Enemy closestEnemy = theUser.enemies[0];
            //check if closest to enemy
            foreach (Enemy entity in theUser.enemies)
            {
                if (Math.Abs(entity.Position.X - entity.Position.X) < Math.Abs(closestEnemy.Position.X - entity.Position.X))
                {
                    closestEnemy = entity;
                }
            }

            if (this == closestEnemy)
            {
                return true;

            }
            else
                return false;
        }

        public override void CalculateDirection()
        {
            if (!closestEnemy() || inAttackRange())
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

            if (MyDirection == Direction.idle && inAttackRange())
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
            Position = new Vector2(Position.X, floor);
        }
    }
}
