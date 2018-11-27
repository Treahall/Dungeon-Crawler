using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFGame.GameEntities;
using WPFGame;
using System.Diagnostics;

namespace WPFGame.Entities
{
    public class Character : GameEntity
    {

        int health;
        int floor = 400;
        int rightspeed = 240;
        int leftspeed = -240;

        List<string> animation;
        int AnimationIndex;
        int frames = 0;

        public Character()
        {
            animation = new Animations().CharacterIdol;
            AnimationIndex = 0;
            jumping = false;
        }

        public override void Draw(WriteableBitmap surface)
        {
            BitmapImage img = new BitmapImage(new Uri(animation[AnimationIndex], UriKind.Relative));
            WriteableBitmap bm = new WriteableBitmap(img);
            surface.Blit(new Point(Position.X, Position.Y), bm, new Rect(new Size(75, 75)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            if (AnimationIndex >= animation.Count - 1)
            {
                AnimationIndex = 0;
            }
            else
            {
                if ((frames += 1) > 10)
                {
                    AnimationIndex += 1;
                    frames = 0;
                }
                

            }
                
            
            base.Draw(surface);
        }

        bool jumping;
        int gforce = -120;
        int force;
        public void RunJumpAlg()
        {
            //wall bounds for when jumping
            if (Position.X < 0)
            {
                Position = new System.Numerics.Vector2(0, Position.Y);
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);
            }
            if (Position.X > 700)
            {
                Position = new System.Numerics.Vector2(700, Position.Y);
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);
            }
 
            //Stop falling at the bottom
            if (Position.Y > floor)
            {
                Velocity = new System.Numerics.Vector2(Velocity.X, 0); // sets vertical velocity to zero
                Position = new System.Numerics.Vector2(Position.X, floor); // resets the base vertical position
                jumping = false; //stops jumping 
                force = gforce;
            }
            else
            {
                force += 15;
                Velocity += new System.Numerics.Vector2(0, force);
            } 
        }

        enum Direction { left, right, idle}
        Direction Currentdirection;
        Direction PreviousDirection;
        public void CalculateDirection()
        {
            PreviousDirection = Currentdirection;
            //Don't move (Left and right cancel out)
            if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Right) && Position.Y == floor)
            {
                Currentdirection = Direction.idle;
            }
            //Move left
            else if (Keyboard.IsKeyDown(Key.Left) && Position.X > 0)
            {
                Currentdirection = Direction.left;
            }
            // Move right
            else if (Keyboard.IsKeyDown(Key.Right) && Position.X < 700)
            {
                Currentdirection = Direction.right;
            }
            // Idle
            else if (Position.Y == floor)
            {
                Currentdirection = Direction.idle;
            }

            //sperated for independent jumping action
            if (Keyboard.IsKeyDown(Key.Space) && jumping == false)
            {
                jumping = true;
            }
        }

        public override void SetVelocity()
        {
            CalculateDirection();
            if (PreviousDirection != Currentdirection)
            {
                AnimationIndex = 0;
                switch (Currentdirection)
                {
                    case Direction.idle:
                        Velocity = new System.Numerics.Vector2(0, Velocity.Y);
                        animation = new Animations().CharacterIdol;
                        break;
                    case Direction.left:
                        if(Position.X >= 0) Velocity = new System.Numerics.Vector2(leftspeed, Velocity.Y);
                        animation = new Animations().CharacterLeft;
                        break;
                    case Direction.right:
                        if(Position.X <= 700) Velocity = new System.Numerics.Vector2(rightspeed, Velocity.Y);
                        animation = new Animations().CharacterRight;
                        break;
                }


            }
        }

        public override void GameTick(float millisecondsPassed)
        {
            SetVelocity();
            if (jumping == true)
            {
                RunJumpAlg();
            }
            Position += Velocity * (millisecondsPassed/1000f);
        }
    }
}
