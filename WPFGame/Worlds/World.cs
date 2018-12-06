using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGame.Data;
using WPFGame.Entities;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Drawing.Brushes;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace WPFGame.Worlds
{
    public abstract class World
    {
        public bool Leave = false, InChurch = false, doordrawn = false;
        public int backgroundIndex, numMaps;
        public TimeSpan previousGameTick;
        public List<Enemy> GameEnemies { get; set; }
        public Player WorldUser = null;
        public Stopwatch GameTimer { get; }
        
        public World()
        {
            calculateNumMaps();
            GameTimer = new Stopwatch();
            GameEnemies = new List<Enemy>();
            CreateStageIndexes();
        }

        public abstract void calculateNumMaps();
        public void AddEnemy(Enemy entity) { GameEnemies.Add(entity); }
        public void AddUser(Player user) { WorldUser = user; }
        public void StartTimer() { GameTimer.Start(); }

        public void removeDeadEnemies()
        {
            List<Enemy> enemiesToRemove = new List<Enemy>();
            foreach(Enemy entity in GameEnemies)
            {
                if (entity.CurrentHealth <= 0) enemiesToRemove.Add(entity);
            }
            foreach(Enemy enemy in enemiesToRemove)
            {
                GameEnemies.Remove(enemy);
            }

            WorldUser.coins += enemiesToRemove.Count;

        }

        public void DisplayHearts(WriteableBitmap surface)
        {
            if (WorldUser == null) return;

            int indexToUse = 0;
            List<int> percents = new List<int> { 0, 20, 40, 60, 80, 100 };
            double myHealthPercent = ((double) WorldUser.CurrentHealth / WorldUser.MaxHealth) * 100;
            for (int i = 0; i <= percents.Count - 2; i += 1)
            {
                if (myHealthPercent > percents[i] && myHealthPercent <= percents[i+1])
                    indexToUse = i+1;
            }
            //create WriteableBitmap from image source
            BitmapImage heartsImage = new BitmapImage(new Uri(new StageGraphics().Hearts[indexToUse], UriKind.Relative));
            WriteableBitmap hearts = new WriteableBitmap(heartsImage);
            //Merge it to the surface at the top left
            surface.Blit(new Point(0, 0), hearts, new Rect(new Size((double)hearts.PixelWidth, (double)hearts.PixelHeight)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }

        public void DisplayCoins(WriteableBitmap surface)
        {
            Bitmap coinBitmap = new Bitmap(new StageGraphics().coin);
            Bitmap coinAndText = new Bitmap(coinBitmap.Width + 30, coinBitmap.Height);

            using (Graphics g = Graphics.FromImage(coinAndText))
            {
                g.Clear(System.Drawing.Color.Transparent);
                g.DrawImage(coinBitmap, 0, 0, coinBitmap.Width, coinBitmap.Height);
                g.DrawString(WorldUser.coins.ToString(), new Font("Times New Roman", 8), Brushes.Gold, new System.Drawing.Point(coinBitmap.Width, 5));
            }
            
            //Bitmap textImage = new Bitmap(coinBitmap.Width, coinBitmap.Height, g);
            BitmapSource Imagesource = Imaging.CreateBitmapSourceFromHBitmap(coinAndText.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            WriteableBitmap ImageBitmap = new WriteableBitmap(Imagesource);

            surface.Blit(new Point(0, 40), ImageBitmap ,new Rect(new Size((double)ImageBitmap.PixelWidth, 
                (double)ImageBitmap.PixelHeight)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);
        }

        public float MillisecondsPassedSinceLastTick
        {
            get { return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds; }
        }

        //gets index of random stage bg
        public virtual void CreateStageIndexes()
        {
            Random random = new Random();
            backgroundIndex = random.Next(numMaps);
        }

        public abstract void DrawStage(WriteableBitmap surface);

        public virtual void GameTick()
        {
            WorldUser.GameTick(MillisecondsPassedSinceLastTick);

            foreach (var entity in GameEnemies)
                entity.GameTick(MillisecondsPassedSinceLastTick);

            removeDeadEnemies();

            previousGameTick = GameTimer.Elapsed;
        }

    }
}
