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
        public Item item { get{ return ExtractItemID(); }}
        public Tiles(Tile Maintile, Point Loc)
        {
            this.maintile = Maintile;
            this.loc = Loc;
        }
        private Item ExtractItemID()
        {
            int num8 =-1;
            if (maintile.type == 13)
            {

                if (maintile.frameX == 18)
                {
                    num8 = 28;
                }
                else
                {
                    if (maintile.frameX == 36)
                    {
                        num8 = 110;
                    }
                    else
                    {
                        if (maintile.frameX == 54)
                        {
                            num8 = 350;
                        }
                        else
                        {
                            if (maintile.frameX == 72)
                            {
                                num8 = 351;
                            }
                            else
                            {
                                num8 = 31;
                            }
                        }
                    }
                }
            }
            else
            {
                if (maintile.type == 19)
                {
                    num8 = 94;
                }
                else
                {
                    if (maintile.type == 22)
                    {
                        num8 = 56;
                    }
                    else
                    {
                        if (maintile.type == 140)
                        {
                            num8 = 577;
                        }
                        else
                        {
                            if (maintile.type == 23)
                            {
                                num8 = 2;
                            }
                            else
                            {
                                if (maintile.type == 25)
                                {
                                    num8 = 61;
                                }
                                else
                                {
                                    if (maintile.type == 30)
                                    {
                                        num8 = 9;
                                    }
                                    else
                                    {
                                        if (maintile.type == 33)
                                        {
                                            num8 = 105;
                                        }
                                        else
                                        {
                                            if (maintile.type == 37)
                                            {
                                                num8 = 116;
                                            }
                                            else
                                            {
                                                if (maintile.type == 38)
                                                {
                                                    num8 = 129;
                                                }
                                                else
                                                {
                                                    if (maintile.type == 39)
                                                    {
                                                        num8 = 131;
                                                    }
                                                    else
                                                    {
                                                        if (maintile.type == 40)
                                                        {
                                                            num8 = 133;
                                                        }
                                                        else
                                                        {
                                                            if (maintile.type == 41)
                                                            {
                                                                num8 = 134;
                                                            }
                                                            else
                                                            {
                                                                if (maintile.type == 43)
                                                                {
                                                                    num8 = 137;
                                                                }
                                                                else
                                                                {
                                                                    if (maintile.type == 44)
                                                                    {
                                                                        num8 = 139;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (maintile.type == 45)
                                                                        {
                                                                            num8 = 141;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (maintile.type == 46)
                                                                            {
                                                                                num8 = 143;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (maintile.type == 47)
                                                                                {
                                                                                    num8 = 145;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (maintile.type == 48)
                                                                                    {
                                                                                        num8 = 147;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (maintile.type == 49)
                                                                                        {
                                                                                            num8 = 148;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (maintile.type == 51)
                                                                                            {
                                                                                                num8 = 150;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (maintile.type == 53)
                                                                                                {
                                                                                                    num8 = 169;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (maintile.type == 54)
                                                                                                    {
                                                                                                        num8 = 170;

                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (maintile.type == 56)
                                                                                                        {
                                                                                                            num8 = 173;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (maintile.type == 57)
                                                                                                            {
                                                                                                                num8 = 172;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (maintile.type == 58)
                                                                                                                {
                                                                                                                    num8 = 174;
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (maintile.type == 60)
                                                                                                                    {
                                                                                                                        num8 = 176;
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (maintile.type == 70)
                                                                                                                        {
                                                                                                                            num8 = 176;
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (maintile.type == 75)
                                                                                                                            {
                                                                                                                                num8 = 192;
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (maintile.type == 76)
                                                                                                                                {
                                                                                                                                    num8 = 214;
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (maintile.type == 78)
                                                                                                                                    {
                                                                                                                                        num8 = 222;
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (maintile.type == 81)
                                                                                                                                        {
                                                                                                                                            num8 = 275;
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (maintile.type == 80)
                                                                                                                                            {
                                                                                                                                                num8 = 276;
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if (maintile.type == 107)
                                                                                                                                                {
                                                                                                                                                    num8 = 364;
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if (maintile.type == 108)
                                                                                                                                                    {
                                                                                                                                                        num8 = 365;
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if (maintile.type == 111)
                                                                                                                                                        {
                                                                                                                                                            num8 = 366;
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if (maintile.type == 112)
                                                                                                                                                            {
                                                                                                                                                                num8 = 370;
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if (maintile.type == 116)
                                                                                                                                                                {
                                                                                                                                                                    num8 = 408;
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    if (maintile.type == 117)
                                                                                                                                                                    {
                                                                                                                                                                        num8 = 409;
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        if (maintile.type == 129)
                                                                                                                                                                        {
                                                                                                                                                                            num8 = 502;
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            if (maintile.type == 118)
                                                                                                                                                                            {
                                                                                                                                                                                num8 = 412;
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                if (maintile.type == 119)
                                                                                                                                                                                {
                                                                                                                                                                                    num8 = 413;
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    if (maintile.type == 120)
                                                                                                                                                                                    {
                                                                                                                                                                                        num8 = 414;
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        if (maintile.type == 121)
                                                                                                                                                                                        {
                                                                                                                                                                                            num8 = 415;
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            if (maintile.type == 122)
                                                                                                                                                                                            {
                                                                                                                                                                                                num8 = 416;
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                if (maintile.type == 136)
                                                                                                                                                                                                {
                                                                                                                                                                                                    num8 = 538;
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (maintile.type == 137)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        num8 = 539;
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (maintile.type == 141)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            num8 = 580;
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (maintile.type == 145)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                num8 = 586;
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (maintile.type == 146)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    num8 = 591;
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (maintile.type == 147)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        num8 = 593;
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (maintile.type == 148)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            num8 = 594;
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (maintile.type == 135)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (maintile.frameY == 0)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    num8 = 529;
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                if (maintile.frameY == 18)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    num8 = 541;
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                if (maintile.frameY == 36)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    num8 = 542;
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                if (maintile.frameY == 54)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    num8 = 543;
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (maintile.type == 144)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (maintile.frameX == 0)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        num8 = 583;
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    if (maintile.frameX == 18)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        num8 = 584;
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    if (maintile.frameX == 36)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        num8 = 585;
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (maintile.type == 130)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        num8 = 511;
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (maintile.type == 131)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            num8 = 512;
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (maintile.type == 61 || maintile.type == 74)
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (maintile.frameX == 144)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    num8 = 331;
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (maintile.frameX == 162)
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        num8 = 223;
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (maintile.frameX >= 108 && maintile.frameX <= 126 && Mod.genRand.Next(100) == 0)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            num8 = 208;
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            if (Mod.genRand.Next(100) == 0)
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                num8 = 195;
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (maintile.type == 59 || maintile.type == 60)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    num8 = 176;
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (maintile.type == 71 || maintile.type == 72)
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (Mod.genRand.Next(50) == 0)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            num8 = 194;
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            if (Mod.genRand.Next(2) == 0)
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                num8 = 183;
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (maintile.type >= 63 && maintile.type <= 68)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            num8 = (int)(maintile.type - 63 + 177);
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            if (maintile.type == 50)
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                if (maintile.frameX == 90)
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    num8 = 165;
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    num8 = 149;
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                if (Main.tileAlch[(int)maintile.type] && maintile.type > 82)
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    int num11 = (int)(maintile.frameX / 18);
                                                                                                                                                                                                                                                                    bool flag = false;
                                                                                                                                                                                                                                                                    if (maintile.type == 84)
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        flag = true;
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                    if (num11 == 0 && Main.dayTime)
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        flag = true;
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                    if (num11 == 1 && !Main.dayTime)
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        flag = true;
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                    if (num11 == 3 && Main.bloodMoon)
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        flag = true;
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                    num8 = 313 + num11;
                                                                                                                                                                                                                                                                    if (flag)
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        num8 = num11;
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

             Item T = new Item();
             T.SetDefaults(num8);
             return T;





        }






    }
}