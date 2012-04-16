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

namespace Editor_Mod
{
    public class XG_Panel : XG_Control
    {
        public bool Fill { get; set; }
        public bool DrawTopBorder { get; set; }
        private int borderThickness = 2;
        protected Rectangle _top;
        private Rectangle left, right, bottom;

        public XG_Panel(Rectangle rectangle)
            : base(rectangle, true)
        {
            Init();
        }

        public XG_Panel(Rectangle rectangle, bool canFocus)
            : base(rectangle, canFocus)
        {
            Init();
        }

        private void Init()
        {
            //BkgColor = new Color(0.0f, 0.0f, 0.0f, 0.5f); // transparent black
            Fill = true;
            DrawTopBorder = true;
        }

        public int BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }


        public override void Update(GameTime gameTime)
        {
            float frameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (hiliteTime > 0.0f)
                hiliteTime -= frameTime;

            base.Update(gameTime);
        }

        float hiliteTime = 0.0f;

        protected override void GetFocus()
        {
            hiliteTime = 0.1f;
        }

        public override void Draw(float frameTime)
        {
            if (Fill)
                XnaGUIManager.spriteBatch.Draw(XnaGUIManager.WhiteRect, ToScreen(Rectangle), BkgColor);

            Color bColor = ThisControlColor;

            //if (hiliteTime > 0.0f)
            //    bColor = ForeColor;


            if (DrawTopBorder)
            {
                _top = new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, borderThickness);
                XnaGUIManager.spriteBatch.Draw(XnaGUIManager.WhiteRect, ToScreen(_top), bColor);
            }

            left = new Rectangle(Rectangle.Left, Rectangle.Top + borderThickness, borderThickness, Rectangle.Height - (borderThickness * 2));
            right = new Rectangle(Rectangle.Right - borderThickness, Rectangle.Top + borderThickness, borderThickness, (Rectangle.Height - (borderThickness * 2)));
            bottom = new Rectangle(Rectangle.Left, Rectangle.Bottom - borderThickness, Rectangle.Width, borderThickness);

            XnaGUIManager.spriteBatch.Draw(XnaGUIManager.WhiteRect, ToScreen(left), bColor);
            XnaGUIManager.spriteBatch.Draw(XnaGUIManager.WhiteRect, ToScreen(right), bColor);
            XnaGUIManager.spriteBatch.Draw(XnaGUIManager.WhiteRect, ToScreen(bottom), bColor);

            base.Draw(frameTime);
        }
    }
}
