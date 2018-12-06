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
        int floor;
        WriteableBitmap surface;
        GameEngine game;
        private bool gamestarted = false;

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
            floor = (int)(StageGraphics.FloorPos.Y);

            BitmapImage Image = new BitmapImage(new Uri(StageGraphics.StartScreen, UriKind.Relative));
            WriteableBitmap Message = new WriteableBitmap(Image);

            surface.Blit(new Point(0,0), Message, new Rect(new Size(Message.PixelWidth, Message.PixelHeight)), Colors.White, WriteableBitmapExtensions.BlendMode.Alpha);

            game = new GameEngine();

            CompositionTarget.Rendering += NextFrameEvent;
        }

        private void NextFrameEvent(object sender, EventArgs e)
        {
            if (!gamestarted && Keyboard.IsKeyDown(Key.Enter))
            {
                game.StartGame();
                gamestarted = true;
            }
            if (game.restart == true)
            {
                game = new GameEngine();
                game.StartGame();
            }
            if(gamestarted) game.FrameEvents(surface);
        }
    }
}
