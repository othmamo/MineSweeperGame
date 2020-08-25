using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MenuForm : Form
    {
        private Button[] buttons;
        private string[] texts;
        public MenuForm()
        {
            InitializeComponent();
            buttons=new Button[3];
            texts=new []{"Easy","Medium","Hard"};
            PlaceButtons();
            InitializeClicks();
            

        }

        public void PlaceButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                Button but = new Button();
                but.Height = this.Height/3/2;
                but.Width = this.Width / 3 * 2;
                but.Location=new Point(this.Width/6,(i)*(this.Height/3)+(this.Height/3/4));
                but.Visible = true;
                this.Controls.Add(but);
                but.BringToFront();
                but.Text = texts[i];
                buttons[i] = but;
            }
        }
        public void InitializeClicks()
        {
            foreach (var but in buttons)
            {
                but.MouseDown += new MouseEventHandler(ClickedButton);
            }
        }
        public void ClickedButton(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                switch (((Button) sender).Text)
                {
                    case "Easy":
                        Program.LaunchGame(10, 10);
                        break;
                    case "Medium":
                        Program.LaunchGame(40,16);
                        break;
                    case "Hard":
                        Program.LaunchGame(99,23);
                        break;
                }
        }
    }
}