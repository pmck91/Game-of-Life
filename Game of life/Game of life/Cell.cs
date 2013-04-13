using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_of_life
{
    public class Cell
    {
        bool living { get; set; }
        Color colour { get; set; }
        int friends { get; set; }

        public Cell()
        {
            living = false;
            colour = Color.White;
            friends = 0;
        }

        public void playGod(int friends)
        {
            this.friends = friends;

            if (living && friends < 2)
            {
                murder();
            }
            else if (living && (friends == 2))
            {
                revive();
                colour = Color.DeepSkyBlue;
            }
            else if (living && (friends == 3))
            {
                revive();
                colour = Color.Cyan;
            }
            else if (living && friends > 3)
            {
                murder();
            }
            else if (!living && friends == 3)
            {
                revive();
                colour = Color.LawnGreen;
            }

            this.friends = 0;
        }

        public void murder()
        {
            living = false;
        }

        public void revive()
        {
            living = true;
        }

        public bool happilyAlive()
        {
            return living;
        }

        public Color health()
        {
            return colour;
        }

        public void setColour(Color colour)
        {
            this.colour = colour;
        }
    }
}
