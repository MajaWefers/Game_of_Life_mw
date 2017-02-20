using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zeichnen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static System.Windows.Forms.Timer zeit = new System.Windows.Forms.Timer();

        static int grid = 10;
        static int abstand = 20;

        int[,] feld = new int[grid, grid];

        int p_x = abstand;
        int p_y = abstand;

        zeit.Tick += new EventHandler(TimerEventProcessor);
        zeit.Interval = 2000;
        //zeit.Elapsed += OnTick;
        zeit.Start();

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Update(feld);

            //TODO: Time-Loop
        

            Start_Stellung(feld);

            for (int x = 0; x < feld.GetLength(0); x++)
            {
                p_x = 0;

                for (int y = 0; y < feld.GetLength(1); y++)
                {

                    /*if (y % 2 == 0)
                    {
                        feld[x, y] = 0;
                    }

                    if (y % 2 == 1)
                    {
                        feld[x, y] = 1;
                    }*/

                    p_x += abstand;

                    if (feld[x, y] == 1)
                    {
                        e.Graphics.FillRectangle(Brushes.Green, p_x, p_y, 20, 20);
                        //p_y += 20;
                    }


                    if (feld[x, y] == 0)
                    {
                        e.Graphics.DrawRectangle(Pens.Blue, p_x, p_y, 20, 20);
                        ///p_y += 20;
                    }
                }

                p_y += abstand;
            }

            //Update(feld);
            //Ende Loop
                
        }

        private void OnTick(object source, ElapsedEventArgs e)
        {

        }

        static void Start_Stellung(int[,] feld)
        {
            feld[5, 4] = 1;
            feld[5, 5] = 1;
            feld[5, 6] = 1;
        }

        static void Update(int[,] feld)
        {
            for (int x = 1; x < feld.GetLength(0); x++)
            {
                for (int y = 1; y < feld.GetLength(1); y++)
                {
                    if (feld[x, y] == 1)
                    {
                        
                        if (   feld[x - 1, y - 1]   + feld[x - 1, y]    + feld[x - 1, y + 1]
                             + feld[x, y - 1]                           + feld[x, y + 1]
                             + feld[x + 1, y - 1]   + feld[x + 1, y]    + feld[x + 1, y + 1]
                             < 2)
                        {
                            feld[x, y] = 0;
                        }

                        if (   feld[x - 1, y - 1]   + feld[x - 1, y] + feld[x - 1, y + 1]
                             + feld[x, y - 1]                        + feld[x, y + 1]
                             + feld[x + 1, y - 1]   + feld[x + 1, y] + feld[x + 1, y + 1]
                             > 3)
                        {
                            feld[x, y] = 0;
                        }
                    }

                    if (feld[x, y] == 0)
                    {
                        if (   feld[x - 1, y - 1]   + feld[x - 1, y]    + feld[x - 1, y + 1]
                             + feld[x, y - 1]       + feld[x, y]        + feld[x, y + 1]
                             + feld[x + 1, y - 1]   + feld[x + 1, y]    + feld[x + 1, y + 1]
                             == 3)
                        {
                            feld[x, y] = 1;
                        }
                    }
                }//for y
            }   //for x
        }       //Update
    }
}
