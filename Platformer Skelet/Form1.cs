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
        public bool SuppressKeyPress { get; set; }
        bool rightSide = true;
        bool go = false;
        bool jump = false;
        int walkFrame = 0;
        int stayFrame = 0;
        int jumpFrame = 0;
        int blockFrame = 0;
        int attackFrame = 0;
        int typeAnimation = 0;

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Keyboard);
            this.KeyUp += new KeyEventHandler(FreeKeyb);
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(Update);
            timer2.Tick += new EventHandler(UpdateMoving);
            timer2.Interval = 20;
            timer1.Start();
            timer2.Start();
        }

        private void UpdateMoving(object sender, EventArgs e)
        {
            if(typeAnimation == 2)
            {
                if(rightSide == true)
                    characterModel.Location = new Point(characterModel.Location.X + 4, characterModel.Location.Y);
                else
                    characterModel.Location = new Point(characterModel.Location.X - 4, characterModel.Location.Y);

            }
            else if (typeAnimation == 3)
            {
                if (go == false) { }
                else if (go == true && rightSide == true) { }
                else if (go == true && rightSide == false) { }

            }
            else if(typeAnimation == 4)
            {
                if (go == false) { }
                else if (go == true && rightSide == true) { }
                else if (go == true && rightSide == false) { }
            }
            else if(typeAnimation == 5)
            {
                if (go == false) { }
                else if (go == true && rightSide == true) { }
                else if (go == true && rightSide == false) { }
            }
        }

        private void FreeKeyb(object sender, KeyEventArgs e)
        {
            if(rightSide == true) {
                typeAnimation = 0;
                go = false;
                jump = false;
            }
            if(rightSide == false) { 
                typeAnimation = 1;
                go = false;
                jump = false;
            }
        }

        private void Keyboard(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.D)) {
                rightSide = true;
                go = true;
                jump = false;
                typeAnimation = 2;
            }
            else if (e.KeyCode.Equals(Keys.A)) {
                rightSide = false;
                go = true;
                jump = false;
                typeAnimation = 2;
            }
            else if (e.KeyCode == Keys.Space)
            {
                go = false;
                jump = true;
                typeAnimation = 3;
            }
            else if (e.KeyCode == Keys.D & e.KeyCode == Keys.Space) 
            {
                rightSide = true;
                go = true;
                jump = true;
                typeAnimation = 3;
            }
            else if (e.KeyCode == Keys.A & e.KeyCode == Keys.Space)
            {
                rightSide = false;
                go = true;
                jump = true;
                typeAnimation = 3;
            }
            else if (e.KeyCode == Keys.Q)
            {
                go = false;
                jump = false;
                typeAnimation = 4;
            }

            else if (e.KeyCode == Keys.D & e.KeyCode == Keys.Q)
            {
                rightSide = true;
                go = true;
                jump = false;
                typeAnimation = 4;
            }
            else if (e.KeyCode == Keys.A & e.KeyCode == Keys.Q)
            {
                rightSide = false;
                go = true;
                jump = false;
                typeAnimation = 4;
            }
            else if (e.KeyCode == Keys.E)
            {
                go = false;
                jump = false;
                typeAnimation = 5;
            }
            else if (e.KeyCode == Keys.D & e.KeyCode == Keys.E)
            {
                rightSide = true;
                go = true;
                jump = false;
                typeAnimation = 5;
            }
            else if (e.KeyCode == Keys.A & e.KeyCode == Keys.E)
            {
                rightSide = false;
                go = true;
                jump = false;
                typeAnimation = 5;
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
                jumpFrame++;
                if (jumpFrame == 1) jumpFrame = 0;
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
                    g.DrawImage(Properties.Resources.jumpright,0,0, new Rectangle(new Point(150*jumpFrame,0), new Size(150,150)), GraphicsUnit.Pixel);
                    characterModel.Image = part;
                }
                else
                {
                    Image part = new Bitmap(150, 150);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(Properties.Resources.jumpleft, 0, 0, new Rectangle(new Point(150 * jumpFrame, 0), new Size(150, 150)), GraphicsUnit.Pixel);
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
    }
}
