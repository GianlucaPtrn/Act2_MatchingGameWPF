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
using System.Windows.Threading;

namespace Act2_MatchingGame2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        TextBlock derniereTBClique;
        int tempsEcoule = 0;
        int nbPairesTrouvees = 0;
        bool trouvePaire = false;
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();

            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(.1);
        }

        private void SetUpGame()
        {
            Random nbAlea = new Random();
            int index;
            string nextEmoji;

            List<string> animalEmoji = new List<string>()
            {
            "🍉","🍉",
            "🍎","🍎",
            "🍌","🍌",
            "🍒","🍒",
            "🥝","🥝",
            "🍓","🍓",
            "🍍","🍍",
            "🍇","🍇",
            };

            foreach (TextBlock textBlock in grdMain.Children.OfType<TextBlock>())
            {

                if (textBlock.Name != "txtTemps")
                {
                    index = nbAlea.Next(animalEmoji.Count);
                    nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Interval = TimeSpan.FromSeconds(.1);
            tempsEcoule = 0;
            nbPairesTrouvees = 0;
            timer.Start();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlockActif = sender as TextBlock;

            if (!trouvePaire)
            {

                textBlockActif.Visibility = Visibility.Hidden;
                derniereTBClique = textBlockActif;
                trouvePaire = true;
            }
            else if (textBlockActif.Text == derniereTBClique.Text)
            {

                nbPairesTrouvees++;
                textBlockActif.Visibility = Visibility.Hidden;
                trouvePaire = false;
            }
            else
            {

                derniereTBClique.Visibility = Visibility.Visible;
                trouvePaire = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tempsEcoule++;
            txtTemps.Text = (tempsEcoule / 10F).ToString("0.00s");
            if (nbPairesTrouvees == 8)
            {

                timer.Stop();
                txtTemps.Text = txtTemps + " - Rejouer ? ";
            }
        }

        private void TxtTemps_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (nbPairesTrouvees == 8)
            {

                SetUpGame();
            }
        }
    }
}
