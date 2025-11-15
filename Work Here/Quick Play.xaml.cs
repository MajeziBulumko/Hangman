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
using System.Windows.Threading;
using ThinkLib;

namespace Work_Here
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    
    public partial class Window1 : Window
    {
        // GLobal Variables

        Turtle Hang;
        string word, clue, file;
        List<string[]> words = new List<string[]>();
        List<char> chars = new List<char>();
        Random rng = new Random();
        int death = 0, foundcount = 0, RightW = 0, Score = 0, difficulty = 0;
        List<int> vs = new List<int>();
        List<string> already = new List<string>();
        private int oneup=0;
        DispatcherTimer timer = new DispatcherTimer();

        //global variables
        public Window1(string man, string boy)
        {
            
            file = man + boy;

            if (man == "\\Easy") { difficulty = 1; }
            if (man == "\\Medium") { difficulty = 2; }
            if (man == "\\Hard") { difficulty = 3; }

            wordDictionary(); // loads the words 
            InitializeComponent();
            Timer();
            Hang = new Turtle(HangQuick);
            Hang.Visible = false;
            Hang.BrushWidth = 7;
            Hang.LineBrush = Brushes.Black;  
            //Hang.SetAppearance(null,null,Brushes.Black);
            int use = rng.Next(0, words.Count); vs.Add(use);
            word = words[use][0];
            clue = words[use][1];
            stripes();
            btnClue.Visibility = Visibility.Hidden;
            //if () { ToDraw(); death += 1; }
            already.Add(word);
        }
        void stripes() // when a person has not entered a word, just shows the number of letters in the word
        {
            lblWord.Content = "";
            foreach (char i in word)
            {
                lblWord.Content += "  __";
            }
        }
        public void Timer()
        {
           
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timertick;
            timer.Start();
        }
        void timertick(object sender,EventArgs e)
        {
            oneup++;
            lblTime.Content = oneup.ToString();
        }
        void NewWord()// gets you a new word
        {
            int kin = rng.Next(0, words.Count);
            death = 0; foundcount = 0;
            Hang.Clear();
            Hang.Reset();
            Hang.BrushWidth = 7;
            Hang.Visible = false;
            Hang.LineBrush = Brushes.Black;
            while (already.Contains(word))
            {
                kin = rng.Next(0, words.Count); word = words[kin][0];
            }
            if (RightW != 10)
            {
                word = words[kin][0];
                clue = words[kin][1];
                
            }

            else
            {
                MessageBox.Show("You've won quickplay","Winner",MessageBoxButton.OK,MessageBoxImage.Information);
                MessageBoxButton button = default(MessageBoxButton);
                if(MessageBoxButton.OK == button)
                {
                    ((Quick_Play)System.Windows.Application.Current.MainWindow).Show();
                    this.Close();
                }
            }
            //stripes();

        }
        private void btnPause_Click(object sender, RoutedEventArgs e)//Pauses the game 
        {
            timer.Stop();
            //hide all
            this.Hide();
            MessageBox.Show("Game is paused");
            //unhide all
            this.Show();
            timer.Start();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)//Takes you back to the main menu  
        {
            //MessageBox.Show("Are you sure you want quit","Quit",MessageBoxButton.YesNo,MessageBoxImage.Question);
            //MessageBoxButton Yes = default(MessageBoxButton);
            ((Quick_Play)System.Windows.Application.Current.MainWindow).Show();
            this.Close();
            

        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        { 
            btnA.Visibility = Visibility.Hidden;
            checking('a');
        }
        int count(List<char> wordList, char letter)//Checks which letter or letters to show  
        {
            int num = 0;
            foreach (char let in wordList)
            {
                if (letter == let) num+=1; 
            }
            return num;
        }
        private void checking(char v)//checks if the letter entered is in the word 
        {
            if (Score < 100)
            {
                btnClue.Visibility = Visibility.Hidden;
            }
            else
            {
                btnClue.Visibility = Visibility.Visible;
            }
            List<char> wordC = new List<char>();
            chars.Add(v);
            foreach(char c in word.ToLower())
            {
                wordC.Add(c);
            }
            if (wordC.Contains(v))
            {

                foundcount += count(wordC,v);
                labelWord(wordC,chars);
                Score += 50 * count(wordC, v);
                score();
                if (foundcount == wordC.Count)
                {
                    MessageBox.Show("You got the word in "+ oneup + " seconds");
                    RightW++;
                    restart();
                    stripes();
                    oneup=0;
                    
                }
                
            }
            else
            {
                death++;
                ToDraw();
                score();
            }
            
            
            switch (difficulty)
            {
                case 1: if(death == 10) { MessageBox.Show("You have failed to find the word, Try Again.\nThe word was " + word); restart();stripes(); } break;
                case 2: if(death == 6) { MessageBox.Show("You have failed to find the word, Try Again.\nThe word was " + word); restart();stripes(); } break;
                case 3: if(death == 4) { MessageBox.Show("You have failed to find the word, Try Again.\nThe word was " + word); restart();stripes(); } break;
            }
           
        }
        void ToDraw()
        {
            switch (difficulty)
            {
                case 1:
                    switch (death)
                    {
                        case 1: ground(); break;
                        case 2: pole(); break;
                        case 3: top(); break;
                        case 4: rope(); break;
                        case 5: head(); break;
                        case 6: upper(); break;
                        case 7: arms(); break;
                        case 8: body(); break;
                        case 9: leg1(); break;
                        case 10: leg2(); break;

                    }
                    break;
                case 2:
                    switch (death)
                    {
                        case 1: ground(); pole(); break;
                        case 2: top();  break;
                        case 3: rope(); head(); break;
                        case 4: upper(); break;
                        case 5: arms(); body(); break;
                        case 6: leg1(); leg2(); break;
                    }
                    break;
                case 3:
                    switch (death)
                    {
                        case 1: ground(); pole(); top(); break;
                        case 2: rope(); head(); upper(); break;
                        case 3: arms(); body(); leg1(); timer.Stop(); MessageBox.Show("You have one leg to stand on"); timer.Start(); break;
                        case 4: leg2();break;
                    }
                    break;
            }
        }
        void labelWord(List <char> clist, List <char> blist) //for writing the lines indicating the number of words in the word
        {
            lblWord.Content = "";
            for (int i = 0; i < clist.Count; i++)
            {
                if (blist.Contains(clist[i]))
                {
                    lblWord.Content += "  " + clist[i];
                }
                else
                {
                    lblWord.Content += "  __";
                }
            }
        }
        void restart()// calls all the methods that reset the game
        {
            NewWord();
            ButtonsAppear();
            chars.Clear();
            lblWord.Content = "";
            lblClue.Content = "";
            oneup = 0;
        }
        void score() // prints your score
        {
            lblScore.Content = Score;
        }
        void ButtonsAppear() // makes all the buttons
        {
            btnA.Visibility = Visibility.Visible;
            btnB.Visibility = Visibility.Visible;
            btnC.Visibility = Visibility.Visible;
            btnD.Visibility = Visibility.Visible;
            btnE.Visibility = Visibility.Visible;
            btnF.Visibility = Visibility.Visible;
            btnG.Visibility = Visibility.Visible;
            btnH.Visibility = Visibility.Visible;
            btnI.Visibility = Visibility.Visible;
            btnJ.Visibility = Visibility.Visible;
            btnK.Visibility = Visibility.Visible;
            btnL.Visibility = Visibility.Visible;
            btnM.Visibility = Visibility.Visible;
            btnN.Visibility = Visibility.Visible;
            btnO.Visibility = Visibility.Visible;
            btnP.Visibility = Visibility.Visible;
            btnQ.Visibility = Visibility.Visible;
            btnR.Visibility = Visibility.Visible;
            btnS.Visibility = Visibility.Visible;
            btnT.Visibility = Visibility.Visible;
            btnU.Visibility = Visibility.Visible;
            btnV.Visibility = Visibility.Visible;
            btnW.Visibility = Visibility.Visible;
            btnX.Visibility = Visibility.Visible;
            btnY.Visibility = Visibility.Visible;
            btnZ.Visibility = Visibility.Visible;
        }
        public void wordDictionary() // loads all the words to a list
        {
            string[] vocab;
            string line; 
            string vocabPath = "..\\..\\.." + "\\Words\\QuickPlay" + file;  // In the solution folder
            vocab = File.ReadAllLines(vocabPath);
            int word = 0;
            while (word != vocab.Length)
            {
                line = vocab[word];
                words.Add(line.Split(','));
                word++;
            }

        }
        //Draws all the functions
        public void ground()
        {

            Hang.WarpTo(10, 375);
            Hang.Forward(250);

        }  
        public void pole()
        {
            Hang.WarpTo(20, 375);
            Hang.Left(90);
            Hang.Forward(300);

        } 
        public void top()
        {

            Hang.Left(-90);
            Hang.Forward(-10);
            Hang.Forward(125);

        } 
        public void rope()
        {

            Hang.Right(90);
            Hang.Forward(20);

        } 
        public void head()
        {

            Point p = Hang.Position;
            Hang.BrushDown = false;
            Hang.WarpTo(p.X - 15, p.Y + 15);
            //Hang.Forward(5);
            Hang.BrushDown = true;

            for (int i = 0; i < 36; i++)
            {
                Hang.Left(10);
                Hang.Forward(2.5);
            }

        } 
        public void upper()
        {

            Point p = Hang.Position;
            Hang.WarpTo(p.X + 14, p.Y + 14);
            Hang.Forward(30);


        } 
        public void arms()
        {
            Hang.Heading = 0;
            Hang.Forward(-20);
            Hang.Forward(40);
            Hang.Forward(-20);
            Hang.Heading = 90;
        }
        public void body()
        {
            Hang.Forward(50);
        }
        public void leg1()
        {
            
            Hang.Right(45);
            Hang.Forward(30);
            Hang.Forward(-30);
            


        } 
        public void leg2()
        {
            Hang.Right(-90);
            Hang.Forward(30);
            Hang.Forward(-30);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            btnB.Visibility = Visibility.Hidden;
            checking('b');
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            btnC.Visibility = Visibility.Hidden;
            checking('c');
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            btnD.Visibility = Visibility.Hidden;
            checking('d');
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            btnE.Visibility = Visibility.Hidden;
            checking('e');
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            btnF.Visibility = Visibility.Hidden;
            checking('f');
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            btnG.Visibility = Visibility.Hidden;
            checking('g');
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            btnH.Visibility = Visibility.Hidden;
            checking('h');
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            btnI.Visibility = Visibility.Hidden;
            checking('i');
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            btnJ.Visibility = Visibility.Hidden;
            checking('j');
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            btnK.Visibility = Visibility.Hidden;
            checking('k');
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            btnL.Visibility = Visibility.Hidden;
            checking('l');
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            btnM.Visibility = Visibility.Hidden;
            checking('m');
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            btnN.Visibility = Visibility.Hidden;
            checking('n');
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            btnO.Visibility = Visibility.Hidden;
            checking('o');
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            btnP.Visibility = Visibility.Hidden;
            checking('p');
        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            btnQ.Visibility = Visibility.Hidden;
            checking('q');
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            btnR.Visibility = Visibility.Hidden;
            checking('r');
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            btnS.Visibility = Visibility.Hidden;
            checking('s');
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            btnT.Visibility = Visibility.Hidden;
            checking('t');
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            btnU.Visibility = Visibility.Hidden;
            checking('u');
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            btnV.Visibility = Visibility.Hidden;
            checking('v');
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            btnW.Visibility = Visibility.Hidden;
            checking('w');
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure you want to exit?");
            Environment.Exit(0);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            btnX.Visibility = Visibility.Hidden;
            checking('x');
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            btnY.Visibility = Visibility.Hidden;
            checking('y');
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            btnZ.Visibility = Visibility.Hidden;
            checking('z');
        }

        private void btnClue_Click(object sender, RoutedEventArgs e)//makes the clues appaer 
        {
            string wordas = lblClue.ToString();
            if ( wordas.CompareTo( clue) == 0)
            {
                return;
            }
            Score -= 50;
            score();
            string[] wr = clue.Split();
            int res = 0;
            for (int i = 0; i<wr.Length;i++)
            {
                res++;
                lblClue.Content += wr[i] +" ";
                if (res > 6) { lblClue.Content += "\n"; res = 0; }
            }
             
        }
    }
}
