using System;
using System.Collections.Generic;

namespace demineur
{
    public class game
    {
        private int nbMines;
        private Board boardBack;
        private Mines mines;
        private int[,] boardInCourse;
        private int sizeBoard;
        private int caseClicked;
        private List<int> radar;

        public game(int _nbMines, int _sizeBoard)
        {
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
        public void ChooseWhatToDO()
        {
            int cursorX = 0;
            int cursorY = 0;
            Write(0,0);
            while (caseClicked < sizeBoard * sizeBoard - nbMines)
            {
                ConsoleKey a = Console.ReadKey().Key;
                switch (a)
                {
                    case ConsoleKey.F:
                        if(boardInCourse[cursorY, cursorX] == -3)
                            boardInCourse[cursorY, cursorX] = 0;
                        else if(boardInCourse[cursorY, cursorX] == 0)
                            boardInCourse[cursorY, cursorX] = -3;
                        break;
                    case ConsoleKey.Enter:
                        var cl = Clique(cursorY,cursorX);
                        RadarFree(cursorY,cursorX);
                        if (cl == 1)
                        {
                            LooseGame();
                            return;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if(cursorY<sizeBoard-1)
                            cursorY += 1;
                        break;
                    case ConsoleKey.UpArrow:
                        if(cursorY>0)
                            cursorY -= 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        if(cursorX>0)
                            cursorX -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        if(cursorX<sizeBoard-1)
                            cursorX += 1;
                        break;
                    default:
                        break;
                }
                Console.Clear();
                Write(cursorY,cursorX);
            }
            
            Console.WriteLine('\n' + "You Won !");
        }

        public int Clique(int x, int y)
        {
            if (boardInCourse[x, y] != 0)
            {
                return 0;
            }
            caseClicked += 1;
            switch (boardBack.GetPlateau()[x, y])
            {
                case -1 :
                    return 1;
                default:
                    boardInCourse[x, y] = boardBack.GetPlateau()[x, y];
                    return 0;
            }
        }

        public void Write(int cX, int cY)
        {
            for (int i = 0; i < sizeBoard; i++)
            {
                for (int j = 0; j < sizeBoard; j++)
                {
                    if (i == cX && j == cY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    switch (boardInCourse[i,j])
                    {
                        case -3:
                            Console.Write(" F ");
                            break;
                        case -1:
                            Console.Write(" * ");
                            break;
                        case -2:
                            if (!(i == cX && j == cY))
                                Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" 0 ");
                            break;
                        case 0:
                            Console.Write(" W ");
                            break;
                        default:
                            if (!(i == cX && j == cY))
                                Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" "+boardInCourse[i,j]+" ");
                            break;
                    }
                    Console.ResetColor();
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        public void LooseGame()
        {
            foreach (var mine in mines.GetMines())
            {
                boardInCourse[mine.GetPosX(), mine.GetPosY()] = -1;
            }
            Console.Clear();
            Write(0,0);
            
            Console.WriteLine('\n' + "You Loose !");
        }
        
        
    }
}