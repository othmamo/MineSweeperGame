using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp2
{
    public class Mines
    {
        private List<Mine> mines;
        private int nbMines;
        private int sizeBoard;
        
        public Mines(int _nbMines, int _sizeBoard)
        {
            nbMines = _nbMines;
            sizeBoard = _sizeBoard;
            mines = new List<Mine>();
            PlaceMines();
        }

        public void PlaceMines()
        {
            for (int i = 0; i < nbMines; i++)
            {
                int pX;
                int pY;
                do
                {
                    pX = new Random().Next(sizeBoard);
                    pY = new Random().Next(sizeBoard);
                } while (AddMine(pX,pY) == false);      
            }
        }

        public bool AddMine(int pX,int pY)
        {
            foreach (var min in mines)
            {
                if (min.GetPosX() == pX && min.GetPosY() == pY)
                {
                    return false;
                }
            }
            mines.Add(new Mine(pX,pY));
            return true;
        }

        public List<Mine> GetMines()
        {
            return mines;
        }
    }
}