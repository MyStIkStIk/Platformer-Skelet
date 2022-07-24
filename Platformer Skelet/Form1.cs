using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer_Skelet
{
    public partial class Form1 : Form
    {
        bool rightSide = true;
        bool go = false;
        bool jump = false;
        int walkFrame = 0;
        int stayFrame = 0;
        int blockFrame = 0;
        int attackFrame = 0;
        int typeAnimation = 0;
        int gravity = 0;
        const int mapHeight = 12;
        const int mapWidth = 40;
        const int blockSize = 75;
        static int[,] map = new int[mapHeight, mapWidth]
        {
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };

        public Form1()
        {
            InitializeComponent();
            characterModel.Location = new Point( 0 , 220 );
            this.KeyDown += new KeyEventHandler(Keyboard);
            this.KeyUp += new KeyEventHandler(FreeKeyb);
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(Update);
            timer2.Tick += new EventHandler(UpdateMoving);
            timer2.Interval = 30;
            timer1.Start();
            timer2.Start();
        }
        private void AnimationCharacter()
        {
            //stayright
            if (typeAnimation == 0)
            {
                Image part = new Bitmap(150, 150);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(Properties.Resources.posright, 0, 0, new Rectangle(new Point(150 * stayFrame, 0), new Size(150, 150)), GraphicsUnit.Pixel);
                characterModel.Image = part;
            }
            //stayleft
            if (typeAnimation == 1)
            {
                Image part = new Bitmap(150, 150);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(Properties.Resources.posleft, 0, 0, new Rectangle(new Point(150 * stayFrame, 0), new Size(150, 150)), GraphicsUnit.Pixel);
                characterModel.Image = part;
            }
            //walk
            if (typeAnimation == 2)
            {
                if (rightSide)
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(Properties.Resources.moveright, 0, 0, new Rectangle(new Point(150 * walkFrame, 0), new Size(150, 150)), GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
                else 
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(Properties.Resources.moveleft, 0, 0, new Rectangle(new Point(150 * walkFrame, 0), new Size(150, 150)), GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
            }
            //jump
            if (typeAnimation == 3)
            {
                if (rightSide)
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(Properties.Resources.jumpright,0,0, new Rectangle(new Point(150,0), new Size(150,150)), GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
                else
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(Properties.Resources.jumpleft, 0, 0, new Rectangle(new Point(0, 0), new Size(150, 150)), GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
            }
            //block
            if (typeAnimation == 4)
            {
                if (rightSide)
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(Properties.Resources.shieldright,0,0, new Rectangle(new Point(150*blockFrame,0), new Size(150,150)),GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
                else
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(Properties.Resources.shieldleft, 0, 0, new Rectangle(new Point(150 * blockFrame, 0), new Size(150, 150)), GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
            }
            //attack
            if (typeAnimation == 5)
            {
                if (rightSide) 
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage (Properties.Resources.hitright, 0, 0, new Rectangle(new Point(150*attackFrame,0), new Size(150,150)), GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
                else
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(Properties.Resources.hitleft, 0, 0, new Rectangle(new Point(150 * attackFrame, 0), new Size(150, 150)), GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
            }
        }
        private void Update(object sender, EventArgs e)
        {
            if (typeAnimation == 0 || typeAnimation == 1)
            {
                AnimationCharacter();
                stayFrame++;
                if (stayFrame == 3) stayFrame = 0;
            }
            else if (typeAnimation == 2)
            {
                AnimationCharacter();
                walkFrame++;
                if (walkFrame == 7) walkFrame = 0;
            }
            else if (typeAnimation == 3)
            {
                AnimationCharacter();
            }
            else if (typeAnimation == 4)
            {
                AnimationCharacter();
                blockFrame++;
                if (blockFrame == 4) blockFrame = 2;
            }
            else if(typeAnimation == 5)
            {
                AnimationCharacter();
                attackFrame++;
                if (attackFrame == 3) attackFrame = 0;
            }
        }
        private void UpdateMoving(object sender, EventArgs e)
        {
            bool collide = Collide();
            if (typeAnimation != 2 && typeAnimation != 4 && typeAnimation != 5)
            {
                CalculateJump();
                if (go == true && rightSide)
                    characterModel.Location = new Point(characterModel.Location.X + 3, characterModel.Location.Y);
                else if (go == true && rightSide == false)
                    characterModel.Location = new Point(characterModel.Location.X - 3, characterModel.Location.Y);
            }
            else if (typeAnimation == 2)
            {
                if (rightSide == true && collide == false) characterModel.Location = new Point(characterModel.Location.X + 4, characterModel.Location.Y);
                else if (rightSide == false && collide == false) characterModel.Location = new Point(characterModel.Location.X - 4, characterModel.Location.Y);

            }
            else if (typeAnimation == 4)
            {
            }
            else if (typeAnimation == 5)
            {
                if (go == true && rightSide == true) characterModel.Location = new Point(characterModel.Location.X + 2, characterModel.Location.Y);
                else if (go == true && rightSide == false) characterModel.Location = new Point(characterModel.Location.X - 2, characterModel.Location.Y);
            }
        }
        public void CalculateJump()
        {
            if (characterModel.Location.Y < 220 || jump)
            {
                characterModel.Location = new Point(characterModel.Location.X, characterModel.Location.Y + gravity);
                gravity += 2;
            }
            if (characterModel.Location.Y >= 220)
            {
                jump = false;
                if (go)
                    typeAnimation = 2;
                else if (rightSide)
                    typeAnimation = 0;
                else if (!rightSide)
                    typeAnimation = 1;


            }
        }
        public void AddForce()
        {
            if (!jump)
            {
                jump = true;
                gravity = -14;
            }
        }
        private void FreeKeyb(object sender, KeyEventArgs e)
        {
            bool collide = Collide();
            if (e.KeyCode.Equals(Keys.Space))
            {
                typeAnimation = 3;
                AddForce();
            }
            else if (rightSide == true)
            {
                if(collide)
                    characterModel.Location = new Point(characterModel.Location.X - 5, characterModel.Location.Y);
                typeAnimation = 0;
                go = false;
            }
            else if (rightSide == false)
            {
                if (collide)
                    characterModel.Location = new Point(characterModel.Location.X + 5, characterModel.Location.Y);
                typeAnimation = 1;
                go = false;
            }
        }
        private void Keyboard(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.D))
            {
                rightSide = true;
                go = true;
                if (!jump)
                    typeAnimation = 2;
            }
            else if (e.KeyCode.Equals(Keys.A))
            {
                rightSide = false;
                go = true;
                if (!jump)
                    typeAnimation = 2;
            }
            else if (e.KeyCode == Keys.Q)
            {
                go = false;
                typeAnimation = 4;
            }
            else if (e.KeyCode == Keys.E)
            {
                go = false;
                typeAnimation = 5;
            }
            else if (jump)
                typeAnimation = 3;
        }
        public static void DrawMap(Graphics g)
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i,j] == 1)
                        g.DrawImage(Properties.Resources.trava, j * blockSize, i * blockSize, new Rectangle(new Point(0, 0), new Size(blockSize, blockSize)), GraphicsUnit.Pixel);
                    else if (map[i, j] == 2)
                        g.DrawImage(Properties.Resources.shipi, j * blockSize, i * blockSize, new Rectangle(new Point(0, 0), new Size(blockSize, blockSize)), GraphicsUnit.Pixel);
                }
            } 
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawMap(g);
        }
        public bool Collide()
        {
            bool result = false;
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 1)
                    {
                        if (characterModel.Location.X >= (j * blockSize - 4) && characterModel.Location.X <= (j * blockSize + 79) && characterModel.Location.Y >= (i * blockSize) && characterModel.Location.Y <= (i * blockSize + 75))
                            result = true;
                        else if(characterModel.Location.X+150 >= (j * blockSize - 4) && characterModel.Location.X+150 <= (j * blockSize + 79) && characterModel.Location.Y+150 >= (i * blockSize) && characterModel.Location.Y+150 <= (i * blockSize + 75))
                            result = true;
                    }

                }
            }
            return result;
        }
    }
    
}
