using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Blackjack
{
    public partial class Form1 : Form
    {
        Game game;
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game();
            game.nextRound();
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            drawCards(e, true);
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            drawCards(e, false);
        }

        private void drawCards(PaintEventArgs e, bool isDealerHand)
        {
            if (game == null)
            {
                return;
            }

            HashSet<Card> hand = isDealerHand ? game.dilerHand : game.playerHand;

            int x = 0;
            foreach (Card card in hand)
            {
                string cardResourceName = "card_back";
                if (!card.isFlippedBack)
                {
                    cardResourceName = card.suit.ToString().ToLower() + "_" + card.rank.ToString().ToLower();
                }

                Bitmap bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(cardResourceName);
                e.Graphics.DrawImage(bitmap, x, 0);
                x = x + 50;
            }
        }

        private void buttonHit_Click(object sender, EventArgs e)
        {
            if (game == null || game.playerIsStand)
            {
                return;
            }

            game.playerHit();
            pictureBox2.Refresh();

            if(game.playerTotalScore > 21)
            {
                MessageBox.Show("You loose the game!");
                pictureBox1.Refresh();
            }

            if(game.playerTotalScore == 21)
            {
                game.playerIsStand = true;
                game.dealerHit();
                pictureBox1.Refresh();
                showRoundResult();
            }

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                game = GameIO.load(openFileDialog1.FileName);
                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }

            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                GameIO.save(game, saveFileDialog1.FileName);
            }
        }

        private void buttonStand_Click(object sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }

            game.playerIsStand = true;
            game.dealerHit();
            pictureBox1.Refresh();
            showRoundResult();
        }

        private void showRoundResult()
        {
            if (game == null)
            {
                return;
            }

            string result;

            if(game.playerTotalScore > game.dealerTotalScore)
            {
                result = String.Format("Player win! {0}:{1}", game.playerTotalScore, game.dealerTotalScore);
            }
            else if(game.dealerTotalScore > game.playerTotalScore)
            {
                result = String.Format("You loose! {0}:{1}", game.playerTotalScore, game.dealerTotalScore);
            } else
            {
                result = String.Format("It's draw! {0}:{1}", game.playerTotalScore, game.dealerTotalScore);
            }

            MessageBox.Show(result);
        }
    }
}
