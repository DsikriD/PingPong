using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {
        private int speadVertical = 4;
        private int speadHorizontal = 4;
        private int score = 0;


        public Form1()
        {
            InitializeComponent();

            timer.Enabled = true;
            Cursor.Hide();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;// Всегда показываетя поверх других программ

            this.Bounds = Screen.PrimaryScreen.Bounds;// Когда программа запуститься ,
            // то разрмеры окна программы = размерам экрана

            gamePanel.Top = background.Bottom - (background.Bottom / 10);

            loselabel.Visible = false;
            loselabel.Left = (background.Width / 2 - (loselabel.Width/2));
            loselabel.Top = (background.Height / 2 - (loselabel.Height / 2));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
                this.Close();   
            if(e.KeyCode == Keys.R)
            {
                gameBall.Top = 50;
                gameBall.Left = 70;
                speadHorizontal = 2;
                speadVertical = 2;
                loselabel.Visible=false;
                timer.Enabled=true;
                result.Text = "Score:0";
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Панель следует за курсором 
            gamePanel.Left = Cursor.Position.X - (gamePanel.Width/2);

            // Передвежение мячика
            gameBall.Left += speadHorizontal;
            gameBall.Top += speadVertical;

            // Выход за границы 
            if (gameBall.Left <= background.Left)
                speadHorizontal *= -1;
            if (gameBall.Right >= background.Right)
                speadHorizontal *= -1;
            if(gameBall.Top <= background.Top)
                speadVertical *= -1;
            if (gameBall.Bottom >= background.Bottom)
            {// Проигрышь
                timer.Enabled = false;
                loselabel.Visible = true;
            }


            // Cопрекоснулся с игровой панелью ?
            if (gameBall.Bottom >= gamePanel.Top && gameBall.Bottom <= gamePanel.Bottom && gameBall.Left >= gamePanel.Left && gameBall.Right <= gamePanel.Right)
            {
                speadHorizontal += 1;
                speadVertical += 1;

                speadVertical *= -1;
                score++;
                result.Text = "Score:"+score.ToString();


                Random randColor = new Random();
                background.BackColor = Color.FromArgb(randColor.Next(150,155), randColor.Next(150, 155), randColor.Next(150, 155));

            }
        }
    }
}
