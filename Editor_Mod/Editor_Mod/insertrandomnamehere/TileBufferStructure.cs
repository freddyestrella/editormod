using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
namespace Editor_Mod
{
    public class Tiles
    {
        public Tile maintile;
       
        public Point loc;

        public Tiles(Tile Maintile, Point Loc)
        {

            this.maintile = Maintile;
             this.loc = Loc;

        }

    }
}