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
        int maxspeed = 240;
        List<string> animation;
        int animationCounter;
        public Stopwatch AnimationTimer { get; }
        private TimeSpan PreviousAnimation;

        public Character()
        {
            AnimationTimer = new Stopwatch();
            animation = new Animations().CharacterIdol;
            animationCounter = 0;
        }

        

        //Make lists for images 200ms each

        public override void Draw(WriteableBitmap surface)
        {
            BitmapImage img = new BitmapImage(new Uri(animation[animationCounter], UriKind.Relative));
            WriteableBitmap bm = new WriteableBitmap(img);
            surface.Blit(new Point(Position.X, Position.Y), bm, new Rect(new Size(75, 75)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            if (animationCounter >= animation.Count-1)
            {
                animationCounter = 0;
            }
            else if((float)(AnimationTimer.Elapsed - PreviousAnimation).TotalMilliseconds >= 200)
            {
                animationCounter += 1;
                PreviousAnimation = AnimationTimer.Elapsed;
            }
                
            
            base.Draw(surface);
        }

        bool jumping = false;
        int gforce = -120;
        int force;
        public override void move()
        {
            //Don't move (Left and right cancel out)
            if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Right))
            {
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);
                if(animation != new Animations().CharacterIdol)
                {
                    animation = new Animations().CharacterIdol;
                    animationCounter = 0;
                }
                    
                

            }
            //Move Left
            else if(Keyboard.IsKeyDown(Key.Left) && Position.X >= 0)
            {
                Velocity = new System.Numerics.Vector2(-240, Velocity.Y);
                if(animation != new Animations().CharacterLeft)
                {
                    animation = new Animations().CharacterLeft;
                    animationCounter = 0;
                }
                
                
            }
            //Move Right
            else if (Keyboard.IsKeyDown(Key.Right) && Position.X < 700)
            {
                Velocity = new System.Numerics.Vector2(maxspeed, Velocity.Y);
                if(animation != new Animations().CharacterRight)
                {
                    animation = new Animations().CharacterRight;
                    animationCounter = 0;
                }
                
            }
            //Idle
            else
            {
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);
                if(animation != new Animations().CharacterIdol)
                {
                    animation = new Animations().CharacterIdol;
                    animationCounter = 0;
                }
                
            }
                

            //sperate for independent jumping action
            if (Keyboard.IsKeyDown(Key.Space))
            {
                if (jumping == false)
                {
                    jumping = true;
                    force = gforce;
                }

            }
            //allows character to jump with acceleration and deceleration
            if (jumping == true)
            {
                Velocity += new System.Numerics.Vector2(0, force);
                if(force < 120)
                    force += 15;
            }

            //Stop falling at the bottom
            if(Position.Y > floor && jumping == true)
            {
                Velocity = new System.Numerics.Vector2(Velocity.X, 0); // sets vertical velocity to zero
                Position = new System.Numerics.Vector2(Position.X, floor); // resets the base vertical position
                jumping = false; //stops jumping 
                force = gforce;
            }
        }

        public override void GameTick(float millisecondsPassed)
        {
            move();
            Position += Velocity * (millisecondsPassed/1000f);
        }
    }
}
