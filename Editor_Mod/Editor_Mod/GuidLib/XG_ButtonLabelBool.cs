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
 //   public delegate void XG_ClickedEvent(XG_Control sender);
    public class XG_ButtonLabelBool : XG_Control
    {

        public bool _Value;
        public bool Value { get { return this._Value; } set { this._Value = value; } }
        public bool DisplayValue;
        public bool Selfdisplay;
        private bool Firstime;
        public XG_ButtonLabelBool(Rectangle rect, string text, XG_ClickedEvent clickedHandler,bool selfdisplay= false,bool firsttimer = false)
            : base(rect, true)
        {
            this.Firstime = firsttimer;
            Selfdisplay = selfdisplay;
            ClickedHandler = clickedHandler;
            Rectangle partRect = rect;
            partRect.X = partRect.Y = 0; // reset and relative to  relative




            Rectangle buttonSq = new Rectangle(partRect.X, partRect.Top + 25, 50, 20);
            this.button = new XG_Button(buttonSq, "", this.Button_Clicked);
            Children.Add(button);

 
            this.Valuetext = new XG_Label(new Rectangle(buttonSq.Right + 1, buttonSq.Center.Y, 0, 0), "");
            Children.Add(Valuetext);

            Children.Add(new XG_Label(new Rectangle(buttonSq.X, buttonSq.Y - 7, 0, 0), text));



        }
        public void Button_Clicked(XG_Control sender )
        {
            if (Firstime)
            {
                Selfdisplay = true;
                Firstime = false;
            }
            if (!Selfdisplay)
            {
                if (this.DisplayValue)
                {
                    this._Value = false;
                }
                else
                {
                    this._Value = true;
                }
            }
            else
             {
                 if (this._Value)
                {
                    this._Value = false;
                }
                else
                {
                    this._Value = true;
                }
            }

            if (sender != null)
            {
                if (sender.Enabled)
                {
                    this.NotifyClicked();
                }
            }
            else { this.NotifyClicked(); }

        }
 
        void NotifyClicked()
        {
            XG_ClickedEvent handler = ClickedHandler;
            if (handler != null)
            {
                handler(this);
                this.reset = true;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Selfdisplay)
            {
                this.DisplayValue = this.Value;
            }
            else { if (this.reset) { _Value = false; this.reset = false; } }
            this.button.Text = this.DisplayValue.ToString();
           
        }

        public XG_Button button { get; set; }
 

        public XG_ClickedEvent ClickedHandler { get; set; }

        public XG_Label Valuetext { get; set; }

        public bool reset { get; set; }
    }
}
