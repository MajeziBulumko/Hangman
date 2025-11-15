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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThinkLib;

namespace Work_Here
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        // Global Elements
        Turtle Hang;
        string word, clue;
        List<string[]> words = new List<string[]>();
        List<char> chars = new List<char>();
        Random rng = new Random();
        int death = 0, foundcount = 0, RightW = 0, Score = 100, level = 1,cluebreak = 0;
        List<int> vs = new List<int>();
        string file1 = "\\Easy", file2 = "\\Medium", file3 = "\\Hard", user, userI;
        List<string> fields = new List<string> {"\\Animals.txt","\\Media.txt","\\Regions.txt", "\\Food.txt", "\\Science.txt"};
        List<string> foundW;

        // Global Elements
        public MainWindow(List <string> foundWords, int num, string userID, string User)
        {
            foundW = foundWords;
            userI = userID;
            user = User;
            Score = Convert.ToInt32(num);
            for (int i = 0; i < fields.Count;i++)
            {
                wordDictionary(fields[i]);
            }
            if (foundW.Count > 20 ||foundW.Count<39) level = 2;
            if (foundW.Count > 40) level = 3;
            InitializeComponent();
            Hang = new Turtle(drawer);
            Hang.Visible = false;
            Hang.BrushWidth = 7;
            Hang.LineBrush = Brushes.Black;
            if (Score <= 100) { btnClue_.Visibility = Visibility.Hidden; }
            NewWord();
            stripes();
            playerInfo();
        }
        void NewWord()
        {
            int kin = rng.Next(0, words.Count);
            death = 0; foundcount = 0;
            Hang.Clear();
            Hang.Reset();
            Hang.BrushWidth = 7;
            Hang.LineBrush = Brushes.Black;
            Hang.Visible = false;
            while (vs.Contains(kin))
            {
                kin = rng.Next(0, words.Count);
            }
            if (RightW != 20)
            {
                word = words[kin][0];
                clue = words[kin][1];
                stripes();
            }
            else
            {
                MessageBox.Show("YOU HAVE PASSED LEVEL " + level, "WINNER", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
                
                
                level += 1;
                if (level > 3)
                {
                    MessageBox.Show("YOU HAVE CONQUERED THE GAME");
                    ((Quick_Play)System.Windows.Application.Current.MainWindow).Show();
                }
                else
                {
                    for (int i = 0; i < fields.Count; i++)
                    {
                        wordDictionary(fields[i]);
                    }
                }
                MessageBox.Show("Welcome to Level" + level);
                
            }

        }
        int count(List<char> wordList, char letter)
        {
            int num = 0;
            foreach (char let in wordList)
            {
                if (letter == let) num += 1;
            }
            return num;
        }
        private void checking(char v)
        {
            if (Score <= 100) { btnClue_.Visibility = Visibility.Hidden; }
            else { btnClue_.Visibility = Visibility.Visible; }
            List<char> wordC = new List<char>();
            chars.Add(v);
            foreach (char c in word.ToLower())
            {
                wordC.Add(c);
            }
            if (wordC.Contains(v))
            {

                foundcount += count(wordC, v);
                labelWord(wordC, chars);
                Score += 50 * count(wordC, v);
                if (foundcount == wordC.Count)
                {
                    MessageBox.Show("You got the word");
                    RightW++;
                    restart();
                    score();
                }

            }
            else
            {
                death++;
                ToDraw();
                score();
            }

            switch (level)
            {
                case 1: if (death == 10) { MessageBox.Show("You have failed to find the word, Try Again.\nThe word was " + word); restart(); stripes(); } break;
                case 2: if (death == 6) { MessageBox.Show("You have failed to find the word, Try Again.\nThe word was " + word); restart(); stripes(); } break;
                case 3: if (death == 4) { MessageBox.Show("You have failed to find the word, Try Again.\nThe word was " + word); restart(); stripes(); } break;
            }


        }
        void ToDraw()
        {
            switch (level)
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
                        case 2: top(); break;
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
                        case 3: arms(); body(); leg1(); MessageBox.Show("You have one leg to stand on");  break;
                        case 4: leg2(); break;
                    }
                    break;
            }
        }
        void labelWord(List<char> clist, List<char> blist)
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
        void restart()
        {
            NewWord();
            while (foundW.Contains(word))
            {
                NewWord();
            }
            foundW.Add(word);
            
            ButtonsAppear();
            chars.Clear();
            lblWord.Content = "";
            lblClue.Content = "";
            stripes();
        }
        void score()
        {
            lblScore.Content = Score;
        }
        void ButtonsAppear()
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
        public void wordDictionary(string files)
        {
            string[] vocab;
            string line, toOpen;
            toOpen = file1;
            switch (level)
            {
                case 1: toOpen = file1;break;
                case 2: toOpen = file2;break;
                case 3: toOpen = file3;break;
            }
            string vocabPath = "..\\..\\.." + "\\Words\\Career"+toOpen + files;  // In the solution folder
            vocab = File.ReadAllLines(vocabPath);
            int word = 0;
            while (word != vocab.Length)
            {
                line = vocab[word];
                words.Add(line.Split(','));
                word++;
            }

        }
        private void btnClue__Click(object sender, RoutedEventArgs e)
        {
            if(cluebreak > 3) { MessageBox.Show("You cant have any clues for now come back to check later"); cluebreak--; return; }
            string wordas = lblClue.ToString();
            if (wordas.CompareTo(clue) == 0)
            {
                return;
            }
            Score -= 50;
            score();
            string[] wr = clue.Split();
            int res = 0;
            for (int i = 0; i < wr.Length; i++)
            {
                res++;
                lblClue.Content += wr[i] + " ";
                if (res > 6) { lblClue.Content += "\n"; res = 0; }
            }
            cluebreak++;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\.." + "\\Users"; // path to file
           
            // writing data in string
            string words = "\n";
            for (int i = 0; i < foundW.Count; i ++)
            {
                words += foundW[i] + "\n";
            }
            
            StreamWriter dataasstring = new StreamWriter(path + user) ; //your data
            dataasstring.Write(string.Format("{0} \n{1}", userI, Score) + words);
            dataasstring.Close();                     
            
            ((Quick_Play)System.Windows.Application.Current.MainWindow).Show();
            this.Close();
        }
        void stripes() // when a person has not entered a word, just shows the number of letters in the word
        {
            lblWord.Content = "";
            foreach (char i in word)
            {
                lblWord.Content += "  __";
            }
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MessageBox.Show("Game is paused", "Game Pause", MessageBoxButton.OK, MessageBoxImage.Question);
            this.Show();
            
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            

            btnA.Visibility = Visibility.Hidden; checking('a');
        }
        
        
        public void ground()
        {
            
            Hang.WarpTo(10, 375);
            Hang.Forward(250);
            
        } // needs to be in a class 
        public void pole()
        {
            
            Hang.WarpTo(20, 375);
            Hang.Left(90);
            Hang.Forward(300);
            
        } // needs to be in a class
        public void top()
        {
           
            Hang.Left(-90);
            Hang.Forward(-10);
            Hang.Forward(125);
            
        } // needs to in a class
        public void rope()
        {
            
            Hang.Right(90);
            Hang.Forward(20);
            
        } // needs to be in a class
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
            
        } // needs to be in a class
        public void upper()
        {
           
            Point p = Hang.Position;
            Hang.WarpTo(p.X + 14, p.Y + 14);
            Hang.Forward(30);
            
            
        } // needs to be in a class
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



        } // needs to be in a class
        public void leg2()
        {
            Hang.Right(-90);
            Hang.Forward(30);
            Hang.Forward(-30);
        } // needs to be in a class

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            checking('b'); btnB.Visibility = Visibility.Hidden;
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            
            btnC.Visibility = Visibility.Hidden; checking('c');
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            
            btnD.Visibility = Visibility.Hidden; checking('d');
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            
            btnE.Visibility = Visibility.Hidden; checking('e');
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            
            btnF.Visibility = Visibility.Hidden; checking('f');
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            
            btnG.Visibility = Visibility.Hidden; checking('g');
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            
            btnH.Visibility = Visibility.Hidden; checking('h');
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            
            btnI.Visibility = Visibility.Hidden; checking('i');
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            
            btnJ.Visibility = Visibility.Hidden; checking('j');
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
           
            btnK.Visibility = Visibility.Hidden; checking('k');
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            
            btnL.Visibility = Visibility.Hidden; checking('l');
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            
            btnM.Visibility = Visibility.Hidden; checking('m');
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            
            btnN.Visibility = Visibility.Hidden; checking('n');
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            
            btnO.Visibility = Visibility.Hidden; checking('o');
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            
            btnP.Visibility = Visibility.Hidden; checking('p');
        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            
            btnQ.Visibility = Visibility.Hidden; checking('q');
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            
            btnR.Visibility = Visibility.Hidden; checking('r');
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            
            btnS.Visibility = Visibility.Hidden; checking('s');
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            
            btnT.Visibility = Visibility.Hidden; checking('t');
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            
            btnU.Visibility = Visibility.Hidden; checking('u');
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            
            btnV.Visibility = Visibility.Hidden; checking('v');
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            
            btnW.Visibility = Visibility.Hidden; checking('w');
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            
            btnX.Visibility = Visibility.Hidden; checking('x');
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            
            btnY.Visibility = Visibility.Hidden; checking('y');
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            
            btnZ.Visibility = Visibility.Hidden; checking('z');
        }
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\.." + "\\Users\\"; // path to file

            // writing data in string
            string words = "\n";
            for (int i = 0; i < foundW.Count; i++)
            {
                words += foundW[i] + "\n";
            }

            StreamWriter dataasstring = new StreamWriter(path+user); //your data
            dataasstring.Write(string.Format("{0} \n{1}", userI, Score) + words);
            dataasstring.Close();

            ((Quick_Play)System.Windows.Application.Current.MainWindow).Show();
            this.Close();
        }
        void playerInfo()
        {
            lblPlayer.Content = user + " " + userI ;
        }
    }
}
