﻿using System;
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

        public Player() : base()
        {
            enemyPositions = new List<int>();
            force = -jumpForce;
            animation = new Animations().CharacterIdle;
            AnimationIndex = 0;
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
                jumping = false; falling = false; //stops jumping 
                force = -jumpForce;
                animation = new Animations().CharacterIdle;
            }
            else if (force < jumpForce)
            {
                force += 15;
                Velocity = new System.Numerics.Vector2(Velocity.X, force);
            }
            if (force >= 0)
            {
                if(falling == false)
                {
                    falling = true;
                    AnimationIndex = 0;
                }
            }
        }



        public override void CalculateDirection()
        {
            PreviousDirection = Currentdirection;

            //Don't move (Left and right cancel out)
            if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Right) && !jumping)
                Currentdirection = Direction.idle;
            //Move left
            else if (Keyboard.IsKeyDown(Key.Left) && Position.X > leftbound)
                Currentdirection = Direction.left;
            // Move right
            else if (Keyboard.IsKeyDown(Key.Right) && Position.X < rightbound)
                Currentdirection = Direction.right;
            // Idle
            else if(!jumping)
                Currentdirection = Direction.idle;

            //sperated for independent jumping action
            if (Keyboard.IsKeyDown(Key.Space) && !jumping)
            {
                jumping = true;
                if(attacking == false)
                    AnimationIndex = 0;
                animation = new Animations().CharacterJump;
            }

            if (Keyboard.IsKeyDown(Key.V) && !attacking)
            {
                attacking = true;
                AnimationIndex = 0;
                animation = new Animations().CharacterJump;
                Fpa = 5;
            }

        }

        public override void SetVelocity()
        {
            CalculateDirection();

            //Independent wall bounds for when jumping.
            if (!(Position.X >= leftbound && Position.X <= rightbound))
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);

            //resets index when animation is changed.
            if (PreviousDirection != Currentdirection && !jumping && !attacking) AnimationIndex = 0;

            switch (Currentdirection)
            {
                case Direction.idle:
                    Velocity = new System.Numerics.Vector2(0, Velocity.Y);
                    if (!jumping || falling) { animation = new Animations().CharacterIdle; }
                    break;
                case Direction.left:
                    if (Position.X >= leftbound) Velocity = new System.Numerics.Vector2(-speed, Velocity.Y);
                    if (!jumping || falling) { animation = new Animations().CharacterRun; }
                    FlipEntity = true;
                    break;
                case Direction.right:
                    if (Position.X <= rightbound) Velocity = new System.Numerics.Vector2(speed, Velocity.Y);
                    if (!jumping || falling) { animation = new Animations().CharacterRun; }
                    FlipEntity = false;
                    break;
            }
            if (jumping) RunJumpAlg();

            if (attacking)
            {
                animation = new Animations().CharacterAtk;
                if (AnimationIndex >= new Animations().CharacterAtk.Count - 1) { attacking = false; Fpa = 10; } 
            }

        }

        public override void GameTick(float millisecondsPassed)
        {
            base.GameTick(millisecondsPassed);
        }
    }
}
