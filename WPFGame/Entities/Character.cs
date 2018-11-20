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

namespace WPFGame.Entities
{
    public class Character : GameEntity
    {
        
        int health;
        int floor = 400;
        int maxspeed = 240;

        //Make lists for images

        public override void Draw(WriteableBitmap surface)
        {
            BitmapImage img = new BitmapImage(new Uri("../../VisualAssets/Actors/Player_Sprites/Movement/Run/Right/PlayerRunR_0.png", UriKind.Relative));
            WriteableBitmap bm = new WriteableBitmap(img);
            surface.Blit(new Point(Position.X, Position.Y), bm, new Rect(new Size(75, 75)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            base.Draw(surface);
        }

        bool jumping = false;
        int gforce = -120;
        int force;
        public override void move()
        {
            if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Right))
            {
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);
            }

            else if(Keyboard.IsKeyDown(Key.Left) && Position.X >= 0)
            {
                Velocity = new System.Numerics.Vector2(-240, Velocity.Y);
                
            }

            else if (Keyboard.IsKeyDown(Key.Right) && Position.X < 700)
            {
                Velocity = new System.Numerics.Vector2(maxspeed, Velocity.Y);
                
            }
            else
                Velocity = new System.Numerics.Vector2(0, Velocity.Y);

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
