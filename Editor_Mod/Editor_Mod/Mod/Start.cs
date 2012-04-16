using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Editor_Mod
{
  public static class Start
    {
      public static void Entry(string path)
        {
         
            try
            {
 
                GamePath = path;
                Mod start = new Mod();
                start.Run();

            }
            finally
            {

            }
        }
      public static string GamePath;
    }
}
