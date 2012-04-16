using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
namespace Editor_Mod
{
    public class LazyInventory
    {


        private static List<int> type = new List<int>();
        private static List<int> stack = new List<int>();
        private static List<string> name = new List<string>();

        public static bool Saved;
        public static itemInventory InuseCategory;




        public static void ONclearinv(bool force = false)
        {
            if (force)
            {
                for (int num = 0; num < 40; num++)
                {
                    Main.player[Main.myPlayer].inventory[num].stack = 0;
                    Main.player[Main.myPlayer].inventory[num].SetDefaults(0, false);
                }
            }
        }
        public static bool Save()
        {

            if (!Saved)
            {
                for (int num = 0; num < 40; num++)
                {
                    type.Add(Main.player[Main.myPlayer].inventory[num].type);
                    name.Add(Main.player[Main.myPlayer].inventory[num].name);
                    stack.Add(Main.player[Main.myPlayer].inventory[num].stack);
                }
                Saved = true;

            }
            return Saved;
        }
        public static bool Restore(bool resut)
        {
            if (resut)
            {
                for (int num = 0; num < 40; num++)
                {
                    Main.player[Main.myPlayer].inventory[num].SetDefaults(type[num], false);
                    Main.player[Main.myPlayer].inventory[num].SetDefaults(name[num]);
                    Main.player[Main.myPlayer].inventory[num].stack = stack[num];
                }
                type.Clear();
                name.Clear();
                stack.Clear();
                Saved = false;
            }
            return resut;
        }



        public static void Changeinv(int index = 0)
        {
            if (LazyInventory.InuseCategory == null) { return; }

            if (index != 0)
            {
                index *= 40;
            }
            Main.playerInventory = true;
            ONclearinv(Save());
            int SlotCount = 0;
            for (int i = index; i < InuseCategory.invetory.Count; i++)
            {

                if (SlotCount <= 40)
                {
                    Item T = new Item();
                    T.SetDefaults(InuseCategory.invetory[i]);
                    T.stack = T.maxStack;
                    Main.player[Main.myPlayer].inventory[SlotCount] = T;
                    SlotCount++;

                }
                else { break; }
            }
        }
    }
}
