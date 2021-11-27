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

        private void button1_Click(object sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }

            pictureBox2.Refresh();

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

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
