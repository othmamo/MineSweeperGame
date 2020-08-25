using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class game
    {
        private int nbMines;
        private Board boardBack;
        private Mines mines;
        private int[,] boardInCourse;
        private int sizeBoard;
        private int caseClicked;
        private int caseFree;
        private List<int> radar;
        private Form1 form1;

        private Cases[,] buttons;

        public game(int _nbMines, int _sizeBoard, Cases[,] _buttons, Form1 _form1)
        {
            form1 = _form1;
            caseFree = _sizeBoard * _sizeBoard - _nbMines;
            buttons = _buttons;
            caseClicked = 0;
            nbMines = _nbMines;
            sizeBoard = _sizeBoard;
            mines = new Mines(_nbMines, _sizeBoard);
            boardBack = new Board(_sizeBoard);
            boardBack.PlaceMinesBoard(mines);
            boardBack.PlaceNumbers();
            boardInCourse = new int[_sizeBoard,_sizeBoard];
            radar = new List<int>();
        }

        public void RadarFree(int x, int y)
        {
            if (boardBack.GetPlateau()[x, y] == -2 && !radar.Contains(Int32.Parse(x + "" + y)))
            {
                radar.Add(Int32.Parse(x+""+y));
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (x + i >= 0 && x + i < sizeBoard && y + j >= 0 && y + j < sizeBoard)
                        {
                            Clique(x + i, y + j);
                            RadarFree(i+x,y+j);
                        }
                    }
                }
            }
        }
        

        public int Clique(int x, int y)
        {
            if (boardInCourse[x, y] <=-10 )
            {
                return -3;
            }
            if (boardInCourse[x, y] != 0 )
            {
                return 0;
            }
            switch (boardBack.GetPlateau()[x, y])
            {
                case -1 :
                    return 1;
                default:
                    caseClicked += 1;
                    WingGame();
                    boardInCourse[x, y] = boardBack.GetPlateau()[x, y];
                    
                    return 0;
            }
        }

        public void Flag(int x, int y)
        {
            if (boardInCourse[x, y] == 0)
            {
                boardInCourse[x, y] -= 10;
            }
            else if (boardInCourse[x, y] <=-10)
            {
                boardInCourse[x, y] += 10;
            }
        }

        public void Write()
        {
            for (int i = 0; i < sizeBoard; i++)
            {
                for (int j = 0; j < sizeBoard; j++)
                {
                    if(boardInCourse[i, j]<=-10)
                        buttons[i, j].getButton().Text = "F";
                    else
                        switch (boardInCourse[i, j])
                        {
                            case -1:
                                buttons[i, j].getButton().Text= "*";
                                break;
                            case -2:
                                buttons[i, j].getButton().Text= "";
                                buttons[i, j].getButton().BackColor=Color.FromArgb(185,182,182);
                                break;
                            case 0:
                                buttons[i, j].getButton().Text= "";
                                break;
                            default:
                                buttons[i, j].getButton().Text= "" + boardInCourse[i, j] ;
                                buttons[i, j].getButton().BackColor=Color.FromArgb(185,182,182);
                                break;
                        }
                }
            }
        }

        public void LooseGame()
        {
            foreach (var mine in mines.GetMines())
            {
                boardInCourse[mine.GetPosX(), mine.GetPosY()] = -1;
            }
            Write();
            MessageBox.Show("You Loose !");
            Program.LaunchMenu();
        }
        
        public void WingGame()
        {
            if (caseClicked == caseFree)
            {
                MessageBox.Show("YouWin");
                form1.disableButtons();
                Program.LaunchMenu();
            }
        }
    }
}