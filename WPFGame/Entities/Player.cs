using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.GameEntities;
using static System.Windows.Media.Imaging.WriteableBitmapExtensions;


namespace WPFGame.Entities
{
    public class Player : GameEntity
    {
        //int speedstat, healthstat, damagestat; for stat changes.

        bool jumping = false;
        int jumpForce = -120, force;
        enum Direction { left, right, idle }
        Direction Currentdirection, PreviousDirection;

        public Player() : base()
        {
            animation = new Animations().CharacterIdol;
            AnimationIndex = 0;
            floor = (int)Math.Floor(WPFsize.Height) - 164; //164 is floor height
        }

        public override void Draw(WriteableBitmap surface)
        {
            base.Draw(surface);
        }

        public void JumpAlg()
        {
            //left/right bounds
            if (!(Position.X >= leftbound && Position.X <= rightbound))
            {
                //if on the right, set position in bounds on right edge
                if (Position.X >= WPFsize.Width/2)
                     Position = new System.Numerics.Vector2((float)rightbound, Position.Y);
                //if on the left, set position in bounds on left edge
                else
                    Position = new System.Numerics.Vector2(0, Position.Y);

                //set horizontal velocity to zero.
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);
            }

            //Stop falling at the bottom
            if (Position.Y > floor)
            {
                Velocity = new System.Numerics.Vector2(Velocity.X, 0); //sets vertical velocity to zero
                Position = new System.Numerics.Vector2(Position.X, floor); //resets the base vertical position
                jumping = false; 
                force = jumpForce;
                Fpa = 10;
                animation = new Animations().CharacterIdol;
            }
            else
            {
                force += 15;
                Velocity += new System.Numerics.Vector2(0, force);
            } 
        }

        public void CalculateDirection()
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
                AnimationIndex = 0;
                animation = new Animations().CharacterJump;
                Fpa = 5;
            }
        }

        public override void SetVelocity()
        {
            CalculateDirection();

            //wall bounds for when jumping.
            if (!(Position.X >= leftbound && Position.X <= rightbound))
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);

            //resets index when animation is changed.
            if (PreviousDirection != Currentdirection) AnimationIndex = 0;

            switch (Currentdirection)
            {
                case Direction.idle:
                    Velocity = new System.Numerics.Vector2(0, Velocity.Y);
                    if (!jumping) animation = new Animations().CharacterIdol;
                    break;
                case Direction.left:
                    if (Position.X >= leftbound) Velocity = new System.Numerics.Vector2(-speed, Velocity.Y);
                    if (!jumping) animation = new Animations().CharacterRun;
                    FlipEntity = true;
                    break;
                case Direction.right:
                    if (Position.X <= rightbound) Velocity = new System.Numerics.Vector2(speed, Velocity.Y);
                    if (!jumping) animation = new Animations().CharacterRun;
                    FlipEntity = false;
                    break;
            }
            if (jumping == true) JumpAlg();
        }

        public override void GameTick(float millisecondsPassed)
        {
            base.GameTick(millisecondsPassed);
        }
    }
}
