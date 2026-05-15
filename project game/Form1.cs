using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_game
{

    public class hero
    {

        public int x, y;
        public List<Bitmap> imgs;
        public List<Bitmap> imgs2;
        public List<Bitmap> imgs3;
        public List<Bitmap> imgs4;
        public List<Bitmap> imgs5;
        public List<Bitmap> imgs6;

        public int IF;
        public int IF2;
        public int IF3;
        public int IF4;
        public int IF5;
        public int IF6;
        public int direc;
        public int move;
        public int fall;

        public int verticalVelocity = 0;
        public bool isRightPressed = false;
        public bool isLeftPressed = false;
        public bool isJumping = false;
        public bool issprinting = false;
        public bool isidle = false;
        public bool ismoving = false;
        public bool isattack = false;
        public bool onGround = true;
        public bool isClimbing = false;
        public bool isOnLadder = false;

        
    }


    public class ladder
     {
         public int x, y, width, height;
        public Bitmap img;

        
    }
    






    ///aaaaaa
    public partial class Form1 : Form
    {
        Bitmap off;
        List<hero> L = new List<hero>();// hero
        List<ladder> Ladders = new List<ladder>();
        Timer t = new Timer();
        int currentSpeed;
        int cttick = 0;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            t.Interval = 60;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            Paint += Form1_Paint;
            MouseDown += Form1_MouseDown;
            MouseUp += Form1_MouseUp;
            t.Tick += T_Tick;
            t.Start();
            InitializeComponent();

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            hero ptrav = L[0];

            if(ptrav.isattack==false)
            {
                ptrav.isattack = true;
                ptrav.IF4 = 0;
            }

           
        }

        private void isclimping()
        {
          

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            hero ptrav = L[0];
            if (e.KeyCode == Keys.D)
            {
                ptrav.isRightPressed = false;
            }

            if (e.KeyCode == Keys.A)
            {

                ptrav.isLeftPressed = false;
            }

            if (e.KeyCode == Keys.ShiftKey)
            {

                ptrav.issprinting = false;

            }

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space)
            {
                ptrav.isJumping = false;
            }

          
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            hero ptrav = L[0];
            if (e.KeyCode == Keys.D)
            {

                ptrav.isRightPressed = true;
             
            }

            if (e.KeyCode == Keys.A)
            {

                ptrav.isLeftPressed = true;
              
            }

            if (e.KeyCode == Keys.ShiftKey)
            {

                ptrav.issprinting = true;
               
            }

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space)
            {

                ptrav.isJumping = true;
               
            }

        }

        private void T_Tick(object sender, EventArgs e)
        {
            hero ptrav = L[0];
            ptrav.ismoving = false;

            if (ptrav.isattack == true)
            {
                ptrav.IF4++;
                if (ptrav.IF4 >= ptrav.imgs4.Count) 
                {
                    ptrav.IF4 = 0;
                    ptrav.isattack = false;
                }
            }

            if (ptrav.issprinting == true)
            {
                currentSpeed = 13;
            }
            else
            {
                currentSpeed = 7;
            }

            if (ptrav.isRightPressed == true)
            {
                ptrav.x += currentSpeed;
                ptrav.ismoving = true;

                if (ptrav.issprinting == true)
                {
                    ptrav.IF5 = (ptrav.IF5 + 1) % ptrav.imgs5.Count; 
                }
                else
                {
                    ptrav.IF = (ptrav.IF + 1) % ptrav.imgs.Count; 
                }
            }

            if (ptrav.isLeftPressed == true)
            {
                ptrav.x -= currentSpeed;
                ptrav.ismoving = true;

                if (ptrav.issprinting == true)
                {
                    ptrav.IF5 = (ptrav.IF5 + 1) % ptrav.imgs5.Count; 
                }
                else
                {
                    ptrav.IF = (ptrav.IF + 1) % ptrav.imgs.Count; 
                }
            }

            int groundY = ClientSize.Height - 220;

            if (ptrav.isJumping == true && ptrav.onGround == true)
            {
                ptrav.verticalVelocity = -20;
                ptrav.onGround = false;
                ptrav.ismoving = true;
            }

            if (ptrav.ismoving == false && ptrav.onGround == true)
            {
                ptrav.isidle = true;
                ptrav.IF3 = (ptrav.IF3 + 1) % ptrav.imgs3.Count; 
            }
            else
            {
                ptrav.isidle = false;
                ptrav.IF3 = 0;
            }

            if (ptrav.onGround == false)
            {
               
                int middleFrame = ptrav.imgs2.Count / 2;

                if (ptrav.verticalVelocity < 0) 
                {
                    ptrav.IF2 = (ptrav.IF2 + 1) % middleFrame;
                }
                else if (ptrav.verticalVelocity > 0) 
                {
                   
                    if (ptrav.IF2 < middleFrame) ptrav.IF2 = middleFrame;
                    ptrav.IF2 = ((ptrav.IF2 - middleFrame + 1) % (ptrav.imgs2.Count - middleFrame)) + middleFrame;
                }

                ptrav.y += ptrav.verticalVelocity;
                ptrav.verticalVelocity += 2;

                if (ptrav.y >= groundY)
                {
                    ptrav.y = groundY;
                    ptrav.onGround = true;
                    ptrav.verticalVelocity = 0;
                    ptrav.IF2 = 0;
                }
            }
            drawDubb(this.CreateGraphics());
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawDubb(this.CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            drawhero();
            ladder();
        }


        void ladder()
        {
            Ladder pnn = new Ladder();
             pnn.img = new Bitmap("L.png");
             pnn.x = ClientSize.Width/2;
             pnn.y = 200;
             Ladders.Add(pnn);
             drawDubb(this.CreateGraphics());
        }

        void drawhero()
        {

            hero pnn = new hero();
            pnn.x = 100;
            pnn.y = ClientSize.Height - 220;
            pnn.IF = 0;
            pnn.IF2 = 0;
            pnn.IF3 = 0;
            pnn.IF4 = 0;
            pnn.IF5 = 0;
            pnn.imgs = new List<Bitmap>(); //walk
            pnn.imgs2 = new List<Bitmap>(); // jump
            pnn.imgs3 = new List<Bitmap>(); //Idle
            pnn.imgs4 = new List<Bitmap>(); //Attack
            pnn.imgs5 = new List<Bitmap>(); // Sprint
            pnn.move = 1;
            pnn.direc = 1;
            for (int i = 1; i < 9; i++)
            {
                Bitmap sora = new Bitmap(  (i) + ".png");
                sora.MakeTransparent(sora.GetPixel(0, 0));
                pnn.imgs.Add(sora);
            }

            for (int i = 1; i < 6; i++)
            {
                Bitmap sora2 = new Bitmap("J" + i + ".png");
                sora2.MakeTransparent(sora2.GetPixel(0, 0));
                pnn.imgs2.Add(sora2);

            }


            for (int i = 1; i < 8; i++)
            {
                Bitmap sora3 = new Bitmap("I" + i + ".png");
                sora3.MakeTransparent(sora3.GetPixel(0, 0));
                pnn.imgs3.Add(sora3);

            }

            for (int i = 1; i < 7; i++)
            {
                Bitmap sora4 = new Bitmap("A" + i + ".png");
                sora4.MakeTransparent(sora4.GetPixel(0, 0));
                pnn.imgs4.Add(sora4);

            }


            for (int i = 0; i < 8; i++)
            {
                Bitmap sora5 = new Bitmap("R" + i + ".png");
                sora5.MakeTransparent(sora5.GetPixel(0, 0));
                pnn.imgs5.Add(sora5);

            }



            L.Add(pnn);
            drawDubb(this.CreateGraphics());
        }

        void drawDubb(Graphics g)
        {

            Graphics g2 = Graphics.FromImage(off);
            drawscene(g2);
            g.DrawImage(off, 0, 0);


        }
        void drawscene(Graphics g2)
        {
            g2.Clear(Color.DarkOrange);

            for (int i = 0; i < L.Count; i++)
            {
                hero Ptrav = L[i];


                if (Ptrav.isattack == true)
                {
                    g2.DrawImage(Ptrav.imgs4[Ptrav.IF4], Ptrav.x, Ptrav.y);
                }
               
                else if (Ptrav.onGround == false)
                {
                    g2.DrawImage(Ptrav.imgs2[Ptrav.IF2], Ptrav.x, Ptrav.y);
                }
            
                else if (Ptrav.isidle == true)
                {
                    g2.DrawImage(Ptrav.imgs3[Ptrav.IF3], Ptrav.x, Ptrav.y);
                }
              
                else
                {
                   
                    if (Ptrav.issprinting && Ptrav.ismoving)
                    {
                        g2.DrawImage(Ptrav.imgs5[Ptrav.IF5], Ptrav.x, Ptrav.y);
                    }
                    else
                    {
                        g2.DrawImage(Ptrav.imgs[Ptrav.IF], Ptrav.x, Ptrav.y);
                    }
                }
            }

            for( int i =0;i<Ladders.count;i++)
            { 
                ladder ptrav = Ladders[i];
                g2.drawImage(ptrav.img,ptrav.x,ptrav.y);         
            }




        }

    }
}
