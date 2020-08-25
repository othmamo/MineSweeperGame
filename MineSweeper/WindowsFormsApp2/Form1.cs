using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    
    public partial class Form1 : Form
    {
        private Cases[,] _buttons;
        private Cases currentBut;

        private game jeu;
        public Form1(int nbMines,int size)
        {
            InitializeComponent();
            _buttons = new Cases[size,size];
            initializeMatrice();
            InitializeClicks();
            jeu = new game(nbMines,size,_buttons,this);
        }
        public void initializeMatrice()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < _buttons.GetLength(0); i++)
            {
                for (int j = 0; j < _buttons.GetLength(1); j++)
                {
                    Cases c = new Cases(this.Height/_buttons.GetLength(0),i,j,this);
                    _buttons[i, j] = c;
                    x += 50;
                }

                x = 0;
                y += 50;
            }
        }

        public void InitializeClicks()
        {
            foreach (var but in _buttons)
            {
                but.getButton().MouseDown += new MouseEventHandler(ClickedButton);
            }
        }

        public void ClickedButton(object sender, MouseEventArgs e)
        {
            bool flag = false;
            foreach (var but in _buttons)
            {
                if (but.getButton() == sender as Button)
                {
                    currentBut = but;
                    if (e.Button == MouseButtons.Right)
                    {
                        flag = true;
                    }
                }
            }

            if (flag)
            {
                jeu.Flag(currentBut.getPosX(),currentBut.getPosY());
            }
            else
            {
                var cl = jeu.Clique(currentBut.getPosX(), currentBut.getPosY());
                if(cl!=-3)
                    jeu.RadarFree(currentBut.getPosX(), currentBut.getPosY());
                if (cl == 1)
                {
                    jeu.LooseGame();
                    disableButtons();
                    return;
                }
            }

            jeu.Write();
        }

        public void disableButtons()
        {
            foreach (var but in _buttons)
            {
                but.getButton().Enabled = false;
            }
        }
    }
}