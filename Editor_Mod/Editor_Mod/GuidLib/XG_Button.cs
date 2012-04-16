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
    public delegate void XG_ClickedEvent(XG_Control sender);

    public class XG_Button : XG_Control
    {
        XG_ClickedEvent ClickedHandler = null;

        public XG_Button(Rectangle rect, string text, XG_ClickedEvent clickedHandler)
            : base(rect, true)
        {
            Text = text;
            ClickedHandler = clickedHandler;
            Alignment = GUIAlignment.HCenter | GUIAlignment.VCenter;
        }

        public override void Update(GameTime gameTime)
        {
            float frameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (hiliteTime > 0.0f)
                hiliteTime -= frameTime;

            if (XnaGUIManager.GetFocusControl() == this)
            {
                if (XnaGUIManager.mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (this.Contains(new Point(XnaGUIManager.mouseState.X, XnaGUIManager.mouseState.Y)))
                    {
                        this.Pressingbutton = true;
                        hiliteTime = 0.1f; // Active down over button
                    }
                    else
                    {
                      //  this.Pressingbutton = false;
                        hiliteTime = 0.0f; // moved off of button
                    }
                }
                else if (XnaGUIManager.prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (this.Contains(new Point(XnaGUIManager.mouseState.X, XnaGUIManager.mouseState.Y)))
                    {
                        // Clicked
                        this.Pressingbutton = true;
                        hiliteTime = 0.0f;
                        NotifyClicked();
                    }
                    else {// this.Pressingbutton = false;
                    }
                }
                if (XnaGUIManager.mouseState.LeftButton != ButtonState.Pressed)
                {
                    this.Pressingbutton = false;
                }
                if (KeyJustPressed(Keys.Space, false, false, false))
                {
                   // hiliteTime = 0.1f;
                 //   NotifyClicked();
                }
            }

            base.Update(gameTime);
        }

        void NotifyClicked()
        {
            XG_ClickedEvent handler = ClickedHandler;
            if (handler != null)
                handler(this);
        }

        float hiliteTime = 0.0f;
        public bool Pressingbutton;

        public override void Draw(float frameTime)
        {
            Rectangle rect = ToScreen(Rectangle);

            Color color = ThisControlColor;
            if (hiliteTime > 0.0f)
                color = ControlDarkColor;

            DrawBorder(rect, 2, color, true);
            Rectangle fillRect = rect;
            fillRect.Inflate(-2, -2);
            XnaGUIManager.spriteBatch.Draw(XnaGUIManager.WhiteRect, fillRect, ControlDarkColor);

            Vector2 textPos = GetTextPosition();

            XnaGUIManager.spriteBatch.DrawString(XnaGUIManager.spriteFont, Text, textPos, ThisForeColor);

            base.Draw(frameTime);
        }
    }
}
