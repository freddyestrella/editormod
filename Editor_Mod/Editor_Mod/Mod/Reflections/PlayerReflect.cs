using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace Editor_Mod
{
  public  class PlayerReflect
    {
        public static Type Player { get; set; }


        public static int jumpHeight
        {
            get
            {
                return (int)Player.GetField("jumpHeight", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                Player.GetField("jumpHeight", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }
        public static float jumpSpeed
        {
            get
            {
                return (float)Player.GetField("jumpSpeed", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                Player.GetField("jumpSpeed", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }






        public static int itemGrabRange
        {
            get
            {
                return (int)Player.GetField("itemGrabRange", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                Player.GetField("itemGrabRange", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }

        public static float itemGrabSpeed
        {
            get
            {
                return (float)Player.GetField("itemGrabSpeed", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                Player.GetField("itemGrabSpeed", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }


        public static float itemGrabSpeedMax
        {
            get
            {
                return (float)Player.GetField("itemGrabSpeedMax", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                Player.GetField("itemGrabSpeedMax", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }





    }
}
