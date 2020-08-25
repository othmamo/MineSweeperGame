using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class Cases
    {
        private Button _button ;
        private int x;
        private int y;
        public Cases(int size,int posX, int posY, Form1 f)
        {
            _button = new Button();
            _button.Height = size;
            _button.Width = size;
            _button.Font=new Font(FontFamily.GenericMonospace, 15);
            x = posX;
            y = posY;
            _button.Location = new Point(posX*size,posY*size);
            _button.Visible = true;
            f.Controls.Add(_button);
            _button.BringToFront();
        }

        public Button getButton()
        {
            return _button;
        }

        public int getPosX()
        {
            return x;
        }
        
        public int getPosY()
        {
            return y;
        }
        
        
    }
}