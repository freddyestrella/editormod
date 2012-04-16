
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Editor_Mod
{
   public class itemInventory
   {
       public string Classification = "";
       public List<string> invetory = new List<string>();
       public itemInventory(List<string> inv, string cl)
       {
           this.invetory = inv;
           this.Classification = cl;
       }

    }
}
