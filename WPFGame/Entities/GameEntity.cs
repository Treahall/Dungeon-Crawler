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

namespace WPFGame.GameEntities
{
    //Is the abstract game entity and all entities should inherit from it.
    public abstract class GameEntity
    {
        public int floor, health, AnimationIndex, frames = 0, Fpa = 10, speed;
        public bool FlipEntity;
        public List<string> animation;
        public double leftbound = 0, rightbound;
        //stores height and width of WPF window
        public Rect WPFsize; 
        //Stores character image size(pixels).
        public Rect Mysize = new Rect();

        public GameEntity()
        {
            WPFsize = new Rect(new Size((double)Application.Current.Resources["WindowWidth"], (double)Application.Current.Resources["WindowHeight"]));
            FlipEntity = false;
            speed = 240;
        }

        //Location of game entity. = (X, Y).
        public Vector2 Position { get; set; }

        //Velocity of game entity in pixels/sec. = (Change of X, Change of Y)
        public Vector2 Velocity { get; set; }

        //Called everyframe and calls all the functions needed to update. //milliseconds passed = time since last execution.
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
            //make writeablebitmap from png at uri
            BitmapImage img = new BitmapImage(new Uri(animation[AnimationIndex], UriKind.Relative));
            WriteableBitmap bm = new WriteableBitmap(img);

            //Record image size in pixels
            Mysize = new Rect(new Size(img.PixelWidth, img.PixelHeight));
            rightbound = WPFsize.Width - Mysize.Width;

            //merge image onto screen sepreated for flipped horizontally for left versions of animations.
            if (FlipEntity)
                surface.Blit(new Point(Position.X, Position.Y), bm.Flip(new FlipMode()), new Rect(new Size(Mysize.Width, Mysize.Height)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            else
                surface.Blit(new Point(Position.X, Position.Y), bm, new Rect(new Size(Mysize.Width, Mysize.Height)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            //Indexes animationIndex every Fpa frames 
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
