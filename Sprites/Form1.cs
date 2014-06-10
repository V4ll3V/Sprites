using Sprites.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace Sprites
{
    public partial class Form1 : Form
    {

        

        Graphics gfx = null;
        Sprite chrono;
        private const int FRAMES_PER_SECOND = 25;
        private Timer m_timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            this.DoubleBuffered = true;

            //Timer for FPS 
            m_timer.Interval = (int)(1000.0 * (1.0 / FRAMES_PER_SECOND));
            m_timer.Tick += new EventHandler(timer1_Tick);
            m_timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gfx = canvas1.CreateGraphics();
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            chrono = new Sprite("megaman-walkleft.png", gfx);           
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Bitmap background = new Bitmap("background.jpg");
            gfx.DrawImage(background,0,0, 450, 300);
            Rectangle destRect = new Rectangle(chrono.newPosX, chrono.newPosY, chrono.newPosW, chrono.newPosH);
            Rectangle srcRect = new Rectangle(chrono.destX, chrono.destY, chrono.newPosW, chrono.newPosH);
            GraphicsUnit units = GraphicsUnit.Pixel;
            gfx.DrawImage(chrono.sprite, destRect, srcRect, units);
           // e.Graphics.Clear(Color.Black);

            //canvas.Invalidate();
        }
        //Function to controll movement. Each case calls the sprite Obj. walk function with the attributes for the movement direction, and movement on x,y axis
        //The bool variable is for the state that the button is pressed( look at sprites.getAnim() )
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    {
                        chrono.setDirection("left", -1, 0, true);
                        break;
                    }
                case Keys.S:
                    {
                        if (chrono.jumpHeight == 1)
                        {
                            chrono.setDirection("jumpd", 0, +1, true); 
                        }
                       
                        break;
                    }
                case Keys.D:
                    {
                        chrono.setDirection("right", +1, 0, true);
                        break;
                    }
                case Keys.W:
                    {
                        int jumpDirection = 0;

                        //the jump direction depents on the last direction
                        if (chrono.lastDirection.Equals("left"))
                        {
                            jumpDirection = -1;
                        }
                        else if (chrono.lastDirection.Equals("right"))
                        {
                            jumpDirection = +1;
                        }
                        //the height change for the jump
                        if (chrono.jumpHeight == 0)
                        {
                            chrono.newPosY -= 65;
                            chrono.setDirection("jump" + chrono.lastDirection, jumpDirection, 0, true);
                        }
                        
                        break;
                    }
            }
        }
        //keyListner to stop when the buttons are no longer pressed
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    {
                        chrono.setDirection("left", -1, 0, false);
                        break;
                    }
                case Keys.S:
                    {
                        if (chrono.jumpHeight == 1)
                        {
                            chrono.setDirection("jumpd", 0, +1, true);
                            chrono.newPosY += 65;
                            chrono.jumpHeight = 0;
                        }
                        break;
                    }
                case Keys.D:
                    {
                        chrono.setDirection("right", +1, 0, false);
                        break;
                    }

                case Keys.W:
                    {
                        {
                            chrono.setDirection("jump" + chrono.lastDirection, +1, 0, false);
                            chrono.jumpHeight = 1;
                            break;

                        }
                    }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_timer.Stop();
            //Function call to set the right sprite so it cdan be painted.
            chrono.getAnim();
            Invalidate();
            m_timer.Start();
        }
        
    }
}