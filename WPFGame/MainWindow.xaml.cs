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
        WriteableBitmap surface;
        GameEngine game;

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

            //store as resource files.
            Application.Current.Resources["WindowWidth"] = W;
            Application.Current.Resources["WindowHeight"] = H;

            surface = BitmapFactory.New((int)W, (int)H);
            ScreenImage.Source = surface;
            floor = (int)(new StageGraphics().FloorPos.Y);
            CompositionTarget.Rendering += NextFrameEvent;
        }

        private void NextFrameEvent(object sender, EventArgs e)
        {

            //records player x cord and gives it to all enemies
            foreach(GameEntity entity in game.GameEntities)
            {
                if(entity is Player)
                    positionX = (int)entity.Position.X;
                else
                    entity.PlayerXpos = positionX;
            }
        }
    }
}
