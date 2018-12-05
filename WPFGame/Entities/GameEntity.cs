using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Windows.Media.Imaging.WriteableBitmapExtensions;
using WPFGame.Data;
using System.Resources;

namespace WPFGame.Entities
{
    //Is the abstract game entity and all entities should inherit from it.
    public abstract class GameEntity
    {
        public int floor = (int)(new StageGraphics().FloorPos.Y);

        public int health, healthstat, damage, damagestat, PlayerXpos, AnimationIndex = 0, AttackIndex, speed;
        public int frames = 0, Fpa = 10;
        public double leftbound = 0, rightbound;
        public bool FlipEntity, attacking;

        //stores all animations for an entity
        public List<string> CurrentAnimation = null;
        public List<string> previousAnimation = null;
        public List<string> attackAnimation = null;
        public List<string> runAnimation = null;
        public List<string> idleAnimation = null;

        public Vector2 Position { get; set; } //Location of game entity. = (X, Y).
        public Vector2 Velocity { get; set; } //Velocity of game entity in pixels/sec. = (Change of X, Change of Y)

        public enum Direction { left, right, idle }
        public Direction MyDirection;

        /// <summary>
        /// end of variables ///////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        public GameEntity()
        {
            setSpeed();
            FlipEntity = false; attacking = false;
        }

        public abstract void setSpeed();
        public abstract void CalculateDirection();
        public abstract void SetVelocity(); //sets the velocity for the next movement.
        public abstract void LoadAnimations();
        public abstract void setAnimation();

        public Size GetSpriteSize()
        {
            if (CurrentAnimation != null)
            {
                return new Size((double)new BitmapImage(new Uri(CurrentAnimation[AnimationIndex], UriKind.Relative)).PixelWidth, 
                    (double)new BitmapImage(new Uri(CurrentAnimation[AnimationIndex], UriKind.Relative)).PixelHeight);
            }
            else return new Size(0,0);
        }

        //Called every frame, useses functions needed to update. //milliseconds passed = time since last execution.
        public virtual void GameTick(float millisecondsPassed)
        {
            SetVelocity();
            Position += Velocity * (millisecondsPassed / 1000f);
        }
        
        //Entities draw themselves.
        public virtual void Draw(WriteableBitmap surface)
        {
            BitmapImage img = new BitmapImage(new Uri(CurrentAnimation[AnimationIndex], UriKind.Relative));
            WriteableBitmap bm = new WriteableBitmap(img);

            //Record image size in pixels
            rightbound = new StageGraphics().WindowWidth - GetSpriteSize().Width;

            //Calculate what floor should be.
            floor = (int)(new StageGraphics().FloorPos.Y - GetSpriteSize().Height);

            //merge image onto screen sepreated for flipped horizontally for left versions of animations.
            if (FlipEntity)
                surface.Blit(new Point(Position.X, Position.Y), bm.Flip(new FlipMode()), new Rect(GetSpriteSize()), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
            else
                surface.Blit(new Point(Position.X, Position.Y), bm, new Rect(GetSpriteSize()), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            //Increases animation index every fpa frames 
            if (AnimationIndex >= CurrentAnimation.Count - 1)
                AnimationIndex = 0;
            else if ((frames += 1) > Fpa)
            {
                AnimationIndex += 1;
                frames = 0;
            }
        }
    }
}
