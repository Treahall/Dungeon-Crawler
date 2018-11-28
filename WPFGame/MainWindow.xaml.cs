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
using WPFGame.stageGraphics;
using System.Resources;

namespace WPFGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double H, W;
        int floor;
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

            //store as a resource files and create bitmap on whole screen
            Application.Current.Resources["WindowWidth"] = W;
            Application.Current.Resources["WindowHeight"] = H;
            WindowBM = BitmapFactory.New((int)W, (int)H);
            ScreenImage.Source = WindowBM;
            floor = (int)(new StageGraphic().FloorPos.Y);
            CreateWorld();
            game.StartTimer();
            CompositionTarget.Rendering += NextFrameEvent;
        }

        private void NextFrameEvent(object sender, EventArgs e)
        {
            WindowBM.Clear();
            game.DrawStage(WindowBM);

            game.GameTick();

            foreach(GameEntity entity in game.GameEntities)
            {
                entity.Draw(WindowBM);
            }
        }
      
        private void CreateWorld()
        {
            game = new GameWorld();
            var player = new Player();
            game.AddEntity(player);
        }
    }
}
