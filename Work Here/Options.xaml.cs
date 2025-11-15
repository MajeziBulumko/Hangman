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
using System.Windows.Shapes;

namespace Work_Here
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    
    public partial class Options : Window
    {
        
        public Options()
        {
            
            InitializeComponent();
            btnAnimals.MouseEnter += OnMouseEnter;
            btnAnimals.MouseLeave += OnMouseLeave;

            btnFood.MouseEnter += OnMouseEnter1;
            btnFood.MouseLeave += OnMouseLeave1;

            btnScience.MouseEnter += OnMouseEnter3;
            btnScience.MouseLeave += OnMouseLeave3;

            btnMedia.MouseEnter += OnMouseEnter2;
            btnMedia.MouseLeave += OnMouseLeave2;

            btnRegions.MouseEnter += OnMouseEnter4;
            btnRegions.MouseLeave += OnMouseLeave4;

        }
        bool clicked = false;
        ILocation where , file;
        private string location(ILocation source)
        {
            return source.Location();
        }
        private void btnEasy_Click(object sender, RoutedEventArgs e)
        {
            Quick_Play quick = new Quick_Play();
            quick.Hide();
            btnHard.Visibility = Visibility.Hidden;
            btnMedium.Visibility = Visibility.Hidden;
            clicked = true;
            where = new Easy();
            
        }

        private void btnMedium_Click(object sender, RoutedEventArgs e)
        {
            Quick_Play quick = new Quick_Play();
            quick.Hide();
            btnHard.Visibility = Visibility.Hidden;
            btnEasy.Visibility = Visibility.Hidden;
            clicked = true;
            where = new Medium();
            
        }

        private void btnHard_Click(object sender, RoutedEventArgs e)
        {
            Quick_Play quick = new Quick_Play();
            quick.Hide();
            
            btnEasy.Visibility = Visibility.Hidden;
            btnMedium.Visibility = Visibility.Hidden;
            clicked = true;
            where = new Hard();
            
        }

        private void btnAnimals_Click(object sender, RoutedEventArgs e)
        {
            if (!clicked)
            {
                MessageBox.Show("Please choose a difficulty first!", "Important", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            btnFood.Visibility = Visibility.Hidden;
            btnRegions.Visibility = Visibility.Hidden;
            btnScience.Visibility = Visibility.Hidden;
            btnMedia.Visibility = Visibility.Hidden;
            file = new Animals();
            Window1 window1 = new Window1(location(where),location(file));
            window1.Show();
            this.Hide();
        }
        
        private void btnMedia_Click(object sender, RoutedEventArgs e)
        {
            if (!clicked)
            {
                MessageBox.Show("Please choose a difficulty first!", "Important", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            btnFood.Visibility = Visibility.Hidden;
            btnRegions.Visibility = Visibility.Hidden;
            btnScience.Visibility = Visibility.Hidden;
            btnAnimals.Visibility = Visibility.Hidden;
            file = new Media();
            Window1 window1 = new Window1(location(where), location(file));
            window1.Show();
            this.Hide();
        }

        private void btnScience_Click(object sender, RoutedEventArgs e)
        {
            if (!clicked)
            {
                MessageBox.Show("Please choose a difficulty first!", "Important", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            btnRegions.Visibility = Visibility.Hidden;
            btnAnimals.Visibility = Visibility.Hidden;
            btnMedia.Visibility = Visibility.Hidden;
            btnFood.Visibility = Visibility.Hidden;
            file = new Science();
            Window1 window1 = new Window1(location(where), location(file));
            window1.Show();
            this.Hide();
        }
        private void btnRegions_Click(object sender, RoutedEventArgs e)
        {
            if (!clicked)
            {
                MessageBox.Show("Please choose a difficulty first!", "Important", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
               
            
            btnScience.Visibility = Visibility.Hidden;
            btnAnimals.Visibility = Visibility.Hidden;
            btnMedia.Visibility = Visibility.Hidden;
            btnFood.Visibility = Visibility.Hidden;
            file = new Regions();
            Window1 window1 = new Window1(location(where), location(file));
            window1.Show();
            this.Hide();
        }

        private void btnFood_Click(object sender, RoutedEventArgs e)
        {
            if (!clicked)
            {
                MessageBox.Show("Please choose a difficulty first!", "Important", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            btnRegions.Visibility = Visibility.Hidden;
            btnScience.Visibility = Visibility.Hidden;
            btnAnimals.Visibility = Visibility.Hidden;
            btnMedia.Visibility = Visibility.Hidden;
            file = new Food();
            Window1 window1 = new Window1(location(where), location(file));
            window1.Show();
            this.Hide();
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            btnAnimals.FontSize = 20;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            btnAnimals.FontSize = 14;
        }
        private void OnMouseEnter1(object sender, EventArgs e)
        {
            btnFood.FontSize = 20;
        }

        private void OnMouseLeave1(object sender, EventArgs e)
        {
            btnFood.FontSize = 14;
        }

        private void OnMouseEnter2(object sender, EventArgs e)
        {
            btnMedia.FontSize = 20;
        }

        private void OnMouseLeave2(object sender, EventArgs e)
        {
            btnMedia.FontSize = 14;
        }

        private void OnMouseEnter3(object sender, EventArgs e)
        {
            btnScience.FontSize = 20;
        }

        private void OnMouseLeave3(object sender, EventArgs e)
        {
            btnScience.FontSize = 14;
        }

        private void OnMouseEnter4(object sender, EventArgs e)
        {
            btnRegions.FontSize = 20;
        }

        private void OnMouseLeave4(object sender, EventArgs e)
        {
            btnRegions.FontSize = 14;
        }
    }
}
