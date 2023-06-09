using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FighterPlaneShootingGame
{
    public partial class Form1 : Form
    {

        bool goLeft, goRigth, shooting, isGameOver;
        int score;
        int playerSpeed = 12;
        int enemySpeed;
        int bulletSpeed;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {

            txtScore.Text = score.ToString();

            enemyOne.Top += enemySpeed;
            enemyTwo.Top += enemySpeed;
            enemyThree.Top += enemySpeed;

            if(enemyOne.Top > 710 || enemyTwo.Top > 710 || enemyThree.Top > 710)
            {
                gameOver();
            }
            
            //player movement logic starts
            
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (goRigth == true && player.Left < 510)
            {
                player.Left += playerSpeed;
            }

            //player movement logic ends

            //bullet logic starts

            if(shooting == true)
            {
                bulletSpeed = 20;
                bullet.Top -= bulletSpeed;
            }
            else
            {
                bullet.Left = -300;
            }
            if(bullet.Top < -30)
            {
                shooting = false;
                bulletSpeed = 0;
            }

            //bullet logic ends
            
            //colliding logic with enemy starts
            
            if (bullet.Bounds.IntersectsWith(enemyOne.Bounds))
            {
                score += 1;
                enemyOne.Top = -450;
                enemyOne.Left = rnd.Next(20, 490);
                shooting = false;
            }
            if (bullet.Bounds.IntersectsWith(enemyTwo.Bounds))
            {
                score += 1;
                enemyTwo.Top = -450;
                enemyTwo.Left = rnd.Next(20, 490);
                shooting = false;
            }
            if (bullet.Bounds.IntersectsWith(enemyThree.Bounds))
            {
                score += 1;
                enemyThree.Top = -450;
                enemyThree.Left = rnd.Next(20, 490);
                shooting = false;
            }

            //colliding logic with enemy ends 

            if (score == 5)
            {
                enemySpeed = 9;
            }
            if(score == 10)
            {
                enemySpeed = 12;
            }

        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRigth = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRigth = false;
            }
            if(e.KeyCode == Keys.Space && shooting == false)
            {
                shooting = true;

                bullet.Top = player.Top - 30;
                bullet.Left = player.Left + (player.Width / 2);
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }
        }

        private void resetGame()
        {
            enemySpeed = 6;
            gameTimer.Start();

            enemyOne.Left = rnd.Next(20, 490);
            enemyTwo.Left = rnd.Next(20, 490);
            enemyThree.Left = rnd.Next(20, 490);

            enemyOne.Top = rnd.Next(0, 200) * (-1);
            enemyTwo.Top = rnd.Next(0, 200) * (-1);
            enemyThree.Top = rnd.Next(0, 200) * (-1);

            score = 0;
            bulletSpeed = 0;
            bullet.Left = -300;
            shooting = false;

            txtScore.Text = score.ToString();
        }

        private void gameOver()
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text += Environment.NewLine + "Game over!" + Environment.NewLine + "Press Enter to try again.";
        }

    }
}
