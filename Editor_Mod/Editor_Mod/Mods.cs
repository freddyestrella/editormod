using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Terraria;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
 
namespace Editor_Mod
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Mod : Main
    {
        public static string  Editorversion = @"2.0 - beta 2";
        public   const string quote = "\"";
        public static Keys keyPressed { get; set; }
        public static Player SelectedPlayer = new Player();
        public static List<itemInventory> Lazyinventory = new List<itemInventory>();
        public static List<NPCspawnText> spawnText = new List<NPCspawnText>();
        public Random random = new Random();
        public static List<Tiles> tilecopies = new List<Tiles>();
        public static bool arecomplete;
        public static Texture2D m_pixel;
        public static PolyGon polygons = new PolyGon();
        public static ToolWindow toolWindow;
        public static TileSelectionW TileSellW;
        public static NpcDictionaries NpcD;
        public static ItemDictionaries ItemD;
   
        public static bool IsVisible = true;
        SpriteBatch spriteBatch;
        private bool moveToolwindow;

        public static Game game;
 

        public static bool stopVectoring = true;
        public static bool forcepaste;
        private static Vector2 OldmoveMouse;
        private static Vector2 mouse;
        
        public static string NPCNAME;
 
        public static Item tileitem = new Item();
        public static Item wallitem = new Item();
        public static Item allitems = new Item();

        public static List<Tiles> LastAction { get; set; }
 
 
  
        private Vector2 P_Oldposition;
        private bool firsttime;
        private bool moveTileSelectionw;
 
        private int off;
        private float PlayersX = 250;
        private float playersY = 100;
        private static bool addvertice;
        private int olditem;

        public static Vector2 MouseOnMap { get { return new Vector2((((float)Main.mouseState.X + Main.screenPosition.X) / 16f), (((float)Main.mouseState.Y + Main.screenPosition.Y) / 16f)); }   }
    

        public float playersX { get { return Main.screenWidth - PlayersX; } set { PlayersX = value; } }
                 




        public Mod()
        {   
         
            Content.RootDirectory = Start.GamePath + @"\Content";
            

        }


        public static void addinv()
        {
            List<string> pick = new List<string>();
            List<string> axe = new List<string>();
            List<string> hammer = new List<string>();
            List<string> createTile = new List<string>();
            List<string> createWall = new List<string>();
            List<string> potion = new List<string>();
            List<string> headSlot = new List<string>();
            List<string> bodySlot = new List<string>();
            List<string> legSlot = new List<string>();
            List<string> healLife = new List<string>();
            List<string> healMana = new List<string>();
            List<string> vanity = new List<string>();
            List<string> material = new List<string>();
            List<string> melee = new List<string>();
            List<string> magic = new List<string>();
            List<string> ranged = new List<string>();
            List<string> mech = new List<string>();
            List<string> channel = new List<string>();
            List<string> accessory = new List<string>();
            List<string> buffType = new List<string>();
            List<string> ammo = new List<string>();
            List<string> useAmmo = new List<string>();



            foreach (string name in Main.itemName)
            {
                Item T = new Item();
                T.SetDefaults(name);
                if (T.useAmmo > 0)
                {
                    ammo.Add(T.name);
                }
                if (T.ammo > 0)
                {
                    ammo.Add(T.name);
                }
                if (T.pick > 0)
                {
                    pick.Add(T.name);
                }
                if (T.axe > 0)
                {
                    axe.Add(T.name);
                }
                if (T.hammer > 0)
                {
                    hammer.Add(T.name);
                }
                if (T.createTile > -1)
                {
                    createTile.Add(T.name);
                }
                if (T.createWall > -1)
                {
                    createWall.Add(T.name);
                }
                if (T.headSlot > -1)
                {
                    headSlot.Add(T.name);
                }
                if (T.bodySlot > -1)
                {
                    bodySlot.Add(T.name);
                }
                if (T.legSlot > -1)
                {
                    legSlot.Add(T.name);
                }
                if (T.healLife > 0)
                {
                    healLife.Add(T.name);
                }
                if (T.potion)
                {
                    potion.Add(T.name);
                }
                if (T.healMana>0)
                {
                    healMana.Add(T.name);
                }
                if (T.vanity)
                {
                    vanity.Add(T.name);
                }
                if (T.material)
                {
                    material.Add(T.name);
                }
                if (T.melee)
                {
                    melee.Add(T.name);
                }
                if (T.magic)
                {
                    magic.Add(T.name);
                }
                if (T.ranged)
                {
                    ranged.Add(T.name);
                }
                if (T.mech)
                {
                    mech.Add(T.name);
                }
                if (T.channel)
                {
                    channel.Add(T.name);
                }
                if (T.accessory)
                {
                    accessory.Add(T.name);
                }
                if (T.useAmmo>0)
                {
                    useAmmo.Add(T.name);
                }
                if (T.buffType > 0)
                {
                    buffType.Add(T.name);
                }
                 

            }
            Lazyinventory.Add(new itemInventory(pick, "pick"));
            Lazyinventory.Add(new itemInventory(axe, "axe"));
            Lazyinventory.Add(new itemInventory(hammer, "hammer"));
            Lazyinventory.Add(new itemInventory(createWall, "createWall"));
            Lazyinventory.Add(new itemInventory(createTile, "createTile"));
            Lazyinventory.Add(new itemInventory(mech, "mech"));
            Lazyinventory.Add(new itemInventory(legSlot, "legSlot"));
            Lazyinventory.Add(new itemInventory(bodySlot, "bodySlot"));
            Lazyinventory.Add(new itemInventory(headSlot, "headSlot"));
            Lazyinventory.Add(new itemInventory(accessory, "accessory"));
            Lazyinventory.Add(new itemInventory(potion, "potion"));
            Lazyinventory.Add(new itemInventory(buffType, "buffType"));
            Lazyinventory.Add(new itemInventory(healMana, "healMana"));
            Lazyinventory.Add(new itemInventory(healLife, "healLife"));
            Lazyinventory.Add(new itemInventory(channel, "channel"));
            Lazyinventory.Add(new itemInventory(ranged, "ranged"));
            Lazyinventory.Add(new itemInventory(ammo, "ammo"));
            Lazyinventory.Add(new itemInventory(useAmmo, "useAmmo"));
            Lazyinventory.Add(new itemInventory(magic, "magic"));
            Lazyinventory.Add(new itemInventory(melee, "melee"));
            Lazyinventory.Add(new itemInventory(material, "material"));
            Lazyinventory.Add(new itemInventory(vanity, "vanity"));


          
        }
        protected override void Initialize()
        {
         


            #region Terraria Field mods
            Main.versionNumber = "Terraria " + Main.versionNumber2 + @" / Editor Mod " + Editorversion ;
            maxMsg = 162;
            rxMsgType = new int[162];
            rxDataType = new int[162];
            txMsgType = new int[162];
            txDataType = new int[162];
  
	         #endregion
            NpcD = new NpcDictionaries();
            ItemD = new ItemDictionaries();
          
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
            addinv();
            XnaGUIManager.spriteFont = Main.fontMouseText;
            XnaGUIManager.WhiteRect = CreateThePixel(spriteBatch);
            XnaGUIManager.Initialize(this);

            // create the tool window
            toolWindow = new ToolWindow(this);
            TileSellW = new TileSelectionW(this);
          

            MemoryStream stream = new MemoryStream();
            Assembly asm = Assembly.Load(new AssemblyName("Terraria"));
            WorldGenReflect.WorldGen = asm.GetType("Terraria.WorldGen");
            PlayerReflect.Player = asm.GetType("Terraria.Player");
            MainReflect.Main = asm.GetType("Terraria.Main");



        }

        protected override void LoadContent()
        {

            base.LoadContent();
      



        }


        protected override void UnloadContent()
        {

        }



        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
          //  Main.player[Main.myPlayer].statLife = Main.player[Main.myPlayer].statLifeMax;
  
            if (Main.gameMenu)
            {
                return;
            }
            updateWindows(gameTime);
            
            Mod.polygons.Update();

 
 
        }





        public static void mykeys()
        {

              keyboardState = Keyboard.GetState();

            if (Main.chatMode) { return; }

            if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
            {
                Main.player[Main.myPlayer].controlDown = false;
                Main.player[Main.myPlayer].controlJump = false;
                Main.player[Main.myPlayer].controlLeft = false;
                Main.player[Main.myPlayer].controlRight = false;
            }
            
            if (keyboardState.IsKeyUp(keyPressed))
            {
                keyPressed = Keys.None;
            }

            if (keyboardState.IsKeyDown(keyPressed))
            {
                return;
            }

      if ((keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl)) && keyboardState.IsKeyDown(Keys.C))
            {
                Main.PlaySound(12, -1, -1, 1);

 
 
                keyPressed = Keys.C;
                if (Mod.toolWindow.brush.Visible && Mod.toolWindow.brush.copyPaste.Visible)
                {
                    Mod.polygons.canfill = true;
                    Mod.polygons.fillin(true);
                    Mod.polygons.SetShape();
                    Mod.tilecopies = Mod.Copythis();
                    Mod.polygons.canpaste = true;
                }
            }
            else if ((keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl)) && keyboardState.IsKeyDown(Keys.V))
            {
             
                keyPressed = Keys.V;
                if (Mod.toolWindow.brush.Visible && Mod.toolWindow.brush.copyPaste.Visible)
                {
                    Main.PlaySound(12, -1, -1, 1);
                    if (Mod.toolWindow.brush.copyPaste.Visible)
                    {
                        Mod.setnewpos();
                        if (Mod.stopVectoring && !Mod.polygons.onwindow)
                        {



                            Mod.Pastethis();

                        }
                    }
                }

            }
              else if ((keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl)) && keyboardState.IsKeyDown(Keys.Z))
              {

                  keyPressed = Keys.Z;

                  Pastethis(LastAction);

              }
      if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.F))
              {
                  Main.PlaySound(12, -1, -1, 1);

                  keyPressed = Keys.F;
                  Mod.toolWindow.modify.shapes.Fill.Button_Clicked(null);
             
              }


              else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.S))
            {

                keyPressed = Keys.S;
                Mod.polygons.SetShape();

            }

              else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.C))
            {

                keyPressed = Keys.C;
                Mod.polygons.Clear();

            }

              else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.W))
            {

                keyPressed = Keys.W;
                Mod.toolWindow.modify.shapes.walls.Button_Clicked(null);
            }

      else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.H))
      {

          keyPressed = Keys.H;
          Mod.toolWindow.modify.shapes.hollow.Button_Clicked(null);
      }


      else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.T))
      {

          keyPressed = Keys.T;
          Mod.toolWindow.modify.shapes.tiles.Button_Clicked(null);
      }


      else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.Q))
      {




          Mod.toolWindow.modify.shapes.Rotation.ButtonMinus.Pressingbutton = true;



      }

      else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.E))
      {



          Mod.toolWindow.modify.shapes.Rotation.ButtonPlus.Pressingbutton = true;

      }

      else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.B))
      {

          keyPressed = Keys.B;
          Mod.toolWindow.modify.shapes.Build_Clicked();
      }
      else if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) && keyboardState.IsKeyDown(Keys.Z))
      {

          keyPressed = Keys.Z;
          Mod.toolWindow.modify.shapes.undoVert_Clicked();
      }




        }
 
   








        public static void shapeselection(bool forcefill = false)
        {

  

            mouse = new Vector2((int)Decimal.Truncate((int)(((float)Main.mouseState.X + Main.screenPosition.X) / 16f)), (int)Decimal.Truncate((int)(((float)Main.mouseState.Y + Main.screenPosition.Y) / 16f)));
            #region shapes
      
            if (!polygons.onwindow && !stopVectoring)
            {
                if (polygons.verTices.Count > 0 && polygons.candrawline)
                {
                    polygons.point1 = polygons.verTices[polygons.verTices.Count - 1];

                    if (Main.mouseState.LeftButton == ButtonState.Released)
                    {
                        polygons.point2 = mouse;
                    }
                }
                if (Main.mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (addvertice)
                    {
                        polygons.verTices.Add(mouse);
                        polygons.candrawline = true;
                        polygons.cansetlines = true;
                        polygons.canfill = true;
                        addvertice = false;
                    }
                }
                else
                {
                    addvertice = true;
                }

            }

            if (Main.mouseState.RightButton == ButtonState.Pressed)
            {
                if (mouse != OldmoveMouse)
                {


                    polygons.point1 = Vector2.Zero;
                    polygons.point2 = Vector2.Zero;
                    polygons.candrawline = false;
                    polygons.MouseIntercepsPoint();
                    OldmoveMouse = mouse;
                    polygons.Move(mouse);

                }
            }
            else
            {
                polygons.indexlocked = false;
                polygons.mouseOnListindex = -1;
             
            }
 
            polygons.setlines();
            polygons.fillin(forcefill);
  
            #endregion





        }
        #region copypaste

        public static void Pastethis(List<Tiles> list = null)
        {
            List<Tiles> laststate = new List<Tiles>();
            List<Tiles> count = new List<Tiles>();
            if (list != null) { count = list; }
            else { count = Mod.tilecopies; }
            if (count.Count > 0)
            {
                for (int i = 0; i < count.Count; i++)
                {
                    if (Main.tile[(int)count[i].loc.X, (int)count[i].loc.Y] == null)
                    {
                        Main.tile[(int)count[i].loc.X, (int)count[i].loc.Y] = new Tile();
                    }

                    if (!Main.tile[(int)count[i].loc.X, (int)count[i].loc.Y].active || forcepaste)
                    {
                        laststate.Add(new Tiles((Tile)(Main.tile[count[i].loc.X, count[i].loc.Y].Clone()), count[i].loc));
                        Main.tile[(int)count[i].loc.X, (int)count[i].loc.Y] = (Tile)(count[i].maintile.Clone());
                        if (Main.netMode == 1)
                        {
                            SendPackets.SendTileSquare2(Main.player[Main.myPlayer].whoAmi, (int)count[i].loc.X, (int)count[i].loc.Y, 1);
                        }
                    }
                    if (Main.netMode == 1)
                    {
                        if (Main.tile[(int)count[i].loc.X, (int)count[i].loc.Y].liquid > 0 || forcepaste)
                        {

                            SendPackets.SendData(148, -1, -1, "", count[i].loc.X, (float)count[i].loc.Y, 0f, 0f, 0);
                        }
                    }
                }
            }
            if (laststate.Count > 0)
            {
                Mod.LastAction = laststate;
            }
        }
        public static List<Tiles> Copythis()
        {
            List<Tiles> copies = new List<Tiles>();
            foreach (Point P in polygons.activeLine)
            {
                if (Main.tile[P.X, P.Y] == null)
                {
                    Main.tile[P.X, P.Y] = new Tile();
                }
                copies.Add(new Tiles((Tile)(Main.tile[P.X, P.Y].Clone()), P));
            }
            foreach (Point P in polygons.activeFill)
            {

                if (Main.tile[P.X, P.Y] == null)
                {
                    Main.tile[P.X, P.Y] = new Tile();
                }
                copies.Add(new Tiles((Tile)(Main.tile[P.X, P.Y].Clone()), P));
            }
         
            stopVectoring = true;

            return copies;


        }

    
        public static void setnewpos()
        {
            List<Point> temp = new List<Point>();
            temp.AddRange(polygons.activeLine);
            temp.AddRange(polygons.activeFill);
            if (tilecopies.Count == temp.Count)
            {
                for (int i = 0; i < tilecopies.Count; i++)
                {
                    tilecopies[i].loc = temp[i];
                }
            }
        }



        #endregion
        #region updateWindows update
        private void updateWindows(GameTime gameTime)
        {
            XnaGUIManager.Update(gameTime);
            Mod.mykeys();
            polygons.onwindow = false;
            int MouseX = Main.mouseState.X;
            int MouseY = Main.mouseState.Y;
            if (MouseX > Main.screenWidth - 100)
            {
                MouseX = Main.screenWidth - 100;
            }
            if (MouseX < 100)
            {
                MouseX = 100;
            }
            if (MouseY > Main.screenHeight - 100)
            {
                MouseY = Main.screenHeight - 100;
            }
            if (MouseY < 100)
            {

                MouseY = 100;

            }


            Rectangle MouseScreenRec = new Rectangle((int)Main.mouseState.X, (int)Main.mouseState.Y, Main.itemTexture[0xa9].Width, Main.itemTexture[0xa9].Height);
            Rectangle ToolWindowRec = new Rectangle(ToolWindow.X - 32, ToolWindow.Y, Main.itemTexture[0xa9].Width, Main.itemTexture[0xa9].Height);

            Rectangle TileSelectionw = new Rectangle(TileSelectionW.X + TileSellW.Rectangle.Width + 16, TileSelectionW.Y, Main.itemTexture[0xa9].Width, Main.itemTexture[0xa9].Height);

            if (Main.mouseState.RightButton == ButtonState.Pressed)
            {
                if ((TileSelectionw.Intersects(MouseScreenRec)))
                {
                    moveTileSelectionw = true;
                }
                if ((ToolWindowRec.Intersects(MouseScreenRec)))
                {
                    moveToolwindow = true;
                }
            }
            else
            {
                moveTileSelectionw = false;
                moveToolwindow = false;
            }
            if (Main.mouseState.LeftButton == ButtonState.Pressed)
            {
                if (Mod.MouseLeftRelease)
                {
                    if ((TileSelectionw.Intersects(MouseScreenRec)))
                    {
                        if (TileSellW.Visible)
                        {TileSellW.Enabled = false;
                            TileSellW.Visible = false;
                        }
                        else { TileSellW.Visible = true; TileSellW.Enabled = true; }
                    }
                    if ((ToolWindowRec.Intersects(MouseScreenRec)))
                    {
                        if (toolWindow.Visible)
                        {
                            toolWindow.Enabled = false;
                            toolWindow.Visible = false;
                        }
                        else { toolWindow.Visible = true; toolWindow.Enabled = true; }
                    }
                    Mod.MouseLeftRelease = false;
                }
            }
            else { Mod.MouseLeftRelease = true; }

            if ((toolWindow.Rectangle.Intersects(MouseScreenRec)) && toolWindow.Enabled)
            {
                polygons.onwindow = true;
                Main.player[Main.myPlayer].mouseInterface = true;
            }
            if ((TileSellW.Rectangle.Intersects(MouseScreenRec)) && TileSellW.Enabled)
            {
                polygons.onwindow = true;
                Main.player[Main.myPlayer].mouseInterface = true;
            }

            if ((moveToolwindow) && (moveTileSelectionw))
            {
                off = 16;

            }
            else { off = 0; }




            if (moveToolwindow)
            {
                var rec = toolWindow.Rectangle;
                rec.X = ToolWindow.X = mouseX + 32 + off;
                rec.Y = ToolWindow.Y = mouseY;
                toolWindow.Rectangle = rec;
            }

            if (moveTileSelectionw)
            {
                var rec = TileSellW.Rectangle;
                rec.X = TileSelectionW.X = mouseX - TileSellW.Rectangle.Width - 16 - off;
                rec.Y = TileSelectionW.Y = mouseY;
                TileSellW.Rectangle = rec;
            }
 



            if (!Mod.toolWindow.brush.copyPaste.Visible) { Mod.toolWindow.brush.copyPaste.firsttime = true; }


            if (!Mod.toolWindow.modify.replace.Visible) { Mod.toolWindow.modify.replace.firsttime = true; }


            if (!Mod.toolWindow.modify.delete.Visible) { Mod.toolWindow.modify.delete.firsttime = true; }

            if (!Mod.toolWindow.modify.liquids.Visible) { Mod.toolWindow.modify.liquids.firsttime = true; }

            UpdatePlayer_CH();
            if (!polygons.onwindow)
            {
                olditem = Main.player[Main.myPlayer].selectedItem;


            }
            else { Main.player[Main.myPlayer].selectedItem = olditem; }

        }
     
 
 
        public void UpdatePlayer_CH()   
        {
            if (Mod.toolWindow.playerSettings.player_CH.Light.Value)
            {
                int minX = (int)((Main.screenPosition.X) / 16f);
                int minY = (int)((Main.screenPosition.Y) / 16f);
                int MaxX = (int)((Main.screenPosition.X + (float)Main.screenWidth) / 16f);
                int MaxY = (int)((Main.screenPosition.Y + (float)Main.screenHeight) / 16f);
                for (int Y = minY; Y < MaxY; Y++)
                { 
                    for (int X = minX; X < MaxX; X++)
                    {

                        Lighting.addLight(X, Y, 10f);

                    }
                }
            }
            
           
            if (toolWindow.playerSettings.player_CH.Fly.Value ) { Mod.UpdateNoclip(); }
 
            if (toolWindow.playerSettings.player_CH.infAir.Value)
            {
                Main.player[Main.myPlayer].breath = Main.player[Main.myPlayer].breathMax;
            }
            if (toolWindow.playerSettings.player_CH.inflife.Value)
            {
                Main.player[Main.myPlayer].immune = true;
                Main.player[Main.myPlayer].dead = false;
                Main.player[Main.myPlayer].immuneTime = 100;
                Main.player[Main.myPlayer].pvpDeath = false;
                Main.player[Main.myPlayer].statLife = Main.player[Main.myPlayer].statLifeMax = 400;
            }
            if (toolWindow.playerSettings.player_CH.infmana.Value)
            {
                Main.player[Main.myPlayer].statMana = Main.player[Main.myPlayer].statManaMax = Main.player[Main.myPlayer].statManaMax2 = 200;
            }

    
            if (toolWindow.playerSettings.player_CH.blockRange.Value > 0)
            {

                Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].tileBoost = (int)toolWindow.playerSettings.player_CH.blockRange.Value;

            }


            toolWindow.playerSettings.player_CH.itemGrabRange.DisplayValue = PlayerReflect.itemGrabRange;
            toolWindow.playerSettings.player_CH.itemGrabSpeed.DisplayValue = PlayerReflect.itemGrabSpeed;


            if (PlayerReflect.itemGrabSpeedMax < PlayerReflect.itemGrabSpeed)
            {
                PlayerReflect.itemGrabSpeedMax = PlayerReflect.itemGrabSpeed;

            }
            if (toolWindow.playerSettings.player_CH.rocketTimeMax.Value > 0)
            {
                Main.player[Main.myPlayer].rocketTimeMax =(int) toolWindow.playerSettings.player_CH.rocketTimeMax.Value;
            }
 
        }

        public static string addquotes(string text)
        { 
            string result = text;
            if (text.Split(' ').Length > 1)
            {
                result = quote + text + quote;
            }
            return result;
        }
 
        #endregion
 



        #region draw methods


        protected override void Draw(GameTime gameTime)
        {


            base.Draw(gameTime);


            if (Main.gameMenu)
            {
                return;
            }

           if (m_pixel == null) { m_pixel = CreateThePixel(spriteBatch); }
            if (XnaGUIManager.WhiteRect == null) { XnaGUIManager.WhiteRect = CreateThePixel(spriteBatch); }
            float frameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            XnaGUIManager.Draw(frameTime);
              spriteBatch.Begin();
            this.spriteBatch.Draw(Main.itemTexture[0xa9],
                new Vector2((float)(Mod.TileSellW.Rectangle.X +
                    Mod.TileSellW.Rectangle.Width - off),
                    (float)Mod.TileSellW.Rectangle.Y),
                    new Rectangle(0, 0, Main.itemTexture[0xa9].Width, Main.itemTexture[0xa9].Height),
                    Color.Blue, 0f,
                    new Vector2(),
                    (float)1.5f, SpriteEffects.None, 0f);


            this.spriteBatch.Draw(Main.itemTexture[0xa9],
                new Vector2((float)(Mod.toolWindow.Rectangle.X - 32 + off),
                    (float)Mod.toolWindow.Rectangle.Y),
                    new Rectangle(0, 0, Main.itemTexture[0xa9].Width, Main.itemTexture[0xa9].Height),
                    Color.Red, 0f,
                    new Vector2(), (float)1.5f, SpriteEffects.None, 0f);
                    spriteBatch.End();



            if (Main.playerInventory)
            {
                return;
            }


  spriteBatch.Begin();
  
            if (Mod.toolWindow.brush.copyPaste.Visible)
            {
                Drawwalls();
                drawTiles();
                drawWater(true);
            }
            DrawmeStuffs();

            DrawnpcText();




            if (Main.netMode == 1)
            {
                List<Player> thisplayer = new List<Player>();
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active)
                    {
                        thisplayer.Add((Player)Main.player[i].Clone());
                    }
                }
                for (int i = 0; i < thisplayer.Count; i++)
                {
                    if (i != 0)
                    {
                        thisplayer[i].position.X = thisplayer[i - 1].position.X + (thisplayer[i - 1].width * 2);
                        thisplayer[i].position.Y = thisplayer[i - 1].position.Y;
                        if ((thisplayer[i].position.X- Main.screenPosition.X) >= (Main.screenWidth - 100))
                        {
                            thisplayer[i].position.X = playersX + Main.screenPosition.X;
                            thisplayer[i].position.Y = thisplayer[i - 1].position.Y + (thisplayer[i - 1].height * 2);
                        }
                        if ((thisplayer[i].position.Y - Main.screenPosition.Y) > (Main.screenHeight   - 50))
                        {
                            playersX += thisplayer[i - 1].width * 2;
                        }
                    }
                    else
                    {
                        thisplayer[i].position.X = playersX + Main.screenPosition.X;
                        thisplayer[i].position.Y = playersY + Main.screenPosition.Y;
                    }
                    drawPlayer(thisplayer[i]);
                    Rectangle mouserec = new Rectangle((int)(Main.mouseState.X), (int)(Main.mouseState.Y), Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height);
                    Rectangle playerec = new Rectangle((int)(thisplayer[i].position.X - Main.screenPosition.X), (int)(thisplayer[i].position.Y - Main.screenPosition.Y), thisplayer[i].width, thisplayer[i].height);
                    if (playerec.Intersects(mouserec))
                    {
                        floatingtext(thisplayer[i].name + @"/" + thisplayer[i].statLife, Main.mouseX, Main.mouseY, 1, 0);
                        if (Main.mouseState.LeftButton == ButtonState.Pressed)
                        {
                            SelectedPlayer = (Player)Main.player[thisplayer[i].whoAmi].Clone();


                            if (Main.mouseState.LeftButton == ButtonState.Pressed)
                            { 
                                if (Mod.toolWindow.server.serverCommand2.Visible && Mod.toolWindow.server.Visible)
                                {
                                    if (Mod.toolWindow.server.serverCommand2.teleport.Value)
                                    {



                                        if (Mod.SelectedPlayer.whoAmi != Main.myPlayer)
                                        {
                                            if (!firsttime) { P_Oldposition = Main.player[Main.myPlayer].position; firsttime = true; }
                                            Main.player[Main.myPlayer].position = Main.player[Mod.SelectedPlayer.whoAmi].position;
                                        }
                                        else { if (firsttime) { Main.player[Main.myPlayer].position = P_Oldposition; firsttime = false; } }


                                                                    
                                        SendPackets.SendData(113, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);

                                    }
                                }
                            }
                        }
                    }
                }
                thisplayer.Clear();
            }



            Vector2 origin = new Vector2();
            SpriteBatch arg_FDB0_0 = this.spriteBatch;
            Texture2D arg_FDB0_1 = Main.cursorTexture;
            Vector2 arg_FDB0_2 = new Vector2((float)(Main.mouseX + 1), (float)(Main.mouseY + 1));
            Rectangle? arg_FDB0_3 = new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height));
            Color arg_FDB0_4 = new Color((int)((float)Main.cursorColor.R * 0.2f), (int)((float)Main.cursorColor.G * 0.2f), (int)((float)Main.cursorColor.B * 0.2f), (int)((float)Main.cursorColor.A * 0.5f));
            float arg_FDB0_5 = 0f;
            origin = default(Vector2);
            arg_FDB0_0.Draw(arg_FDB0_1, arg_FDB0_2, arg_FDB0_3, arg_FDB0_4, arg_FDB0_5, origin, Main.cursorScale * 1.1f, SpriteEffects.None, 0f);
            SpriteBatch arg_FE14_0 = this.spriteBatch;
            Texture2D arg_FE14_1 = Main.cursorTexture;
            Vector2 arg_FE14_2 = new Vector2((float)Main.mouseX, (float)Main.mouseY);
            Rectangle? arg_FE14_3 = new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height));
            Color arg_FE14_4 = Main.cursorColor;
            float arg_FE14_5 = 0f;
            origin = default(Vector2);
            arg_FE14_0.Draw(arg_FE14_1, arg_FE14_2, arg_FE14_3, arg_FE14_4, arg_FE14_5, origin, Main.cursorScale, SpriteEffects.None, 0f);
			

            spriteBatch.End();



        }



        protected void floatingtext(string cursorText, int X, int Y, int rare = 0, byte diff = 0)
        {

            if (cursorText == null)
            {
                return;
            }
            int num = X + 10;
            int num2 = Y + 10;
            Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
            float num20;
            Vector2 origin;

            Vector2 vector3 = Main.fontMouseText.MeasureString(cursorText);
            if ((float)num + vector3.X + 4f > (float)Main.screenWidth)
            {
                num = (int)((float)Main.screenWidth - vector3.X - 4f);
            }
            if ((float)num2 + vector3.Y + 4f > (float)Main.screenHeight)
            {
                num2 = (int)((float)Main.screenHeight - vector3.Y - 4f);
            }
            SpriteBatch arg_191E_0 = this.spriteBatch;
            SpriteFont arg_191E_1 = Main.fontMouseText;
            Vector2 arg_191E_3 = new Vector2((float)num, (float)(num2 - 2));
            Color arg_191E_4 = Color.Black;
            float arg_191E_5 = 0f;
            origin = default(Vector2);
            arg_191E_0.DrawString(arg_191E_1, cursorText, arg_191E_3, arg_191E_4, arg_191E_5, origin, 1f, SpriteEffects.None, 0f);
            SpriteBatch arg_1959_0 = this.spriteBatch;
            SpriteFont arg_1959_1 = Main.fontMouseText;
            Vector2 arg_1959_3 = new Vector2((float)num, (float)(num2 + 2));
            Color arg_1959_4 = Color.Black;
            float arg_1959_5 = 0f;
            origin = default(Vector2);
            arg_1959_0.DrawString(arg_1959_1, cursorText, arg_1959_3, arg_1959_4, arg_1959_5, origin, 1f, SpriteEffects.None, 0f);
            SpriteBatch arg_1994_0 = this.spriteBatch;
            SpriteFont arg_1994_1 = Main.fontMouseText;
            Vector2 arg_1994_3 = new Vector2((float)(num - 2), (float)num2);
            Color arg_1994_4 = Color.Black;
            float arg_1994_5 = 0f;
            origin = default(Vector2);
            arg_1994_0.DrawString(arg_1994_1, cursorText, arg_1994_3, arg_1994_4, arg_1994_5, origin, 1f, SpriteEffects.None, 0f);
            SpriteBatch arg_19CF_0 = this.spriteBatch;
            SpriteFont arg_19CF_1 = Main.fontMouseText;
            Vector2 arg_19CF_3 = new Vector2((float)(num + 2), (float)num2);
            Color arg_19CF_4 = Color.Black;
            float arg_19CF_5 = 0f;
            origin = default(Vector2);
            arg_19CF_0.DrawString(arg_19CF_1, cursorText, arg_19CF_3, arg_19CF_4, arg_19CF_5, origin, 1f, SpriteEffects.None, 0f);
            num20 = (float)Main.mouseTextColor / 255f;
            Color color3 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
            if (rare == -1)
            {
                color3 = new Color((int)((byte)(130f * num20)), (int)((byte)(130f * num20)), (int)((byte)(130f * num20)), (int)Main.mouseTextColor);
            }
            if (rare == 6)
            {
                color3 = new Color((int)((byte)(210f * num20)), (int)((byte)(160f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
            }
            if (rare == 1)
            {
                color3 = new Color((int)((byte)(150f * num20)), (int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
            }
            if (rare == 2)
            {
                color3 = new Color((int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
            }
            if (rare == 3)
            {
                color3 = new Color((int)((byte)(255f * num20)), (int)((byte)(200f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
            }
            if (rare == 4)
            {
                color3 = new Color((int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
            }
            if (rare == 5)
            {
                color3 = new Color((int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
            }
            if (diff == 1)
            {
                color3 = new Color((int)((byte)((float)Main.mcColor.R * num20)), (int)((byte)((float)Main.mcColor.G * num20)), (int)((byte)((float)Main.mcColor.B * num20)), (int)Main.mouseTextColor);
            }
            if (diff == 2)
            {
                color3 = new Color((int)((byte)((float)Main.hcColor.R * num20)), (int)((byte)((float)Main.hcColor.G * num20)), (int)((byte)((float)Main.hcColor.B * num20)), (int)Main.mouseTextColor);
            }
            SpriteBatch arg_1BB9_0 = this.spriteBatch;
            SpriteFont arg_1BB9_1 = Main.fontMouseText;
            Vector2 arg_1BB9_3 = new Vector2((float)num, (float)num2);
            Color arg_1BB9_4 = color3;
            float arg_1BB9_5 = 0f;
            origin = default(Vector2);
            arg_1BB9_0.DrawString(arg_1BB9_1, cursorText, arg_1BB9_3, arg_1BB9_4, arg_1BB9_5, origin, 1f, SpriteEffects.None, 0f);
        }



        public static Color GetColor(int x, int y, Color oldColor, int m)
        {

            Color white = Color.White;
            int num3 = (int)((float)oldColor.R * m);
            int num4 = (int)((float)oldColor.G * m);
            int num5 = (int)((float)oldColor.B * m);
            if (num3 > 255)
            {
                num3 = 255;
            }
            if (num4 > 255)
            {
                num4 = 255;
            }
            if (num5 > 255)
            {
                num5 = 255;
            }
            white.R = (byte)num3;
            white.G = (byte)num4;
            white.B = (byte)num5;
            return white;
        }


        private static Color BuffColor(Color newColor, float R, float G, float B, float A)
        {
            newColor.R = (byte)((float)newColor.R * R);
            newColor.G = (byte)((float)newColor.G * G);
            newColor.B = (byte)((float)newColor.B * B);
            newColor.A = (byte)((float)newColor.A * A);
            return newColor;
        }
        protected void drawPlayer(Player drawPlayer)
        {
            SpriteEffects effects = SpriteEffects.None;
            SpriteEffects effects2 = SpriteEffects.FlipHorizontally;
            Color color = drawPlayer.GetImmuneAlpha(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White, 1));
            Color color2 = drawPlayer.GetImmuneAlpha(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.eyeColor, 1));
            Color color3 = drawPlayer.GetImmuneAlpha(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.hairColor, 1));
            Color color4 = drawPlayer.GetImmuneAlpha(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.skinColor, 1));
            Color color5 = drawPlayer.GetImmuneAlpha(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.skinColor, 1));
            Color immuneAlpha = drawPlayer.GetImmuneAlpha(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.skinColor, 1));
            Color color6 = drawPlayer.GetImmuneAlpha2(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.shirtColor, 1));
            Color color7 = drawPlayer.GetImmuneAlpha2(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.underShirtColor, 1));
            Color color8 = drawPlayer.GetImmuneAlpha2(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.pantsColor, 1));
            Color color9 = drawPlayer.GetImmuneAlpha2(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.shoeColor, 1));
            Color color10 = drawPlayer.GetImmuneAlpha2(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16, Color.White, 1));
            Color color11 = drawPlayer.GetImmuneAlpha2(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16, Color.White, 1));
            Color color12 = drawPlayer.GetImmuneAlpha2(GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16, Color.White, 1));



            float num = 1f;
            float num2 = 1f;
            float num3 = 1f;
            float num4 = 1f;
            if (drawPlayer.poisoned)
            {
                if (Main.rand.Next(50) == 0)
                {
                    int num5 = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 46, 0f, 0f, 150, default(Color), 0.2f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].fadeIn = 1.9f;
                }
                num *= 0.65f;
                num3 *= 0.75f;
            }
            if (drawPlayer.onFire)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int num6 = Dust.NewDust(new Vector2(drawPlayer.position.X - 2f, drawPlayer.position.Y - 2f), drawPlayer.width + 4, drawPlayer.height + 4, 6, drawPlayer.velocity.X * 0.4f, drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[num6].noGravity = true;
                    Dust expr_640 = Main.dust[num6];
                    expr_640.velocity *= 1.8f;
                    Dust expr_662_cp_0 = Main.dust[num6];
                    expr_662_cp_0.velocity.Y = expr_662_cp_0.velocity.Y - 0.5f;
                }
                num3 *= 0.6f;
                num2 *= 0.7f;
            }
            if (drawPlayer.onFire2)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int num7 = Dust.NewDust(new Vector2(drawPlayer.position.X - 2f, drawPlayer.position.Y - 2f), drawPlayer.width + 4, drawPlayer.height + 4, 75, drawPlayer.velocity.X * 0.4f, drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[num7].noGravity = true;
                    Dust expr_72B = Main.dust[num7];
                    expr_72B.velocity *= 1.8f;
                    Dust expr_74D_cp_0 = Main.dust[num7];
                    expr_74D_cp_0.velocity.Y = expr_74D_cp_0.velocity.Y - 0.5f;
                }
                num3 *= 0.6f;
                num2 *= 0.7f;
            }
            if (drawPlayer.noItems)
            {
                num2 *= 0.8f;
                num *= 0.65f;
            }
            if (drawPlayer.blind)
            {
                num2 *= 0.65f;
                num *= 0.7f;
            }
            if (drawPlayer.bleed)
            {
                num2 *= 0.9f;
                num3 *= 0.9f;
                if (!drawPlayer.dead && Main.rand.Next(30) == 0)
                {
                    int num8 = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 5, 0f, 0f, 0, default(Color), 1f);
                    Dust expr_820_cp_0 = Main.dust[num8];
                    expr_820_cp_0.velocity.Y = expr_820_cp_0.velocity.Y + 0.5f;
                    Dust expr_839 = Main.dust[num8];
                    expr_839.velocity *= 0.25f;
                }
            }
            if (num != 1f || num2 != 1f || num3 != 1f || num4 != 1f)
            {
                if (drawPlayer.onFire || drawPlayer.onFire2)
                {
                    color = drawPlayer.GetImmuneAlpha(Color.White);
                    color2 = drawPlayer.GetImmuneAlpha(drawPlayer.eyeColor);
                    color3 = drawPlayer.GetImmuneAlpha(drawPlayer.hairColor);
                    color4 = drawPlayer.GetImmuneAlpha(drawPlayer.skinColor);
                    color5 = drawPlayer.GetImmuneAlpha(drawPlayer.skinColor);
                    color6 = drawPlayer.GetImmuneAlpha(drawPlayer.shirtColor);
                    color7 = drawPlayer.GetImmuneAlpha(drawPlayer.underShirtColor);
                    color8 = drawPlayer.GetImmuneAlpha(drawPlayer.pantsColor);
                    color9 = drawPlayer.GetImmuneAlpha(drawPlayer.shoeColor);
                    color10 = drawPlayer.GetImmuneAlpha(Color.White);
                    color11 = drawPlayer.GetImmuneAlpha(Color.White);
                    color12 = drawPlayer.GetImmuneAlpha(Color.White);
                }
                else
                {
                    color = BuffColor(color, num, num2, num3, num4);
                    color2 = BuffColor(color2, num, num2, num3, num4);
                    color3 = BuffColor(color3, num, num2, num3, num4);
                    color4 = BuffColor(color4, num, num2, num3, num4);
                    color5 = BuffColor(color5, num, num2, num3, num4);
                    color6 = BuffColor(color6, num, num2, num3, num4);
                    color7 = BuffColor(color7, num, num2, num3, num4);
                    color8 = BuffColor(color8, num, num2, num3, num4);
                    color9 = BuffColor(color9, num, num2, num3, num4);
                    color10 = BuffColor(color10, num, num2, num3, num4);
                    color11 = BuffColor(color11, num, num2, num3, num4);
                    color12 = BuffColor(color12, num, num2, num3, num4);
                }
            }
            if (drawPlayer.gravDir == 1f)
            {
                if (drawPlayer.direction == 1)
                {
                    effects = SpriteEffects.None;
                    effects2 = SpriteEffects.None;
                }
                else
                {
                    effects = SpriteEffects.FlipHorizontally;
                    effects2 = SpriteEffects.FlipHorizontally;
                }
                if (!drawPlayer.dead)
                {
                    drawPlayer.legPosition.Y = 0f;
                    drawPlayer.headPosition.Y = 0f;
                    drawPlayer.bodyPosition.Y = 0f;
                }
            }
            else
            {
                if (drawPlayer.direction == 1)
                {
                    effects = SpriteEffects.FlipVertically;
                    effects2 = SpriteEffects.FlipVertically;
                }
                else
                {
                    effects = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
                    effects2 = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
                }
                if (!drawPlayer.dead)
                {
                    drawPlayer.legPosition.Y = 6f;
                    drawPlayer.headPosition.Y = 6f;
                    drawPlayer.bodyPosition.Y = 6f;
                }
            }
            Vector2 vector = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.75f);
            Vector2 origin = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.5f);
            Vector2 vector2 = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.4f);
            if (drawPlayer.merman)
            {
                drawPlayer.headRotation = drawPlayer.velocity.Y * (float)drawPlayer.direction * 0.1f;
                if ((double)drawPlayer.headRotation < -0.3)
                {
                    drawPlayer.headRotation = -0.3f;
                }
                if ((double)drawPlayer.headRotation > 0.3)
                {
                    drawPlayer.headRotation = 0.3f;
                }
            }
            else
            {
                if (!drawPlayer.dead)
                {
                    drawPlayer.headRotation = 0f;
                }
            }
            if (drawPlayer.wings > 0)
            {
                this.spriteBatch.Draw(Main.wingsTexture[drawPlayer.wings], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X + (float)(drawPlayer.width / 2) - (float)(9 * drawPlayer.direction))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)(drawPlayer.height / 2) + 2f * drawPlayer.gravDir))), new Rectangle?(new Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4)), color11, drawPlayer.bodyRotation, new Vector2((float)(Main.wingsTexture[drawPlayer.wings].Width / 2), (float)(Main.wingsTexture[drawPlayer.wings].Height / 8)), 1f, effects, 0f);
            }
            if (!drawPlayer.invis)
            {
                this.spriteBatch.Draw(Main.skinBodyTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                this.spriteBatch.Draw(Main.skinLegsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.legFrame), immuneAlpha, drawPlayer.legRotation, origin, 1f, effects, 0f);
            }
            if (drawPlayer.legs > 0 && drawPlayer.legs < 25)
            {
                this.spriteBatch.Draw(Main.armorLegTexture[drawPlayer.legs], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color12, drawPlayer.legRotation, vector, 1f, effects, 0f);
            }
            else
            {
                if (!drawPlayer.invis)
                {
                    if (!drawPlayer.male)
                    {
                        this.spriteBatch.Draw(Main.femalePantsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color8, drawPlayer.legRotation, vector, 1f, effects, 0f);
                        this.spriteBatch.Draw(Main.femaleShoesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color9, drawPlayer.legRotation, vector, 1f, effects, 0f);
                    }
                    else
                    {
                        this.spriteBatch.Draw(Main.playerPantsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color8, drawPlayer.legRotation, vector, 1f, effects, 0f);
                        this.spriteBatch.Draw(Main.playerShoesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color9, drawPlayer.legRotation, vector, 1f, effects, 0f);
                    }
                }
            }
            if (drawPlayer.body > 0 && drawPlayer.body < 26)
            {
                if (!drawPlayer.male)
                {
                    this.spriteBatch.Draw(Main.femaleBodyTexture[drawPlayer.body], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color11, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                }
                else
                {
                    this.spriteBatch.Draw(Main.armorBodyTexture[drawPlayer.body], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color11, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                }
                if ((drawPlayer.body == 10 || drawPlayer.body == 11 || drawPlayer.body == 12 || drawPlayer.body == 13 || drawPlayer.body == 14 || drawPlayer.body == 15 || drawPlayer.body == 16 || drawPlayer.body == 20) && !drawPlayer.invis)
                {
                    this.spriteBatch.Draw(Main.playerHandsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                }
            }
            else
            {
                if (!drawPlayer.invis)
                {
                    if (!drawPlayer.male)
                    {
                        this.spriteBatch.Draw(Main.femaleUnderShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                        this.spriteBatch.Draw(Main.femaleShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color6, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                    }
                    else
                    {
                        this.spriteBatch.Draw(Main.playerUnderShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                        this.spriteBatch.Draw(Main.playerShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color6, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                    }
                    this.spriteBatch.Draw(Main.playerHandsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                }
            }
            if (!drawPlayer.invis && drawPlayer.head != 38)
            {
                this.spriteBatch.Draw(Main.playerHeadTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color4, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                this.spriteBatch.Draw(Main.playerEyeWhitesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                this.spriteBatch.Draw(Main.playerEyesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color2, drawPlayer.headRotation, vector2, 1f, effects, 0f);
            }
            if (drawPlayer.head == 10 || drawPlayer.head == 12 || drawPlayer.head == 28)
            {
                this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                if (!drawPlayer.invis)
                {
                    Rectangle bodyFrame = drawPlayer.bodyFrame;
                    bodyFrame.Y -= 336;
                    if (bodyFrame.Y < 0)
                    {
                        bodyFrame.Y = 0;
                    }
                    this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                }
            }
            if (drawPlayer.head == 14 || drawPlayer.head == 15 || drawPlayer.head == 16 || drawPlayer.head == 18 || drawPlayer.head == 21 || drawPlayer.head == 24 || drawPlayer.head == 25 || drawPlayer.head == 26 || drawPlayer.head == 40 || drawPlayer.head == 44)
            {
                Rectangle bodyFrame2 = drawPlayer.bodyFrame;
                bodyFrame2.Y -= 336;
                if (bodyFrame2.Y < 0)
                {
                    bodyFrame2.Y = 0;
                }
                if (!drawPlayer.invis)
                {
                    this.spriteBatch.Draw(Main.playerHairAltTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame2), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                }
            }
            if (drawPlayer.head == 23)
            {
                Rectangle bodyFrame3 = drawPlayer.bodyFrame;
                bodyFrame3.Y -= 336;
                if (bodyFrame3.Y < 0)
                {
                    bodyFrame3.Y = 0;
                }
                if (!drawPlayer.invis)
                {
                    this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame3), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                }
                this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
            }
            else
            {
                if (drawPlayer.head == 14)
                {
                    Rectangle bodyFrame4 = drawPlayer.bodyFrame;
                    int num9 = 0;
                    if (bodyFrame4.Y == bodyFrame4.Height * 6)
                    {
                        bodyFrame4.Height -= 2;
                    }
                    else
                    {
                        if (bodyFrame4.Y == bodyFrame4.Height * 7)
                        {
                            num9 = -2;
                        }
                        else
                        {
                            if (bodyFrame4.Y == bodyFrame4.Height * 8)
                            {
                                num9 = -2;
                            }
                            else
                            {
                                if (bodyFrame4.Y == bodyFrame4.Height * 9)
                                {
                                    num9 = -2;
                                }
                                else
                                {
                                    if (bodyFrame4.Y == bodyFrame4.Height * 10)
                                    {
                                        num9 = -2;
                                    }
                                    else
                                    {
                                        if (bodyFrame4.Y == bodyFrame4.Height * 13)
                                        {
                                            bodyFrame4.Height -= 2;
                                        }
                                        else
                                        {
                                            if (bodyFrame4.Y == bodyFrame4.Height * 14)
                                            {
                                                num9 = -2;
                                            }
                                            else
                                            {
                                                if (bodyFrame4.Y == bodyFrame4.Height * 15)
                                                {
                                                    num9 = -2;
                                                }
                                                else
                                                {
                                                    if (bodyFrame4.Y == bodyFrame4.Height * 16)
                                                    {
                                                        num9 = -2;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    bodyFrame4.Y += num9;
                    this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f + (float)num9))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame4), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                }
                else
                {
                    if (drawPlayer.head > 0 && drawPlayer.head < 45 && drawPlayer.head != 28)
                    {
                        this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                    }
                    else
                    {
                        if (!drawPlayer.invis)
                        {
                            Rectangle bodyFrame5 = drawPlayer.bodyFrame;
                            bodyFrame5.Y -= 336;
                            if (bodyFrame5.Y < 0)
                            {
                                bodyFrame5.Y = 0;
                            }
                            this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame5), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
                        }
                    }
                }
            }
            if (drawPlayer.heldProj >= 0)
            {
                //  spriteBatch.Begin();
                //    this.DrawProj(drawPlayer.heldProj);
                //   spriteBatch.End();
            }
            Color color13 = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
            if ((drawPlayer.itemAnimation > 0 || drawPlayer.inventory[drawPlayer.selectedItem].holdStyle > 0) && drawPlayer.inventory[drawPlayer.selectedItem].type > 0 && !drawPlayer.dead && !drawPlayer.inventory[drawPlayer.selectedItem].noUseGraphic && (!drawPlayer.wet || !drawPlayer.inventory[drawPlayer.selectedItem].noWet))
            {
                if (drawPlayer.inventory[drawPlayer.selectedItem].useStyle == 5)
                {
                    int num10 = 10;
                    Vector2 vector3 = new Vector2((float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width / 2), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
                    if (drawPlayer.inventory[drawPlayer.selectedItem].type == 95)
                    {
                        num10 = 10;
                        vector3.Y += 2f * drawPlayer.gravDir;
                    }
                    else
                    {
                        if (drawPlayer.inventory[drawPlayer.selectedItem].type == 96)
                        {
                            num10 = -5;
                        }
                        else
                        {
                            if (drawPlayer.inventory[drawPlayer.selectedItem].type == 98)
                            {
                                num10 = -5;
                                vector3.Y -= 2f * drawPlayer.gravDir;
                            }
                            else
                            {
                                if (drawPlayer.inventory[drawPlayer.selectedItem].type == 534)
                                {
                                    num10 = -2;
                                    vector3.Y += 1f * drawPlayer.gravDir;
                                }
                                else
                                {
                                    if (drawPlayer.inventory[drawPlayer.selectedItem].type == 533)
                                    {
                                        num10 = -7;
                                        vector3.Y -= 2f * drawPlayer.gravDir;
                                    }
                                    else
                                    {
                                        if (drawPlayer.inventory[drawPlayer.selectedItem].type == 506)
                                        {
                                            num10 = 0;
                                            vector3.Y -= 2f * drawPlayer.gravDir;
                                        }
                                        else
                                        {
                                            if (drawPlayer.inventory[drawPlayer.selectedItem].type == 494 || drawPlayer.inventory[drawPlayer.selectedItem].type == 508)
                                            {
                                                num10 = -2;
                                            }
                                            else
                                            {
                                                if (drawPlayer.inventory[drawPlayer.selectedItem].type == 434)
                                                {
                                                    num10 = 0;
                                                    vector3.Y -= 2f * drawPlayer.gravDir;
                                                }
                                                else
                                                {
                                                    if (drawPlayer.inventory[drawPlayer.selectedItem].type == 514)
                                                    {
                                                        num10 = 0;
                                                        vector3.Y += 3f * drawPlayer.gravDir;
                                                    }
                                                    else
                                                    {
                                                        if (drawPlayer.inventory[drawPlayer.selectedItem].type == 435 || drawPlayer.inventory[drawPlayer.selectedItem].type == 436 || drawPlayer.inventory[drawPlayer.selectedItem].type == 481 || drawPlayer.inventory[drawPlayer.selectedItem].type == 578)
                                                        {
                                                            num10 = -2;
                                                            vector3.Y -= 2f * drawPlayer.gravDir;
                                                        }
                                                        else
                                                        {
                                                            if (drawPlayer.inventory[drawPlayer.selectedItem].type == 197)
                                                            {
                                                                num10 = -5;
                                                                vector3.Y += 4f * drawPlayer.gravDir;
                                                            }
                                                            else
                                                            {
                                                                if (drawPlayer.inventory[drawPlayer.selectedItem].type == 126)
                                                                {
                                                                    num10 = 4;
                                                                    vector3.Y += 4f * drawPlayer.gravDir;
                                                                }
                                                                else
                                                                {
                                                                    if (drawPlayer.inventory[drawPlayer.selectedItem].type == 127)
                                                                    {
                                                                        num10 = 4;
                                                                        vector3.Y += 2f * drawPlayer.gravDir;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (drawPlayer.inventory[drawPlayer.selectedItem].type == 157)
                                                                        {
                                                                            num10 = 6;
                                                                            vector3.Y += 2f * drawPlayer.gravDir;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (drawPlayer.inventory[drawPlayer.selectedItem].type == 160)
                                                                            {
                                                                                num10 = -8;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (drawPlayer.inventory[drawPlayer.selectedItem].type == 164 || drawPlayer.inventory[drawPlayer.selectedItem].type == 219)
                                                                                {
                                                                                    num10 = 2;
                                                                                    vector3.Y += 4f * drawPlayer.gravDir;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (drawPlayer.inventory[drawPlayer.selectedItem].type == 165 || drawPlayer.inventory[drawPlayer.selectedItem].type == 272)
                                                                                    {
                                                                                        num10 = 4;
                                                                                        vector3.Y += 4f * drawPlayer.gravDir;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (drawPlayer.inventory[drawPlayer.selectedItem].type == 266)
                                                                                        {
                                                                                            num10 = 0;
                                                                                            vector3.Y += 2f * drawPlayer.gravDir;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (drawPlayer.inventory[drawPlayer.selectedItem].type == 281)
                                                                                            {
                                                                                                num10 = 6;
                                                                                                vector3.Y -= 6f * drawPlayer.gravDir;
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
                    Vector2 origin2 = new Vector2((float)(-(float)num10), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
                    if (drawPlayer.direction == -1)
                    {
                        origin2 = new Vector2((float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width + num10), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
                    }
                    this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X + vector3.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y + vector3.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, origin2, drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
                    if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
                    {
                        this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X + vector3.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y + vector3.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, origin2, drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
                    }
                }
                else
                {
                    if (drawPlayer.gravDir == -1f)
                    {
                        this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, 0f), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
                        if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
                        {
                            this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, 0f), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
                        }
                    }
                    else
                    {
                        if (drawPlayer.inventory[drawPlayer.selectedItem].type == 425 || drawPlayer.inventory[drawPlayer.selectedItem].type == 507)
                        {
                            if (drawPlayer.gravDir == 1f)
                            {
                                if (drawPlayer.direction == 1)
                                {
                                    effects2 = SpriteEffects.FlipVertically;
                                }
                                else
                                {
                                    effects2 = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
                                }
                            }
                            else
                            {
                                if (drawPlayer.direction == 1)
                                {
                                    effects2 = SpriteEffects.None;
                                }
                                else
                                {
                                    effects2 = SpriteEffects.FlipHorizontally;
                                }
                            }
                        }
                        this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
                        if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
                        {
                            this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
                        }
                    }
                }
            }
            if (drawPlayer.body > 0 && drawPlayer.body < 26)
            {
                this.spriteBatch.Draw(Main.armorArmTexture[drawPlayer.body], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color11, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                if ((drawPlayer.body == 10 || drawPlayer.body == 11 || drawPlayer.body == 12 || drawPlayer.body == 13 || drawPlayer.body == 14 || drawPlayer.body == 15 || drawPlayer.body == 16 || drawPlayer.body == 20) && !drawPlayer.invis)
                {
                    this.spriteBatch.Draw(Main.playerHands2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                    return;
                }
            }
            else
            {
                if (!drawPlayer.invis)
                {
                    if (!drawPlayer.male)
                    {
                        this.spriteBatch.Draw(Main.femaleUnderShirt2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                        this.spriteBatch.Draw(Main.femaleShirt2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color6, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                    }
                    else
                    {
                        this.spriteBatch.Draw(Main.playerUnderShirt2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                    }
                    this.spriteBatch.Draw(Main.playerHands2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
                }
            }
        }
        private void DrawmeStuffs()
        {
            float minX = (Main.screenPosition.X) / 16f;
            float minY = (Main.screenPosition.Y) / 16f;
            float MaxX = (Main.screenPosition.X + (float)Main.screenWidth) / 16f;
            float MaxY = (Main.screenPosition.Y + (float)Main.screenHeight) / 16f;

            int myheight = 0x10;
            int mywidth = 0x10;
            int num9 = 0;
            float num32 = 0.4f;
            float num33 = 255 * num32;
            float num34 = 255 * num32;
            float num35 = 255 * num32;
            float num36 = 0 * 0.2f;
            Color color3 = new Color((int)((byte)num33), (int)((byte)num34), (int)((byte)num35), (int)((byte)num36));
            Vector2 vector2 = new Vector2();

            if (!Mod.polygons.canpaste)
            {
                for (int g = 0; g < polygons.activeFill.Count; g++)
                {
                    if ((polygons.activeFill[g].X >= minX)
             && (polygons.activeFill[g].Y >= minY)
             && (polygons.activeFill[g].X <= MaxX)
             && (polygons.activeFill[g].Y <= MaxY))
                    {




                        spriteBatch.Draw(m_pixel,
                         new Vector2((float)(polygons.activeFill[g].X * 16 - (int)Main.screenPosition.X), (float)(polygons.activeFill[g].Y * 16 - (int)Main.screenPosition.Y)),
                           null,
                                                 Color.DarkGreen,
                                                 0,
                                                 Vector2.Zero,
                                                 new Vector2(10, 10),
                                                           SpriteEffects.None,
                                                 0);

                    }
                }
            }


            for (int g = 0; g < polygons.activeLine.Count; g++)
            {
                if ((polygons.activeLine[g].X >= minX)
                 && (polygons.activeLine[g].Y >= minY)
                 && (polygons.activeLine[g].X <= MaxX)
                 && (polygons.activeLine[g].Y <= MaxY))
                {

 
              
                    this.spriteBatch.Draw(Main.gridTexture, new Vector2(((polygons.activeLine[g].X * 0x10) - ((int)Main.screenPosition.X)) - ((mywidth - 16f) / 2f), (float)(((polygons.activeLine[g].Y * 0x10) - ((int)Main.screenPosition.Y)) + num9)), new Rectangle(0, 0, mywidth, myheight), color3, 0f, vector2, (float)1f, SpriteEffects.None, 0f);


                


                    this.spriteBatch.DrawString(Main.fontMouseText, g.ToString(), new Vector2(((polygons.activeLine[g].X * 0x10) - ((int)Main.screenPosition.X)) - ((mywidth - 16f) / 2f), (float)(((polygons.activeLine[g].Y * 0x10) - ((int)Main.screenPosition.Y)) + num9)), Color.Aquamarine, 0f, new Vector2(), (float)0.6f, SpriteEffects.None, 0f);






                }
            }

            for (int g = 0; g < polygons.verTices.Count; g++)
            {

                if ((polygons.verTices[g].X >= minX)
                  && (polygons.verTices[g].Y >= minY)
                  && (polygons.verTices[g].X <= MaxX)
                  && (polygons.verTices[g].Y <= MaxY))
                {
                    spriteBatch.Draw(m_pixel,
                  new Vector2((float)(polygons.verTices[g].X * 16 - (int)Main.screenPosition.X), (float)(polygons.verTices[g].Y * 16 - (int)Main.screenPosition.Y)),

                                                           null,
                                                           Color.Red,
                                                           0,
                                                           Vector2.Zero,
                                                           new Vector2(16, 16),
                                                                     SpriteEffects.None,
                                                           0);

                }
            }

            if (polygons.point1 != Vector2.Zero && polygons.point2 != Vector2.Zero)
            {
                float distance = Vector2.Distance(polygons.point1, polygons.point2);
                Vector2 direction = ((polygons.point2 - polygons.point1) / distance);
                direction.Normalize();
                for (int z = 0; z < distance; z++)
                {
                    Vector2 point = (polygons.point1 + direction * (distance * (z / distance)));
                    var unactiveLines = (new Point((int)(Math.Round(point.X)), (int)(Math.Round(point.Y))));


                    if ((unactiveLines.X >= minX)
                    && (unactiveLines.Y >= minY)
                    && (unactiveLines.X <= MaxX)
                    && (unactiveLines.Y <= MaxY))
                    {


                        this.spriteBatch.Draw(Main.gridTexture, new Vector2(((unactiveLines.X * 0x10) - ((int)Main.screenPosition.X)) - ((mywidth - 16f) / 2f), (float)(((unactiveLines.Y * 0x10) - ((int)Main.screenPosition.Y)))), new Rectangle(0, 0, mywidth, myheight), color3, 0f, vector2, (float)1f, SpriteEffects.None, 0f);
                        this.spriteBatch.DrawString(Main.fontMouseText, z.ToString(), new Vector2(((unactiveLines.X * 0x10) - ((int)Main.screenPosition.X)) - ((mywidth - 16f) / 2f), (float)(((unactiveLines.Y * 0x10) - ((int)Main.screenPosition.Y)))), Color.Aquamarine, 0f, new Vector2(), (float)0.6f, SpriteEffects.None, 0f);
                    }
                }

            }

            if (Mod.toolWindow.brush.Visible && Mod.toolWindow.brush.freeStyle.Visible)
            {

                int x = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
                int y = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                for (int i = (int)-Mod.toolWindow.brush.freeStyle.Selsize.Value; i <= Mod.toolWindow.brush.freeStyle.Selsize.Value; i++)
                {
                    for (int j = (int)Mod.toolWindow.brush.freeStyle.Selsize.Value; j >= -Mod.toolWindow.brush.freeStyle.Selsize.Value; j--)
                    {
                        int X = x + i;
                        int Y = y + j;
                        if ((X >= minX)
                && (Y >= minY)
                && (X <= MaxX)
                && (Y <= MaxY))
                        {

                            spriteBatch.Draw(m_pixel,
                             new Vector2((float)(X * 16 - (int)Main.screenPosition.X), (float)(Y * 16 - (int)Main.screenPosition.Y)),
                               null,
                                                     Color.DarkGreen,
                                                     0,
                                                     Vector2.Zero,
                                                     new Vector2(10, 10),
                                                               SpriteEffects.None,
                                                     0);
                        }
                    }
                }
            }
        }
        public void Drawwalls()
        {
            float minX = (Main.screenPosition.X) / 16f;
            float minY = (Main.screenPosition.Y) / 16f;
            float MaxX = (Main.screenPosition.X + (float)Main.screenWidth) / 16f;
            float MaxY = (Main.screenPosition.Y + (float)Main.screenHeight) / 16f;





            for (int g = 0; g < Mod.tilecopies.Count; g++)
            {

                if ((tilecopies[g].loc.X >= minX)
                  && (tilecopies[g].loc.Y >= minY)
                  && (tilecopies[g].loc.X <= MaxX)
                  && (tilecopies[g].loc.Y <= MaxY))
                {


                    if (tilecopies[g].maintile.wall > 0)
                    {

                        Rectangle value2 = new Rectangle((int)(tilecopies[g].maintile.wallFrameX * 2), (int)(tilecopies[g].maintile.wallFrameY * 2), 32, 32);
                        SpriteBatch arg_2826_0 = this.spriteBatch;
                        Texture2D arg_2826_1 = Main.wallTexture[(int)tilecopies[g].maintile.wall];
                        Vector2 arg_2826_2 = new Vector2((float)(tilecopies[g].loc.X * 16 - (int)Main.screenPosition.X - 8), (float)(tilecopies[g].loc.Y * 16 - (int)Main.screenPosition.Y - 8));
                        Rectangle? arg_2826_3 = new Rectangle?(value2);
                        Color arg_2826_4 = Color.White;
                        float arg_2826_5 = 0f;
                        Vector2 origin = default(Vector2);
                        arg_2826_0.Draw(arg_2826_1, arg_2826_2, arg_2826_3, arg_2826_4, arg_2826_5, origin, 1f, SpriteEffects.None, 0f);
                    }
                }


            }
        }
        protected void drawWater(bool bg = false)
        {
            float minX = (Main.screenPosition.X) / 16f;
            float minY = (Main.screenPosition.Y) / 16f;
            float MaxX = (Main.screenPosition.X + (float)Main.screenWidth) / 16f;
            float MaxY = (Main.screenPosition.Y + (float)Main.screenHeight) / 16f;





            for (int g = 0; g < Mod.tilecopies.Count; g++)
            {

                if ((tilecopies[g].loc.X >= minX)
                  && (tilecopies[g].loc.Y >= minY)
                  && (tilecopies[g].loc.X <= MaxX)
                  && (tilecopies[g].loc.Y <= MaxY))
                {

                    if (tilecopies[g].maintile.liquid > 0)
                    {
                        Color color = Color.White;
                        float num5 = (float)(256 - (int)tilecopies[g].maintile.liquid);
                        num5 /= 32f;
                        int num6 = 0;
                        if (tilecopies[g].maintile.lava)
                        {
                            num6 = 1;
                        }
                        float num7 = 0.5f;
                        if (bg)
                        {
                            num7 = 1f;
                        }
                        Vector2 value = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 + (int)num5 * 2));
                        Rectangle value2 = new Rectangle(0, 0, 16, 16 - (int)num5 * 2);
                        if (tilecopies[g].maintile.liquid < 245 && (!tilecopies[g].maintile.active || !Main.tileSolid[(int)tilecopies[g].maintile.type] || Main.tileSolidTop[(int)tilecopies[g].maintile.type]))
                        {
                            float num8 = (float)(256 - (int)tilecopies[g].maintile.liquid);
                            num8 /= 32f;
                            num7 = 0.5f * (8f - num5) / 4f;
                            if ((double)num7 > 0.55)
                            {
                                num7 = 0.55f;
                            }
                            if ((double)num7 < 0.35)
                            {
                                num7 = 0.35f;
                            }
                            float num9 = num5 / 2f;
                            if (tilecopies[g].maintile.liquid < 200)
                            {
                                if (bg)
                                {
                                    goto IL_774;
                                }
                                if (tilecopies[g].maintile.liquid > 0 && tilecopies[g].maintile.liquid > 0)
                                {
                                    value2 = new Rectangle(0, 4, 16, 16);
                                    num7 = 0.5f;
                                }
                                else
                                {
                                    if (tilecopies[g].maintile.liquid > 0)
                                    {
                                        value = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 + 4));
                                        value2 = new Rectangle(0, 4, 16, 12);
                                        num7 = 0.5f;
                                    }
                                    else
                                    {
                                        if (tilecopies[g].maintile.liquid > 0)
                                        {
                                            value = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 + (int)num5 * 2 + (int)num8 * 2));
                                            value2 = new Rectangle(0, 4, 16, 16 - (int)num5 * 2);
                                        }
                                        else
                                        {
                                            value = new Vector2((float)(tilecopies[g].loc.X * 16 + (int)num9), (float)(tilecopies[g].loc.Y * 16 + (int)num9 * 2 + (int)num8 * 2));
                                            value2 = new Rectangle(0, 4, 16 - (int)num9 * 2, 16 - (int)num9 * 2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                num7 = 0.5f;
                                value2 = new Rectangle(0, 4, 16, 16 - (int)num5 * 2 + (int)num8 * 2);
                            }
                        }
                        else
                        {
                            if (tilecopies[g].maintile.liquid > 32)
                            {
                                value2 = new Rectangle(0, 4, value2.Width, value2.Height);
                            }
                            else
                            {
                                if (num5 < 1f && tilecopies[g].maintile.active && Main.tileSolid[(int)tilecopies[g].maintile.type] && !Main.tileSolidTop[(int)tilecopies[g].maintile.type])
                                {
                                    value = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                    value2 = new Rectangle(0, 4, 16, 16);
                                }
                                else
                                {
                                    bool flag = true;
                                    int num10 = tilecopies[g].loc.Y + 1;
                                    while (num10 < tilecopies[g].loc.Y + 6 && (!tilecopies[g].maintile.active || !Main.tileSolid[(int)tilecopies[g].maintile.type] || Main.tileSolidTop[(int)tilecopies[g].maintile.type]))
                                    {
                                        if (tilecopies[g].maintile.liquid < 200)
                                        {
                                            flag = false;
                                            break;
                                        }
                                        num10++;
                                    }
                                    if (!flag)
                                    {
                                        num7 = 0.5f;
                                        value2 = new Rectangle(0, 4, 16, 16);
                                    }
                                    else
                                    {
                                        if (tilecopies[g].maintile.liquid > 0)
                                        {
                                            value2 = new Rectangle(0, 2, value2.Width, value2.Height);
                                        }
                                    }
                                }
                            }
                        }
                        if (tilecopies[g].maintile.lava)
                        {
                            num7 *= 1.8f;
                            if (num7 > 1f)
                            {
                                num7 = 1f;
                            }
                            if (base.IsActive && !Main.gamePaused && Dust.lavaBubbles < 200)
                            {
                                if (tilecopies[g].maintile.liquid > 200 && Main.rand.Next(700) == 0)
                                {
                                    Dust.NewDust(new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16)), 16, 16, 35, 0f, 0f, 0, default(Color), 1f);
                                }
                                if (value2.Y == 0 && Main.rand.Next(350) == 0)
                                {
                                    int num11 = Dust.NewDust(new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16) + num5 * 2f - 8f), 16, 8, 35, 0f, 0f, 50, default(Color), 1.5f);
                                    Dust expr_64A = Main.dust[num11];
                                    expr_64A.velocity *= 0.8f;
                                    Dust expr_66C_cp_0 = Main.dust[num11];
                                    expr_66C_cp_0.velocity.X = expr_66C_cp_0.velocity.X * 2f;
                                    Dust expr_68A_cp_0 = Main.dust[num11];
                                    expr_68A_cp_0.velocity.Y = expr_68A_cp_0.velocity.Y - (float)Main.rand.Next(1, 7) * 0.1f;
                                    if (Main.rand.Next(10) == 0)
                                    {
                                        Dust expr_6C4_cp_0 = Main.dust[num11];
                                        expr_6C4_cp_0.velocity.Y = expr_6C4_cp_0.velocity.Y * (float)Main.rand.Next(2, 5);
                                    }
                                    Main.dust[num11].noGravity = true;
                                }
                            }
                        }
                        float num12 = (float)color.R * num7;
                        float num13 = (float)color.G * num7;
                        float num14 = (float)color.B * num7;
                        float num15 = (float)color.A * num7;
                        color = new Color((int)((byte)num12), (int)((byte)num13), (int)((byte)num14), (int)((byte)num15));
                        this.spriteBatch.Draw(Main.liquidTexture[num6], value - Main.screenPosition, new Rectangle?(value2), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    }
                IL_774: ;

                }
            }
        }
        protected void drawTiles()
        {
            float minX = (Main.screenPosition.X) / 16f;
            float minY = (Main.screenPosition.Y) / 16f;
            float MaxX = (Main.screenPosition.X + (float)Main.screenWidth) / 16f;
            float MaxY = (Main.screenPosition.Y + (float)Main.screenHeight) / 16f;



            int height = 16;
            int num5 = 16;


            for (int g = 0; g < Mod.tilecopies.Count; g++)
            {

                if ((tilecopies[g].loc.X >= minX)
                  && (tilecopies[g].loc.Y >= minY)
                  && (tilecopies[g].loc.X <= MaxX)
                  && (tilecopies[g].loc.Y <= MaxY))
                {




                    if (tilecopies[g].maintile.active)
                    {
                        Color color = Lighting.GetColor(tilecopies[g].loc.X, tilecopies[g].loc.Y);
                        int num6 = 0;
                        if (tilecopies[g].maintile.type == 78 || tilecopies[g].maintile.type == 85)
                        {
                            num6 = 2;
                        }
                        if (tilecopies[g].maintile.type == 33 || tilecopies[g].maintile.type == 49)
                        {
                            num6 = -4;
                        }
                        if (tilecopies[g].maintile.type == 3 || tilecopies[g].maintile.type == 4 || tilecopies[g].maintile.type == 5 || tilecopies[g].maintile.type == 24 || tilecopies[g].maintile.type == 33 || tilecopies[g].maintile.type == 49 || tilecopies[g].maintile.type == 61 || tilecopies[g].maintile.type == 71)
                        {
                            height = 20;
                        }
                        else
                        {
                            if (tilecopies[g].maintile.type == 15 || tilecopies[g].maintile.type == 14 || tilecopies[g].maintile.type == 16 || tilecopies[g].maintile.type == 17 || tilecopies[g].maintile.type == 18 || tilecopies[g].maintile.type == 20 || tilecopies[g].maintile.type == 21 || tilecopies[g].maintile.type == 26 || tilecopies[g].maintile.type == 27 || tilecopies[g].maintile.type == 32 || tilecopies[g].maintile.type == 69 || tilecopies[g].maintile.type == 72 || tilecopies[g].maintile.type == 77 || tilecopies[g].maintile.type == 80)
                            {
                                height = 18;
                            }
                            else
                            {
                                height = 16;
                            }
                        }
                        if (tilecopies[g].maintile.type == 4 || tilecopies[g].maintile.type == 5)
                        {
                            num5 = 20;
                        }
                        else
                        {
                            num5 = 16;
                        }
                        if (tilecopies[g].maintile.type == 73 || tilecopies[g].maintile.type == 74)
                        {
                            num6 -= 12;
                            height = 32;
                        }
                        if (tilecopies[g].maintile.type == 81)
                        {
                            num6 -= 8;
                            height = 26;
                            num5 = 24;
                        }
                        if (Main.player[Main.myPlayer].findTreasure && (tilecopies[g].maintile.type == 6 || tilecopies[g].maintile.type == 7 || tilecopies[g].maintile.type == 8 || tilecopies[g].maintile.type == 9 || tilecopies[g].maintile.type == 12 || tilecopies[g].maintile.type == 21 || tilecopies[g].maintile.type == 22 || tilecopies[g].maintile.type == 28 || (tilecopies[g].maintile.type >= 63 && tilecopies[g].maintile.type <= 68) || Main.tileAlch[(int)tilecopies[g].maintile.type]))
                        {
                            if (color.R < Main.mouseTextColor / 2)
                            {
                                color.R = (byte)(Main.mouseTextColor / 2);
                            }
                            if (color.G < 70)
                            {
                                color.G = 70;
                            }
                            if (color.B < 210)
                            {
                                color.B = 210;
                            }
                            color.A = Main.mouseTextColor;
                            if (!Main.gamePaused && base.IsActive && Main.rand.Next(150) == 0)
                            {
                                Vector2 arg_5E2_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                int arg_5E2_1 = 16;
                                int arg_5E2_2 = 16;
                                int arg_5E2_3 = 15;
                                float arg_5E2_4 = 0f;
                                float arg_5E2_5 = 0f;
                                int arg_5E2_6 = 150;
                                Color newColor = default(Color);
                                int num7 = Dust.NewDust(arg_5E2_0, arg_5E2_1, arg_5E2_2, arg_5E2_3, arg_5E2_4, arg_5E2_5, arg_5E2_6, newColor, 0.8f);
                                Dust expr_5F1 = Main.dust[num7];
                                expr_5F1.velocity *= 0.1f;
                                Main.dust[num7].noLight = true;
                            }
                        }
                        if (!Main.gamePaused && base.IsActive)
                        {
                            if (tilecopies[g].maintile.type == 4 && Main.rand.Next(40) == 0)
                            {
                                if (tilecopies[g].maintile.frameX == 22)
                                {
                                    Dust.NewDust(new Vector2((float)(tilecopies[g].loc.X * 16 + 6), (float)(tilecopies[g].loc.Y * 16)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
                                }
                                if (tilecopies[g].maintile.frameX == 44)
                                {
                                    Dust.NewDust(new Vector2((float)(tilecopies[g].loc.X * 16 + 2), (float)(tilecopies[g].loc.Y * 16)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
                                }
                                else
                                {
                                    Dust.NewDust(new Vector2((float)(tilecopies[g].loc.X * 16 + 4), (float)(tilecopies[g].loc.Y * 16)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
                                }
                            }
                            if (tilecopies[g].maintile.type == 33 && Main.rand.Next(40) == 0)
                            {
                                Dust.NewDust(new Vector2((float)(tilecopies[g].loc.X * 16 + 4), (float)(tilecopies[g].loc.Y * 16 - 4)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
                            }
                            if (tilecopies[g].maintile.type == 93 && Main.rand.Next(40) == 0 && tilecopies[g].maintile.frameY == 0)
                            {
                                Dust.NewDust(new Vector2((float)(tilecopies[g].loc.X * 16 + 4), (float)(tilecopies[g].loc.Y * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
                            }
                            if (tilecopies[g].maintile.type == 100 && Main.rand.Next(40) == 0 && tilecopies[g].maintile.frameY == 0)
                            {
                                if (tilecopies[g].maintile.frameX == 0)
                                {
                                    if (Main.rand.Next(3) == 0)
                                    {
                                        Vector2 arg_894_0 = new Vector2((float)(tilecopies[g].loc.X * 16 + 4), (float)(tilecopies[g].loc.Y * 16 + 2));
                                        int arg_894_1 = 4;
                                        int arg_894_2 = 4;
                                        int arg_894_3 = 6;
                                        float arg_894_4 = 0f;
                                        float arg_894_5 = 0f;
                                        int arg_894_6 = 100;
                                        Color newColor = default(Color);
                                        Dust.NewDust(arg_894_0, arg_894_1, arg_894_2, arg_894_3, arg_894_4, arg_894_5, arg_894_6, newColor, 1f);
                                    }
                                    else
                                    {
                                        Vector2 arg_8D3_0 = new Vector2((float)(tilecopies[g].loc.X * 16 + 14), (float)(tilecopies[g].loc.Y * 16 + 2));
                                        int arg_8D3_1 = 4;
                                        int arg_8D3_2 = 4;
                                        int arg_8D3_3 = 6;
                                        float arg_8D3_4 = 0f;
                                        float arg_8D3_5 = 0f;
                                        int arg_8D3_6 = 100;
                                        Color newColor = default(Color);
                                        Dust.NewDust(arg_8D3_0, arg_8D3_1, arg_8D3_2, arg_8D3_3, arg_8D3_4, arg_8D3_5, arg_8D3_6, newColor, 1f);
                                    }
                                }
                                else
                                {
                                    if (Main.rand.Next(3) == 0)
                                    {
                                        Vector2 arg_91B_0 = new Vector2((float)(tilecopies[g].loc.X * 16 + 6), (float)(tilecopies[g].loc.Y * 16 + 2));
                                        int arg_91B_1 = 4;
                                        int arg_91B_2 = 4;
                                        int arg_91B_3 = 6;
                                        float arg_91B_4 = 0f;
                                        float arg_91B_5 = 0f;
                                        int arg_91B_6 = 100;
                                        Color newColor = default(Color);
                                        Dust.NewDust(arg_91B_0, arg_91B_1, arg_91B_2, arg_91B_3, arg_91B_4, arg_91B_5, arg_91B_6, newColor, 1f);
                                    }
                                    else
                                    {
                                        Vector2 arg_954_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 + 2));
                                        int arg_954_1 = 4;
                                        int arg_954_2 = 4;
                                        int arg_954_3 = 6;
                                        float arg_954_4 = 0f;
                                        float arg_954_5 = 0f;
                                        int arg_954_6 = 100;
                                        Color newColor = default(Color);
                                        Dust.NewDust(arg_954_0, arg_954_1, arg_954_2, arg_954_3, arg_954_4, arg_954_5, arg_954_6, newColor, 1f);
                                    }
                                }
                            }
                            if (tilecopies[g].maintile.type == 98 && Main.rand.Next(40) == 0 && tilecopies[g].maintile.frameY == 0 && tilecopies[g].maintile.frameX == 0)
                            {
                                Vector2 arg_9DD_0 = new Vector2((float)(tilecopies[g].loc.X * 16 + 12), (float)(tilecopies[g].loc.Y * 16 + 2));
                                int arg_9DD_1 = 4;
                                int arg_9DD_2 = 4;
                                int arg_9DD_3 = 6;
                                float arg_9DD_4 = 0f;
                                float arg_9DD_5 = 0f;
                                int arg_9DD_6 = 100;
                                Color newColor = default(Color);
                                Dust.NewDust(arg_9DD_0, arg_9DD_1, arg_9DD_2, arg_9DD_3, arg_9DD_4, arg_9DD_5, arg_9DD_6, newColor, 1f);
                            }
                            if (tilecopies[g].maintile.type == 49 && Main.rand.Next(20) == 0)
                            {
                                Vector2 arg_A3C_0 = new Vector2((float)(tilecopies[g].loc.X * 16 + 4), (float)(tilecopies[g].loc.Y * 16 - 4));
                                int arg_A3C_1 = 4;
                                int arg_A3C_2 = 4;
                                int arg_A3C_3 = 29;
                                float arg_A3C_4 = 0f;
                                float arg_A3C_5 = 0f;
                                int arg_A3C_6 = 100;
                                Color newColor = default(Color);
                                Dust.NewDust(arg_A3C_0, arg_A3C_1, arg_A3C_2, arg_A3C_3, arg_A3C_4, arg_A3C_5, arg_A3C_6, newColor, 1f);
                            }
                            if ((tilecopies[g].maintile.type == 34 || tilecopies[g].maintile.type == 35 || tilecopies[g].maintile.type == 36) && Main.rand.Next(40) == 0 && tilecopies[g].maintile.frameY == 18 && (tilecopies[g].maintile.frameX == 0 || tilecopies[g].maintile.frameX == 36))
                            {
                                Vector2 arg_B0D_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 + 2));
                                int arg_B0D_1 = 14;
                                int arg_B0D_2 = 6;
                                int arg_B0D_3 = 6;
                                float arg_B0D_4 = 0f;
                                float arg_B0D_5 = 0f;
                                int arg_B0D_6 = 100;
                                Color newColor = default(Color);
                                Dust.NewDust(arg_B0D_0, arg_B0D_1, arg_B0D_2, arg_B0D_3, arg_B0D_4, arg_B0D_5, arg_B0D_6, newColor, 1f);
                            }
                            if (tilecopies[g].maintile.type == 22 && Main.rand.Next(400) == 0)
                            {
                                Vector2 arg_B6C_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                int arg_B6C_1 = 16;
                                int arg_B6C_2 = 16;
                                int arg_B6C_3 = 14;
                                float arg_B6C_4 = 0f;
                                float arg_B6C_5 = 0f;
                                int arg_B6C_6 = 0;
                                Color newColor = default(Color);
                                Dust.NewDust(arg_B6C_0, arg_B6C_1, arg_B6C_2, arg_B6C_3, arg_B6C_4, arg_B6C_5, arg_B6C_6, newColor, 1f);
                            }
                            else
                            {
                                if ((tilecopies[g].maintile.type == 23 || tilecopies[g].maintile.type == 24 || tilecopies[g].maintile.type == 32) && Main.rand.Next(500) == 0)
                                {
                                    Vector2 arg_BFE_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                    int arg_BFE_1 = 16;
                                    int arg_BFE_2 = 16;
                                    int arg_BFE_3 = 14;
                                    float arg_BFE_4 = 0f;
                                    float arg_BFE_5 = 0f;
                                    int arg_BFE_6 = 0;
                                    Color newColor = default(Color);
                                    Dust.NewDust(arg_BFE_0, arg_BFE_1, arg_BFE_2, arg_BFE_3, arg_BFE_4, arg_BFE_5, arg_BFE_6, newColor, 1f);
                                }
                                else
                                {
                                    if (tilecopies[g].maintile.type == 25 && Main.rand.Next(700) == 0)
                                    {
                                        Vector2 arg_C62_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                        int arg_C62_1 = 16;
                                        int arg_C62_2 = 16;
                                        int arg_C62_3 = 14;
                                        float arg_C62_4 = 0f;
                                        float arg_C62_5 = 0f;
                                        int arg_C62_6 = 0;
                                        Color newColor = default(Color);
                                        Dust.NewDust(arg_C62_0, arg_C62_1, arg_C62_2, arg_C62_3, arg_C62_4, arg_C62_5, arg_C62_6, newColor, 1f);
                                    }
                                    else
                                    {
                                        if (tilecopies[g].maintile.type == 31 && Main.rand.Next(20) == 0)
                                        {
                                            Vector2 arg_CC4_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                            int arg_CC4_1 = 16;
                                            int arg_CC4_2 = 16;
                                            int arg_CC4_3 = 14;
                                            float arg_CC4_4 = 0f;
                                            float arg_CC4_5 = 0f;
                                            int arg_CC4_6 = 100;
                                            Color newColor = default(Color);
                                            Dust.NewDust(arg_CC4_0, arg_CC4_1, arg_CC4_2, arg_CC4_3, arg_CC4_4, arg_CC4_5, arg_CC4_6, newColor, 1f);
                                        }
                                        else
                                        {
                                            if (tilecopies[g].maintile.type == 26 && Main.rand.Next(20) == 0)
                                            {
                                                Vector2 arg_D26_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                                int arg_D26_1 = 16;
                                                int arg_D26_2 = 16;
                                                int arg_D26_3 = 14;
                                                float arg_D26_4 = 0f;
                                                float arg_D26_5 = 0f;
                                                int arg_D26_6 = 100;
                                                Color newColor = default(Color);
                                                Dust.NewDust(arg_D26_0, arg_D26_1, arg_D26_2, arg_D26_3, arg_D26_4, arg_D26_5, arg_D26_6, newColor, 1f);
                                            }
                                            else
                                            {
                                                if ((tilecopies[g].maintile.type == 71 || tilecopies[g].maintile.type == 72) && Main.rand.Next(500) == 0)
                                                {
                                                    Vector2 arg_DA5_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                                    int arg_DA5_1 = 16;
                                                    int arg_DA5_2 = 16;
                                                    int arg_DA5_3 = 41;
                                                    float arg_DA5_4 = 0f;
                                                    float arg_DA5_5 = 0f;
                                                    int arg_DA5_6 = 250;
                                                    Color newColor = default(Color);
                                                    Dust.NewDust(arg_DA5_0, arg_DA5_1, arg_DA5_2, arg_DA5_3, arg_DA5_4, arg_DA5_5, arg_DA5_6, newColor, 0.8f);
                                                }
                                                else
                                                {
                                                    if ((tilecopies[g].maintile.type == 17 || tilecopies[g].maintile.type == 77) && Main.rand.Next(40) == 0)
                                                    {
                                                        if (tilecopies[g].maintile.frameX == 18 & tilecopies[g].maintile.frameY == 18)
                                                        {
                                                            Vector2 arg_E51_0 = new Vector2((float)(tilecopies[g].loc.X * 16 + 2), (float)(tilecopies[g].loc.Y * 16));
                                                            int arg_E51_1 = 8;
                                                            int arg_E51_2 = 6;
                                                            int arg_E51_3 = 6;
                                                            float arg_E51_4 = 0f;
                                                            float arg_E51_5 = 0f;
                                                            int arg_E51_6 = 100;
                                                            Color newColor = default(Color);
                                                            Dust.NewDust(arg_E51_0, arg_E51_1, arg_E51_2, arg_E51_3, arg_E51_4, arg_E51_5, arg_E51_6, newColor, 1f);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (tilecopies[g].maintile.type == 37 && Main.rand.Next(250) == 0)
                                                        {
                                                            Vector2 arg_EBB_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                                            int arg_EBB_1 = 16;
                                                            int arg_EBB_2 = 16;
                                                            int arg_EBB_3 = 6;
                                                            float arg_EBB_4 = 0f;
                                                            float arg_EBB_5 = 0f;
                                                            int arg_EBB_6 = 0;
                                                            Color newColor = default(Color);
                                                            int num8 = Dust.NewDust(arg_EBB_0, arg_EBB_1, arg_EBB_2, arg_EBB_3, arg_EBB_4, arg_EBB_5, arg_EBB_6, newColor, (float)Main.rand.Next(3));
                                                            if (Main.dust[num8].scale > 1f)
                                                            {
                                                                Main.dust[num8].noGravity = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if ((tilecopies[g].maintile.type == 58 || tilecopies[g].maintile.type == 76) && Main.rand.Next(250) == 0)
                                                            {
                                                                Vector2 arg_F65_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                                                int arg_F65_1 = 16;
                                                                int arg_F65_2 = 16;
                                                                int arg_F65_3 = 6;
                                                                float arg_F65_4 = 0f;
                                                                float arg_F65_5 = 0f;
                                                                int arg_F65_6 = 0;
                                                                Color newColor = default(Color);
                                                                int num9 = Dust.NewDust(arg_F65_0, arg_F65_1, arg_F65_2, arg_F65_3, arg_F65_4, arg_F65_5, arg_F65_6, newColor, (float)Main.rand.Next(3));
                                                                if (Main.dust[num9].scale > 1f)
                                                                {
                                                                    Main.dust[num9].noGravity = true;
                                                                }
                                                                Main.dust[num9].noLight = true;
                                                            }
                                                            else
                                                            {
                                                                if (tilecopies[g].maintile.type == 61)
                                                                {
                                                                    if (tilecopies[g].maintile.frameX == 144)
                                                                    {
                                                                        if (Main.rand.Next(60) == 0)
                                                                        {
                                                                            Vector2 arg_101B_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                                                            int arg_101B_1 = 16;
                                                                            int arg_101B_2 = 16;
                                                                            int arg_101B_3 = 44;
                                                                            float arg_101B_4 = 0f;
                                                                            float arg_101B_5 = 0f;
                                                                            int arg_101B_6 = 250;
                                                                            Color newColor = default(Color);
                                                                            int num10 = Dust.NewDust(arg_101B_0, arg_101B_1, arg_101B_2, arg_101B_3, arg_101B_4, arg_101B_5, arg_101B_6, newColor, 0.4f);
                                                                            Main.dust[num10].fadeIn = 0.7f;
                                                                        }
                                                                        color.A = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
                                                                        color.R = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
                                                                        color.B = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
                                                                        color.G = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
                                                                    }
                                                                }



                                                                else
                                                                {
                                                                    if (Main.tileShine[(int)tilecopies[g].maintile.type] > 0 && color.R > 60 && Main.rand.Next(Main.tileShine[(int)tilecopies[g].maintile.type]) < (int)(color.R / 50) && (tilecopies[g].maintile.type != 21 || (tilecopies[g].maintile.frameX >= 36 && tilecopies[g].maintile.frameX < 180)))
                                                                    {
                                                                        Vector2 arg_117D_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                                                        int arg_117D_1 = 16;
                                                                        int arg_117D_2 = 16;
                                                                        int arg_117D_3 = 43;
                                                                        float arg_117D_4 = 0f;
                                                                        float arg_117D_5 = 0f;
                                                                        int arg_117D_6 = 254;
                                                                        Color newColor = default(Color);
                                                                        int num11 = Dust.NewDust(arg_117D_0, arg_117D_1, arg_117D_2, arg_117D_3, arg_117D_4, arg_117D_5, arg_117D_6, newColor, 0.5f);
                                                                        Dust expr_118C = Main.dust[num11];
                                                                        expr_118C.velocity *= 0f;
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
                        if (tilecopies[g].maintile.type == 5 && tilecopies[g].maintile.frameY >= 198 && tilecopies[g].maintile.frameX >= 22)
                        {
                            int num12 = 0;
                            if (tilecopies[g].maintile.frameX == 22)
                            {
                                if (tilecopies[g].maintile.frameY == 220)
                                {
                                    num12 = 1;
                                }
                                else
                                {
                                    if (tilecopies[g].maintile.frameY == 242)
                                    {
                                        num12 = 2;
                                    }
                                }
                                int num13 = 0;
                                int num14 = 80;
                                int num15 = 80;
                                int num16 = 32;
                                for (int k = tilecopies[g].loc.Y; k < tilecopies[g].loc.Y + 100; k++)
                                {
                                    if (Main.tile[tilecopies[g].loc.X, k].type == 2)
                                    {
                                        num13 = 0;
                                        break;
                                    }
                                    if (Main.tile[tilecopies[g].loc.X, k].type == 23)
                                    {
                                        num13 = 1;
                                        break;
                                    }
                                    if (Main.tile[tilecopies[g].loc.X, k].type == 60)
                                    {
                                        num13 = 2;
                                        num14 = 114;
                                        num15 = 96;
                                        num16 = 48;
                                        break;
                                    }
                                }
                                SpriteBatch arg_1346_0 = this.spriteBatch;
                                Texture2D arg_1346_1 = Main.treeTopTexture[num13];
                                Vector2 arg_1346_2 = new Vector2((float)(tilecopies[g].loc.X * 16 - (int)Main.screenPosition.X - num16), (float)(tilecopies[g].loc.Y * 16 - (int)Main.screenPosition.Y - num15 + 16));
                                Rectangle? arg_1346_3 = new Rectangle?(new Rectangle(num12 * (num14 + 2), 0, num14, num15));
                                Color arg_1346_4 = Lighting.GetColor(tilecopies[g].loc.X, tilecopies[g].loc.Y);
                                float arg_1346_5 = 0f;
                                Vector2 origin = default(Vector2);
                                arg_1346_0.Draw(arg_1346_1, arg_1346_2, arg_1346_3, arg_1346_4, arg_1346_5, origin, 1f, SpriteEffects.None, 0f);
                            }
                            else
                            {
                                if (tilecopies[g].maintile.frameX == 44)
                                {
                                    if (tilecopies[g].maintile.frameY == 220)
                                    {
                                        num12 = 1;
                                    }
                                    else
                                    {
                                        if (tilecopies[g].maintile.frameY == 242)
                                        {
                                            num12 = 2;
                                        }
                                    }
                                    int num17 = 0;
                                    for (int l = tilecopies[g].loc.Y; l < tilecopies[g].loc.Y + 100; l++)
                                    {
                                        if (Main.tile[tilecopies[g].loc.X + 1, l].type == 2)
                                        {
                                            num17 = 0;
                                            break;
                                        }
                                        if (Main.tile[tilecopies[g].loc.X + 1, l].type == 23)
                                        {
                                            num17 = 1;
                                            break;
                                        }
                                        if (Main.tile[tilecopies[g].loc.X + 1, l].type == 60)
                                        {
                                            num17 = 2;
                                            break;
                                        }
                                    }
                                    SpriteBatch arg_148B_0 = this.spriteBatch;
                                    Texture2D arg_148B_1 = Main.treeBranchTexture[num17];
                                    Vector2 arg_148B_2 = new Vector2((float)(tilecopies[g].loc.X * 16 - (int)Main.screenPosition.X - 24), (float)(tilecopies[g].loc.Y * 16 - (int)Main.screenPosition.Y - 12));
                                    Rectangle? arg_148B_3 = new Rectangle?(new Rectangle(0, num12 * 42, 40, 40));
                                    Color arg_148B_4 = Lighting.GetColor(tilecopies[g].loc.X, tilecopies[g].loc.Y);
                                    float arg_148B_5 = 0f;
                                    Vector2 origin = default(Vector2);
                                    arg_148B_0.Draw(arg_148B_1, arg_148B_2, arg_148B_3, arg_148B_4, arg_148B_5, origin, 1f, SpriteEffects.None, 0f);
                                }
                                else
                                {
                                    if (tilecopies[g].maintile.frameX == 66)
                                    {
                                        if (tilecopies[g].maintile.frameY == 220)
                                        {
                                            num12 = 1;
                                        }
                                        else
                                        {
                                            if (tilecopies[g].maintile.frameY == 242)
                                            {
                                                num12 = 2;
                                            }
                                        }
                                        int num18 = 0;
                                        for (int m = tilecopies[g].loc.Y; m < tilecopies[g].loc.Y + 100; m++)
                                        {
                                            if (Main.tile[tilecopies[g].loc.X - 1, m].type == 2)
                                            {
                                                num18 = 0;
                                                break;
                                            }
                                            if (Main.tile[tilecopies[g].loc.X - 1, m].type == 23)
                                            {
                                                num18 = 1;
                                                break;
                                            }
                                            if (Main.tile[tilecopies[g].loc.X - 1, m].type == 60)
                                            {
                                                num18 = 2;
                                                break;
                                            }
                                        }
                                        SpriteBatch arg_15CE_0 = this.spriteBatch;
                                        Texture2D arg_15CE_1 = Main.treeBranchTexture[num18];
                                        Vector2 arg_15CE_2 = new Vector2((float)(tilecopies[g].loc.X * 16 - (int)Main.screenPosition.X), (float)(tilecopies[g].loc.Y * 16 - (int)Main.screenPosition.Y - 12));
                                        Rectangle? arg_15CE_3 = new Rectangle?(new Rectangle(42, num12 * 42, 40, 40));
                                        Color arg_15CE_4 = Lighting.GetColor(tilecopies[g].loc.X, tilecopies[g].loc.Y);
                                        float arg_15CE_5 = 0f;
                                        Vector2 origin = default(Vector2);
                                        arg_15CE_0.Draw(arg_15CE_1, arg_15CE_2, arg_15CE_3, arg_15CE_4, arg_15CE_5, origin, 1f, SpriteEffects.None, 0f);
                                    }
                                }
                            }
                        }
                        if (tilecopies[g].maintile.type == 72 && tilecopies[g].maintile.frameX >= 36)
                        {
                            int num19 = 0;
                            if (tilecopies[g].maintile.frameY == 18)
                            {
                                num19 = 1;
                            }
                            else
                            {
                                if (tilecopies[g].maintile.frameY == 36)
                                {
                                    num19 = 2;
                                }
                            }
                            SpriteBatch arg_16B1_0 = this.spriteBatch;
                            Texture2D arg_16B1_1 = Main.shroomCapTexture;
                            Vector2 arg_16B1_2 = new Vector2((float)(tilecopies[g].loc.X * 16 - (int)Main.screenPosition.X - 22), (float)(tilecopies[g].loc.Y * 16 - (int)Main.screenPosition.Y - 26));
                            Rectangle? arg_16B1_3 = new Rectangle?(new Rectangle(num19 * 62, 0, 60, 42));
                            Color arg_16B1_4 = Lighting.GetColor(tilecopies[g].loc.X, tilecopies[g].loc.Y);
                            float arg_16B1_5 = 0f;
                            Vector2 origin = default(Vector2);
                            arg_16B1_0.Draw(arg_16B1_1, arg_16B1_2, arg_16B1_3, arg_16B1_4, arg_16B1_5, origin, 1f, SpriteEffects.None, 0f);
                        }
                        if (color.R > 0)
                        {
                            if (Main.tileSolid[(int)tilecopies[g].maintile.type] && !Main.tileSolidTop[(int)tilecopies[g].maintile.type] && (Main.tile[tilecopies[g].loc.X - 1, tilecopies[g].loc.Y].liquid > 0 || Main.tile[tilecopies[g].loc.X + 1, tilecopies[g].loc.Y].liquid > 0 || Main.tile[tilecopies[g].loc.X, tilecopies[g].loc.Y - 1].liquid > 0 || Main.tile[tilecopies[g].loc.X, tilecopies[g].loc.Y + 1].liquid > 0))
                            {
                                Color color2 = Lighting.GetColor(tilecopies[g].loc.X, tilecopies[g].loc.Y);
                                int num20 = 0;
                                bool flag = false;
                                bool flag2 = false;
                                bool flag3 = false;
                                bool flag4 = false;
                                int num21 = 0;
                                bool flag5 = false;
                                if ((int)Main.tile[tilecopies[g].loc.X - 1, tilecopies[g].loc.Y].liquid > num20)
                                {
                                    num20 = (int)Main.tile[tilecopies[g].loc.X - 1, tilecopies[g].loc.Y].liquid;
                                    flag = true;
                                }
                                else
                                {
                                    if (Main.tile[tilecopies[g].loc.X - 1, tilecopies[g].loc.Y].liquid > 0)
                                    {
                                        flag = true;
                                    }
                                }
                                if ((int)Main.tile[tilecopies[g].loc.X + 1, tilecopies[g].loc.Y].liquid > num20)
                                {
                                    num20 = (int)Main.tile[tilecopies[g].loc.X + 1, tilecopies[g].loc.Y].liquid;
                                    flag2 = true;
                                }
                                else
                                {
                                    if (Main.tile[tilecopies[g].loc.X + 1, tilecopies[g].loc.Y].liquid > 0)
                                    {
                                        num20 = (int)Main.tile[tilecopies[g].loc.X + 1, tilecopies[g].loc.Y].liquid;
                                        flag2 = true;
                                    }
                                }
                                if (Main.tile[tilecopies[g].loc.X, tilecopies[g].loc.Y - 1].liquid > 0)
                                {
                                    flag3 = true;
                                }
                                if (Main.tile[tilecopies[g].loc.X, tilecopies[g].loc.Y + 1].liquid > 240)
                                {
                                    flag4 = true;
                                }
                                if (Main.tile[tilecopies[g].loc.X - 1, tilecopies[g].loc.Y].liquid > 0)
                                {
                                    if (Main.tile[tilecopies[g].loc.X - 1, tilecopies[g].loc.Y].lava)
                                    {
                                        num21 = 1;
                                    }
                                    else
                                    {
                                        flag5 = true;
                                    }
                                }
                                if (Main.tile[tilecopies[g].loc.X + 1, tilecopies[g].loc.Y].liquid > 0)
                                {
                                    if (Main.tile[tilecopies[g].loc.X + 1, tilecopies[g].loc.Y].lava)
                                    {
                                        num21 = 1;
                                    }
                                    else
                                    {
                                        flag5 = true;
                                    }
                                }
                                if (Main.tile[tilecopies[g].loc.X, tilecopies[g].loc.Y - 1].liquid > 0)
                                {
                                    if (Main.tile[tilecopies[g].loc.X, tilecopies[g].loc.Y - 1].lava)
                                    {
                                        num21 = 1;
                                    }
                                    else
                                    {
                                        flag5 = true;
                                    }
                                }
                                if (Main.tile[tilecopies[g].loc.X, tilecopies[g].loc.Y + 1].liquid > 0)
                                {
                                    if (Main.tile[tilecopies[g].loc.X, tilecopies[g].loc.Y + 1].lava)
                                    {
                                        num21 = 1;
                                    }
                                    else
                                    {
                                        flag5 = true;
                                    }
                                }
                                if (!flag5 || num21 != 1)
                                {
                                    Vector2 value = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                    Rectangle value2 = new Rectangle(0, 4, 16, 16);
                                    if (flag4 && (flag || flag2))
                                    {
                                        flag = true;
                                        flag2 = true;
                                    }
                                    if ((!flag3 || (!flag && !flag2)) && (!flag4 || !flag3))
                                    {
                                        if (flag3)
                                        {
                                            value2 = new Rectangle(0, 4, 16, 4);
                                        }
                                        else
                                        {
                                            if (flag4 && !flag && !flag2)
                                            {
                                                value = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 + 12));
                                                value2 = new Rectangle(0, 4, 16, 4);
                                            }
                                            else
                                            {
                                                float num22 = (float)(256 - num20);
                                                num22 /= 32f;
                                                if (flag && flag2)
                                                {
                                                    value = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 + (int)num22 * 2));
                                                    value2 = new Rectangle(0, 4, 16, 16 - (int)num22 * 2);
                                                }
                                                else
                                                {
                                                    if (flag)
                                                    {
                                                        value = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 + (int)num22 * 2));
                                                        value2 = new Rectangle(0, 4, 4, 16 - (int)num22 * 2);
                                                    }
                                                    else
                                                    {
                                                        value = new Vector2((float)(tilecopies[g].loc.X * 16 + 12), (float)(tilecopies[g].loc.Y * 16 + (int)num22 * 2));
                                                        value2 = new Rectangle(0, 4, 4, 16 - (int)num22 * 2);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    float num23 = 0.5f;
                                    if (num21 == 1)
                                    {
                                        num23 *= 1.6f;
                                    }
                                    if ((double)tilecopies[g].loc.Y < Main.worldSurface || num23 > 1f)
                                    {
                                        num23 = 1f;
                                    }
                                    float num24 = (float)color2.R * num23;
                                    float num25 = (float)color2.G * num23;
                                    float num26 = (float)color2.B * num23;
                                    float num27 = (float)color2.A * num23;
                                    color2 = new Color((int)((byte)num24), (int)((byte)num25), (int)((byte)num26), (int)((byte)num27));
                                    SpriteBatch arg_1B5D_0 = this.spriteBatch;
                                    Texture2D arg_1B5D_1 = Main.liquidTexture[num21];
                                    Vector2 arg_1B5D_2 = value - Main.screenPosition;
                                    Rectangle? arg_1B5D_3 = new Rectangle?(value2);
                                    Color arg_1B5D_4 = color2;
                                    float arg_1B5D_5 = 0f;
                                    Vector2 origin = default(Vector2);
                                    arg_1B5D_0.Draw(arg_1B5D_1, arg_1B5D_2, arg_1B5D_3, arg_1B5D_4, arg_1B5D_5, origin, 1f, SpriteEffects.None, 0f);
                                }
                            }
                            if (tilecopies[g].maintile.type == 51)
                            {
                                Color color3 = Lighting.GetColor(tilecopies[g].loc.X, tilecopies[g].loc.Y);
                                float num28 = 0.5f;
                                float num29 = (float)color3.R * num28;
                                float num30 = (float)color3.G * num28;
                                float num31 = (float)color3.B * num28;
                                float num32 = (float)color3.A * num28;
                                color3 = new Color((int)((byte)num29), (int)((byte)num30), (int)((byte)num31), (int)((byte)num32));
                                SpriteBatch arg_1C80_0 = this.spriteBatch;
                                Texture2D arg_1C80_1 = Main.tileTexture[(int)tilecopies[g].maintile.type];
                                Vector2 arg_1C80_2 = new Vector2((float)(tilecopies[g].loc.X * 16 - (int)Main.screenPosition.X) - ((float)num5 - 16f) / 2f, (float)(tilecopies[g].loc.Y * 16 - (int)Main.screenPosition.Y + num6));
                                Rectangle? arg_1C80_3 = new Rectangle?(new Rectangle((int)tilecopies[g].maintile.frameX, (int)tilecopies[g].maintile.frameY, num5, height));
                                Color arg_1C80_4 = color3;
                                float arg_1C80_5 = 0f;
                                Vector2 origin = default(Vector2);
                                arg_1C80_0.Draw(arg_1C80_1, arg_1C80_2, arg_1C80_3, arg_1C80_4, arg_1C80_5, origin, 1f, SpriteEffects.None, 0f);
                            }
                            else
                            {
                                if (Main.tileAlch[(int)tilecopies[g].maintile.type])
                                {
                                    height = 20;
                                    num6 = -1;
                                    int num33 = (int)tilecopies[g].maintile.type;
                                    int num34 = (int)(tilecopies[g].maintile.frameX / 18);
                                    if (num33 > 82)
                                    {
                                        if (num34 == 0 && Main.dayTime)
                                        {
                                            num33 = 84;
                                        }
                                        if (num34 == 1 && !Main.dayTime)
                                        {
                                            num33 = 84;
                                        }
                                        if (num34 == 3 && Main.bloodMoon)
                                        {
                                            num33 = 84;
                                        }
                                    }
                                    if (num33 == 84)
                                    {
                                        if (num34 == 0 && Main.rand.Next(100) == 0)
                                        {
                                            Vector2 arg_1D69_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 - 4));
                                            int arg_1D69_1 = 16;
                                            int arg_1D69_2 = 16;
                                            int arg_1D69_3 = 19;
                                            float arg_1D69_4 = 0f;
                                            float arg_1D69_5 = 0f;
                                            int arg_1D69_6 = 160;
                                            Color newColor = default(Color);
                                            int num35 = Dust.NewDust(arg_1D69_0, arg_1D69_1, arg_1D69_2, arg_1D69_3, arg_1D69_4, arg_1D69_5, arg_1D69_6, newColor, 0.1f);
                                            Dust expr_1D7D_cp_0 = Main.dust[num35];
                                            expr_1D7D_cp_0.velocity.X = expr_1D7D_cp_0.velocity.X / 2f;
                                            Dust expr_1D9B_cp_0 = Main.dust[num35];
                                            expr_1D9B_cp_0.velocity.Y = expr_1D9B_cp_0.velocity.Y / 2f;
                                            Main.dust[num35].noGravity = true;
                                            Main.dust[num35].fadeIn = 1f;
                                        }
                                        if (num34 == 1 && Main.rand.Next(100) == 0)
                                        {
                                            Vector2 arg_1E14_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                            int arg_1E14_1 = 16;
                                            int arg_1E14_2 = 16;
                                            int arg_1E14_3 = 41;
                                            float arg_1E14_4 = 0f;
                                            float arg_1E14_5 = 0f;
                                            int arg_1E14_6 = 250;
                                            Color newColor = default(Color);
                                            Dust.NewDust(arg_1E14_0, arg_1E14_1, arg_1E14_2, arg_1E14_3, arg_1E14_4, arg_1E14_5, arg_1E14_6, newColor, 0.8f);
                                        }
                                        if (num34 == 3)
                                        {
                                            if (Main.rand.Next(200) == 0)
                                            {
                                                Vector2 arg_1E65_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                                int arg_1E65_1 = 16;
                                                int arg_1E65_2 = 16;
                                                int arg_1E65_3 = 14;
                                                float arg_1E65_4 = 0f;
                                                float arg_1E65_5 = 0f;
                                                int arg_1E65_6 = 100;
                                                Color newColor = default(Color);
                                                int num36 = Dust.NewDust(arg_1E65_0, arg_1E65_1, arg_1E65_2, arg_1E65_3, arg_1E65_4, arg_1E65_5, arg_1E65_6, newColor, 0.2f);
                                                Main.dust[num36].fadeIn = 1.2f;
                                            }
                                            if (Main.rand.Next(75) == 0)
                                            {
                                                Vector2 arg_1EBE_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                                int arg_1EBE_1 = 16;
                                                int arg_1EBE_2 = 16;
                                                int arg_1EBE_3 = 27;
                                                float arg_1EBE_4 = 0f;
                                                float arg_1EBE_5 = 0f;
                                                int arg_1EBE_6 = 100;
                                                Color newColor = default(Color);
                                                int num37 = Dust.NewDust(arg_1EBE_0, arg_1EBE_1, arg_1EBE_2, arg_1EBE_3, arg_1EBE_4, arg_1EBE_5, arg_1EBE_6, newColor, 1f);
                                                Dust expr_1ED2_cp_0 = Main.dust[num37];
                                                expr_1ED2_cp_0.velocity.X = expr_1ED2_cp_0.velocity.X / 2f;
                                                Dust expr_1EF0_cp_0 = Main.dust[num37];
                                                expr_1EF0_cp_0.velocity.Y = expr_1EF0_cp_0.velocity.Y / 2f;
                                            }
                                        }
                                        if (num34 == 4 && Main.rand.Next(150) == 0)
                                        {
                                            Vector2 arg_1F4D_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16));
                                            int arg_1F4D_1 = 16;
                                            int arg_1F4D_2 = 8;
                                            int arg_1F4D_3 = 16;
                                            float arg_1F4D_4 = 0f;
                                            float arg_1F4D_5 = 0f;
                                            int arg_1F4D_6 = 0;
                                            Color newColor = default(Color);
                                            int num38 = Dust.NewDust(arg_1F4D_0, arg_1F4D_1, arg_1F4D_2, arg_1F4D_3, arg_1F4D_4, arg_1F4D_5, arg_1F4D_6, newColor, 1f);
                                            Dust expr_1F61_cp_0 = Main.dust[num38];
                                            expr_1F61_cp_0.velocity.X = expr_1F61_cp_0.velocity.X / 3f;
                                            Dust expr_1F7F_cp_0 = Main.dust[num38];
                                            expr_1F7F_cp_0.velocity.Y = expr_1F7F_cp_0.velocity.Y / 3f;
                                            Dust expr_1F9D_cp_0 = Main.dust[num38];
                                            expr_1F9D_cp_0.velocity.Y = expr_1F9D_cp_0.velocity.Y - 0.7f;
                                            Main.dust[num38].alpha = 50;
                                            Main.dust[num38].scale *= 0.1f;
                                            Main.dust[num38].fadeIn = 0.9f;
                                            Main.dust[num38].noGravity = true;
                                        }
                                        if (num34 == 5)
                                        {
                                            if (Main.rand.Next(40) == 0)
                                            {
                                                Vector2 arg_203E_0 = new Vector2((float)(tilecopies[g].loc.X * 16), (float)(tilecopies[g].loc.Y * 16 - 6));
                                                int arg_203E_1 = 16;
                                                int arg_203E_2 = 16;
                                                int arg_203E_3 = 6;
                                                float arg_203E_4 = 0f;
                                                float arg_203E_5 = 0f;
                                                int arg_203E_6 = 0;
                                                Color newColor = default(Color);
                                                int num39 = Dust.NewDust(arg_203E_0, arg_203E_1, arg_203E_2, arg_203E_3, arg_203E_4, arg_203E_5, arg_203E_6, newColor, 1.5f);
                                                Dust expr_2052_cp_0 = Main.dust[num39];
                                                expr_2052_cp_0.velocity.Y = expr_2052_cp_0.velocity.Y - 2f;
                                                Main.dust[num39].noGravity = true;
                                            }
                                            color.A = (byte)(Main.mouseTextColor / 2);
                                            color.G = Main.mouseTextColor;
                                            color.B = Main.mouseTextColor;
                                        }
                                    }
                                    SpriteBatch arg_2132_0 = this.spriteBatch;
                                    Texture2D arg_2132_1 = Main.tileTexture[num33];
                                    Vector2 arg_2132_2 = new Vector2((float)(tilecopies[g].loc.X * 16 - (int)Main.screenPosition.X) - ((float)num5 - 16f) / 2f, (float)(tilecopies[g].loc.Y * 16 - (int)Main.screenPosition.Y + num6));
                                    Rectangle? arg_2132_3 = new Rectangle?(new Rectangle((int)tilecopies[g].maintile.frameX, (int)tilecopies[g].maintile.frameY, num5, height));
                                    Color arg_2132_4 = color;
                                    float arg_2132_5 = 0f;
                                    Vector2 origin = default(Vector2);
                                    arg_2132_0.Draw(arg_2132_1, arg_2132_2, arg_2132_3, arg_2132_4, arg_2132_5, origin, 1f, SpriteEffects.None, 0f);
                                }
                                else
                                {


                                    SpriteBatch arg_21E7_0 = this.spriteBatch;
                                    Texture2D arg_21E7_1 = Main.tileTexture[(int)tilecopies[g].maintile.type];
                                    Vector2 arg_21E7_2 = new Vector2((float)(tilecopies[g].loc.X * 16 - (int)Main.screenPosition.X) - ((float)num5 - 16f) / 2f, (float)(tilecopies[g].loc.Y * 16 - (int)Main.screenPosition.Y + num6));
                                    Rectangle? arg_21E7_3 = new Rectangle?(new Rectangle((int)tilecopies[g].maintile.frameX, (int)tilecopies[g].maintile.frameY, num5, height));
                                    Color arg_21E7_4 = color;
                                    float arg_21E7_5 = 0f;
                                    Vector2 origin = default(Vector2);
                                    arg_21E7_0.Draw(arg_21E7_1, arg_21E7_2, arg_21E7_3, arg_21E7_4, arg_21E7_5, origin, 1f, SpriteEffects.None, 0f);
                                }
                            }
                        }
                    }
                }
            }
        }
        public void DrawnpcText()
        {
            float minX = (Main.screenPosition.X) / 16f;
            float minY = (Main.screenPosition.Y) / 16f;
            float MaxX = (Main.screenPosition.X + (float)Main.screenWidth) / 16f;
            float MaxY = (Main.screenPosition.Y + (float)Main.screenHeight) / 16f;
            for (int g = 0; g < spawnText.Count; g++)
            {

                if ((spawnText[g].X >= minX)
                 && (spawnText[g].Y >= minY)
                 && (spawnText[g].X <= MaxX)
                 && (spawnText[g].Y <= MaxY))
                {
                    this.spriteBatch.DrawString(Main.fontMouseText,
                        spawnText[g].NpcName + " : " + spawnText[g].size.ToString(),
                        new Vector2((((spawnText[g].X) * 16) - ((int)Main.screenPosition.X)), (float)((((spawnText[g].Y) * 16) - ((int)Main.screenPosition.Y)))),
                        Color.Aquamarine,
                        0f,
                        new Vector2(),
                        (float)1f,
                        SpriteEffects.None, 0f);
                }
            }
        }



        public Texture2D CreateThePixel(SpriteBatch spriteBatch)
        {

            Color white = new Color(255, 255, 255, 10);
            Texture2D result = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            result.SetData(new Color[] { white });
            return result;
        }



        #endregion


        public static void placetilesandwalls(int x, int y, bool tiles = false, bool walls = false)
        {
            if (tiles)
            {

                if (Mod.tileitem.createTile >= 0)
                {

                    int num8 = Mod.tileitem.placeStyle;
                    if (Mod.tileitem.createTile == 141)
                    {
                        num8 = Main.rand.Next(2);
                    }
                    if (Mod.tileitem.createTile == 128 || Mod.tileitem.createTile == 137)
                    {
                        if (Main.player[Main.myPlayer].direction < 0)
                        {
                            num8 = -1;
                        }
                        else
                        {
                            num8 = 1;
                        }
                    }
                    if (WorldGenReflect.PlaceTile(x, y, Mod.tileitem.createTile, false, false, Main.player[Main.myPlayer].whoAmi, num8))
                    {
                        Main.player[Main.myPlayer].itemTime = Mod.tileitem.useTime;
                        if (Main.netMode == 1)
                        {
                            SendPackets.SendData(117, -1, -1, "", 1, (float)x, (float)y, (float)Mod.tileitem.createTile, num8);
                        }
                        if (Mod.tileitem.createTile == 15)
                        {

                            if (Main.netMode == 1)
                            {
                                SendPackets.SendTileSquare(-1, x - 1, y - 1, 3);
                            }
                        }
                        else
                        {
                            if ((Mod.tileitem.createTile == 79 || Mod.tileitem.createTile == 90) && Main.netMode == 1)
                            {
                                SendPackets.SendTileSquare(-1, x, y, 5);
                            }
                        }
                    }
                }

            }
            if (walls)
            {
                if (Mod.wallitem.createWall > 0)
                {
                  
                    WorldGenReflect.PlaceWall(x, y, Mod.wallitem.createWall, false);
                    if ((int)Main.tile[x, y].wall == Mod.wallitem.createWall)
                    {

                        if (Main.netMode == 1)
                        {
                            SendPackets.SendData(117, -1, -1, "", 3, (float)x, (float)y, (float)Mod.wallitem.createWall, 0);
                        }


                    }

                }
            }
        }
        public static void UpdateNoclip()
        {
            bool move = false;
            if (Main.player[Main.myPlayer].velocity.Y != 0)
            {
                Main.player[Main.myPlayer].oldVelocity.Y = Main.player[Main.myPlayer].velocity.Y = 0;
            }
            if (Main.player[Main.myPlayer].position == null)
            {
                Main.player[Main.myPlayer].position = new Vector2();

            }

            Main.player[Main.myPlayer].position = Main.player[Main.myPlayer].oldPosition;
            if (!keyboardState.IsKeyDown(Keys.LeftShift) && !keyboardState.IsKeyDown(Keys.RightShift))
            {
                if (!Main.chatMode)
                {
                    float oldX = Main.player[Main.myPlayer].position.X;
                    float oldY = Main.player[Main.myPlayer].position.Y;

                    if (Main.keyState.IsKeyDown(Keys.A))
                    {
                        if (Main.keyState.IsKeyDown(Keys.LeftShift) || (Main.keyState.IsKeyDown(Keys.RightShift))) Main.player[Main.myPlayer].position = new Vector2((oldX -= 64f), oldY);
                        else Main.player[Main.myPlayer].position = new Vector2((oldX -= 16f), oldY);
                        move = true;
                    }
                    if (Main.keyState.IsKeyDown(Keys.D))
                    {
                        if (Main.keyState.IsKeyDown(Keys.LeftShift) || (Main.keyState.IsKeyDown(Keys.RightShift))) Main.player[Main.myPlayer].position = new Vector2((oldX += 64f), oldY);
                        else Main.player[Main.myPlayer].position = new Vector2((oldX += 16f), oldY);
                        move = true;
                    }
                    if (Main.keyState.IsKeyDown(Keys.W))
                    {
                        if (Main.keyState.IsKeyDown(Keys.LeftShift) || (Main.keyState.IsKeyDown(Keys.RightShift))) Main.player[Main.myPlayer].position = new Vector2(oldX, (oldY -= 64f));
                        else Main.player[Main.myPlayer].position = new Vector2(oldX, (oldY -= 16f));
                        move = true;
                    }
                    if (Main.keyState.IsKeyDown(Keys.S))
                    {
                        if (Main.keyState.IsKeyDown(Keys.LeftShift) || (Main.keyState.IsKeyDown(Keys.RightShift))) Main.player[Main.myPlayer].position = new Vector2(oldX, (oldY += 64f));
                        else Main.player[Main.myPlayer].position = new Vector2(oldX, (oldY += 16f));
                        move = true;
                    }
                    if (move && Main.netMode == 1)
                    {
                        SendPackets.SendData(113, -1, -1, "", Main.myPlayer, 0f, 0f, 0f);
                    }

                }
            }
        }
        public static bool canTeleport { get; set; }

        public static bool MouseLeftRelease { get; set; }


        public static void privatText(string string3, Color color = new Color())
        {
            int num52 = Main.myPlayer;
            if (Main.netMode == 1)
            {
                string newText = string3;
                if (num52 < 255)
                {
                    newText = "<" + Main.player[num52].name + "> " + string3;
                    Main.player[num52].chatText = string3;
                    Main.player[num52].chatShowTime = Main.chatLength / 2;
                }
                Main.NewText(newText, color.R, color.G, color.B);
                return;
            }
        }

        public static KeyboardState keyboardState { get; set; }
    }
}