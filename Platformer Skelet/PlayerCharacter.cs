using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_Skelet
{
    internal class PlayerCharacter
    {
        public Image sprites;
        public int x, y;
        public Size size;
        public int currFrame = 0;
        public int currAnimation = 0;
        public int speed;
        public PlayerCharacter(Size size, int x, int y, Image CharacterModel)
        {
            this.size = size;
            this.x = x;
            this.y = y;
            this.sprites = CharacterModel;
            speed = 4;
        }
        public void Left()
        {
            x -= speed;
        }
        public void Right()
        {
            x += speed;
        }
    }
}
