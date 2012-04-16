using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework.Input;
using System.Threading;
namespace Editor_Mod
{
    public class PolyGon
    {
        const double Rad2Deg = 360 / (Math.PI * 2);
        const double Deg2Rad = (Math.PI * 2) / 360;
        public List<Vector2> verTices = new List<Vector2>();
        public List<Point> activeFill = new List<Point>();
        public List<Point> activeLine = new List<Point>();
        public int P1X;
        public int P2X;
        public int P1Y;
        public int P2Y;
 
        public bool onwindow;
        public int mouseOnListindex;
        public int oldwheel;
        public float Rotation;
        public int CircleSides;
        public float radius;
        public float selsize;
        public bool cansetlines=false;
        public Vector2 point2 = Vector2.Zero;
        public Vector2 point1 = Vector2.Zero;
        public int temptile;
        public bool candrawline = false;
        public bool indexlocked;
        private static float selSize = 1.001f;

     
        #region mesurements

        #region square area positions
        public static void CreateNewHouse()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(BuildHouse), 1);
        }

        public static void BuildHouse(object threadContext)
        {
          Mod.polygons.House();
        
        }



        #region generic pos
        public Vector2 centerXY(Vector2 start, Vector2 end)
        {
            return new Vector2(((start.X + end.X) / 2), ((start.Y + end.Y) / 2));
        }
        #endregion


        public Vector2 Center()
        {
            return new Vector2(((this.P1X + this.P2X) / 2), ((this.P1Y + this.P2Y) / 2));
        }
        public Vector2 CenterTop()
        {
            return new Vector2(((this.P1X + this.P2X) / 2), this.P1Y);
        }
        public Vector2 CenterBotton()
        {
            return new Vector2(((this.P1X + this.P2X) / 2), this.P2Y);
        }
        public Vector2 CenterRight()
        {
            return new Vector2(this.P2X, ((this.P1Y + this.P2Y) / 2));
        }
        public Vector2 CenterLeft()
        {
            return new Vector2(this.P1X, ((this.P1Y + this.P2Y) / 2));
        }
        public Vector2 CornerTopLeft()
        {
            return new Vector2(this.P1X, this.P1Y);
        }
        public Vector2 CornerTopRight()
        {
            return new Vector2(this.P2X, this.P1Y);
        }
        public Vector2 CornerBottonLeft()
        {
            return new Vector2(this.P1X, this.P2Y);
        }
        public Vector2 CornerBottonRight()
        {
            return new Vector2(this.P2X, this.P2Y);
        }
        #endregion
        public int sides()
        {
            var result = this.verTices.Count-1;
            if (result < 3)
            {
                return 3;
            }
            else
            {
                return result;
            }
        }
        public void getDimentions()
        {
            this.P1X = 0;
            this.P2X = 0;
            this.P1Y = 0;
            this.P2Y = 0;
            for (int i = 0; i < this.verTices.Count; i++)
            {
                if (this.verTices[i].X < this.P1X || this.P1X == 0)
                {
                    this.P1X = (int)this.verTices[i].X;

                }
                if (this.verTices[i].Y < this.P1Y || this.P1Y == 0)
                {
                    this.P1Y = (int)this.verTices[i].Y;

                }
                if (this.verTices[i].X > this.P2X || this.P2X == 0)
                {
                    this.P2X = (int)this.verTices[i].X;

                }
                if (this.verTices[i].Y > this.P2Y || this.P2Y == 0)
                {
                    this.P2Y = (int)this.verTices[i].Y;
                }
            }
        }


        public Vector2 GetCenter(List<Point> entry)
        {
           int x1 = 0;
           int x2 = 0;
           int y1 = 0;
           int y2 = 0;
           for (int i = 0; i < entry.Count; i++)
           {
               if (entry[i].X < x1 || x1 == 0)
               {
                   x1 = entry[i].X;

               }
               if (entry[i].Y < y1 || y1 == 0)
               {
                   y1 = entry[i].Y;

               }
               if (entry[i].X > x2 || x2 == 0)
               {
                   x2 = entry[i].X;

               }
               if (entry[i].Y > y2 || y2 == 0)
               {
                   y2 = entry[i].Y;
               }
           }
            return new Vector2(((x1 + x2) / 2), ((y1 + y2) / 2));
        }


        public bool IsPointInPolygon(float X, float Y)
        {
            int i, j;
            bool c = false;
            for (i = 0, j = this.verTices.Count - 1; i < this.verTices.Count; j = i++)
            {
                if ((((this.verTices[i].X <= X) && (X < this.verTices[j].X)) ||
                    ((this.verTices[j].X <= X) && (X < this.verTices[i].X))) &&
                    (Y < (this.verTices[j].Y - this.verTices[i].Y) * (X - this.verTices[i].X) / (this.verTices[j].X - this.verTices[i].X) + this.verTices[i].Y))
                    c = !c;
            }
            return c;
        }

        #endregion 
        #region move and rotspeed
        public void Move(Vector2 center)
        {
            if (this.verTices.Count > 0)
            {

                this.canfill = true;
                this.cansetlines = true;
                var distance = new Vector2();
                if (this.mouseOnListindex < this.verTices.Count && this.mouseOnListindex != -1)
                {
                    distance = center - this.verTices[this.mouseOnListindex];

                }
                else
                {
                    this.getDimentions();
                    distance = center - this.Center();
                }
                List<Vector2> vectors = new List<Vector2>();
                for (int i = 0; i < verTices.Count; i++)
                {
                    vectors.Add(verTices[i] + distance);
                }
                this.verTices = vectors;
            }
            if (this.verTices.Count > 0 && this.candrawline)
            {
                this.point1 = this.verTices[this.verTices.Count - 1];
            }
        }
         //public void RotateTiles(Vector2 center = new Vector2(), float rotspeed = 0,int mouseonindex )
         //{
         //    if (this.activeTile.Count > 0 && mouseonindex < this.activeTile.Count)
         //    {

         //        if (center == Vector2.Zero) { center = this.activeTile[mouseonindex]; }
         //    }
         //    if (center == Vector2.Zero) { this.getDimentions(); center = this.Center(); }

         //    List<Vector2> vectors = new List<Vector2>();
         //    for (int z = 0; z < this.activeTile.Count; z++)
         //    {

         //        vectors.Add(RotatePointM(this.activeTile[z], center, rotspeed));


         //    }

         //    this.activeTile = vectors;
         //}

         public void RotateVertices(float rotspeed = 0)
        {
            Vector2 center = new Vector2();
         
            if (this.verTices.Count > 0 && this.mouseOnListindex < this.verTices.Count && this.mouseOnListindex != -1)
            {

                center = this.verTices[this.mouseOnListindex];
                
            }
            else
            {


                this.getDimentions();
                center = this.Center(); 
            }
            List<Vector2> vectors = new List<Vector2>();
            for (int i = 0; i < this.verTices.Count; i++)
            {
                vectors.Add(RotatePointM(this.verTices[i], center, rotspeed));


            }
            this.verTices = vectors;

            //List<Point> temp = new List<Point>();
            //for (int iXSS = 0; iXSS < this.activeTile.Count; iXSS++)
            //{
            //    Vector2 P = RotatePointM(new Vector2((int)activeTile[iXSS].X, (int)activeTile[iXSS].Y), center, rotspeed);
            //    temp.Add(new Point((int)P.X, (int)P.Y));
            //}
            //this.activeTile = temp;


            //List<Point> temp2 = new List<Point>();
            //for (int iXSS = 0; iXSS < this.activeLine.Count; iXSS++)
            //{
            //    Vector2 P = RotatePointM(new Vector2((int)activeLine[iXSS].X, (int)activeLine[iXSS].Y), center, rotspeed);
            //    temp2.Add(new Point((int)P.X, (int)P.Y));
            //}
            //this.activeLine = temp2;



 
            this.canfill = true;
            this.cansetlines = true;
            if (this.verTices.Count > 0 && this.candrawline)
            {
                this.point1 = this.verTices[this.verTices.Count - 1];
            }
            
        }

         public Vector2 RotatePoint(Vector2 point, Vector2 origin, float rotation)
         {
             // translate back to rotate about (0,0) 
             Vector2 pointAtOrigin = point - origin;

             // rotate 
             Vector2 rotatedPoint = new Vector2();
             rotatedPoint.X = pointAtOrigin.X * (float)Math.Cos(rotation) - pointAtOrigin.Y * (float)Math.Sin(rotation);
             rotatedPoint.Y = pointAtOrigin.Y * (float)Math.Cos(rotation) + pointAtOrigin.X * (float)Math.Sin(rotation);

             // translate back to origin 
             rotatedPoint += origin;

             return rotatedPoint;
         }
        public Vector2 RotatePointM(Vector2 point, Vector2 origin, float rotation)
        {
            Matrix m =
                Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *      // translate back to rotate about (0,0) 
                Matrix.CreateRotationZ(rotation) *                            // rotate 
                Matrix.CreateTranslation(new Vector3(origin, 0.0f));        // translate back to origin 

            return Vector2.Transform(point, m);
        }
        public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            Vector2 u = point - origin; //point relative to origin  
            if (u == Vector2.Zero)
                return point;

            float a = (float)Math.Atan2(u.Y, u.X); //angle relative to origin  
            a += rotation; //rotate  

            //u is now the new point relative to origin  
            u = u.Length() * new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
            return u + origin;
        } 
        #endregion
        #region shape transformation
        private List<Vector2> CreateArc(Vector2 center = new Vector2(),double radius = 0, int sides = 0, float startingAngle =0, float degrees =0)
        {

            if (center == Vector2.Zero) { this.getDimentions(); center = this.Center(); }
            if (sides == 0) { sides = this.sides(); }
            if (radius == 0) { this.getDimentions(); radius = Vector2.Distance(new Vector2(this.P1X, this.P1Y), new Vector2(this.P2X, this.P1Y)) / 2; }

            if (center == Vector2.Zero) { center = Main.player[Main.myPlayer].position; }


            List<Vector2> points = new List<Vector2>();
            points.AddRange(CreateCircleL(center,radius, sides));
            points.RemoveAt(points.Count - 1);		// remove the last point because it's a duplicate of the first

            // The circle starts at (radius, 0)
            double curAngle = 0.0;
            double anglePerSide = 360.0 / sides;

            // "Rotate" to the starting point
            while ((curAngle + (anglePerSide / 2.0)) < startingAngle)
            {
                curAngle += anglePerSide;

                // move the first point to the end
                points.Add(points[0]);
                points.RemoveAt(0);
            }

            // Add the first point, just in case we make a full circle
            points.Add(points[0]);

            // Now remove the points at the end of the circle to create the arc
            int sidesInArc = (int)((degrees / anglePerSide) + 0.5);
            points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);
            return points;
        }
        public  List<Vector2> CreateCircleL(Vector2 center = new Vector2(), double radius = 0, int sides = 0)
        {
 
            List<Vector2> vectors = new List<Vector2>();
            const double max = 2.0 * Math.PI;
            double step = max / sides;
            for (double theta = 0.0; theta < max; theta += step)
            {
                vectors.Add(new Vector2((float)(center.X + radius * Math.Cos(theta)), (float)(center.Y + radius * Math.Sin(theta))));
            }
            // then add the start vector again so it's a complete loop
            vectors.Add(new Vector2((float)(center.X + radius * Math.Cos(0)), (float)(center.Y + radius * Math.Sin(0))));

           return vectors;
        }
        public void CreateCircle(Vector2 center = new Vector2(), double radius = 0, int sides = 0)
        {

            if (center == Vector2.Zero) { this.getDimentions(); center = this.Center(); }
            if (sides == 0) { sides = this.sides(); }
            if (radius == 0) { this.getDimentions(); radius = Vector2.Distance(new Vector2(this.P1X,this.P1Y),new Vector2(this.P2X,this.P1Y)) / 2; }

            if (center == Vector2.Zero) { center = Main.player[Main.myPlayer].position;}

            List<Vector2> vectors = new List<Vector2>();
            const double max = 2.0 * Math.PI;
            double step = max / sides;
            for (double theta = 0.0; theta < max; theta += step)
            {
                vectors.Add(new Vector2((float)(center.X + radius * Math.Cos(theta)), (float)(center.Y + radius * Math.Sin(theta))));
            }
            // then add the start vector again so it's a complete loop
            vectors.Add(new Vector2((float)(center.X + radius * Math.Cos(0)), (float)(center.Y + radius * Math.Sin(0))));

            this.verTices = vectors;
            this.canfill = true;
            this.cansetlines = true;
        }
        public void CreateTriangle(Vector2 center = new Vector2(), float radius = 0)
        {

            this.getDimentions(); 
            if (center == Vector2.Zero) { center = this.Center(); }
 
            if (radius == 0) { radius = Vector2.Distance(new Vector2(this.P1X, this.P1Y), new Vector2(this.P2X, this.P1Y))/2; }
            List<Vector2> vectors = new List<Vector2>();
            vectors.Add(new Vector2((float)(center.X + radius * Math.Cos((270 ) * Deg2Rad)), (float)(center.Y + radius * Math.Sin((270 ) * Deg2Rad))));
            vectors.Add(new Vector2((float)(center.X + radius * Math.Cos((30 ) * Deg2Rad)), (float)(center.Y + radius * Math.Sin((30 ) * Deg2Rad))));
            vectors.Add(new Vector2((float)(center.X + radius * Math.Cos((150 ) * Deg2Rad)), (float)(center.Y + radius * Math.Sin((150) * Deg2Rad))));
            //complete rectangle.
            vectors.Add(new Vector2((float)(center.X + radius * Math.Cos((270) * Deg2Rad)), (float)(center.Y + radius * Math.Sin((270) * Deg2Rad))));

            this.verTices = vectors;
            this.canfill = true;
            this.cansetlines = true;
        }
        public void CreateRectangle(Vector2 start = new Vector2(), Vector2 end = new Vector2(), bool custom = false)
        {
            List<Vector2> vectors = new List<Vector2>();

            if (custom)
            {
                this.P1X = (int)start.X;
                this.P2X = (int)end.X;
                this.P1Y = (int)start.Y;
                this.P2Y = (int)end.Y;
            }
            else
            {
                this.getDimentions();
            }
            vectors.Add(this.CornerTopLeft());
            vectors.Add(this.CornerTopRight());
            vectors.Add(this.CornerBottonRight());
            vectors.Add(this.CornerBottonLeft());
            vectors.Add(this.CornerTopLeft());
            this.verTices = vectors;

            this.canfill = true;
            this.cansetlines = true;
        }
        public void casa()
        {

            this.getDimentions();
            Vector2 CornerBottonLeft = this.CornerBottonLeft();
            Vector2 CenterRight = this.CenterRight();
            Vector2 CornerBottonRight = this.CornerBottonRight();
            Vector2 CenterLeft = this.CenterLeft();
            Vector2 CenterTop = this.CenterTop();
            List<Vector2> temp = new List<Vector2>();
            //frame
            temp.Add(CenterRight);//0
            temp.Add(CornerBottonRight);//1
            temp.Add(CornerBottonLeft);//2
            temp.Add(CenterLeft);//3
            //roof
            temp.Add(CenterTop);//4
            temp.Add(CenterRight);//5
            temp.Add(CenterLeft);//6
            //floor
            var ccenterleft = this.centerXY(CornerBottonLeft, CenterLeft);
            var ccenterright = this.centerXY(CenterRight, CornerBottonRight);
            temp.Add(ccenterleft);//7
            temp.Add(ccenterright);//8

            this.verTices = temp;

            this.canfill = true;
            this.cansetlines = true;
        }
        public void chimney(Vector2 top, Vector2 botton)
        {



            var roofcenter = this.centerXY(top, botton);
            var roofcenterup = this.centerXY(top, roofcenter);
            var roofcenterdown = this.centerXY(botton, roofcenter);
            List<Vector2> temp = new List<Vector2>();
            temp.Add(roofcenterdown);//0
            temp.Add(new Vector2(roofcenterdown.X, top.Y));//1
            temp.Add(new Vector2(roofcenterup.X, top.Y));//2
            temp.Add(roofcenterup);//3

            this.verTices = temp;
            this.canfill = true;
            this.cansetlines = true;
        }



        public void House()
        {

            this.getDimentions();
            int x1 = this.P1X;
            int y1 = this.P1Y;
            int x2 = this.P2X;
            int y2 = this.P2Y;
            //placearooft start
            Vector2 leftroof;
            Vector2 rightroof;
            int distance = x2-x1;
            Vector2 EditStart = new Vector2(x1, y1);
            Vector2 EditEnd = new Vector2(x2, y2);
            Vector2 Vcenter = new Vector2(((EditStart.X + EditEnd.X) / 2), ((EditStart.Y + EditEnd.Y) / 2));
            Vector2 VcenterTopRoof = new Vector2(((EditStart.X + EditEnd.X) / 2), (((EditStart.Y + EditEnd.Y) / 2) - 15));
            Vector2 Vtopcenter = new Vector2(((EditStart.X + EditEnd.X) / 2), EditStart.Y);
            Vector2 Vbottoncenter = new Vector2(((EditStart.X + EditEnd.X) / 2), EditEnd.Y);
            Vector2 Vrightcenter = new Vector2(EditStart.X, ((EditStart.Y + EditEnd.Y) / 2));
            Vector2 Vleftcenter = new Vector2(EditEnd.X, ((EditStart.Y + EditEnd.Y) / 2));
            int vcX = (int)Vcenter.X;
            int vcY = (int)Vcenter.Y;
            //center top roof triangle out side square
            int vctRX = (int)VcenterTopRoof.X;
            int vctRY = (int)VcenterTopRoof.Y;
            //top center inside square
            int tcX = (int)Vtopcenter.X;
            int tcY = (int)Vtopcenter.Y;

            //botton center inside square
            int btcX = (int)Vbottoncenter.X;
            int btcY = (int)Vbottoncenter.Y;

            //left center inside square
            int rcX = (int)Vrightcenter.X;
            int rcY = (int)Vrightcenter.Y;

            //right center inside square
            int lcX = (int)Vleftcenter.X;
            int lcY = (int)Vleftcenter.Y;
            #region walls
          
          
                for (int x = 0; x <= distance; x++)
                {

                    leftroof = new Vector2(x1 + x, y1 - x);
                    rightroof = new Vector2(x2 - x, y1 - x);
                    int leftstopitX = (int)leftroof.X;
                    int rightstopitX = (int)rightroof.X;
                    int leftstopitY = (int)leftroof.Y;
                    int rightstopitY = (int)rightroof.Y;
                    for (int i = leftstopitX; i <= rightstopitX; i++)
                    {
                        for (int j = leftstopitY; j <= rightstopitY; j++)
                        {
                            Mod.placetilesandwalls(false, i, j, Mod.wallitem.createWall, Mod.wallitem.placeStyle, true, true);
                        }
                    }
                }
          
            #endregion
            #region roof
            for (int x = 0; x < distance; x++)
            {

                leftroof = new Vector2(x1 + x, y1 - x);
                rightroof = new Vector2(x2 - x, y1 - x);
                int leftstopitX = (int)leftroof.X;
                int rightstopitX = (int)rightroof.X;
                int leftstopitY = (int)leftroof.Y;
                int rightstopitY = (int)rightroof.Y;
                //left roof     
                Mod.placetilesandwalls(true, leftstopitX, leftstopitY - 1, Mod.tileitem.createTile, Mod.tileitem.placeStyle, true, true);
                Mod.placetilesandwalls(true, leftstopitX, leftstopitY - 2, Mod.tileitem.createTile, Mod.tileitem.placeStyle, true, true);
                //right roof
                Mod.placetilesandwalls(true, rightstopitX, rightstopitY - 1, Mod.tileitem.createTile, Mod.tileitem.placeStyle, true, true);
                Mod.placetilesandwalls(true, rightstopitX, rightstopitY - 2, Mod.tileitem.createTile, Mod.tileitem.placeStyle, true, true);
                if ((leftstopitX == rightstopitX) || (leftstopitX + 1 == rightstopitX) || (leftstopitX == rightstopitX + 1))
                {
                    break;
                }
            }

            
            #endregion

            #region frame
     
            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {


                    if (((i == x1) || (i == x2)) || (j == y2))
                    {

                        Mod.placetilesandwalls(true, i, j, Mod.tileitem.createTile, Mod.tileitem.placeStyle, true, true);
                    }
                }
            }

            x1++;
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    Mod.placetilesandwalls(false, i, j, Mod.wallitem.createWall, Mod.wallitem.placeStyle, true, true);
                }
            }


            #endregion
            x1 = this.P1X;
            y1 = this.P1Y;
            x2 = this.P2X;
            y2 = this.P2Y; 

  
            x1++;
            x2--;
            y1++;
            y2--;

 

             EditStart = new Vector2(x1, y1);
             EditEnd = new Vector2(x2, y2);
             Vcenter = new Vector2(((EditStart.X + EditEnd.X) / 2), ((EditStart.Y + EditEnd.Y) / 2));
             VcenterTopRoof = new Vector2(((EditStart.X + EditEnd.X) / 2), (((EditStart.Y + EditEnd.Y) / 2) - 15));
             Vtopcenter = new Vector2(((EditStart.X + EditEnd.X) / 2), EditStart.Y);
             Vbottoncenter = new Vector2(((EditStart.X + EditEnd.X) / 2), EditEnd.Y);
             Vrightcenter = new Vector2(EditStart.X, ((EditStart.Y + EditEnd.Y) / 2));
             Vleftcenter = new Vector2(EditEnd.X, ((EditStart.Y + EditEnd.Y) / 2));
             vcX = (int)Vcenter.X;
             vcY = (int)Vcenter.Y;
            //center top roof triangle out side square
             vctRX = (int)VcenterTopRoof.X;
             vctRY = (int)VcenterTopRoof.Y;
            //top center inside square
             tcX = (int)Vtopcenter.X;
             tcY = (int)Vtopcenter.Y;

            //botton center inside square
             btcX = (int)Vbottoncenter.X;
             btcY = (int)Vbottoncenter.Y;

            //left center inside square
             rcX = (int)Vrightcenter.X;
             rcY = (int)Vrightcenter.Y;

            //right center inside square
             lcX = (int)Vleftcenter.X;
              lcY = (int)Vleftcenter.Y;
              #region levels
              int d = x2 - x1;
              for (int x = 0; x <= d; x++)
              {
                  int right = (x2 - x);
                  int left = (x1 + x);
                  Mod.placetilesandwalls(true, left, y1, 19, Main.recipe[19].createItem.placeStyle, true, true);
                  Mod.placetilesandwalls(true, right, rcY, 19, Main.recipe[19].createItem.placeStyle, true, true);
                 
              }

 


              #endregion



            #region goodies


            //places table
            Mod.placetilesandwalls(true, btcX, btcY, 14, Main.recipe[14].createItem.placeStyle, false, true);
            //places chair
            Mod.placetilesandwalls(true, btcX + 2, btcY, 15, Main.recipe[15].createItem.placeStyle, false, true);
            //torch          
            Mod.placetilesandwalls(true, x1 - 2, rcY, 4, Main.recipe[4].createItem.placeStyle, false, true);
            //torch
            Mod.placetilesandwalls(true, x2 + 2, lcY, 4, Main.recipe[4].createItem.placeStyle, false, true);
            //bed
            Mod.placetilesandwalls(true, x1 + 1, lcY - 1, 79, Main.recipe[79].createItem.placeStyle, false, true);
            //chest
            Mod.placetilesandwalls(true, tcX, tcY - 1, 21, Main.recipe[21].createItem.placeStyle, false, true);
            //pot
            Mod.placetilesandwalls(true, tcX - 2, tcY - 1, 28, Main.recipe[28].createItem.placeStyle, false, true);
            //clay pot
            Mod.placetilesandwalls(true, btcX, btcY - 2, 78, Main.recipe[78].createItem.placeStyle, false, true);
            //workbench
            Mod.placetilesandwalls(true, vcX, vcY - 1, 18, Main.recipe[18].createItem.placeStyle, false, true);
            //Furnace
            Mod.placetilesandwalls(true, vcX + 3, vcY - 1, 17, Main.recipe[17].createItem.placeStyle, false, true);
            //anvil
            Mod.placetilesandwalls(true, vcX + 5, vcY - 1, 16, Main.recipe[16].createItem.placeStyle, false, true);
            //left door 
            #endregion
 
            WorldGenReflect.KillTile(x2 + 1, y2, false, false, true);
            if (Main.netMode == 1)
            {
                Mod.mySendTileSquare(-1, x2 + 1, y2, 1);
            }
            WorldGenReflect.KillTile(x2 + 1, y2 - 1, false, false, true);
            if (Main.netMode == 1)
            {
                Mod.mySendTileSquare(-1, x2 + 1, y2 - 1, 1);
            }
            WorldGenReflect.KillTile(x2 + 1, y2 - 2, false, false, true);
            if (Main.netMode == 1)
            {
                Mod.mySendTileSquare(-1, x2 + 1, y2 - 2, 1);
            }
            Mod.placetilesandwalls(true, x2 + 1, y2, 10, Main.recipe[10].createItem.placeStyle, false, true);
            //left sign
            Mod.placetilesandwalls(true, x2 + 2, y2 - 5, 55, Main.recipe[55].createItem.placeStyle, false, true);
            WorldGenReflect.KillTile(x1 - 1, y2, false, false, true);
            if (Main.netMode == 1)
            {
                Mod.mySendTileSquare(-1,  x1 - 1, y2, 1);
            }

            WorldGenReflect.KillTile( x1 - 1, y2 - 1, false, false, true);
            if (Main.netMode == 1)
            {
                Mod.mySendTileSquare(-1, x1 - 1, y2 - 1, 1);
            }
            WorldGenReflect.KillTile(x1 - 1, y2 - 2, false, false, true);
            if (Main.netMode == 1)
            {
                Mod.mySendTileSquare(-1, x1 - 1, y2 - 2, 1);
            }
            Mod.placetilesandwalls(true, x1 - 1, y2, 10, Main.recipe[10].createItem.placeStyle, false, true);
            //right sign
            Mod.placetilesandwalls(true, x1 - 2, y2 - 5, 55, Main.recipe[55].createItem.placeStyle, false, true);

        }













        #endregion
        #region update

        public void Update()
        {

            if (!this.fill)
            {
                if (this.activeFill.Count > 0)
                {
                    this.activeFill.Clear();

                }
            }
        }



        public void MouseIntercepsPoint()
        {
            if (!this.indexlocked)
            {
                Vector2 mouseMap = new Vector2((int)Decimal.Truncate((int)(((float)Main.mouseState.X + Main.screenPosition.X))), (int)Decimal.Truncate((int)(((float)Main.mouseState.Y + Main.screenPosition.Y))));

                for (int i = 0; i < this.verTices.Count; i++)
                {
                    Rectangle mouserecmap = new Rectangle((int)mouseMap.X, (int)mouseMap.Y, 32, 32);
                    Rectangle verindex = new Rectangle((int)this.verTices[i].X * 16, (int)this.verTices[i].Y * 16, 32, 32);
                    if (verindex.Intersects(mouserecmap))
                    {

                        this.mouseOnListindex = i;
                        this.indexlocked = true;
                        break;
                    }

                }
            }
        }
        public void Mousewheelrotate()
        {

        
                if (Main.mouseState.ScrollWheelValue > this.oldwheel)
                {
                    if (!this.onwindow)
                    {
                        this.RotateVertices(+(this.Rotation));
                    }
                    else
                    {
                        if (Mod.toolWindow.brush.Visible && Mod.toolWindow.brush.freeStyle.Visible)
                        {
                            Mod.toolWindow.brush.freeStyle.Selsize.Plus_Clicked();


                        }
                    }


                    this.oldwheel = Main.mouseState.ScrollWheelValue;


                }
                else
                {
                    if (Main.mouseState.ScrollWheelValue < this.oldwheel)
                    {
                        if (!this.onwindow)
                        {
                            this.RotateVertices(-(this.Rotation));
                        }
                        else
                        {
                            if (Mod.toolWindow.brush.Visible && Mod.toolWindow.brush.freeStyle.Visible)
                            {
                                Mod.toolWindow.brush.freeStyle.Selsize.Minus_Clicked();


                            }
                        }
                        this.oldwheel = Main.mouseState.ScrollWheelValue;

                    }
                }
            
        }

           public void invertX()
        {
            var c = this.Center();
            for (int i = 0; i < this.verTices.Count; i++)
            {
                var ver = this.verTices[i];

                var dis = Vector2.Negate(ver);
                this.verTices[i] = dis;
            }
        }
           public void invertY()
        {
            for (int i = 0; i < this.verTices.Count; i++)
            {
                var ver = this.verTices[i];
                ver.Y -= this.verTices[i].Y * 2;
                this.verTices[i] = ver;
            }
        }

        
        #endregion
        #region tilecopies update
        public void fillin(bool force)
        {
            if ((this.fill && this.canfill) || (force && this.canfill))
            {
                this.getDimentions();
                List<Point> activeFillTemp = new List<Point>();
                for (int i = this.P1X; i <= this.P2X; i++)
                {
                    for (int j = this.P1Y; j <= this.P2Y; j++)
                    {

                        if (this.IsPointInPolygon((int)Math.Round((float)i), (int)Math.Round((float)j)))
                        {

                            activeFillTemp.Add(new Point((int)Math.Round((float)i), (int)Math.Round((float)j)));
                          
                        }
                    }
                }
                this.activeFill = activeFillTemp;
             



             this.canfill = false;

            }
        }
    
        public void setlines()
        {
            if (this.cansetlines)
            {
               // this.activeFill.Clear();
                this.activeLine.Clear();
                for (int i = 0; i < this.verTices.Count; i++)
                {
                    if (i > 0)
                    {


                        SetLine2(this.verTices[i - 1], this.verTices[i]);
                       
               
                    }
                }
                this.cansetlines = false;
            }
        }



        public void SetLine2(Vector2 point1, Vector2 point2)
        {
            float distance = Vector2.Distance(point1, point2);
            Vector2 direction = ((point2 - point1) / distance);
            direction.Normalize();
            float passedDist = 0;
            while (passedDist < distance)
            {
                point1 += direction;
                passedDist += 1;
                this.activeLine.Add(new Point((int)(Math.Round(point1.X)), (int)(Math.Round(point1.Y))));
            }
        }

        public void SetLine(Vector2 point1, Vector2 point2)
        {
            float distance = Vector2.Distance(point1, point2);
            if (distance != 0.0f)
            {
                Vector2 direction = ((point2 - point1) / distance);
                direction.Normalize();
                for (int z = 0; z < distance; z++)
                {
                    Vector2 point = (point1 + direction * (distance * (z / distance)));
                    this.activeLine.Add(new Point((int)(Math.Round(point.X)), (int)(Math.Round(point.Y))));
                }
            }
        }


        public void Line()
        {
            if (this.cansetlines)
            {
                this.activeFill.Clear();
                this.activeLine.Clear();
                for (int i = 0; i < this.verTices.Count; i++)
                {
                    if (i > 0)
                    {
                        int x = Math.Min((int)this.verTices[i - 1].X, (int)this.verTices[i].X);
                        int y = Math.Min((int)this.verTices[i - 1].Y, (int)this.verTices[i].Y);
                        int width = Math.Abs((int)this.verTices[i - 1].X - (int)this.verTices[i].X);
                        int height = Math.Abs((int)this.verTices[i - 1].Y - (int)this.verTices[i].Y);
                        int trueX = (int)this.verTices[i - 1].X;
                        int trueY = (int)this.verTices[i - 1].Y;
                        int trueX2 = (int)this.verTices[i].X;
                        int trueY2 = (int)this.verTices[i].Y;
                        if (x == trueX)
                        {

                            if (y == trueY)
                            {

                                for (int y2 = 0; y2 <= height; y2++)
                                {

                                    for (int x2 = 0; x2 <= width; x2++)
                                    {

                                        if (Math.Abs(-(height / (double)width) * (x2) + y2) / Math.Sqrt(Math.Pow(height / (double)width, 2) + 1) <= selSize / 2)
                                        {


                                            //   x + x2, y + y2
                                            this.activeLine.Add(new Point(x + x2, y + y2));


                                        }

                                    }

                                }

                            }
                            else
                            {

                                for (int y2 = 0; y2 <= height; y2++)
                                {

                                    for (int x2 = 0; x2 <= width; x2++)
                                    {

                                        if (Math.Abs(-(-height / (double)width) * (x2) + y2 - height) / Math.Sqrt(Math.Pow(-height / (double)width, 2) + 1) <= selSize / 2)
                                        {


                                            this.activeLine.Add(new Point(x + x2, y + y2));



                                        }

                                    }

                                }

                            }

                        }
                        else
                        {

                            if (y == trueY)
                            {

                                for (int y2 = 0; y2 <= height; y2++)
                                {

                                    for (int x2 = 0; x2 <= width; x2++)
                                    {

                                        if (Math.Abs(-(height / -(double)width) * (x2 - width) + y2) / Math.Sqrt(Math.Pow(height / -(double)width, 2) + 1) <= selSize / 2)
                                        {

                                            this.activeLine.Add(new Point(x + x2, y + y2));


                                        }

                                    }

                                }

                            }
                            else
                            {

                                for (int y2 = 0; y2 <= height; y2++)
                                {

                                    for (int x2 = 0; x2 <= width; x2++)
                                    {

                                        if (Math.Abs(-(-height / -(double)width) * (x2 - width) + y2 - height) / Math.Sqrt(Math.Pow(-height / -(double)width, 2) + 1) <= selSize / 2)
                                        {

                                            this.activeLine.Add(new Point(x + x2, y + y2));

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        
















        
        #endregion












        internal void Clear()
        {
            this.verTices.Clear();
           this.activeFill.Clear();
           this.activeLine.Clear();
            Mod.tilecopies = new List<Tiles>();
            Mod.stopVectoring = false;
           this.cansetlines = true;
           this.canpaste = false;
        }

        internal void SetShape()
        {
           this.canfill = true;
            Mod.stopVectoring = true;
            this.cansetlines = false;
        }

        public bool canpaste { get; set; }

        public bool canfill { get; set; }

        public bool fill { get; set; }





    }
}