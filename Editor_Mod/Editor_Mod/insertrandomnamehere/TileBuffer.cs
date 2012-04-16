
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Editor_Mod
{
    class HistoryEvent
    {   public List<Tiles> tiles;
        public HistoryEvent(List<Tiles> temp)
        {
            
            tiles = temp;
        }
       
    }
}
