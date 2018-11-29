using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFGame.Entities;
using WPFGame.Worlds;
using WPFGame.Data;
using System.Resources;

namespace WPFGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double H, W;
        int floor, positionX;
        WriteableBitmap WindowBM;
        World game;

        public MainWindow()
        {
            InitializeComponent();
        }

        //functions as initializer for program
        private void Screen_Loaded(object sender, RoutedEventArgs e)
        {
            //Get Window Height and Width
            W = (double)this.ScreenGrid.ActualWidth;
            H = (double)this.ScreenGrid.ActualHeight;

            //store as resource files and create bitmap on whole screen
            Application.Current.Resources["WindowWidth"] = W;
            Application.Current.Resources["WindowHeight"] = H;
            WindowBM = BitmapFactory.New((int)W, (int)H);
            ScreenImage.Source = WindowBM;
            floor = (int)(new StageGraphics().FloorPos.Y);
            CreateWorld();
            game.StartTimer();
            CompositionTarget.Rendering += NextFrameEvent;
        }

        private void NextFrameEvent(object sender, EventArgs e)
        {
            WindowBM.Clear();
            game.DrawStage(WindowBM);
            game.GameTick();

            //records player x cord and gives it to all enemies
            foreach(GameEntity entity in game.GameEntities)
            {
                if(entity is Player)
                    positionX = (int)entity.Position.X;
                else
                    entity.PlayerXpos = positionX;

                entity.Draw(WindowBM);
            }
        }
      
        private void CreateWorld()
        {
            game = new FightingWorld();
            GameEntity entity = new Player();
            game.AddEntity(entity);
            entity = new WereWolf();
            //game.AddEntity(entity);
        }
    }
}
