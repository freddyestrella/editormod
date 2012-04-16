using Microsoft.Xna.Framework;
using System;
using Terraria;
namespace Editor_Mod
{
    public class NPCspawnText
    {
        public int size;
        public int X;
        public int Y;
        public string NpcName;
        public NPCspawnText(int x, int y, string name, int size)
        {
            this.X = x;
            this.Y = y;
            this.NpcName = name;
            this.size = size;
        }
    }
}