using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
        private static Form currentForm = null;
        private static Form localMenu = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LaunchMenu();
        }

        public static void LaunchGame(int nbMines, int size)
        {
            if(localMenu!=null)
                localMenu.Hide();
            currentForm = new Form1(nbMines,size);
            currentForm.Show();
        }
        public static void LaunchMenu()
        {
            if(localMenu==null)
            {
                localMenu=new MenuForm();
                Application.Run(localMenu);
            }
            else if (currentForm != null)
            {
                currentForm.Close();
                localMenu.Show();
            }
        }
        
    }
}