using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Terraria;
namespace Editor_Mod
{
   public class WorldGenReflect
    {
        public static Type WorldGen { get; set; }
        public static bool PlaceTile(int i, int j, int type, bool mute = false, bool forced = false, int plr = -1, int style = 0)
        {
            return (bool)WorldGen.GetMethod("PlaceTile").Invoke(typeof(bool), new object[] { i, j, type, mute , forced , plr = -1, style = 0 });
        }
     
        public static void PlaceWall(int i, int j, int type, bool mute = false)
        {
            WorldGen.GetMethod("PlaceWall").Invoke(null, new object[] { i, j, type, mute = false });
        }
        public void TileFrame(int x, int y, bool reset = false, bool breaks = true)
        {
            WorldGen.GetMethod("TileFrame").Invoke(null, new object[] { x, y, reset = false, breaks = true });
        }
        public static void KillWall(int i, int j, bool fail = false)
      
        {
            WorldGen.GetMethod("KillWall").Invoke(null, new object[] {  i,   j,   fail = false});
   
        }
        public static void KillTile(int i, int j, bool fail = false, bool effectOnly = false, bool noItem = false)
        {
            WorldGen.GetMethod("KillTile").Invoke(null, new object[] { i, j, fail = false, effectOnly = false, noItem = false });
        }
        public static bool shadowOrbSmashed
        {
            get
            {
                return (bool)WorldGen.GetField("shadowOrbSmashed").GetValue(null);
            }
            set
            {
                WorldGen.GetField("shadowOrbSmashed").SetValue(null, value);
            }
        }

    }
}

   
   
   
   
   
   
 