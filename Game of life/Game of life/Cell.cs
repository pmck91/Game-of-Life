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

            if (friends < 2 && living) { colour = Color.Red; murder(); }
            else if (friends == 2 && living) { colour = Color.DeepSkyBlue; }
            else if (friends == 3) { colour = Color.LawnGreen; revive(); }
            else if (friends > 3 && living) { colour = Color.Red; murder(); }
            else { colour = Color.White; murder(); }

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
