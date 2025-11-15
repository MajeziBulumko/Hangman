using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    

    public partial class Profile : Window
    {
        string boy;
        List<string> allfound = new List<string>();
        public Profile()
        {
            InitializeComponent();
            lblID.Visibility = Visibility.Hidden;
            txtID.Visibility = Visibility.Hidden;
            lblUser.Visibility = Visibility.Hidden;
            lblUserEnter.Visibility = Visibility.Hidden;
            txtUserCreate.Visibility = Visibility.Hidden;
            txtUserEnter.Visibility = Visibility.Hidden;
            btnCreate.Visibility = Visibility.Hidden;
            btnEnter.Visibility = Visibility.Hidden;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("PLEASE REGISTER");
            txtID.Visibility = Visibility.Visible;
            lblID.Visibility = Visibility.Visible;
            lblUser.Visibility = Visibility.Visible;
            btnCreate.Visibility = Visibility.Visible;
            txtUserCreate.Visibility = Visibility.Visible;
            btnSign.Visibility = Visibility.Hidden;
            btnNew.Visibility = Visibility.Hidden;
        }

        private void btnSign_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("PLEASE ENTER YOUR USERNAME");
            btnEnter.Visibility = Visibility.Visible;
            txtUserEnter.Visibility = Visibility.Visible;
            lblUserEnter.Visibility = Visibility.Visible;
            btnNew.Visibility = Visibility.Hidden;
            btnSign.Visibility = Visibility.Hidden;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            boy = txtUserCreate.Text; if (boy == "") { return; }
            if (verification(boy))
            {
                // write to a file 
                
                List<string> list = new List<string>(); 
                MainWindow main = new MainWindow(list,0,txtID.Text,boy);
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username is already taken","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }
        bool verification(string man)
        {
            try
            {
                var fileStream = new FileStream("..\\..\\..\\Users" + boy + ".txt", FileMode.Open, FileAccess.Read);
            }
            catch (IOException x)
            {
                return false;
            }
            return true;
        }
        

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            boy = txtUserEnter.Text;
            if (boy == "") { return; }
            try
            {
                // take's the user info to the next screen
                string[] vocab, boys;
                string line, userId, score;
                string vocabPath = "..\\..\\.." + "\\Users\\" + boy;  // In the solution folder
                vocab = File.ReadAllLines(vocabPath);
                int word = 2;
                userId = vocab[0];
                score = vocab[1];
                while (word != vocab.Length)
                {
                    line = vocab[word];
                    boys = line.Split(',');
                    allfound.Add(boys[0]);
                    word++;
                }
                MainWindow main = new MainWindow(allfound, Convert.ToInt32(score), userId, boy);
                main.Show();
                this.Close();
            }
            catch(IOException x)
            {
                MessageBox.Show("Incorrect Username","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }

        private void txtUserEnter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
