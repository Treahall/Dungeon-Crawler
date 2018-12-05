using System;
using System.Resources;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.Entities;
using WPFGame.Data;
using static System.Windows.Media.Imaging.WriteableBitmapExtensions;
using System.Collections;

namespace WPFGame.Entities
{
    public class Player : GameEntity
    {
        bool jumping, falling;
        int jumpForce = 400, force;
        public List<int> enemyPositions;
        public List<string> jumpAnimation;
        

        public Player() : base()
        {
            enemyPositions = new List<int>();
            force = -jumpForce;

            setAnimations();

            jumping = false; falling = false;
            //Initial position
            Position = new System.Numerics.Vector2((float)(new StageGraphics().WindowWidth/2), floor -= GetSpriteHeight());
        }

        public override void setSpeed()
        {
            speed = 240;
        }

        public override void Draw(WriteableBitmap surface)
        {
            base.Draw(surface);
        }

        public override void setAnimations()
        {
            CurrentAnimation = new Animations().CharacterIdle;
            previousAnimation = new Animations().CharacterIdle;
            idleAnimation = new Animations().CharacterIdle;
            attackAnimation = new Animations().CharacterAtk;
            runAnimation = new Animations().CharacterRun;
            jumpAnimation = new Animations().CharacterJump;
        }

        public void RunJumpAlg()
        {
            //wall bounds for when jumping
            if (!(Position.X >= leftbound && Position.X <= rightbound))
            {
                //if further right
                if (Position.X >= new StageGraphics().WindowWidth/2)
                     Position = new System.Numerics.Vector2((float)rightbound, Position.Y);
                //if further left
                else
                    Position = new System.Numerics.Vector2(0, Position.Y);

                Velocity = new System.Numerics.Vector2(0, Velocity.Y);
            }

            //Stop falling at the bottom
            if (Position.Y > floor)
            {
                Velocity = new System.Numerics.Vector2(Velocity.X, 0); // sets vertical velocity to zero
                Position = new System.Numerics.Vector2(Position.X, floor); // resets the base vertical position
                jumping = false; //stops jumping 
                falling = false; 
                force = -jumpForce;
            }
            else if (force < jumpForce)
            {
                force += 15;
                Velocity = new System.Numerics.Vector2(Velocity.X, force);
            }

            if (force >= 0)
            {
                falling = true;
            }
        }



        public override void CalculateDirection()
        {
            //Don't move (Left and right cancel out)
            if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Right) && !jumping && !attacking)
                Currentdirection = Direction.idle;
            //Move left
            else if (Keyboard.IsKeyDown(Key.Left) && Position.X > leftbound)
                Currentdirection = Direction.left;
            // Move right
            else if (Keyboard.IsKeyDown(Key.Right) && Position.X < rightbound)
                Currentdirection = Direction.right;
            // Idle
            else if(!jumping && !attacking)
                Currentdirection = Direction.idle;

            //sperated for independent jumping action
            if (Keyboard.IsKeyDown(Key.Space) && !jumping)
            {
                jumping = true;
            }

            if (Keyboard.IsKeyDown(Key.V) && !attacking)
            {
                attacking = true;
                Fpa = 5;
            }

        }

        public override void SetVelocity()
        {
            CalculateDirection();

            //Independent wall bounds for when jumping.
            if (!(Position.X >= leftbound && Position.X <= rightbound))
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);

            switch (Currentdirection)
            {
                case Direction.idle:
                    Velocity = new System.Numerics.Vector2(0, Velocity.Y);
                    break;
                case Direction.left:
                    if (Position.X >= leftbound) Velocity = new System.Numerics.Vector2(-speed, Velocity.Y);
                    break;
                case Direction.right:
                    if (Position.X <= rightbound) Velocity = new System.Numerics.Vector2(speed, Velocity.Y);
                    break;
            }
            if (jumping) RunJumpAlg();

            if (attacking)
            {
                if (AnimationIndex >= new Animations().CharacterAtk.Count - 1) { attacking = false; Fpa = 10; } 
            }

            setanimation();

        }

        public void setanimation()
        {
            previousAnimation = CurrentAnimation;

            switch (Currentdirection)
            {
                case Direction.idle:
                        CurrentAnimation = idleAnimation;
                    break;
                case Direction.left:
                        CurrentAnimation = runAnimation;
                    FlipEntity = true;
                    break;
                case Direction.right:
                        CurrentAnimation = runAnimation;
                    FlipEntity = false;
                    break;
            }

            if (attacking)
            {
                CurrentAnimation = attackAnimation;
            }
            else if (jumping && !falling)
            {
                CurrentAnimation = jumpAnimation;
            }

            if (previousAnimation != CurrentAnimation) AnimationIndex = 0;
        }

        public override void GameTick(float millisecondsPassed)
        {
            base.GameTick(millisecondsPassed);
        }
    }
}
