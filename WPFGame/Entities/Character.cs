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

namespace WPFGame.Entities
{
    public class Character : GameEntity
    {
        public override void Draw(WriteableBitmap surface)
        {
            BitmapImage img = new BitmapImage(new Uri("C:\\Users\\Trever\\source\\repos\\WPFGame\\WPFGame\\Entities\\34.png", UriKind.Relative));
            WriteableBitmap bm = new WriteableBitmap(img);
            surface.Blit(new Point(Position.X, Position.Y), bm, new Rect(new Size(200, 200)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            
            base.Draw(surface);
        }

        public override void move()
        {
            if(Keyboard.IsKeyDown(Key.Left))
            {
                Velocity = new System.Numerics.Vector2(-120, 0);
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                Velocity = new System.Numerics.Vector2(120, 0);
            }
            else
                Velocity = new System.Numerics.Vector2(0, 0);
        }

        public override void GameTick(float millisecondsPassed)
        {
            move();
            Position += Velocity * (millisecondsPassed/1000f);
        }
    }
}
