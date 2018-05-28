using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        bool goleft;
        bool goright;
        int speed = 5;
        int score = 0;
        bool isPressed;
        int totalEnemies = 12;
        int playerSpeed = 6;

        public Form1()
        {
            InitializeComponent();
        }

        private void gameOver()
        {
            timer1.Stop();
            label1.Text = " GAME OVER ";
        }

        private void makeBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.bullet;
            bullet.Size = new Size(50, 50);
            bullet.Tag = "bullet";
            bullet.Left = Player.Left + Player.Width /2;
            bullet.Top = Player.Top - 20;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //player moving left and right
            if (goleft)
            {
                Player.Left -= playerSpeed;
            }else if (goright)
            {
                Player.Left += playerSpeed;
            }
            //end of player moving left and right

            //enemies moving on the form
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (String)x.Tag == "Invaders")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Player.Bounds)){
                        gameOver();
                    }

                    ((PictureBox)x).Left += speed;
                    if(((PictureBox)x).Left > 720)
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10;
                        ((PictureBox)x).Left = -50;
                    }
                }
            }
            //end of enemies moving on the form

            //animating the bullets and removing them when they have left the scene
            foreach(Control y in this.Controls){
                if(y is PictureBox && (String)y.Tag == "bullet")
                {
                    y.Top -= 20;
                    if (((PictureBox)y).Top < this.Height - 490){
                        this.Controls.Remove(y);
                    }
                }
            }
            //end of animating bullets

            //bullet and enemy collision start
            foreach(Control i in this.Controls)
            {
                foreach(Control j in this.Controls)
                {
                    if(i is PictureBox && i.Tag == "Invaders")
                    {
                        if(j is PictureBox && j.Tag == "bullet")
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                score++;
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                            }
                        }
                    }
                }
            }
            //bullet and enemy collision end

            //keep showing the score
            label1.Text = "Score : " + score;
            if(score>totalEnemies -1)
            {
                gameOver();
                MessageBox.Show("You saved the earth");
            }

            //end of keeping and showing score
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goleft = true;
            }

            if(e.KeyCode == Keys.Right)
            {
                goright = true;
            }

            if(e.KeyCode == Keys.Space && !isPressed)
            {
                isPressed = true;
                makeBullet();
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if(e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (isPressed)
            {
                isPressed = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
