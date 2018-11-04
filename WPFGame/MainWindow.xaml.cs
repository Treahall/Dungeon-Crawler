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
using WPFGame.GameEntities;
using WPFGame.World;

namespace WPFGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int height, width;
        WriteableBitmap writeableBmp;
        GameWorld world;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewPort_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewPortContainer.ActualWidth;
            height = (int)this.ViewPortContainer.ActualHeight;
            writeableBmp = BitmapFactory.New(width, height);
            ViewPort.Source = writeableBmp;
            CreateWorld();
            world.StartTimer();
            CompositionTarget.Rendering += CompositionTarget_Rendering1; ;

        }

        private void CompositionTarget_Rendering1(object sender, EventArgs e)
        {
            world.GameTick();
            writeableBmp.Clear();

            foreach(GameEntity entity in world.GameEntities)
            {
                entity.Draw(writeableBmp);
            }
        }

        private void CompositionTarget_Rendering(object sender, RoutedEventArgs e)
        {
            world.GameTick();

            writeableBmp.Clear();
            foreach(GameEntity entity in world.GameEntities)
            {
                entity.Draw(writeableBmp);
            }
        }

        private void CreateWorld()
        {
            world = new GameWorld();

        }


    }
}
