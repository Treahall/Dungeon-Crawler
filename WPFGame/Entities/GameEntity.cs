using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Windows.Media.Imaging.WriteableBitmapExtensions;
using WPFGame.stageGraphics;
using WPFGame.Animation;

namespace WPFGame.Entities
{
    //Is the abstract game entity and all entities should inherit from it.
    public abstract class GameEntity
    {
        public StageGraphic graphics = new StageGraphic();
        public Animations animationLists = new Animations();
        public int floor, health, AnimationIndex, frames = 0, Fpa = 10, speed;
        public bool FlipEntity;
        public List<string> animation = null;
        public double leftbound = 0, rightbound;
        

        //Stores character image size(pixels).
        public Size Mysize = new Size();

        public GameEntity()
        {
            FlipEntity = false;
            speed = 240;
            floor = (int)graphics.FloorPos.Y;
        }

        //Location of game entity. = (X, Y).
        public Vector2 Position { get; set; }

        public int GetSpriteHeight() { if(animation != null) return new BitmapImage(new Uri(animation[AnimationIndex], UriKind.Relative)).PixelHeight; else return 0; }

        //Velocity of game entity in pixels/sec. = (Change of X, Change of Y)
        public Vector2 Velocity { get; set; }

        //Called every frame, useses functions needed to update. //milliseconds passed = time since last execution.
        public virtual void GameTick(float millisecondsPassed)
        {
            SetVelocity();
            Position += Velocity * (millisecondsPassed / 1000f);
        }

        //sets the velocity for the next movement.
        public virtual void SetVelocity() { }

        //Entities draw themselves.
        public virtual void Draw(WriteableBitmap surface)
        {
            BitmapImage img = new BitmapImage(new Uri(animation[AnimationIndex], UriKind.Relative));
            WriteableBitmap bm = new WriteableBitmap(img);

            //Record image size in pixels
            Mysize = new Size(img.PixelWidth, img.PixelHeight);
            rightbound = graphics.WindowWidth - Mysize.Width;
            //Calculate what floor should be.
            floor = (int)(graphics.FloorPos.Y - Mysize.Height);

            //merge image onto screen sepreated for flipped horizontally for left versions of animations.
            if (FlipEntity)
                surface.Blit(new Point(Position.X, Position.Y), bm.Flip(new FlipMode()), new Rect(Mysize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            else
                surface.Blit(new Point(Position.X, Position.Y), bm, new Rect(Mysize), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            //Increases animation index every fpa frames 
            if (AnimationIndex >= animation.Count - 1)
                AnimationIndex = 0;
            else if ((frames += 1) > Fpa)
            {
                AnimationIndex += 1;
                frames = 0;
            }
        }
    }
}
