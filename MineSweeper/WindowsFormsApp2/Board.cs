using System.Collections.Generic;
using System.Reflection.Emit;

namespace WindowsFormsApp2
{
    public class Board
    {
        private int[,] plateau;
        private int size;
        
        public Board(int _size)
        {
            size = _size;
            plateau = new int[size,size];
        }

        public int[,] GetPlateau()
        {
            return plateau;
        }

        public void PlaceMinesBoard(Mines mines)
        {
            foreach (var mine in mines.GetMines())
            {
               PlaceOneMine(mine.GetPosX(),mine.GetPosY()); 
            }
        }
        public void PlaceOneMine(int x, int y)
        {
            if (x < size && y < size)
                plateau[x, y] = -1;
        }

        public void PlaceNumbers()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (plateau[i, j] != -1)
                    {
                       var r = NumberMine(i, j);
                       if (r == 0)
                           plateau[i, j] = -2;
                       else
                           plateau[i, j] = r;
                    }
                }
            }
        }

        public int NumberMine(int x, int y)
        {
            int res = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!(i == 0 && j == 0) && x+i>=0 && y+j>=0 && x+i<size && y+j<size)
                    {
                        if (plateau[x + i, y + j] == -1)
                        {
                            res += 1;
                        }
                    }
                }
            }

            return res;
        }
    }
}