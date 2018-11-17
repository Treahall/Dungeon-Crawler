﻿using System;
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
        int maxspeed = 240;
        public override void Draw(WriteableBitmap surface)
        {
            BitmapImage img = new BitmapImage(new Uri("../../VisualAssets/Actors/Player_Sprites/Idle/PlayerIdle_3.png", UriKind.Relative));
            WriteableBitmap bm = new WriteableBitmap(img);
            surface.Blit(new Point(Position.X, Position.Y), bm, new Rect(new Size(100, 100)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            
            base.Draw(surface);
        }

        bool jumping = false;
        int gforce = -120;
        int force;
        public override void move()
        {

            if(Keyboard.IsKeyDown(Key.Left))
            {
                if(Position.X >= 0) { Velocity = new System.Numerics.Vector2(-120, Velocity.Y); }
                
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                if(Position.X <= 400) { Velocity = new System.Numerics.Vector2(maxspeed, Velocity.Y); }
                
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
                    force += 12;
            }

            //Stop falling at the bottom
            if(Position.Y > 250 && jumping == true)
            {
                Velocity = new System.Numerics.Vector2(Velocity.X, 0); // sets vertical velocity to zero
                Position = new System.Numerics.Vector2(Position.X, 250); // resets the base vertical position
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