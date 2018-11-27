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
using WPFGame.GameEntities;
using WPFGame.World;
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
        GameWorld game;


        public MainWindow()
        {
            InitializeComponent();
        }


        //functions as initializer for program
        private void Screen_Loaded(object sender, RoutedEventArgs e)
        {
            //Get Window Height and Width
            W = (double)ScreenGrid.ActualWidth;
            H =  (double)ScreenGrid.ActualHeight;

            floor = (int)(H - 100);

            //store as a resource files and create bitmap on whole screen
            Application.Current.Resources["WindowWidth"] = W;
            Application.Current.Resources["WindowHeight"] = H;
            WindowBM = BitmapFactory.New((int)(double)Application.Current.Resources["WindowWidth"], (int)(double)Application.Current.Resources["WindowHeight"]);
            ScreenImage.Source = WindowBM;
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

            var character = new Player()
            {
                // sets position of user mid screen(x) on the floor(y)
                Position = new System.Numerics.Vector2((float)W/2, floor)
            };

            game.AddEntity(character);
        }
    }
}
