using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
namespace Editor_Mod
{
 
    #region tools
    public class ToolWindow : XG_Control
    {

        public Mod Game;
        public XGTabControl TabControl { get; protected set; }
        //  public XGTabPage PageOne { get; protected set; }
        public static int Y = Main.screenHeight - 200;
        public static int X = 500;

 
 

        public ToolWindow(Mod game)
            : base(new Rectangle(X, Y, 420, 180), true)
        {
            Game = game;

            // Add our window to the Controls to be managed by the Editor_Mod GUI manager

            XnaGUIManager.Controls.Add(this);

            // Offset the Tab Control and Tab Page rectangles relative to this Tool Window 

            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control
            pageRect.X = pageRect.Y = 0;  // but use parent relative coordintates


            TabControl = new XGTabControl(pageRect);
            Children.Add(TabControl); // add the tab control to our child control list

            modify = new Modify(game, pageRect);
            TabControl.Children.Add(modify);
 
            brush = new Brush(game, pageRect);
            TabControl.Children.Add(brush);

            npcs = new Npcs(game, pageRect);
            TabControl.Children.Add(npcs);

            itemssettings = new ItemsSettings(game, pageRect);
            TabControl.Children.Add(itemssettings);
 

            playerSettings = new PlayerSettings(game, pageRect);
            TabControl.Children.Add(playerSettings);

            lazyInventory = new InV(game, pageRect);
            TabControl.Children.Add(lazyInventory);
            server = new Server(game, pageRect);
            TabControl.Children.Add(server);
        }


 
        public InV lazyInventory { get; set; }

        public Server server { get; set; }

        public PlayerSettings playerSettings { get; set; }

        public ItemsSettings itemssettings { get; set; }

        public Npcs npcs { get; set; }

        public Modify modify { get; set; }

        public Brush brush { get; set; }
    }




 

    #region Modify

    public class Modify : XG_TabPage
    {
        public Mod Game;

        public Modify(Mod game, Rectangle rect)
            : base(rect, "Modify")
        {
            Game = game;

  
            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control
            pageRect.X = pageRect.Y = 0;
            pageRect.Height = 148;

            Children.Add(new XG_Button(new Rectangle(350, 5, 60, 20), "Clear", this.Clear_Clicked));
            Children.Add(new XG_Button(new Rectangle(260, 5, 80, 20), "Set shape", this.Set_Clicked));
            Children.Add(new XG_Button(new Rectangle(10, 35, 70, 20), "Circle", this.Circle_Clicked));
            Children.Add(new XG_Button(new Rectangle(100, 35, 70, 20), "Triagle", this.Triangle_Clicked));
            Children.Add(new XG_Button(new Rectangle(200, 35, 70, 20), "Square", this.Square_Clicked));
            XGTabControl tab = new XGTabControl(pageRect);
            Children.Add(tab); // add the tab control to our child control list

            shapes = new Shapes(game, pageRect);
            tab.Children.Add(shapes);
            
            
            delete = new Delete(game, pageRect);
            tab.Children.Add(delete);


            replace = new Replace(game, pageRect);
            tab.Children.Add(replace);



            liquids = new Liquids(game, pageRect);
            tab.Children.Add(liquids);
       
	 

        }
        public void Square_Clicked(XG_Control sender=null)
        {   Set_Clicked( sender);
            Mod.polygons.CreateRectangle();
       
        }
        public void Circle_Clicked(XG_Control sender = null)
        {  Set_Clicked(sender);
            Mod.polygons.CreateCircle(Vector2.Zero, Mod.polygons.radius, Mod.polygons.CircleSides);
        
        }
        public void Triangle_Clicked(XG_Control sender = null)
        {          Set_Clicked(sender);
            Mod.polygons.CreateTriangle(Vector2.Zero, Mod.polygons.radius);

        }
        public void Set_Clicked(XG_Control sender = null)
        {
            Mod.polygons.SetShape();
        }
        public void Clear_Clicked(XG_Control sender = null)
        {

            Mod.polygons.Clear();
        }
 



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
         
        }





        public Delete delete { get; set; }

        public Replace replace { get; set; }

        public Liquids liquids { get; set; }

        public Shapes shapes { get; set; }
    }

    #region Delete

    public class Delete : XG_TabPage
    {
        public Mod Game;
        private bool first=true;
 
    
   
        public Delete(Mod game, Rectangle rect)
            : base(rect, "Delete")
        {
            Game = game;

            // Add controls to the tab page

            
            Children.Add(new XG_Label(new Rectangle(10, 25, 200, 20), "click on tile/wall or all to delete"));
   
            DeleteTile = new XG_ButtonLabelBool(new Rectangle(10, 60, 100, 60), "Delete Tile", null,true);
            Children.Add(DeleteTile);
            DeleteWall = new XG_ButtonLabelBool(new Rectangle(120, 60, 100, 60), "Delete Wall", null, true);
            Children.Add(DeleteWall);
            DeleteAll = new XG_ButtonLabelBool(new Rectangle(230, 60, 100, 60), "Delete All", null, true);
            Children.Add(DeleteAll);
             
        }
        void Clear_Clicked(XG_Control sender)
        {
            Mod.stopVectoring = false;
            Mod.polygons.verTices.Clear();
            Mod.polygons.activeFill.Clear();
            Mod.polygons.activeLine.Clear();
            Mod.tilecopies = new List<Tiles>();
        }

        void Set_Clicked(XG_Control sender)
        {

            Mod.stopVectoring = true;


        }
       
 

        public void modify()
        {
            if(Mod.stopVectoring)
            {
                if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && !Main.player[Main.myPlayer].mouseInterface && !Mod.polygons.onwindow)
                {
  
                    int x = (int)(((float)Main.mouseState.X + Main.screenPosition.X) / 16f);
                    int y = (int)(((float)Main.mouseState.Y + Main.screenPosition.Y) / 16f);

                    if (Main.tile[x, y] == null)
                    {
                        Main.tile[x, y] = new Tile();
                    }
                    Tile thistile = (Tile)Main.tile[ x,  y].Clone();
                    List<Point> temp = new List<Point>();
                    temp.AddRange(Mod.polygons.activeLine);
                    temp.AddRange(Mod.polygons.activeFill);
                    if (DeleteTile.Value && thistile.active)
                    {

                        OnRemoveTilesCommand(temp, thistile.type);
                    }
                    if (DeleteWall.Value && thistile.wall > 0)
                    {

                        OnRemoveWallsCommand(temp, thistile.wall);

                    }

                    temp.Clear();



                }




            }
        }
        public void OnRemoveTilesCommand(List<Point> temp, int thisTILE)
        {
             for (int g = 0; g < temp.Count; g++)
            {
                if (Main.tile[temp[g].X, temp[g].Y] == null)
                {
                    Main.tile[temp[g].X, temp[g].Y] = new Tile();
                }
                Tile tile = (Tile)Main.tile[temp[g].X, temp[g].Y].Clone();
                if (((tile.type == thisTILE) || DeleteAll.Value))
                {
                    if (DeleteTile.Value || DeleteAll.Value)
                    {
                        WorldGenReflect.KillTile(temp[g].X, temp[g].Y, false, false, true);
                        if (Main.netMode == 1)
                        {
                            SendPackets.SendData(117, -1, -1, "", 0, (float)temp[g].X, (float)temp[g].Y, 0f, 0);
                        }
                    }
                } 
            }
        }
        public void OnRemoveWallsCommand(List<Point> temp, int thisWALL)
        {
            for (int g = 0; g < temp.Count; g++)
            {
                if (Main.tile[temp[g].X, temp[g].Y] == null)
                {
                    Main.tile[temp[g].X, temp[g].Y] = new Tile();
                }
                Tile tile = (Tile)Main.tile[temp[g].X, temp[g].Y].Clone();
                if ((tile.wall == thisWALL || DeleteAll.Value))
                {
                    if (DeleteWall.Value || DeleteAll.Value)
                    {
                        WorldGenReflect.KillWall(temp[g].X, temp[g].Y, false);
                        if (Main.netMode == 1)
                        {
                            SendPackets.SendTileSquare(-1, (int)temp[g].X, (int)temp[g].Y, 1);
                        }
                    }
                }
            }
        }



 
 
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
       
            if (this.Visible)
            {  Mod.polygons.fill = true;
                if (firsttime)
                {
                    Mod.polygons.canfill = true;
                    firsttime = false;
                }
                Mod.polygons.Mousewheelrotate();
                Mod.shapeselection(true);
                modify();

            }
        }





        public XG_ButtonLabelBool DeleteTile { get; set; }

        public XG_ButtonLabelBool DeleteWall { get; set; }

        public XG_ButtonLabelBool DeleteAll { get; set; }

        public bool firsttime { get { return first; } set { first = value; } }

    }

    #endregion

    #region Replace

    public class Replace : XG_TabPage
    {
 


        public Mod Game;
        private bool first = true;
 
        public XGTextBox TextBox1 { get; protected set; }
        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }
        public XGTree Tree { get; protected set; }

        public Replace(Mod game, Rectangle rect)
            : base(rect, "Replace")
        {
            Game = game;

            // Add controls to the tab page


            Children.Add(new XG_Label(new Rectangle(10, 25, 200, 20), "click on tile/wall to replace with custom "));
 

            ReplaceTile = new XG_ButtonLabelBool(new Rectangle(10, 60, 100, 60), "Replace Tile", null, true);
            Children.Add(ReplaceTile);
            ReplaceWall = new XG_ButtonLabelBool(new Rectangle(120, 60, 100, 60), "Replace Wall", null, true);
            Children.Add(ReplaceWall);
            ReplaceAll = new XG_ButtonLabelBool(new Rectangle(230, 60, 100, 60), "Replace All", null, true);
            Children.Add(ReplaceAll);
 

        }
 
    
 


        public void modify()
        {
            if (Mod.stopVectoring)
            {
                if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && !Main.player[Main.myPlayer].mouseInterface && !Mod.polygons.onwindow)
                {
                    int x = (int)(((float)Main.mouseState.X + Main.screenPosition.X) / 16f);
                    int y = (int)(((float)Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                    if (Main.tile[x, y] == null)
                    {
                        Main.tile[x, y] = new Tile();
                    }
                    Tile mytile = (Tile)Main.tile[(int)x, (int)y].Clone();
                    List<Point> temp = new List<Point>();
                    temp.AddRange(Mod.polygons.activeLine);
                    temp.AddRange(Mod.polygons.activeFill);
                    if (ReplaceTile.Value && mytile.active)
                    {
                        OnReplaceTilesCommand(temp, mytile.type);
                    }
                    if (ReplaceWall.Value && mytile.wall > 0)
                    {
                        OnReplaceWallsCommand(temp, mytile.wall);
                    }
                    temp.Clear();
                }
            }
        }
        public void OnReplaceTilesCommand(List<Point> temp, int thisTILE)
        {

            int createTile = Mod.tileitem.createTile;
            int placeStyle = Mod.tileitem.placeStyle;
            if (createTile < 0 || createTile > Main.maxTileSets || !Main.tileSolid[createTile]) { return; }

            for (int g = 0; g < temp.Count; g++)
            {
                if (Main.tile[temp[g].X, temp[g].Y] == null)
                {
                    Main.tile[temp[g].X, temp[g].Y] = new Tile();
                }

                Tile tile = (Tile)Main.tile[temp[g].X, temp[g].Y].Clone();
                if (((tile.type == thisTILE) || ReplaceAll.Value))
                {

                    if (tile.active && Main.tileSolid[tile.type])
                    {


                        if (WorldGenReflect.PlaceTile(temp[g].X, temp[g].Y, createTile, false, true, Main.player[Main.myPlayer].whoAmi, placeStyle))
                        {

                            if (Main.netMode == 1)
                            {
                                SendPackets.SendTileSquare(-1, (int)temp[g].X, (int)temp[g].Y, 1);
                            }
                        }

                    }
                }
            }
        }
        public void OnReplaceWallsCommand(List<Point> temp, int thisWALL)
        {
            int wall = Mod.wallitem.createWall;
            if (wall < 0 || wall > Main.maxWallTypes) { return; }
            for (int g = 0; g < temp.Count; g++)
            {
                if (Main.tile[temp[g].X, temp[g].Y] == null)
                {
                    Main.tile[temp[g].X, temp[g].Y] = new Tile();
                }
                Tile tile = (Tile)Main.tile[temp[g].X, temp[g].Y].Clone();
                if ((tile.wall == thisWALL || ReplaceAll.Value))
                {
                     
                        if (tile.wall > 0 && (byte)Mod.wallitem.createWall > 0)
                        {
                            Main.tile[temp[g].X, temp[g].Y].wall = (byte)wall;
                            Main.PlaySound(0, temp[g].X * 16, temp[g].Y * 16, 1);

                            if (Main.netMode == 1)
                            {
                                SendPackets.SendTileSquare(-1, (int)temp[g].X, (int)temp[g].Y, 1);
                            }
                        }
                     
                }
            }
        }




        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        
            if (this.Visible)
            { Mod.polygons.fill = true;
                if (firsttime)
                {
                    Mod.polygons.canfill = true;
                    firsttime = false;
                }
                Mod.polygons.Mousewheelrotate();
                Mod.shapeselection(true);
                modify();
            }
        }





        public XG_ButtonLabelBool ReplaceTile { get; set; }

        public XG_ButtonLabelBool ReplaceWall { get; set; }

        public XG_ButtonLabelBool ReplaceAll { get; set; }
        public bool firsttime { get { return first; } set { first = value; } }

    }

    #endregion

    #region Liquids



    public class Liquids : XG_TabPage
    {
        public Mod Game;
        private bool first=true;
       
        public XGTextBox TextBox1 { get; protected set; }
        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }
        public XGTree Tree { get; protected set; }

        public Liquids(Mod game, Rectangle rect)
            : base(rect, "Liquids")
        {
            Game = game;

            // Add controls to the tab page

            Children.Add(new XG_Label(new Rectangle(10, 25, 200, 20), "Adds or remove liquids")); // leave default Left/VCenter alignment

 


          
            Children.Add(new XG_Button(new Rectangle(10, 70, 100, 20), "Add lava", this.AddLava_Clicked));

            Children.Add(new XG_Button(new Rectangle(120, 70, 100, 20), "Add water", this.Addwater_Clicked));
            Children.Add(new XG_Button(new Rectangle(230, 70, 100, 20), "Remove All", this.RemoveAll_Clicked));
 
 
            //  Children.Add(TextBox1);
    
        }
 
  
        void AddLava_Clicked(XG_Control sender)
        {
            modify(1);
        }


        void Addwater_Clicked(XG_Control sender)
        {
            modify(2);
        }
        void RemoveAll_Clicked(XG_Control sender)
        {
            modify(3);
        }
     
 
        public void modify(int action)
        {
            if (Mod.stopVectoring)
            {
                
                    List<Point> temp = new List<Point>();
                    temp.AddRange(Mod.polygons.activeLine);
                    temp.AddRange(Mod.polygons.activeFill);

                    for (int g = 0; g < temp.Count; g++)
                    {


                        if (!Main.tile[temp[g].X, temp[g].Y].active)
                        {
                            if (action == 3 && Main.tile[temp[g].X, temp[g].Y].liquid > 0)
                            {
                                Main.tile[temp[g].X, temp[g].Y].lava = false;
                                Main.tile[temp[g].X, temp[g].Y].liquid = 0;
                            }
                            if (action == 2)
                            {
                                if (Main.tile[temp[g].X, temp[g].Y].liquid == 0)
                                {
                                    Main.tile[temp[g].X, temp[g].Y].liquid = (byte)255;
                                }
                                if (Main.tile[temp[g].X, temp[g].Y].lava)
                                {
                                    Main.tile[temp[g].X, temp[g].Y].lava = false;
                                }

                            }
                            if (action == 1)
                            {
                                if (Main.tile[temp[g].X, temp[g].Y].liquid == 0)
                                {
                                    Main.tile[temp[g].X, temp[g].Y].liquid = (byte)255;
                                }
                                if (!Main.tile[temp[g].X, temp[g].Y].lava)
                                {
                                    Main.tile[temp[g].X, temp[g].Y].lava = true;
                                }
                            }

                            if (Main.netMode == 1)
                            {
                                SendPackets.SendData(148, -1, -1, "", temp[g].X, (float)temp[g].Y, 0f, 0f, 0);
                            }
                        }
                    }

                    temp.Clear();
              
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        

                
 
            if (this.Visible)
            {
                Mod.polygons.fill = true;
                if (firsttime)
                {
                    Mod.polygons.canfill = true;
                    firsttime = false;
                }
                Mod.polygons.Mousewheelrotate();
                Mod.shapeselection(true);

          


            }


        }


        public XG_PlusMinus Size { get; set; }


        public bool firsttime { get { return first; } set { first = value; } }


       
    }

    #endregion

    #endregion

    #region npcs

    public class Npcs : XG_TabPage
    {
        public Mod Game;
        public XGTextBox TextBox1 { get; protected set; }
        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }
        public XGTree Tree { get; protected set; }
 
        public static string NPCNAME;
 

        

 


        public Npcs(Mod game, Rectangle rect)
            : base(rect, "Npcs")
        {
            Game = game;

            // Add controls to the tab page

            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control
            pageRect.X = pageRect.Y = 0;
            pageRect.Height = 148;
            pageRect.Width = (pageRect.Width / 2) - 1;
            XGTabControl tab = new XGTabControl(pageRect);
            Children.Add(tab); // add the tab control to our child control list


            onclick = new Npconclick(game, pageRect);
            tab.Children.Add(onclick);

            spawn = new Npcspawn(game, pageRect);
            tab.Children.Add(spawn);



            Tree = new XGTree(new Rectangle(210, 5, 205, 135), TreeItemClicked);
            Children.Add(Tree);


            XGTreeItem MobsFamilies = new XGTreeItem(Mod.NpcD.MobsFamilies.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(MobsFamilies);
            for (int i = 0; i < Mod.NpcD.MobsFamilies.Sections.Length; i++) // add sections
            {
                XGTreeItem MobsFamilies_Sections = new XGTreeItem(Mod.NpcD.MobsFamilies.Sections[i], Main.inventoryBackTexture);
                MobsFamilies.Items.Add(MobsFamilies_Sections);
                for (int n = 0; n < Mod.NpcD.MobsFamilies.SectionNpcItems[i].Length; n++) // add section items
                {
                    NPC npc = new NPC();
                    npc.SetDefaults(Mod.NpcD.MobsFamilies.SectionNpcItems[i][n]);
                    XGTreeItem MobsFamilies_Section_items = new XGTreeItem(Mod.NpcD.MobsFamilies.SectionNpcItems[i][n], Main.npcTexture[npc.type]);
                    MobsFamilies_Sections.Items.Add(MobsFamilies_Section_items);
                }
            }


            XGTreeItem AtoZ = new XGTreeItem(Mod.NpcD.AtoZ.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(AtoZ);
            for (int i = 0; i < Mod.NpcD.AtoZ.Sections.Length; i++) // add sections
            {
                XGTreeItem AtoZ_Sections = new XGTreeItem(Mod.NpcD.AtoZ.Sections[i], Main.inventoryBackTexture);
                AtoZ.Items.Add(AtoZ_Sections);
                for (int n = 0; n < Mod.NpcD.AtoZ.SectionNpcItems[i].Length; n++) // add section items
                {
                    NPC npc = new NPC();
                    npc.SetDefaults(Mod.NpcD.AtoZ.SectionNpcItems[i][n]);
                    XGTreeItem MobsFamilies_Section_items = new XGTreeItem(Mod.NpcD.AtoZ.SectionNpcItems[i][n], Main.npcTexture[npc.type]);
                    AtoZ_Sections.Items.Add(MobsFamilies_Section_items);
                }
            }



            XGTreeItem ALL = new XGTreeItem("ALL", Main.inventoryBackTexture);
            Tree.Items.Add(ALL);
            for (int i = 0; i <Main.npcName.Length; i++) // add sections
            {
         
       
                    NPC npc = new NPC();
                    npc.SetDefaults(Main.npcName[i]);
                    XGTreeItem alll = new XGTreeItem(npc.name, Main.npcTexture[npc.type]);
                    ALL.Items.Add(alll);
 
            }


            //Tree.ExpandAll();
        }
        void TreeItemClicked(XGTreeItem name)
        {
            Mod.NPCNAME = name.Value.ToString();
        }
        // Button click event handler

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        
        }
    


        public Npconclick onclick { get; set; }

        public Npcspawn spawn { get; set; }
    }





    #region npctabs


    public class Npconclick: XG_TabPage
    {
        public Mod Game;
        private int selSize;
 
        private bool mouseLeftRelease;
        public Npconclick(Mod game, Rectangle rect)
            : base(rect, "onclick")
        {
            Game = game;

            // Add controls to the tab page
            Count = new XG_PlusMinus(new Rectangle(10, -10, 100, 60), "Count", this.Size_Clicked, 1f, true, true, 0, 200);
            Children.Add(Count);
            GodFinger = new XG_ButtonLabelBool(new Rectangle(100, 30, 100, 60), "Hurt", this.Godfinger_Clicked, true);
            Children.Add(GodFinger);


            OnClick = new XG_ButtonLabelBool(new Rectangle(10, 30, 100, 60), "Spawn", this.OnClick_Clicked, true);
            Children.Add(OnClick);
        }
        void Godfinger_Clicked(XG_Control sender)
        {
            if (GodFinger.Value)
            {
  
                OnClick.Value = false;
            }
        }

        void Size_Clicked(XG_Control sender)
        {


            selSize = (int)Count.Value;

        }

        void OnClick_Clicked(XG_Control sender)
        {


            if (OnClick.Value)
            {
                GodFinger.Value = false;
               
            }
        }


   
        public void clickupdate()
        {
            if (mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && !Mod.polygons.onwindow)
            {
                mouseLeftRelease = false;
                int x = (int)Decimal.Truncate((int)(((float)Main.mouseState.X + Main.screenPosition.X)));
                int y = (int)Decimal.Truncate((int)(((float)Main.mouseState.Y + Main.screenPosition.Y)));
                if (GodFinger.Value)
                {
                    OnNpcHurt(x, y);
                }
        
                if (OnClick.Value)
                {

                    sendnpcsingle(x, y, selSize);

                } 
        
            }
            if (Main.mouseState.LeftButton == ButtonState.Released) { mouseLeftRelease = true; }

        }
        public static void OnNpcHurt(int x, int y)
        {
            Rectangle rectangle = new Rectangle((int)x, (int)y, 32, 32);
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active)
                {
                    Rectangle value = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                    if (rectangle.Intersects(value))
                    {
                        int me = Main.player[Main.myPlayer].whoAmi;
                        Main.npc[i].StrikeNPC(9999, 0f, 0, false);
                        if (Main.netMode == 1)
                        {
                            SendPackets.SendData(128, -1, -1, "", i, 9999f, 0f, 0f, 0);
                        }
                    }
                }
            }
        }
        public static void sendnpcsingle(int x, int y, int count)
        {
            if (Main.netMode == 1)
            {
                SendPackets.SendData((byte)123, Main.player[Main.myPlayer].whoAmi, -1, Mod.NPCNAME, 1, x, y);
            }
            else
            {

                for (int i = 0; i < count; i++)
                {
                    int index = NPC.NewNPC((int)x, (int)y, i);
                    Main.npc[index].SetDefaults(Mod.NPCNAME);

                }

            }

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            if (this.Visible)
            {
                selSize = (int)Count.Value;

               
                clickupdate();
            }
        }



        public XG_ButtonLabelBool OnClick { get; set; }

        public XG_ButtonLabelBool GodFinger { get; set; }

        public XG_PlusMinus Count { get; set; }
    }

    public class Npcspawn : XG_TabPage
    {
        public Mod Game;
        public int selSize;
        private bool mouseLeftRelease;
 
        public Npcspawn(Mod game, Rectangle rect)
            : base(rect, "spawn")
        {
            Game = game;

            // Add controls to the tab page
             
             Count = new XG_PlusMinus(new Rectangle(10, -10, 100, 60), "Count", this.Size_Clicked, 1f,true, true,0,200);
            Children.Add(Count);
            SetSpawn = new XG_ButtonLabelBool(new Rectangle(10, 30, 100, 60), "Add Spawn", this.SetSpawn_Clicked, true);
            Children.Add(SetSpawn);
            RemoveSpawn = new XG_ButtonLabelBool(new Rectangle(110, 30, 100, 60), "Del Spawn", this.RemoveSpawn_Clicked, true);
            Children.Add(RemoveSpawn);
            RemoveALL = new XG_Button(new Rectangle(10, 90, 65, 20), "Del All", this.RemoveAllSpawns_Clicked);
            Children.Add(RemoveALL);
            SendGroug = new XG_Button(new Rectangle(100, 90, 65, 20), "Send All", this.SendAllSpawns_Clicked);
            Children.Add(SendGroug);
          




        }
 

        void Size_Clicked(XG_Control sender)
        {


            selSize = (int)Count.Value;

        }

  


        void SetSpawn_Clicked(XG_Control sender)
        {


            if (SetSpawn.Value)
            {
                 
                RemoveSpawn.Value = false;
     
            }


        }


        void RemoveSpawn_Clicked(XG_Control sender)
        {
            if (RemoveSpawn.Value)
            {
               
                SetSpawn.Value = false;
     
            }

        }

        void SendAllSpawns_Clicked(XG_Control sender)
        {
            sendnpcgroup();
        }
        void RemoveAllSpawns_Clicked(XG_Control sender)
        {
            Mod.spawnText.Clear();
        }

        public void clickupdate()
        {
            if (mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && !Mod.polygons.onwindow)
            {
                mouseLeftRelease = false;
                int x = (int)Decimal.Truncate((int)(((float)Main.mouseState.X + Main.screenPosition.X)));
                int y = (int)Decimal.Truncate((int)(((float)Main.mouseState.Y + Main.screenPosition.Y)));
          
                if (RemoveSpawn.Value)
                {
                    onremovespawn(x, y);
                }
           
                if (SetSpawn.Value)
                {
                    if (Mod.NPCNAME != null)
                    {
                    
                        Mod.spawnText.Add(new NPCspawnText((int)(x / 16f), (int)(y / 16f), Mod.NPCNAME, selSize));
                    }
                }

            }
            if (Main.mouseState.LeftButton == ButtonState.Released) { mouseLeftRelease = true; }

        }


        public static void OnNpcHurt(int x, int y)
        {
            Rectangle rectangle = new Rectangle((int)x, (int)y, 32, 32);
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active)
                {
                    Rectangle value = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                    if (rectangle.Intersects(value))
                    {
                        int me = Main.myPlayer;
                        Main.npc[i].StrikeNPC(9999, 0f, 0, false);
                        if (Main.netMode == 1)
                        {
                            SendPackets.SendData(28, -1, -1, "", i, 9999f, 0f, 0f, 0);
                        }
                    }
                }
            }
        }


        public static void onremovespawn(int x, int y)
        {
            Rectangle textloc = new Rectangle((int)(x), (int)(y), 16, 16);

            for (int i = 0; i < Mod.spawnText.Count; i++)
            {
                Rectangle value = new Rectangle((int)(Mod.spawnText[i].X * 16), (int)(Mod.spawnText[i].Y * 16), 16, 16);
                if (value.Intersects(textloc))
                {
                    Mod.spawnText.RemoveAt(i);
                    Main.PlaySound(12, -1, -1, 1);

                }
            }
        }


        public static void sendnpcgroup()
        {
            foreach (NPCspawnText text in Mod.spawnText)
            {
                if (Main.netMode == 1)
                {
                    SendPackets.SendData((byte)123, Main.player[Main.myPlayer].whoAmi, -1, text.NpcName, text.size, text.X * 16, text.Y * 16);

                }
                else
                {

                    for (int i = 0; i < text.size; i++)
                    {
                        int index = NPC.NewNPC((int)text.X * 16, (int)text.Y * 16, i);
                        Main.npc[index].SetDefaults(text.NpcName);

                    }
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            if (this.Visible)
            {
                selSize = (int)Count.Value;

              
                clickupdate();
            }
        }



        public XG_ButtonLabelBool SetSpawn { get; set; }

        public XG_ButtonLabelBool RemoveSpawn { get; set; }

        public XG_Button RemoveALL { get; set; }

        public XG_Button SendGroug { get; set; }

        public XG_PlusMinus Count { get; set; }
    }
    #endregion









    #endregion

    #region brush


    public class Brush : XG_TabPage
    {
        public Mod Game;

        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }


        public Brush(Mod game, Rectangle rect)
            : base(rect, "Brush")
        {
            Game = game;





            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control
            pageRect.X = pageRect.Y = 0;
            pageRect.Height = 148;

            Children.Add(new XG_Button(new Rectangle(350, 5, 60, 20), "Clear", this.Clear_Clicked));

            Children.Add(new XG_Button(new Rectangle(260, 5, 80, 20), "Set shape", this.Set_Clicked));

     
            
            XGTabControl tab = new XGTabControl(pageRect);
            Children.Add(tab); // add the tab control to our child control list


      

            freeStyle = new FreeStyle(game, pageRect);
            tab.Children.Add(freeStyle);

            copyPaste = new CopyPaste(game, pageRect);
            tab.Children.Add(copyPaste);


 



        }
        void Set_Clicked(XG_Control sender)
        {
            Mod.polygons.SetShape();
        }
        void Clear_Clicked(XG_Control sender)
        {

            Mod.polygons.Clear();
        }
 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        
        }







     

        public CopyPaste copyPaste { get; set; }

        public FreeStyle freeStyle { get; set; }

    
 
    }


    #region shapes

    public class Shapes : XG_TabPage
    {
        public Mod Game;
        public int sideCounter = 51;


        public XG_Label Slider1Label { get; protected set; }
        public XGTree Tree { get; protected set; }

        public Shapes(Mod game, Rectangle rect)
            : base(rect, "Shapes")
        {
            Game = game;

            // Add controls to the tab page

         

             
            CircleSides = new XG_PlusMinus(new Rectangle(10, 25, 100, 60), "Circle Sides", null,1f, true, true, 3, 200);
            Children.Add(CircleSides);
 
            Rotation = new XG_PlusMinus(new Rectangle(10, 60, 100, 60), "Rotation", null, 0.0005f, true, true, 0, 0.401f,0.01f, "F3");
            Rotation._Value = 0.020f;
            Children.Add(Rotation);


            hollow = new XG_ButtonLabelBool(new Rectangle(200, 20, 100, 60), "hollow", null, true);
            Children.Add(hollow);

            Fill = new XG_ButtonLabelBool(new Rectangle(200, 60, 100, 60), "Fill", Fill_Clicked, true);
            Children.Add(Fill);

          
            tiles = new XG_ButtonLabelBool(new Rectangle(280, 20, 100, 60), "Tiles", null, true);
            Children.Add(tiles);

          
            walls = new XG_ButtonLabelBool(new Rectangle(280, 65, 100, 60), "Walls", null, true);
            Children.Add(walls);

            Children.Add(new XG_Button(new Rectangle(350, 80, 75, 20), "UndoVert", this.undoVert_Clicked));

             Children.Add(new XG_Button(new Rectangle(350, 5, 60, 20), "House", this.Casa_Clicked));
            Build = new XG_Button(new Rectangle(350, 35, 60, 20), "Build", this.Build_Clicked);
            Children.Add(Build);
           



        }

        public void Fill_Clicked(XG_Control sender = null)
        {
            Mod.polygons.fill = Fill.Value;
            if (Mod.polygons.fill)
            {
                Mod.polygons.canfill = true;
                Mod.polygons.fillin(false);
            }
            else { Mod.polygons.activeFill.Clear(); }
        
        }


        #region shapebuttons
        public void undoVert_Clicked(XG_Control sender = null)
        {
            if (Mod.polygons.verTices.Count > 0)
            {
                Mod.polygons.verTices.RemoveAt(Mod.polygons.verTices.Count - 1);
                Mod.polygons.cansetlines=true;
                Mod.polygons.setlines();
                Mod.polygons.canfill = true;
                Mod.polygons.fillin(false);


            }
        }
        public void Build_Clicked(XG_Control sender = null)
        {
            Mod.polygons.SetShape();

            if (this.tiles.Value || this.walls.Value || this.hollow.Value)
            {
                Mod.LastAction = Mod.Copythis();
            
                if (!this.hollow.Value)
                {
                    for (int g = 0; g < Mod.polygons.activeFill.Count; g++)
                    {
                        Mod.placetilesandwalls(Mod.polygons.activeFill[g].X, Mod.polygons.activeFill[g].Y, this.tiles.Value, this.walls.Value);

                    }


                }
                else
                {
                    for (int g = 0; g < Mod.polygons.activeFill.Count; g++)
                    {
                        Mod.placetilesandwalls(Mod.polygons.activeFill[g].X, Mod.polygons.activeFill[g].Y, false, this.walls.Value);

                    }

                }

    for (int g = 0; g < Mod.polygons.activeLine.Count; g++)
                {
                    Mod.placetilesandwalls(Mod.polygons.activeLine[g].X, Mod.polygons.activeLine[g].Y, this.tiles.Value, this.walls.Value);

                }


            }
        }
                
 
        void Casa_Clicked(XG_Control sender)
        {
            Mod.polygons.CreateRectangle(Vector2.Zero, Vector2.Zero);
            Mod.polygons.casa();

        }

        #endregion
  



        public override void Update(GameTime gameTime)
        {
            Mod.polygons.CircleSides = (int)CircleSides.Value;
 
            Mod.polygons.Rotation = Rotation.Value;
            Mod.polygons.fill = Fill.Value;
            if (!Mod.polygons.fill)
            {
                Mod.polygons.activeFill.Clear();

            }
            if (this.Visible)
            {
                Mod.polygons.Mousewheelrotate();
                Mod.shapeselection();
            }
            base.Update(gameTime);
        }





        public XG_Label Text2 { get; set; }
 
        public XG_PlusMinus Rotation { get; set; }


  
	    
        public XG_PlusMinus Radius { get; set; }

        public XG_PlusMinus CircleSides { get; set; }

 
        public XG_ButtonLabelBool tiles { get; set; }

        public XG_ButtonLabelBool walls { get; set; }

        public XG_Button Build { get; set; }
    
public  XG_ButtonLabelBool hollow { get; set; }
public XG_ButtonLabelBool Fill { get; set; }
    }

    #endregion

    public class FreeStyle : XG_TabPage
    {
        public Mod Game;
     
        

        

        public FreeStyle(Mod game, Rectangle rect)
            : base(rect, "FreeStyle")
        {
            Game = game;


            Children.Add(new XG_Label(new Rectangle(10, 5, 200, 20), "Simple tile placement with selection Count:"));

            Selsize = new XG_PlusMinus(new Rectangle(10, 20, 100, 60), "Selsize", button, 1f, false, true, 0, 100);
            Children.Add(Selsize);

          


            tiles = new XG_ButtonLabelBool(new Rectangle(310, 10, 100, 60), "Tiles", null, true);
            Children.Add(tiles);
            walls = new XG_ButtonLabelBool(new Rectangle(310, 60, 100, 60), "Walls", null, true);
            Children.Add(walls);



            Build = new XG_ButtonLabelBool(new Rectangle(230, 10, 100, 60), "Build", button, true);
            Children.Add(Build);

            Destroy = new XG_ButtonLabelBool(new Rectangle(160, 10, 100, 60), "Destroy", button, true);
            Children.Add(Destroy);

            Replace = new XG_ButtonLabelBool(new Rectangle(90, 10, 100, 60), "Replace", button, true);
            Children.Add(Replace);

            All = new XG_ButtonLabelBool(new Rectangle(160, 65, 100, 60), "include All", button, true);
            Children.Add(All);
        }
        void button(XG_Control sender)
        {
            if (sender == Build)
            {
                if (Build.Value)
                {
                    Destroy.Value = false;
                    Replace.Value = false;
                    All.Value = false;
                }
            }
            if (sender == Destroy)
            {
                if (Destroy.Value)
                {
                    Build.Value = false;
                    Replace.Value = false;
                }
            }
            if (sender == Replace)
            {
                if (Replace.Value)
                {
                    Build.Value = false;
                    Destroy.Value = false;
                }
            }
            if (sender == All)
            {
                if (All.Value)
                {
                    Build.Value = false;

                }
            }

        }


        public void brushaddtiles()
        {
            if (Main.gameMenu || Main.playerInventory|| Mod.polygons.onwindow)
            {
                return;
            }


            int x = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
            int y = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
            if (Main.mouseState.LeftButton == ButtonState.Pressed)
            {
                List<Point> temp = new List<Point>();
                for (int i = (int)-Selsize.Value; i <= Selsize.Value; i++)
                {
                    for (int j = (int)Selsize.Value; j >= -Selsize.Value; j--)
                    {

                        if (Main.gameMenu || Main.playerInventory)
                        {
                            return;
                        } if (Main.tile[x + i, y + j] == null)
                        {
                            Main.tile[x + i, y + j] = new Tile();
                        }
                        if (!Destroy.Value && !Replace.Value)
                        {
                            if (Build.Value)
                            {
                                Mod.placetilesandwalls(x + i, y + j,tiles.Value,walls.Value);
                            }
                        }
                        else
                        {
                            temp.Add(new Point(x + i, y + j));
                        }

                    }
                }

                if (Destroy.Value)
                {
                    modifyDelete(temp, x, y);

                }
                if (Replace.Value)
                {
                    modifyReplace(temp, x, y);

                }
              //  temp.Clear();
            }
        }

        public void modifyDelete(List<Point> temp, int x, int y)
        {
         //   if (Mod.stopVectoring)
            {
            // if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && !Main.player[Main.myPlayer].mouseInterface && !Mod.polygons.onwindow)
                {

     

                    if (Main.tile[x, y] == null)
                    {
                        Main.tile[x, y] = new Tile();
                    }
                    Tile thistile = (Tile)Main.tile[x, y].Clone();
                    if ((tiles.Value|| All.Value) && thistile.active)
                    {

                        OnRemoveTilesCommand(temp, thistile.type);
                    }
                    if ((walls.Value || All.Value) && thistile.wall > 0)
                    {

                        OnRemoveWallsCommand(temp, thistile.wall);

                    }

                   // temp.Clear();



                }




            }
        }
        public void OnRemoveTilesCommand(List<Point> temp, int thisTILE)
        {
            for (int g = 0; g < temp.Count; g++)
            {
                if (Main.tile[temp[g].X, temp[g].Y] == null)
                {
                    Main.tile[temp[g].X, temp[g].Y] = new Tile();
                }
                Tile tile = (Tile)Main.tile[temp[g].X, temp[g].Y].Clone();
                if (((tile.type == thisTILE) ||  All.Value))
                {
                    if (tiles.Value ||  All.Value)
                    {
                        WorldGenReflect.KillTile(temp[g].X, temp[g].Y, false, false, true);
                        if (Main.netMode == 1)
                        {
                            SendPackets.SendData(117, -1, -1, "", 0, (float)temp[g].X, (float)temp[g].Y, 0f, 0);
                        }
                    }
                }
            }
        }
        public void OnRemoveWallsCommand(List<Point> temp, int thisWALL)
        {
            for (int g = 0; g < temp.Count; g++)
            {
                if (Main.tile[temp[g].X, temp[g].Y] == null)
                {
                    Main.tile[temp[g].X, temp[g].Y] = new Tile();
                }
                Tile tile = (Tile)Main.tile[temp[g].X, temp[g].Y].Clone();
                if ((tile.wall == thisWALL ||  All.Value))
                {
                    if (Destroy.Value ||  All.Value)
                    {
                        WorldGenReflect.KillWall(temp[g].X, temp[g].Y, false);
                        if (Main.netMode == 1)
                        {
                            SendPackets.SendTileSquare(-1, (int)temp[g].X, (int)temp[g].Y, 1);
                        }
                    }
                }
            }
        }


        public void modifyReplace(List<Point> temp, int x, int y)
        {
           // if (Mod.stopVectoring)
            {
                if(Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && !Main.player[Main.myPlayer].mouseInterface)
                {
            
                    if (Main.tile[x, y] == null)
                    {
                        Main.tile[x, y] = new Tile();
                    }
                    Tile mytile = (Tile)Main.tile[(int)x, (int)y].Clone();
 
                    if ((tiles.Value|| All.Value) && mytile.active)
                    {
                        OnReplaceTilesCommand(temp, mytile.type);
                    }
                    if ((walls.Value|| All.Value) && mytile.wall > 0)
                    {
                        OnReplaceWallsCommand(temp, mytile.wall);
                    }
                  //  temp.Clear();
                }
            }
        }
        public void OnReplaceTilesCommand(List<Point> temp, int thisTILE)
        {

            int createTile = Mod.tileitem.createTile;
            int placeStyle = Mod.tileitem.placeStyle;
            if (createTile < 0 || createTile > Main.maxTileSets || !Main.tileSolid[createTile]) { return; }

            for (int g = 0; g < temp.Count; g++)
            {
                if (Main.tile[temp[g].X, temp[g].Y] == null)
                {
                    Main.tile[temp[g].X, temp[g].Y] = new Tile();
                }

                Tile tile = (Tile)Main.tile[temp[g].X, temp[g].Y].Clone();
                if (((tile.type == thisTILE) ||  All.Value))
                {

                    if (tile.active && Main.tileSolid[tile.type])
                    {


                        if (WorldGenReflect.PlaceTile(temp[g].X, temp[g].Y, createTile, false, true, Main.player[Main.myPlayer].whoAmi, placeStyle))
                        {

                            if (Main.netMode == 1)
                            {
                                SendPackets.SendTileSquare(-1, (int)temp[g].X, (int)temp[g].Y, 1);
                            }
                        }

                    }
                }
            }
        }
        public void OnReplaceWallsCommand(List<Point> temp, int thisWALL)
        {
            int wall = Mod.wallitem.createWall;
            if (wall < 0 || wall > Main.maxWallTypes) { return; }
            for (int g = 0; g < temp.Count; g++)
            {
                if (Main.tile[temp[g].X, temp[g].Y] == null)
                {
                    Main.tile[temp[g].X, temp[g].Y] = new Tile();
                }
                Tile tile = (Tile)Main.tile[temp[g].X, temp[g].Y].Clone();
                if ((tile.wall == thisWALL ||  All.Value))
                {

                    if (tile.wall > 0 && (byte)Mod.wallitem.createWall > 0)
                    {
                        Main.tile[temp[g].X, temp[g].Y].wall = (byte)wall;
                        Main.PlaySound(0, temp[g].X * 16, temp[g].Y * 16, 1);

                        if (Main.netMode == 1)
                        {
                            SendPackets.SendTileSquare(-1, (int)temp[g].X, (int)temp[g].Y, 1);
                        }
                    }

                }
            }
        }










 
       
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

      

          







            #region brush
            if (this.Visible)
            {

             //   Player.tileRangeY = 99999;
              //  Player.tileRangeX = 99999;
                brushaddtiles();

            }
            else
            {
              //  Player.tileRangeY = 5;
              //  Player.tileRangeX = 4;
            }


            #endregion




        }






        public XG_PlusMinus Selsize { get; set; }

        public XG_ButtonLabelBool tiles { get; set; }

        public XG_ButtonLabelBool walls { get; set; }

        public XG_ButtonLabelBool Destroy { get; set; }

        public XG_ButtonLabelBool Build { get; set; }

        public XG_ButtonLabelBool All { get; set; }

        public XG_ButtonLabelBool Replace { get; set; }
    }

    public class CopyPaste : XG_TabPage
    {
        public Mod Game;
      
 

        public CopyPaste(Mod game, Rectangle rect)
            : base(rect, "Copy/Paste")
        {
            Game = game;






            // Add controls to the tab page

            Children.Add(new XG_Label(new Rectangle(10, 5, 200, 20), "copy selection then paste with left click.")); // leave default Left/VCenter alignment
            Children.Add(new XG_Button(new Rectangle(10, 30, 70, 20), "Circle", this.Circle_Clicked));
            Children.Add(new XG_Button(new Rectangle(100, 30, 70, 20), "Triagle", this.Triangle_Clicked));
            Children.Add(new XG_Button(new Rectangle(200, 30, 70, 20), "Square", this.Square_Clicked));
            Children.Add(new XG_Button(new Rectangle(10, 70, 70, 20), "Copy", this.Copybrush_Clicked));
            Children.Add(new XG_Button(new Rectangle(100, 70, 70, 20), "Paste", this.Paste_Clicked));
    





        }
        void Square_Clicked(XG_Control sender)
        {
            Mod.polygons.CreateRectangle();
        }

        void Circle_Clicked(XG_Control sender)
        {
            Mod.polygons.CreateCircle(Vector2.Zero, Mod.polygons.radius, Mod.polygons.CircleSides);
        }
        void Triangle_Clicked(XG_Control sender)
        {
            Mod.polygons.CreateTriangle(Vector2.Zero, Mod.polygons.radius);
        }

        void Paste_Clicked(XG_Control sender)
        {


            Mod.Pastethis();

        }
        void Copybrush_Clicked(XG_Control sender)
        {

            Mod.tilecopies = Mod.Copythis();

            Mod.polygons.canpaste = true;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.Visible)
            {
                Mod.polygons.fill = true;
                if (firsttime)
                {
                  
                    Mod.polygons.canfill = true;
                    firsttime = false;
                }

                Mod.shapeselection(true);
                Mod.setnewpos();
                if (Mod.polygons.canpaste)
                {
                    if (Mod.stopVectoring && !Mod.polygons.onwindow)
                    {
                        if (Main.mouseState.LeftButton == ButtonState.Pressed)
                        {
                            Mod.Pastethis();
                        }
                    }
                }
            }
        }




        public XG_Label Text2 { get; set; }





        public bool firsttime { get; set; }

        
    }




    #endregion


    #region item settings

    public class ItemsSettings : XG_TabPage
    {
        public Mod Game;
        public int sideCounter = 51;
        public XG_Label Text1 { get; protected set; }
        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }
        public XGTree Tree { get; protected set; }

        public ItemsSettings(Mod game, Rectangle rect)
            : base(rect, "Items")
        {
            Game = game;
            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control
            pageRect.X = pageRect.Y = 0;
            pageRect.Height = 148;
            XGTabControl tab = new XGTabControl(pageRect);
            Children.Add(tab); // add the tab control to our child control list
 
            tab.Children.Add(new Item_PP(game, pageRect));
            tab.Children.Add(new Item_TM(game, pageRect));
            tab.Children.Add(new Item_EP(game, pageRect));
            tab.Children.Add(new Item_LP(game, pageRect));
            tab.Children.Add(new Item_DP(game, pageRect));
            tab.Children.Add(new Item_UT(game, pageRect));
            tab.Children.Add(new Item_SS(game, pageRect));
            tab.Children.Add(new Item_SG(game, pageRect));

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }





        public XG_Label Text2 { get; set; }
    }




    public class Item_PP : XG_TabPage
    {
        public Mod Game;


        public Item_PP(Mod game, Rectangle rect)
            : base(rect, "P.P.")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "projectile properties"));

            // string text = "";
            //text = "Shoot";
            //Shoot = new XG_PlusMinus(new Rectangle(10, 10, 100, 60), text, this.Shoot_Clicked);
            //Children.Add(Shoot);

            Tree = new XGTree(new Rectangle(250, 0, 170, 115), TreeItemClicked);
            Children.Add(Tree);


            XGTreeItem Shoot = new XGTreeItem("Shoot", Main.inventoryBackTexture);
            Tree.Items.Add(Shoot);
            for (int n = 0; n < Main.maxProjectileTypes; n++) // add section items
            {
                if (n != 0)
                {
                    Projectile projectile = new Projectile();
                    projectile.SetDefaults(n);
                    XGTreeItem Shoot_Section_items = new XGTreeItem(projectile.name, Main.projectileTexture[projectile.type]);
                    Tree.Items.Add(Shoot_Section_items);
                }
            }

            string text = "";
            text = "ammo";
            ammo = new XG_PlusMinus(new Rectangle(10, 60, 100, 60), text, this.ammo_Clicked, 1f, false, false, 0, 50, 0, "F0");
            Children.Add(ammo);

            text = "useammo";
            useAmmo = new XG_PlusMinus(new Rectangle(10, 10, 100, 60), text, this.useAmmo_Clicked, 1f, false, false, 0, 50, 0, "F0");
            Children.Add(useAmmo);

            text = "shootspeed";
            shootSpeed = new XG_PlusMinus(new Rectangle(100, 60, 100, 60), text, this.shootSpeed_Clicked, 0.1f, true, false, 0, 100f, 0, "F1");
            Children.Add(shootSpeed);


            text = "knockBack";
            knockBack = new XG_PlusMinus(new Rectangle(100, 10, 100, 60), text, this.knockBack_Clicked, 1f, false, false, 0, 500, 0, "F0");
            Children.Add(knockBack);


            text = "channel";
            channel = new XG_ButtonLabelBool(new Rectangle(200, 10, 100, 60), text, this.channel_Clicked);
            Children.Add(channel);


        }
        //Button click event handler
        void TreeItemClicked(XGTreeItem name)
        {
            for (int n = 0; n < Main.maxProjectileTypes; n++) // add section items
            {
                Projectile projectile = new Projectile();
                projectile.SetDefaults(n);
                if (projectile.name == name.Value.ToString())
                {

                    Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].shoot = n;
                    break;
                }
            }
        }

        void channel_Clicked(XG_Control sender)
        {

            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].channel = channel.Value;

        }
        void knockBack_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].knockBack = knockBack.Value;

        }
     
        void useAmmo_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useAmmo =(int)  useAmmo.Value;

        }
        void shootSpeed_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].shootSpeed = shootSpeed.Value;

        }
        void ammo_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].ammo = (int)ammo.Value;

        } 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

           ammo.DisplayValue = Convert.ToSingle(Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].ammo);
           useAmmo.DisplayValue = Convert.ToSingle(Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useAmmo);
            shootSpeed.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].shootSpeed;
            knockBack.DisplayValue = Convert.ToSingle(Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].knockBack);
            channel.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].channel;
        }


        public XGTextBox TextBox1 { get; set; }

 

        public XG_PlusMinus scale { get; set; }

        public XG_PlusMinus ammo { get; set; }

        public XG_PlusMinus useAmmo { get; set; }
 

        public XG_PlusMinus knockBack { get; set; }

        public XG_ButtonLabelBool channel { get; set; }

        public XG_PlusMinus shootSpeed { get; set; }

        public XGTree Tree { get; set; }
    }


    public class Item_TM : XG_TabPage
    {
        public Mod Game;


        public Item_TM(Mod game, Rectangle rect)
            : base(rect, "T.M.")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "tile modification"));

            string text = "";
            text = "tileBoost";
            tileBoost = new XG_PlusMinus(new Rectangle(10, 10, 100, 60), text, this.tileBoost_Clicked, 1f, true, false, 0, 500, 0, "F0" );
            Children.Add(tileBoost);

            text = "pick";
            pick = new XG_PlusMinus(new Rectangle(10, 60, 100, 60), text, this.pick_Clicked, 1f, true, false, 0, 500, 0, "F0");
            Children.Add(pick);

            text = "hammer";
            hammer = new XG_PlusMinus(new Rectangle(100, 10, 100, 60), text, this.hammer_Clicked, 1f, true, false,0, 500, 0, "F0" );
            Children.Add(hammer);

            text = "axe";
            axe = new XG_PlusMinus(new Rectangle(100, 60, 100, 60), text, this.axe_Clicked, 1f, true, false, 0, 500, 0, "F0" );
            Children.Add(axe);

            text = "createTile";
            createTile = new XG_PlusMinus(new Rectangle(10 + 180, 10, 100, 60), text, this.createTile_Clicked, 1f, false, false ,0,(float)Main.maxTileSets,0,"F0",true);
            Children.Add(createTile);

            text = "createWall";
            createWall = new XG_PlusMinus(new Rectangle(10 + 180, 60, 100, 60), text, this.createWall_Clicked, 1f, false, false,0,(float)Main.maxWallTypes,0,"F0",true );
            Children.Add(createWall);

            text = "placeStyle";
            placeStyle = new XG_PlusMinus(new Rectangle(100 + 180, 10, 100, 60), text, this.placeStyle_Clicked, 1f, false, false,0,6,0,"F0",true);
            Children.Add(placeStyle);

  






        }
        //Button click event handler

 
        void tileBoost_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].tileBoost = (int) tileBoost.Value;

        }
        void hammer_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].hammer = (int) hammer.Value;

        }
        void axe_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].axe = (int)axe.Value;

        }
        void pick_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].pick = (int)pick.Value;

        }




        void createTile_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].createTile = (int)createTile.Value;

        }
        void createWall_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].createWall = (int)createWall.Value;

        }
        void placeStyle_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].placeStyle = (int)placeStyle.Value;

        }




        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            tileBoost.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].tileBoost;
            pick.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].pick;
            hammer.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].hammer;
            axe.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].axe;
            createTile.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].createTile;
            createWall.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].createWall;
            placeStyle.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].placeStyle;
        }


        public XGTextBox TextBox1 { get; set; }

        public XG_PlusMinus tileBoost { get; set; }

        public XG_PlusMinus _scale { get; set; }

        public XG_PlusMinus pick { get; set; }

        public XG_PlusMinus hammer { get; set; }

        public XG_PlusMinus axe { get; set; }

        public XG_PlusMinus createTile { get; set; }

        public XG_PlusMinus createWall { get; set; }

        public XG_PlusMinus placeStyle { get; set; }
    }


    public class Item_EP : XG_TabPage
    {
        public Mod Game;


        public Item_EP(Mod game, Rectangle rect)
            : base(rect, "E.P.")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "Economy Properties"));

            string text = "";
            text = "buy";
            buy = new XG_ButtonLabelBool(new Rectangle(10, 10, 100, 60), text, this.buy_Clicked);
            Children.Add(buy);

            text = "buyOnce";
            buyOnce = new XG_ButtonLabelBool(new Rectangle(10, 60, 100, 60), text, this.buyOnce_Clicked);
            Children.Add(buyOnce);

            text = "value";
            value = new XG_PlusMinus(new Rectangle(100, 10, 100, 60), text, this.value_Clicked, 1f, true );
            Children.Add(value);

            text = "stack";
            stack = new XG_PlusMinus(new Rectangle(100, 60, 100, 60), text, this.stack_Clicked, 1f, true);
            Children.Add(stack);

            text = "maxStack";
            maxStack = new XG_PlusMinus(new Rectangle(10 + 200, 10, 100, 60), text, this.maxStack_Clicked, 1f, true);
            Children.Add(maxStack);

            text = "consumable";
            consumable = new XG_ButtonLabelBool(new Rectangle(10 + 200, 60, 100, 60), text, this.consumable_Clicked);
            Children.Add(consumable);









        }
        //Button click event handler


        void buy_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].buy = buy.Value;

        }
        void buyOnce_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].buyOnce = buyOnce.Value;

        }
        void value_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].value = (int)value.Value;

        }
        void stack_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].stack = (int)stack.Value;

        }
        void maxStack_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].maxStack = (int)maxStack.Value;

        }
        void consumable_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].consumable = consumable.Value;

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            stack.Max = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].maxStack;
             
            buy.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].buy;
            buyOnce.DisplayValue =  Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].buyOnce;
            value.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].value;
            stack.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].stack;
            maxStack.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].maxStack;
            consumable.DisplayValue =  Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].consumable;
        }



        public XG_ButtonLabelBool buy { get; set; }

        public XG_ButtonLabelBool buyOnce { get; set; }

        public XG_PlusMinus value { get; set; }

        public XG_PlusMinus stack { get; set; }

        public XG_PlusMinus maxStack { get; set; }

        public XG_ButtonLabelBool consumable { get; set; }
    }


    public class Item_LP : XG_TabPage
    {
        public Mod Game;


        public Item_LP(Mod game, Rectangle rect)
            : base(rect, "L.P.")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "Liquid Properties"));

            string text = "";
            text = "lavaWet";
            _lavaWet = new XG_ButtonLabelBool(new Rectangle(10, 10, 100, 60), text, this.lavaWet_Clicked);
            Children.Add(_lavaWet);

            text = "noWet";
            _noWet = new XG_ButtonLabelBool(new Rectangle(10, 60, 100, 60), text, this.noWet_Clicked);
            Children.Add(_noWet);

            text = "wet";
            _wet = new XG_ButtonLabelBool(new Rectangle(100, 10, 100, 60), text, this.wet_Clicked);
            Children.Add(_wet);

            text = "wetCount";
            _wetCount = new XG_PlusMinus(new Rectangle(100, 60, 100, 60), text, this.wetCount_Clicked, 1f, true );
            Children.Add(_wetCount);
        }
        //Button click event handler


        void lavaWet_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].lavaWet = _lavaWet.Value;

        }
        void noWet_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].noWet = _noWet.Value;

        }
        void wet_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].wet = _wet.Value;

        }
        void wetCount_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].wetCount = (byte)_wetCount.Value;

        }
 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _lavaWet.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].lavaWet;
            _noWet.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].noWet;
            _wet.DisplayValue =  Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].wet;
            _wetCount.DisplayValue =  Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].wetCount;
 
        }




        public XG_ButtonLabelBool _lavaWet { get; set; }

        public XG_ButtonLabelBool _noWet { get; set; }

        public XG_ButtonLabelBool _wet { get; set; }

        public XG_PlusMinus _wetCount { get; set; }
    }


    public class Item_DP : XG_TabPage
    {
        public Mod Game;


        public Item_DP(Mod game, Rectangle rect)
            : base(rect, "D.P.")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "Default Property"));

            string text = "";
            text = "useTurn";
            magic = new XG_ButtonLabelBool(new Rectangle(10, 10, 100, 60), text, this.magic_Clicked);
            Children.Add(magic);

            text = "autoReuse";
            material = new XG_ButtonLabelBool(new Rectangle(10, 60, 100, 60), text, this.material_Clicked);
            Children.Add(material);

            text = "noMelee";
            noMelee = new XG_ButtonLabelBool(new Rectangle(100, 10, 100, 60), text, this.noMelee_Clicked);
            Children.Add(noMelee);

            text = "melee";
            melee = new XG_ButtonLabelBool(new Rectangle(100, 60, 100, 60), text, this.melee_Clicked);
            Children.Add(melee);




            text = "mech";
            mech = new XG_ButtonLabelBool(new Rectangle(10 + 200, 10, 100, 60), text, this.mech_Clicked);
            Children.Add(mech);

            text = "ranged";
            ranged = new XG_ButtonLabelBool(new Rectangle(10 + 200, 60, 100, 60), text, this.ranged_Clicked);
            Children.Add(ranged);

            text = "potion";
            potion = new XG_ButtonLabelBool(new Rectangle(100 + 200, 10, 100, 60), text, this.potion_Clicked);
            Children.Add(potion);




            text = "rare";
            rare = new XG_PlusMinus(new Rectangle(100 + 200, 60, 100, 60), text, this.rare_Clicked);
            Children.Add(rare);






        }
        //Button click event handler
        void rare_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].rare = (int)rare.Value;

        }

        void magic_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].magic = magic.Value;

        }
        void noMelee_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].noMelee = noMelee.Value;

        }
        void melee_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].melee = melee.Value;

        }
        void material_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].material = material.Value;

        }




        void mech_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mech = mech.Value;

        }
        void ranged_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].ranged = ranged.Value;

        }
        void potion_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].potion = potion.Value;

        }




        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            magic.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].magic;
            material.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].material;
            noMelee.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].noMelee;
            melee.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].melee;
            mech.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mech;
            ranged.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].ranged;
            potion.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].potion;
            rare.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].rare;
     
        
        
        }


        public XGTextBox TextBox1 { get; set; }



        public XG_ButtonLabelBool magic { get; set; }

        public XG_ButtonLabelBool material { get; set; }

        public XG_ButtonLabelBool noMelee { get; set; }

        public XG_ButtonLabelBool melee { get; set; }

        public XG_ButtonLabelBool mech { get; set; }

        public XG_ButtonLabelBool ranged { get; set; }

        public XG_ButtonLabelBool potion { get; set; }

        public XG_PlusMinus rare { get; set; }
    }


    public class Item_UT : XG_TabPage
    {
        public Mod Game;


        public Item_UT(Mod game, Rectangle rect)
            : base(rect, "U.T.")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "Usage and Time"));

            string text = "";
            text = "useTurn";
            useTurn = new XG_ButtonLabelBool(new Rectangle(10, 10, 100, 60), text, this.useTurn_Clicked);
            Children.Add(useTurn);

            text = "autoReuse";
            autoReuse = new XG_ButtonLabelBool(new Rectangle(10, 60, 100, 60), text, this.autoReuse_Clicked);
            Children.Add(autoReuse);

            text = "spawnTime";
            spawnTime = new XG_PlusMinus(new Rectangle(100, 10, 100, 60), text, this.spawnTime_Clicked);
            Children.Add(spawnTime);

            text = "useTime";
            useTime = new XG_PlusMinus(new Rectangle(100, 60, 100, 60), text, this.useTime_Clicked,1,false,false,0,100,0,"F0",true);
            Children.Add(useTime);

            text = "useStyle";
            useStyle = new XG_PlusMinus(new Rectangle(10 + 200, 10, 100, 60), text, this.useStyle_Clicked);
            Children.Add(useStyle);

            text = "holdStyle";
            holdStyle = new XG_PlusMinus(new Rectangle(10 + 200, 60, 100, 60), text, this.holdStyle_Clicked);
            Children.Add(holdStyle);

            text = "release";
            release = new XG_PlusMinus(new Rectangle(100 + 200, 10, 100, 60), text, this.release_Clicked);
            Children.Add(release);




            text = "reuseDelay";
            reuseDelay = new XG_PlusMinus(new Rectangle(100 + 200, 60, 100, 60), text, this.reuseDelay_Clicked);
            Children.Add(reuseDelay);






        }
        //Button click event handler
        void reuseDelay_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].reuseDelay = (int)reuseDelay.Value;

        }
        void useTurn_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useTurn = useTurn.Value;

        }
        void spawnTime_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].spawnTime = (int)spawnTime.Value;

        }
        void useTime_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useTime = (int)useTime.Value;

        }
        void autoReuse_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].autoReuse = autoReuse.Value;

        }
        void useStyle_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useStyle = (int)useStyle.Value;

        }
        void holdStyle_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].holdStyle = (int)holdStyle.Value;

        }
        void release_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].release = (int)release.Value;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            useTurn.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useTurn;
            autoReuse.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].autoReuse;
            spawnTime.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].spawnTime;
            useTime.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useTime;
            autoReuse.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].autoReuse;
            useStyle.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useStyle;
            holdStyle.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].holdStyle;
            release.DisplayValue = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].release;



        }


        public XGTextBox TextBox1 { get; set; }



        public XG_ButtonLabelBool useTurn { get; set; }

        public XG_ButtonLabelBool autoReuse { get; set; }



        public XG_PlusMinus spawnTime { get; set; }



        public XG_PlusMinus useTime { get; set; }

        public XG_PlusMinus useStyle { get; set; }

        public XG_PlusMinus holdStyle { get; set; }

        public XG_PlusMinus release { get; set; }

        public XG_PlusMinus reuseDelay { get; set; }
    }


    public class Item_SS : XG_TabPage
    {
        public Mod Game;


        public Item_SS(Mod game, Rectangle rect)
            : base(rect, "S.S.")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "Stats Settings"));

            string text = "";
            text = "crit";
            crit = new XG_PlusMinus(new Rectangle(10, 10, 100, 60), text, this.crit_Clicked, 1f, true);
            Children.Add(crit);

            text = "damage";
            damage = new XG_PlusMinus(new Rectangle(10, 60, 100, 60), text, this.damage_Clicked, 1f, true);
            Children.Add(damage);

            text = "defense";
            defense = new XG_PlusMinus(new Rectangle(100, 10, 100, 60), text, this.defense_Clicked, 1f, true);
            Children.Add(defense);

            text = "healLife";
            healLife = new XG_PlusMinus(new Rectangle(100, 60, 100, 60), text, this.healLife_Clicked, 1f, true);
            Children.Add(healLife);




            text = "lifeRegen";
            lifeRegen = new XG_PlusMinus(new Rectangle(10 + 180, 10, 100, 60), text, this.lifeRegen_Clicked);
            Children.Add(lifeRegen);

            text = "healMana";
            healMana = new XG_PlusMinus(new Rectangle(10 + 180, 60, 100, 60), text, this.healMana_Clicked);
            Children.Add(healMana);

            text = "mana";
            mana = new XG_PlusMinus(new Rectangle(100 + 180, 10, 100, 60), text, this.placeStyle_Clicked);
            Children.Add(mana);

            text = "manaIncrease";
            manaIncrease = new XG_PlusMinus(new Rectangle(100 + 180, 60, 100, 60), text, this.manaIncrease_Clicked, 1f, true);
            Children.Add(manaIncrease);





        }
        //Button click event handler
 
        void crit_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit = (int)crit.Value;

        }
        void defense_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].defense = (int)defense.Value;

        }
        void healLife_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].healLife = (int)healLife.Value;

        }
        void damage_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].damage = (int)damage.Value;

        }




        void lifeRegen_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].lifeRegen = (int)lifeRegen.Value;

        }
        void healMana_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].healMana = (int)healMana.Value;

        }
        void placeStyle_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mana = (int)mana.Value;

        }

        void manaIncrease_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].manaIncrease = (int)manaIncrease.Value;

        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            crit.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit;
            damage.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].damage;
            defense.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].defense;
            healLife.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].healLife;
            lifeRegen.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].lifeRegen;
            healMana.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].healMana;
            mana.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mana;

        }


        public XGTextBox TextBox1 { get; set; }

        public XG_PlusMinus crit { get; set; }

        public XG_PlusMinus _scale { get; set; }

        public XG_PlusMinus damage { get; set; }

        public XG_PlusMinus defense { get; set; }

        public XG_PlusMinus healLife { get; set; }

        public XG_PlusMinus lifeRegen { get; set; }

        public XG_PlusMinus healMana { get; set; }

        public XG_PlusMinus mana { get; set; }

        public XG_PlusMinus manaIncrease { get; set; }
    }



    public class Item_SG : XG_TabPage
    {
        public Mod Game;


        public Item_SG(Mod game, Rectangle rect)
            : base(rect, "S.G.")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "Sound and Graphics"));

            string text = "";
            text = "Graphic";
            noUseGraphic = new XG_ButtonLabelBool(new Rectangle(10, 10, 100, 60), text, this.noUseGraphic_Clicked );
            Children.Add(noUseGraphic);

            text = "alpha";
            alpha = new XG_PlusMinus(new Rectangle(10, 60, 100, 60), text, this.alpha_Clicked, 1f, true,false,0,100,0,"F0",true);
            Children.Add(alpha);

            text = "Animation";
            useAnimation = new XG_PlusMinus(new Rectangle(100, 10, 100, 60), text, this.useAnimation_Clicked, 1f, false, false, 0, 20, 0, "F0", true);
            Children.Add(useAnimation);

            text = "Sound";
            useSound = new XG_PlusMinus(new Rectangle(100, 60, 100, 60), text, this.useSound_Clicked, 1f, false,false,0,50,0,"F0",true);
            Children.Add(useSound);




            text = "width";
            width = new XG_PlusMinus(new Rectangle(10 + 180, 10, 100, 60), text, this.width_Clicked,1,true);
            Children.Add(width);

            text = "height";
            height = new XG_PlusMinus(new Rectangle(10 + 180, 60, 100, 60), text, this.height_Clicked, 1, true);
            Children.Add(height);

            text = "scale";
            scale = new XG_PlusMinus(new Rectangle(100 + 180, 10, 100, 60),text, this.scale_Clicked,0.1f,true);
            Children.Add(scale);
 





        }
        //Button click event handler

        void noUseGraphic_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].noUseGraphic  = noUseGraphic.Value;

        }
        void useAnimation_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useAnimation = (int)useAnimation.Value;

        }
        void useSound_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useSound = (int)useSound.Value;

        }
        void alpha_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].alpha = (int)alpha.Value;

        } 
        void width_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].width = (int)width.Value;

        }
        void height_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].height = (int)height.Value;

        }
        void scale_Clicked(XG_Control sender)
        {
            Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].scale = scale.Value;
             
        }

  



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            noUseGraphic.DisplayValue =      Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].noUseGraphic;
                   alpha.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].alpha;
            useAnimation.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useAnimation;
                useSound.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].useSound;
                   width.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].width;
                  height.DisplayValue = (int)Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].height;
                   scale.DisplayValue =      Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].scale;
        } 


        public XGTextBox TextBox1 { get; set; }

        public XG_PlusMinus _scale { get; set; }

        public XG_PlusMinus alpha { get; set; }

        public XG_PlusMinus useAnimation { get; set; }

        public XG_PlusMinus useSound { get; set; }

        public XG_PlusMinus width { get; set; }

        public XG_PlusMinus height { get; set; }

        public XG_PlusMinus scale { get; set; }

        public XG_ButtonLabelBool noUseGraphic { get; set; }
    }

     


    #endregion



    #region Player
    
    public class PlayerSettings : XG_TabPage
    {
        public Mod Game;
 
   

        public PlayerSettings(Mod game, Rectangle rect)
            : base(rect, "Player")
        {
            Game = game;
            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control
            pageRect.X = pageRect.Y = 0;
            pageRect.Height = 148;
            XGTabControl tab = new XGTabControl(pageRect);
            Children.Add(tab); // add the tab control to our child control list
            player_CH = new Player_CH(game, pageRect);
            tab.Children.Add(player_CH);
            
            buffsel = new Buffsel(game, pageRect);
            tab.Children.Add(buffsel);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }




 

        public Buffsel buffsel { get; set; }

        public Player_CH player_CH { get; set; }

 
    }



    public class Player_CH : XG_TabPage
    {
        public Mod Game;


        public Player_CH(Mod game, Rectangle rect)
            : base(rect, "Hacks")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "Common Hacks"));
            string text = "";
            text = "inf Air";
            infAir = new XG_ButtonLabelBool(new Rectangle(10, 10, 100, 60), text, null, true);
            Children.Add(infAir);

            text = "inf life";
            inflife = new XG_ButtonLabelBool(new Rectangle(10, 60, 100, 60), text, null, true);
            Children.Add(inflife);

            text = "inf mana";
            infmana = new XG_ButtonLabelBool(new Rectangle(80, 10, 100, 60), text, null, true);
            Children.Add(infmana);
            //-----------------------------------------------------------------------------------------------
 
            text = "rocketMax";
            rocketTimeMax = new XG_PlusMinus(new Rectangle(100, 60, 100, 60), text, null, 1f, true, true, 0, 1000);
            Children.Add(rocketTimeMax);


            text = "BlockRange";
            blockRange = new XG_PlusMinus(new Rectangle(160, 10, 100, 60), text, null, 1f, true, true, 0, 100);
            Children.Add(blockRange);



            text = "GrabRange";
            itemGrabRange = new XG_PlusMinus(new Rectangle(200, 60, 100, 60), text, this.itemGrabRange_Clicked, 1f, true, true);
            Children.Add(itemGrabRange);


            text = "GrabSpeed";
            itemGrabSpeed = new XG_PlusMinus(new Rectangle(270, 10, 100, 60), text, this.itemGrabSpeed_Clicked,1f, true, true, 0f, 500f, 0, "F1");
            Children.Add(itemGrabSpeed);

            text = "Light";
            Light = new XG_ButtonLabelBool(new Rectangle(310, 60, 100, 60), text, null, true);
            Children.Add(Light);

            text = "Fly";
            Fly = new XG_ButtonLabelBool(new Rectangle(365, 10, 100, 60), text, null, true);
            Children.Add(Fly);

        }

     
        void itemGrabRange_Clicked(XG_Control sender)
        {

            PlayerReflect.itemGrabRange = (int)itemGrabRange.Value;

        }

        void itemGrabSpeed_Clicked(XG_Control sender)
        {

            PlayerReflect.itemGrabSpeed = itemGrabSpeed.Value;
        }


 
        

 
 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
             
        }



        public XG_ButtonLabelBool infAir { get; set; }

        public XG_ButtonLabelBool inflife { get; set; }
  
     
        public XG_ButtonLabelBool infmana { get; set; }


        public XG_PlusMinus blockRange { get; set; }

        public XG_PlusMinus itemGrabRange { get; set; }

        public XG_PlusMinus itemGrabSpeed { get; set; }

        public XG_PlusMinus pickSpeed { get; set; }

        public XG_ButtonLabelBool Light { get; set; }

        public XG_ButtonLabelBool Fly { get; set; }

        public XG_PlusMinus rocketTimeMax { get; set; }
    }
   
     
     

    public class Buffsel : XG_TabPage
    {
        public Mod Game;
        private int inminutes;
        public Buffsel(Mod game, Rectangle rect)
            : base(rect, "Buffs")
        {
            Game = game;

            string text = "";
            text = "minutes";
            inmiliseconds = new XG_PlusMinus(new Rectangle(10, 10, 100, 60), text, this.Time_Clicked, 1f, true, true, 0, 1000);
            Children.Add(inmiliseconds);

            bufftree = new XGTree(new Rectangle(100, 10, 300, 90), TreeItemClicked);
            Children.Add(bufftree);
 




            for (int i = 0; i < Main.buffName.Length; i++) // add sections
            {

                if (Main.buffName[i] != null)
                {
                    bufftree.Items.Add(new XGTreeItem(@Main.buffName[i], Main.buffTexture[i]));
                }

            }
        }
        void Time_Clicked(XG_Control sender)
        {

            inminutes = (int)inmiliseconds.Value * 60 * 60;
        }

        void TreeItemClicked(XGTreeItem name)
        {
            for (int t = 0; t < Main.buffName.Length; t++) // add sections
            {
                if (Main.buffName[t] == name.Value.ToString())
                {

                    Main.player[Main.myPlayer].AddBuff(t, inminutes, false);

                    break;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Visible)
            {
          
            }
        }
        public XGTree bufftree { get; set; }


        public XG_PlusMinus inmiliseconds { get; set; }
    }



    
    #endregion

    #region inventory

    public class InV : XG_TabPage
    {
        public Mod Game;


        public InV(Mod game, Rectangle rect)
            : base(rect, "Inv")
        {
            Game = game;
            Children.Add(new XG_Label(new Rectangle(10, 0, 200, 20), "change inventory"));

            string text = "";
            text = "Cathegory";
            Cathegory = new XG_PlusMinus(new Rectangle(10, 10, 100, 60), text, this.Cathegory_Clicked, 1f, false, true, 0, 10, 1, "F0", true);
            Children.Add(Cathegory);

            text = "Inventory";
            Inventor = new XG_PlusMinus(new Rectangle(10, 60, 100, 60), text, this.Inventory_Clicked, 1f, false, true, 0, 10, 1, "F0", true);
            Children.Add(Inventor);
            LAbel = new XG_Label(new Rectangle(100, 85, 200, 20), "Max Inventories: 0");
            Children.Add(LAbel);

            Children.Add(new XG_Button(new Rectangle(210, 40, 100, 20), "Restore", this.Restore_Clicked));
 







        }
        //Button click event handler
        void Restore_Clicked(XG_Control sender)
        {
            LazyInventory.Restore(LazyInventory.Save());
        }
        void Cathegory_Clicked(XG_Control sender)
        {  
            LazyInventory.InuseCategory = Mod.Lazyinventory[(int)Cathegory.Value];
            Inventor._Value = 0;
            LazyInventory.Changeinv();
        }
    
        void Inventory_Clicked(XG_Control sender)
        {
            LazyInventory.Changeinv((int)Inventor.Value);
        }
 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (LazyInventory.InuseCategory == null) { return; }
           
                Cathegory.LabelText.Text = LazyInventory.InuseCategory.Classification;
                Inventor.Max = LazyInventory.InuseCategory.invetory.Count / 40;
                LAbel.Text = "Max: " + Inventor.Max.ToString();
          
            Cathegory.Max = Mod.Lazyinventory.Count - 1;
            Cathegory.Min = 0;
            Inventor.Min = 0;
        }


        public XGTextBox TextBox1 { get; set; }

 

        public XG_PlusMinus Cathegory { get; set; }

        public XG_PlusMinus Inventor { get; set; }

        public XG_Label LAbel { get; set; }
    }




    #endregion

    #region server
    
    public class Server : XG_TabPage
    {
        public Mod Game;
      

        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }


        public Server(Mod game, Rectangle rect)
            : base(rect, "Tshock")
        {
            Game = game;





            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control
            pageRect.X = pageRect.Y = 0;
            pageRect.Height = 148;
            label = new XG_Label(new Rectangle(0, this.Rectangle.Height - 30, 200, 20), "Player name");
            Children.Add(label ); // leave default Left/VCenter alignment
  
            
            XGTabControl tab = new XGTabControl(pageRect);
            Children.Add(tab); // add the tab control to our child control list


            serverCommand1 = new ServerCommand1(game, pageRect);
            tab.Children.Add(serverCommand1);
            serverCommand2 = new ServerCommand2(game, pageRect);
            tab.Children.Add(serverCommand2);
            serverCommand3 = new ServerCommand3(game, pageRect);
            tab.Children.Add(serverCommand3);
            serverCommand4 = new ServerCommand4(game, pageRect);
            tab.Children.Add(serverCommand4);
         
            give = new Give(game, pageRect);
            tab.Children.Add(give);



        }
        void Set_Clicked(XG_Control sender)
        {
            Mod.stopVectoring = true;
            Mod.polygons.cansetlines = false;
        }
        void Clear_Clicked(XG_Control sender)
        {
           
            Mod.polygons.verTices.Clear();
            Mod.polygons.activeFill.Clear();
            Mod.polygons.activeLine.Clear();
            Mod.tilecopies = new List<Tiles>();
            Mod.stopVectoring = false;
            Mod.polygons.cansetlines = true;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.Visible)
            {
                if (Mod.SelectedPlayer != null)
                {
                    this.label.Text ="Selected player: "+ Mod.SelectedPlayer.name + " / Selected Item: "+ Mod.toolWindow.server.give.ItemName;
                }
            }
        }






     
 



 

        public Give give { get; set; }

        public XG_Label label { get; set; }



        public ServerCommand1 serverCommand1 { get; set; }

        public ServerCommand2 serverCommand2 { get; set; }

        public ServerCommand3 serverCommand3 { get; set; }

        public ServerCommand4 serverCommand4 { get; set; }
    }


    public class ServerCommand1 : XG_TabPage
    {
        public Mod Game;


        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }


        public ServerCommand1(Mod game, Rectangle rect)
            : base(rect, "SC1")
        {
            Game = game;





            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control






            Children.Add(new XG_Button(new Rectangle(10, 0, 60, 20), "Login", this.Login_Clicked));

            user = new XGTextBox(new Rectangle(100, 0, 200, 20));
            user.Text = "user";
            Children.Add(user);


            pass = new XGTextBox(new Rectangle(100, 20, 200, 20));
            pass.Text = "pass";
            Children.Add(pass);

         //   Children.Add(new XG_Button(new Rectangle(10, 30, 60, 20), "ban", this.ban_Clicked));

           // Children.Add(new XG_Button(new Rectangle(10, 60, 60, 20), "kick", this.kick_Clicked));

             


        }
        void Login_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/login " + user.Text + " " + pass.Text);
        }
        void genericplayer_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/" + sender.Text + " " + Mod.SelectedPlayer.name);
        }
        void generic_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/" + sender.Text);
        }


    

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }






         

        public XGTextBox user { get; set; }

        public XGTextBox pass { get; set; }
    }

    public class ServerCommand2 : XG_TabPage
    {
        public Mod Game;
 
        private int timeindex;

         


        public ServerCommand2(Mod game, Rectangle rect)
            : base(rect, "SC2")
        {
            Game = game;





            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control



            teleport = new XG_ButtonLabelBool(new Rectangle(350, 10, 100, 60), "Teleport", null, true);

            Children.Add(teleport);
            Children.Add(new XG_Button(new Rectangle(10, 30, 60, 20), "tphere", this.genericplayer_Clicked));
            Children.Add(new XG_Button(new Rectangle(10, 60, 60, 20), "heal", this.genericplayer_Clicked));
 
            Children.Add(new XG_Button(new Rectangle(100, 30, 60, 20), "butcher", this.generic_Clicked));
            Children.Add(new XG_Button(new Rectangle(100, 60, 60, 20), "time", this.time_Clicked));
          
         
           


        }
    
        void genericplayer_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/" + sender.Text +" "+ Mod.SelectedPlayer.name);
        }
        void generic_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/" + sender.Text);
        }
        void time_Clicked(XG_Control sender)
        {
            timeindex++;
            if (timeindex > 5) { timeindex = 1; }
            string time = "day";
            if (timeindex == 2)
            {
                time = "night";
            }
            if (timeindex == 3)
            {
                time = "dusk";
            }
            if (timeindex == 4)
            {
                time = "noon";
            }
            if (timeindex == 5)
            {
                time = "midnight";
            }
          
            NetMessage.SendData(25, -1, -1, "/time " + time);


        }

 
      
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
    

        }





     

        public XG_ButtonLabelBool teleport { get; set; }

       
    }

    public class ServerCommand3 : XG_TabPage
    {
        public Mod Game;


        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }


        public ServerCommand3(Mod game, Rectangle rect)
            : base(rect, "SC3")
        {
            Game = game;





            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control






            Children.Add(new XG_Button(new Rectangle(10, 0, 60, 20), "unban", this.unban_Clicked));
            Children.Add(new XG_Button(new Rectangle(10, 30, 60, 20), "ban", this.ban_Clicked));
            Children.Add(new XG_Button(new Rectangle(10, 60, 60, 20), "kick", this.kick_Clicked));

            Reason = new XGTextBox(new Rectangle(10, 90, 200, 20));
            Reason.Text = "Misbehavior";
            Children.Add(Reason);


        }
        void unban_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/unban " + Mod.addquotes(Mod.SelectedPlayer.name) + " " + Reason.Text);
        }
        void genericplayer_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/" + sender.Text + " " + Mod.SelectedPlayer.name);
        }
        void generic_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/" + sender.Text);
        }


        void kick_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/kick " + Mod.addquotes(Mod.SelectedPlayer.name) + " " + Reason.Text);
        }

        void ban_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/ban " + Mod.addquotes(Mod.SelectedPlayer.name) + " " + Reason.Text);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }






         

        public XGTextBox Reason { get; set; }
    }


    public class ServerCommand4 : XG_TabPage
    {
        public Mod Game;
        private string RegionName="";


        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }


        public ServerCommand4(Mod game, Rectangle rect)
            : base(rect, "SC4")
        {
            Game = game;





            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control






            Children.Add(new XG_Button(new Rectangle(10, 60, 60, 20), "get region", this.name_Clicked));
         //   Children.Add(new XG_Button(new Rectangle(10, 30, 60, 20), "ban", this.ban_Clicked));
           // Children.Add(new XG_Button(new Rectangle(10, 60, 60, 20), "kick", this.kick_Clicked));

            Region = new XGTextBox(new Rectangle(10, 90, 200, 20));
            Region.Text = "selected Region";
            Children.Add(Region);


        }
        void name_Clicked(XG_Control sender)
        {
            if (Main.netMode == 1)
            {
                NetMessage.SendData(25, -1, -1, @"/region name");
                SendPackets.SendData(17, -1, -1, "", 0, Main.player[Main.myPlayer].position.X / 16, Main.player[Main.myPlayer].position.Y / 16);
                for (int i = 0; i < Main.chatLine.Length; i++)
                {
                    string[] words = Main.chatLine[i].text.Split(' ');
                    if (words.Length > 3)
                    {
                        if (words[0] == "Region")
                        {

                            if (words[1] == @"Name:")
                            {

                                RegionName = Mod.addquotes(words[2]);
                                break;
                            }
                        }
                    }
                }
                Region.Text = RegionName;
            }


        }
        void unban_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/unban " + Mod.addquotes(Mod.SelectedPlayer.name) + " " + Region.Text);
        }
        void genericplayer_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/" + sender.Text + " " + Mod.SelectedPlayer.name);
        }
        void generic_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/" + sender.Text);
        }


        void kick_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/kick " + Mod.addquotes(Mod.SelectedPlayer.name) + " " + Region.Text);
        }

        void ban_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/ban " + Mod.addquotes(Mod.SelectedPlayer.name) + " " + Region.Text);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }






 

        public XGTextBox Region { get; set; }
    }



    #region Give

    public class Give : XG_TabPage
    {
        public Mod Game;
        public XGTextBox TextBox1 { get; protected set; }
        public XG_LabeledSlider Slider1 { get; protected set; }
        public XG_Label Slider1Label { get; protected set; }
        public XGTree Tree { get; protected set; }


         

        private static int stack = 1;
 
        private bool mouseLeftRelease;







        public Give(Mod game, Rectangle rect)
            : base(rect, "Give")
        {
            Game = game;

            // Add controls to the tab page
            onclick = new XG_ButtonLabelBool(new Rectangle(10, -10, 100, 60), "on Click", null, true);
        Children.Add(onclick);
        onPlayer = new XG_Button(new Rectangle(10, 50, 60, 20), "on Player", this.Onplayer_Clicked );
        Children.Add(onPlayer);
            
            
            string text = "Stack";
            STACK = new XG_PlusMinus(new Rectangle(10, 70, 100, 60), text, this.Cuantity_Clicked,1f,true,true,1f,250f,1f,"F1",true);
            Children.Add(STACK);

            Tree = new XGTree(new Rectangle(110, 0, 300, 115), TreeItemClicked);
            Children.Add(Tree);


            XGTreeItem Tools = new XGTreeItem(Mod.ItemD.Tools.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(Tools);
            for (int i = 0; i < Mod.ItemD.Tools.Sections.Length; i++) // add sections
            {
                XGTreeItem Tools_Sections = new XGTreeItem(Mod.ItemD.Tools.Sections[i], Main.inventoryBackTexture);
                Tools.Items.Add(Tools_Sections);
                for (int n = 0; n < Mod.ItemD.Tools.SectionNpcItems[i].Length; n++) // add section items
                {
                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Tools.SectionNpcItems[i][n]);
                    XGTreeItem Tools_Section_items = new XGTreeItem(Mod.ItemD.Tools.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                    Tools_Sections.Items.Add(Tools_Section_items);
 
                }
            }


            XGTreeItem Weapons = new XGTreeItem(Mod.ItemD.Weapons.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(Weapons);
            for (int i = 0; i < Mod.ItemD.Weapons.Sections.Length; i++) // add sections
            {
                XGTreeItem Weapons_Sections = new XGTreeItem(Mod.ItemD.Weapons.Sections[i], Main.inventoryBackTexture);
                Weapons.Items.Add(Weapons_Sections);
                for (int n = 0; n < Mod.ItemD.Weapons.SectionNpcItems[i].Length; n++) // add section items
                {

                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Weapons.SectionNpcItems[i][n]);
                    XGTreeItem Weapons_Section_items = new XGTreeItem(Mod.ItemD.Weapons.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                    Weapons_Sections.Items.Add(Weapons_Section_items);

                   

              }
            }

            XGTreeItem Furniture = new XGTreeItem(Mod.ItemD.Furniture.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(Furniture);
            for (int i = 0; i < Mod.ItemD.Furniture.Sections.Length; i++) // add sections
            {
                XGTreeItem Furniture_Sections = new XGTreeItem(Mod.ItemD.Furniture.Sections[i], Main.inventoryBackTexture);
                Furniture.Items.Add(Furniture_Sections);
                for (int n = 0; n < Mod.ItemD.Furniture.SectionNpcItems[i].Length; n++) // add section items
                {
                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Furniture.SectionNpcItems[i][n]);
                    XGTreeItem Furniture_Section_items = new XGTreeItem(Mod.ItemD.Furniture.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                    Furniture_Sections.Items.Add(Furniture_Section_items);

                   
                }
            }



            XGTreeItem Miscellaneous = new XGTreeItem(Mod.ItemD.Miscellaneous.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(Miscellaneous);
            for (int i = 0; i < Mod.ItemD.Miscellaneous.Sections.Length; i++) // add sections
            {
                XGTreeItem Miscellaneous_Sections = new XGTreeItem(Mod.ItemD.Miscellaneous.Sections[i], Main.inventoryBackTexture);
                Miscellaneous.Items.Add(Miscellaneous_Sections);
                for (int n = 0; n < Mod.ItemD.Miscellaneous.SectionNpcItems[i].Length; n++) // add section items
                {

                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Miscellaneous.SectionNpcItems[i][n]);
                    XGTreeItem Miscellaneous_Section_items = new XGTreeItem(Mod.ItemD.Miscellaneous.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                    Miscellaneous_Sections.Items.Add(Miscellaneous_Section_items);


                       }
            }

            XGTreeItem Accessories = new XGTreeItem(Mod.ItemD.Accessories.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(Accessories);
            for (int i = 0; i < Mod.ItemD.Accessories.Sections.Length; i++) // add sections
            {


                XGTreeItem Accessories_Sections = new XGTreeItem(Mod.ItemD.Accessories.Sections[i], Main.inventoryBackTexture);
                Accessories.Items.Add(Accessories_Sections);
                for (int n = 0; n < Mod.ItemD.Accessories.SectionNpcItems[i].Length; n++) // add section items
                {

                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Accessories.SectionNpcItems[i][n]);
                    XGTreeItem Accessories_Section_items = new XGTreeItem(Mod.ItemD.Accessories.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                    Accessories_Sections.Items.Add(Accessories_Section_items);


             }
            }


            XGTreeItem Blocks = new XGTreeItem(Mod.ItemD.Blocks.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(Blocks);
            for (int i = 0; i < Mod.ItemD.Blocks.Sections.Length; i++) // add sections
            {
                XGTreeItem Blocks_Sections = new XGTreeItem(Mod.ItemD.Blocks.Sections[i], Main.inventoryBackTexture);
                Blocks.Items.Add(Blocks_Sections);
                for (int n = 0; n < Mod.ItemD.Blocks.SectionNpcItems[i].Length; n++) // add section items
                {



                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Blocks.SectionNpcItems[i][n]);
                    XGTreeItem Blocks_Section_items = new XGTreeItem(Mod.ItemD.Blocks.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                    Blocks_Sections.Items.Add(Blocks_Section_items);


       
                }
            }


            XGTreeItem Potions = new XGTreeItem(Mod.ItemD.Potions.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(Potions);
            for (int i = 0; i < Mod.ItemD.Potions.Sections.Length; i++) // add sections
            {
                XGTreeItem Potions_Sections = new XGTreeItem(Mod.ItemD.Potions.Sections[i], Main.inventoryBackTexture);
                Potions.Items.Add(Potions_Sections);
                for (int n = 0; n < Mod.ItemD.Potions.SectionNpcItems[i].Length; n++) // add section items
                {
                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Potions.SectionNpcItems[i][n]);
                    XGTreeItem Potions_Section_items = new XGTreeItem(Mod.ItemD.Potions.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                    Potions_Sections.Items.Add(Potions_Section_items);


                    }
            }

            XGTreeItem Defenseitems = new XGTreeItem(Mod.ItemD.Defenseitems.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(Defenseitems);
            for (int i = 0; i < Mod.ItemD.Defenseitems.Sections.Length; i++) // add sections
            {
                if (Mod.ItemD.Defenseitems.Sections[i] != "Armor Sets")
                {
                    XGTreeItem Defenseitems_Sections = new XGTreeItem(Mod.ItemD.Defenseitems.Sections[i], Main.inventoryBackTexture);
                    Defenseitems.Items.Add(Defenseitems_Sections);
                    for (int n = 0; n < Mod.ItemD.Defenseitems.SectionNpcItems[i].Length; n++) // add section items
                    {
                        Item item = new Item();
                        item.SetDefaults(Mod.ItemD.Defenseitems.SectionNpcItems[i][n]);
                        XGTreeItem Defenseitems_Section_items = new XGTreeItem(Mod.ItemD.Defenseitems.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                        Defenseitems_Sections.Items.Add(Defenseitems_Section_items);



                    }

                }
                else
                {

                    XGTreeItem ArmorSets = new XGTreeItem(Mod.ItemD.ArmorSets.Cathegory, Main.inventoryBackTexture);
                    Defenseitems.Items.Add(ArmorSets);
                    for (int t = 0; t < Mod.ItemD.ArmorSets.Sections.Length; t++) // add sections
                    {
                        XGTreeItem ArmorSets_Sections = new XGTreeItem(Mod.ItemD.ArmorSets.Sections[t], Main.inventoryBackTexture);
                        ArmorSets.Items.Add(ArmorSets_Sections);
                        for (int n = 0; n < Mod.ItemD.ArmorSets.SectionNpcItems[t].Length; n++) // add section items
                        {


                            Item item = new Item();
                            item.SetDefaults(Mod.ItemD.ArmorSets.SectionNpcItems[t][n]);
                            XGTreeItem ArmorSets_Section_items = new XGTreeItem(Mod.ItemD.ArmorSets.SectionNpcItems[t][n], Main.itemTexture[item.type]);
                            ArmorSets_Sections.Items.Add(ArmorSets_Section_items);


                       }
                    }
                }
            }




            XGTreeItem VanityItems = new XGTreeItem(Mod.ItemD.VanityItems.Cathegory, Main.inventoryBackTexture);
            Tree.Items.Add(VanityItems);
            for (int i = 0; i < Mod.ItemD.VanityItems.Sections.Length; i++) // add sections
            {
                if (Mod.ItemD.VanityItems.Sections[i] != "Vanity Sets")
                {
                    XGTreeItem VanityItems_Sections = new XGTreeItem(Mod.ItemD.VanityItems.Sections[i], Main.inventoryBackTexture);
                    VanityItems.Items.Add(VanityItems_Sections);
                    for (int n = 0; n < Mod.ItemD.VanityItems.SectionNpcItems[i].Length; n++) // add section items
                    {

                        Item item = new Item();
                        item.SetDefaults(Mod.ItemD.VanityItems.SectionNpcItems[i][n]);
                        XGTreeItem VanityItems_Section_items = new XGTreeItem(Mod.ItemD.VanityItems.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                        VanityItems_Sections.Items.Add(VanityItems_Section_items);

     }

                }
                else
                {

                    XGTreeItem VanitySets = new XGTreeItem(Mod.ItemD.VanitySets.Cathegory, Main.inventoryBackTexture);
                    VanityItems.Items.Add(VanitySets);
                    for (int t = 0; t < Mod.ItemD.VanitySets.Sections.Length; t++) // add sections
                    {
                        XGTreeItem VanitySets_Sections = new XGTreeItem(Mod.ItemD.VanitySets.Sections[t], Main.inventoryBackTexture);
                        VanitySets.Items.Add(VanitySets_Sections);
                        for (int n = 0; n < Mod.ItemD.VanitySets.SectionNpcItems[t].Length; n++) // add section items
                        {




                            Item item = new Item();
                            item.SetDefaults(Mod.ItemD.VanitySets.SectionNpcItems[t][n]);
                            XGTreeItem VanitySets_Section_items = new XGTreeItem(Mod.ItemD.VanitySets.SectionNpcItems[t][n], Main.itemTexture[item.type]);
                            VanitySets_Sections.Items.Add(VanitySets_Section_items);




                        }
                    }
                }
            }










            XGTreeItem All = new XGTreeItem("All", Main.inventoryBackTexture);
            Tree.Items.Add(All);
            for (int i = 0; i < Main.itemName.Length; i++) // add sections
            {
             
                    Item item = new Item();
                    item.SetDefaults(Main.itemName[i]);
                    XGTreeItem All_items = new XGTreeItem(item.name, Main.itemTexture[item.type]);
                    All.Items.Add(All_items);


            }












            //Tree.ExpandAll();
        }
        void Onplayer_Clicked(XG_Control sender)
        {
            NetMessage.SendData(25, -1, -1, "/give " + Mod.allitems.type + " " + Mod.addquotes(Mod.SelectedPlayer.name) + " " + STACK.Value.ToString());
        }
        void TreeItemClicked(XGTreeItem name)
        {
            ItemName = name.Value.ToString();
            Mod.allitems.SetDefaults(ItemName);
        }



 

        void Cuantity_Clicked(XG_Control sender)
        {Item t = new Item();
            t.SetDefaults(ItemName);
            if (t.maxStack > STACK._Value)
            {
                stack = (int)STACK._Value;
            }
            else { stack = (int)(STACK._Value = t.maxStack); }


        }
        public void Itemupdate()
        {
            int X =(int)(Main.mouseState.X + Main.screenPosition.X);
            int Y =(int)(Main.mouseState.Y + Main.screenPosition.Y);
     
                if (Main.mouseState.LeftButton == ButtonState.Released) { mouseLeftRelease = true; }
                if (mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && !Mod.polygons.onwindow && !Main.player[Main.myPlayer].mouseInterface)
                {
                   
                  this.NewItem(X,Y,ItemName, stack, true);
                  mouseLeftRelease = false;
                }
            

        }
        public void NewItem(int X, int Y, string Name = "", int Stack = 1, bool noBroadcast = false)
        {
            if (Stack <= 0) { Stack = stack = 1; }
            int Width = 16;
            int Height = 16;
            if (Main.rand == null)
            {
                Main.rand = new Random();
            }
            int index = 200;
            Main.item[200] = new Item();
            if (Main.netMode != 1)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.item[i].active)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if ((index == 200) && (Main.netMode != 1))
            {
                int spawnTime = 0;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.item[j].spawnTime > spawnTime)
                    {
                        spawnTime = Main.item[j].spawnTime;
                        index = j;
                    }
                }
            }
            Main.item[index] = new Item();
            Main.item[index].SetDefaults(Name);
            //  Main.item[index].Prefix(pfix);
            Main.item[index].position.X = (X + (Width / 2)) - (Main.item[index].width / 2);
            Main.item[index].position.Y = (Y + (Height / 2)) - (Main.item[index].height / 2);
            Main.item[index].wet = Collision.WetCollision(Main.item[index].position, Main.item[index].width, Main.item[index].height);
            Main.item[index].velocity.X = Main.rand.Next(-30, 0x1f) * 0.1f;
            Main.item[index].velocity.Y = Main.rand.Next(-40, -15) * 0.1f;
            if ((Main.item[index].type == 520) || (Main.item[index].type == 0x209))
            {
                Main.item[index].velocity.X = Main.rand.Next(-30, 0x1f) * 0.1f;
                Main.item[index].velocity.Y = Main.rand.Next(-30, 0x1f) * 0.1f;
            }
            Main.item[index].active = true;
            Main.item[index].spawnTime = 0;
            Main.item[index].stack = Stack;
            if (Main.netMode == 0)
            {
                Main.item[index].owner = Main.myPlayer;
            }
            if ((Main.netMode == 1) && noBroadcast)
            {
                NetMessage.SendData(21, -1, -1, "", index, 0f, 0f, 0f, 0);
                Main.item[index].FindOwner(index);
            }

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.Visible)
            {
                Item t = new Item();
                t.SetDefaults(ItemName);
                if (t.maxStack < STACK._Value)
                {
                    stack = (int)(STACK._Value = t.maxStack);
                }
                STACK.DisplayValue = stack;
                if (onclick.Value )
                {
                    Itemupdate();
                }
            }

        }






        public XG_PlusMinus STACK { get; set; }



        public XG_ButtonLabelBool onclick { get; set; }

        public XG_Button  onPlayer { get; set; }

        public string ItemName { get; set; }
    }

    
    #endregion

    
    #endregion
    #endregion
    #region selection windows
		
    public class TileSelectionW : XG_Control
    {

        public Mod Game;
        public XGTabControl TabControl { get; protected set; }
        //  public XGTabPage PageOne { get; protected set; }
        public static int Y = Main.screenHeight-200;
        public static int X = 200;
 
        public XGTree TileTree { get; protected set; }
        public XGTree WallTree { get; protected set; }




        public TileSelectionW(Mod game)
            : base(new Rectangle(X, Y, 250, 180), true)
        {
            Game = game;

            // Add our window to the Controls to be managed by the Editor_Mod GUI manager

            XnaGUIManager.Controls.Add(this);

            // Offset the Tab Control and Tab Page rectangles relative to this Tool Window 

            Rectangle pageRect = this.Rectangle; // tab pages are the size of the tab control
            pageRect.X = pageRect.Y = 0;  // but use parent relative coordintates



            TabControl = new XGTabControl(pageRect);
            Children.Add(TabControl); // add the tab control to our child control list


            tilesel = new TileSel(game, pageRect);
            TabControl.Children.Add(tilesel);

           wallsel = new WallSel(game, pageRect);
           TabControl.Children.Add(wallsel);
 
        }
 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
     
        }

        public XG_TabPage tiletabs { get; set; }


        public TileSel tilesel { get; set; }

        public WallSel wallsel { get; set; }
    } 
    #region tile selection
    public class TileSel : XG_TabPage
    {
        public Mod Game;










        public TileSel(Mod game, Rectangle rect)
            : base(rect, "Tile Selection")
        {
            Game = game;




            TileTree = new XGTree(new Rectangle(10, 5, 230, 135), TreeItemClicked);
            Children.Add(TileTree);

            XGTreeItem Furniture = new XGTreeItem(Mod.ItemD.Furniture.Cathegory, Main.inventoryBackTexture);
            TileTree.Items.Add(Furniture);
            for (int i = 0; i < Mod.ItemD.Furniture.Sections.Length; i++) // add sections
            {
                XGTreeItem Furniture_Sections = new XGTreeItem(Mod.ItemD.Furniture.Sections[i], Main.inventoryBackTexture);
                Furniture.Items.Add(Furniture_Sections);
                for (int n = 0; n < Mod.ItemD.Furniture.SectionNpcItems[i].Length; n++) // add section items
                {



                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Furniture.SectionNpcItems[i][n]);
                    XGTreeItem Furniture_Section_items = new XGTreeItem(Mod.ItemD.Furniture.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                    Furniture_Sections.Items.Add(Furniture_Section_items);



                }
            }

            XGTreeItem Blocks = new XGTreeItem("Blocks", Main.inventoryBackTexture);
            TileTree.Items.Add(Blocks);
            for (int i = 0; i < Mod.ItemD.Blocks.Sections.Length; i++) // add sections
            {


                if (Mod.ItemD.Blocks.Sections[i] != "Walls")
                {
                    XGTreeItem Blocks_Sections = new XGTreeItem(Mod.ItemD.Blocks.Sections[i], Main.inventoryBackTexture);
                    Blocks.Items.Add(Blocks_Sections);

                    for (int n = 0; n < Mod.ItemD.Blocks.SectionNpcItems[i].Length; n++) // add section items
                    {


                        Item item = new Item();
                        item.SetDefaults(Mod.ItemD.Blocks.SectionNpcItems[i][n]);
                        if (item.createTile >= 0)
                        {

                            XGTreeItem Blocks_Section_items = new XGTreeItem(Mod.ItemD.Blocks.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                            Blocks_Sections.Items.Add(Blocks_Section_items);

                        }
                    }
                }
            }

            XGTreeItem Miscellaneous = new XGTreeItem(Mod.ItemD.Miscellaneous.Cathegory, Main.inventoryBackTexture);
            TileTree.Items.Add(Miscellaneous);
            for (int i = 0; i < Mod.ItemD.Miscellaneous.Sections.Length; i++) // add sections
            {
                XGTreeItem Miscellaneous_Sections = new XGTreeItem(Mod.ItemD.Miscellaneous.Sections[i], Main.inventoryBackTexture);
                Miscellaneous.Items.Add(Miscellaneous_Sections);
                for (int n = 0; n < Mod.ItemD.Miscellaneous.SectionNpcItems[i].Length; n++) // add section items
                {
                    Item item = new Item();
                    item.SetDefaults(Mod.ItemD.Miscellaneous.SectionNpcItems[i][n]);

                    if (item.createTile >= 0)
                    {



                        XGTreeItem Miscellaneous_Section_items = new XGTreeItem(Mod.ItemD.Miscellaneous.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                        Miscellaneous_Sections.Items.Add(Miscellaneous_Section_items);
                    }
                }
            }
        }
        void TreeItemClicked(XGTreeItem name)
        {
            Mod.tileitem.SetDefaults(name.Value.ToString());
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); 
        }
        public XGTree TileTree { get; set; }
        public XGTree WallTree { get; set; }
    }

 
    
    #endregion
    #region Wall selection

    
    public class WallSel : XG_TabPage
    {
        public Mod Game;

 







        public WallSel(Mod game, Rectangle rect)
            : base(rect, "Wall Selection")
        {
            Game = game;




          

            WallTree = new XGTree(new Rectangle(10, 5, 230, 135), TreeItemClicked);
            Children.Add(WallTree);
            for (int i = 0; i < Mod.ItemD.Blocks.Sections.Length; i++) // add sections
            {
                if (Mod.ItemD.Blocks.Sections[i] == "Walls")
                {
                    XGTreeItem Walls_Sections = new XGTreeItem(Mod.ItemD.Blocks.Sections[i], Main.inventoryBackTexture);
                    WallTree.Items.Add(Walls_Sections);
                    for (int n = 0; n < Mod.ItemD.Blocks.SectionNpcItems[i].Length; n++) // add section items
                    {
                        Item item = new Item();
                        item.SetDefaults(Mod.ItemD.Blocks.SectionNpcItems[i][n]);
                        XGTreeItem Walls_Section_items = new XGTreeItem(Mod.ItemD.Blocks.SectionNpcItems[i][n], Main.itemTexture[item.type]);
                        Walls_Sections.Items.Add(Walls_Section_items);
                    }
                }
            }


        }
        void TreeItemClicked(XGTreeItem name)
        {
            Mod.wallitem.SetDefaults(name.Value.ToString());
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);



          
        }




       

        public XGTree WallTree { get; set; }
    }



    #endregion
 
	#endregion
    

}
