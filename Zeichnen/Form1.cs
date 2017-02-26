using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Zeichnen
{
    /*public class Thread_1
    {
        System.Threading.Thread thdWorker = new Thread(new ThreadStart(Form1.Form1_Paint()));
    }*/


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //static System.Windows.Forms.Timer zeit = new System.Windows.Forms.Timer();

        static int grid = 10;
        static int feldgroesse = 20;

        static int[,] gen_akt = new int[grid, grid];
        
        static int p_x = feldgroesse;
        static int p_y = feldgroesse;
        static int start = 1;


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if ( start == 1 )
            {
                Start_Stellung(gen_akt);
                start++;
            }
            
            while (1 > 0)
            {

                //Zurücksetzen
                int[,] gen_neu = new int[grid, grid];
                p_y = 0;

                //Zeichnen anhand der Werte in gen_akt
                for (int x = 0; x < gen_akt.GetLength(0); x++)
                {
                    p_x = 0;  //Zurücksetzen

                    p_y += feldgroesse; //Position y

                    for (int y = 0; y < gen_akt.GetLength(1); y++)
                    {
                        p_x += feldgroesse; //Position x
                        
                        if (gen_akt[x, y] == 1)
                        {
                            e.Graphics.FillRectangle(Brushes.Blue, p_x, p_y, feldgroesse, feldgroesse);
                        }
                        
                        if (gen_akt[x, y] == 0)
                        {
                            e.Graphics.DrawRectangle(Pens.Blue, p_x, p_y, feldgroesse, feldgroesse);
                        }
                    }
                }

                Naechste_Gen(gen_akt, gen_neu);
                gen_akt = gen_neu;

                Thread.Sleep(200);
                Form1.ActiveForm.Refresh();

                //Form1.ActiveForm.Update();
                //Form1.ActiveForm.Invalidate();
            }//while

        }//Form1_Paint
        
        public static void Start_Stellung(int[,] feld)
        {
            feld[2, 2] = 1;
            feld[2, 3] = 1;
            feld[2, 4] = 1;
            feld[1, 4] = 1;
            feld[0, 3] = 1;
        }

        public static void Naechste_Gen(int[,] gen_akt, int[,] gen_neu)
        {
            for (int r = 0; r < gen_akt.GetLength(0); r++)
            {
                for (int c = 0; c < gen_akt.GetLength(1); c++)
                {
                    int rO = r;
                    int rU = r;
                    int cL = c;
                    int cR = c;

                    //Sonderbedingung für Ränder
                    if (r == 0)
                    {
                        rO = gen_akt.GetLength(0);
                    }
                    if (r == gen_akt.GetLength(0) - 1)
                    {
                        rU = -1;
                    }
                    if (c == 0)
                    {
                        cL = gen_akt.GetLength(1);
                    }
                    if (c == gen_akt.GetLength(1) - 1)
                    {
                        cR = -1;
                    }

                    //Leben und sterben
                    if (gen_akt[r, c] == 1)
                    {
                        //sterben
                        if (   gen_akt[rO - 1, cL - 1] + gen_akt[rO - 1, c] + gen_akt[rO - 1, cR + 1]
                             + gen_akt[r, cL - 1]                           + gen_akt[r, cR + 1]
                             + gen_akt[rU + 1, cL - 1] + gen_akt[rU + 1, c] + gen_akt[rU + 1, cR + 1]
                             < 2
                             ||
                               gen_akt[rO - 1, cL - 1] + gen_akt[rO - 1, c] + gen_akt[rO - 1, cR + 1]
                             + gen_akt[r, cL - 1]                           + gen_akt[r, cR + 1]
                             + gen_akt[rU + 1, cL - 1] + gen_akt[rU + 1, c] + gen_akt[rU + 1, cR + 1]
                             > 3)
                        {
                            gen_neu[r, c] = 0;
                        }

                        //weiter leben
                        else
                        {
                            gen_neu[r, c] = 1;
                        }
                    }

                    if (gen_akt[r, c] == 0)
                    {
                        //neues leben
                        if (   gen_akt[rO - 1, cL - 1] + gen_akt[rO - 1, c] + gen_akt[rO - 1, cR + 1]
                             + gen_akt[r, cL - 1]                           + gen_akt[r, cR + 1]
                             + gen_akt[rU + 1, cL - 1] + gen_akt[rU + 1, c] + gen_akt[rU + 1, cR + 1]
                             == 3)
                        {
                            gen_neu[r, c] = 1;
                        }
                    }
                }//y
            }//x

        }//Naechste_Gen

        /*static void Feld_zeichnen(int[,] feld,int feldgroesse)
        {
            int p_x = feldgroesse;
            int p_y = feldgroesse;
        }*/

    }
}
