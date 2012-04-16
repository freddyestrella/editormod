using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Terraria;
namespace Editor_Mod
{
    class MainReflect
    {
        public static Type Main { get; set; }
        public static dynamic[] player
        {
            get
            {
                return (dynamic[])Main.GetField("player").GetValue(null);
            }
            set
            {
                Main.GetField("player").SetValue(null, value);
            }
        }

        public static void DrawPlayer(Player drawPlayer )
        {
            Main.GetMethod("DrawPlayer").Invoke(null, new object[] { drawPlayer = new Player()});
        }

    }
}
