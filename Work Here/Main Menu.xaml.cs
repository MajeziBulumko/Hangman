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
    /// Interaction logic for Quick_Play.xaml
    /// </summary>
    
    public interface ILocation
    {
        string Location();
    }
    public class Easy : ILocation
    {
        public string Location()
        {
            return "\\Easy";
        }
    }
    public class Medium : ILocation
    {
        public string Location()
        {
            return "\\Medium";
        }
    }
    public class Hard : ILocation
    {
        public string Location()
        {
            return "\\Hard";
        }
    }
    public class Animals : ILocation
    {
        public string Location()
        {
            return "\\Animals.txt";
        }
    }
    public class Media : ILocation
    {
        public string Location()
        {
            return "\\Media.txt";
        }
    }
    public class Food : ILocation
    {
        public string Location()
        {
            return "\\Food.txt";
        }
    }
    public class Science : ILocation
    {
        public string Location()
        {
            return "\\Science.txt";
        }
    }
    public class Regions : ILocation
    {
        public string Location()
        {
            return "\\Regions.txt";
        }
    }
    public partial class Quick_Play : Window 
    {


       
        public Quick_Play()
        {
            InitializeComponent();

            btnCareer.MouseEnter += OnMouseEnterCareer;
            btnCareer.MouseLeave += OnMouseLeaveCareer;

            btnQuickPlay.MouseEnter += OnMouseEnterPlay;
            btnQuickPlay.MouseLeave += OnMouseLeavePlay;

        }

        private void btnQuickPlay_Click(object sender, RoutedEventArgs e)
        {
            Options options = new Options();
            options.Show();
            this.Visibility = Visibility.Hidden;

        }

        private void btnCareer_Click(object sender, RoutedEventArgs e)
        {
            Profile career = new Profile();
            career.Show();
            
            this.Visibility = Visibility.Hidden;
        }

        private void OnMouseEnterCareer(object sender, EventArgs e)
        {
            btnCareer.FontSize = 26;
        }

        private void OnMouseLeaveCareer(object sender, EventArgs e)
        {
            btnCareer.FontSize = 22;
        }

        private void OnMouseEnterPlay(object sender, EventArgs e)
        {
            btnQuickPlay.FontSize = 26;
        }

        private void OnMouseLeavePlay(object sender, EventArgs e)
        {
            btnQuickPlay.FontSize = 22;
        }
    }
}
