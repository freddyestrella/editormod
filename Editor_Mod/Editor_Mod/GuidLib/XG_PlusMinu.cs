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
    public class XG_PlusMinus  : XG_Control
    {

        public float _Value;
        public float Value { get { return this._Value; } set { this._Value = value; } }
        public float DisplayValue;
        public bool repeat;
        public float Step;
        public string stringValueFormat;
        public float Max;
        public float Min;
        public bool Selfdisplay;
   
        private bool Loop;
        public XG_PlusMinus(Rectangle rect, string text, XG_ClickedEvent clickedHandler, float step = 1f, bool Repeat = false, bool selfdisplay = false, float min = 0f, float max = 0f,float startAt =0f,string StringValueFormat = "F0",bool loop =false)
            : base(rect, true)
        {


             
                this.stringValueFormat = StringValueFormat;




            this._Value = startAt;
            this.Loop = loop;
            this.Selfdisplay = selfdisplay;
            this.Max = max;
            this.Min = min;
               
            this.Step = step;
            this.repeat = Repeat;

            
                if (this.Max == 0 && this.Min == 0)
                {
                
                // this.Loop= false;
                }

            ClickedHandler = clickedHandler;




            Rectangle partRect = rect;
            partRect.X = partRect.Y = 0; // reset and relative to  relative



 
            Rectangle minusSq = new Rectangle(partRect.X, partRect.Top + 25, 16, 16);
            this.ButtonMinus = new XG_Button(minusSq, "-", this.Minus_Clicked);
            Children.Add(ButtonMinus);

            Rectangle plusSq = new Rectangle(minusSq.Right + 16, minusSq.Y, 16, 16);
            this.ButtonPlus = new XG_Button(plusSq, "+", this.Plus_Clicked);
            Children.Add(ButtonPlus);


            this.Valuetext = new XG_Label(new Rectangle(plusSq.Right + 1, plusSq.Center.Y, 0, 0), ": ");
            Children.Add(Valuetext);
            LabelText = new XG_Label(new Rectangle(minusSq.X, minusSq.Y - 7, 0, 0), text);
            Children.Add(LabelText);



        }
      public  void Minus_Clicked(XG_Control sender = null)
        {
       
            if (!this.repeat)
            {
                if ((this.Max == 0 && this.Min == 0) || !(this._Value <= this.Min) || this.Loop)
                {

                    if (sender != null)
                    {
                        if (sender.Enabled)
                        {
                            this._Value -= this.Step;
                            this.NotifyClicked();
                        }
                    }
                    else
                    {
                        this._Value -= this.Step;
                        this.NotifyClicked();
                    }


                }
            }

        }
      public void Plus_Clicked(XG_Control sender = null)
        {
          
            if (!this.repeat)
            {
                if ((this.Max == 0 && this.Min == 0) || !(this._Value >= (this.Max - this.Step)) || this.Loop)
                {


                    if (sender != null)
                    {
                        if (sender.Enabled)
                        {
                            this._Value += this.Step;
                            this.NotifyClicked();
                        }
                    }
                    else
                    {
                        this._Value += this.Step;
                        this.NotifyClicked();
                    }
                }
            }

        }

        void NotifyClicked()
        {
            if (this.Loop)
            {
                if (this._Value > this.Max)
                {
                    this._Value = this.Min;
                }
                if (this._Value < this.Min)
                {
                    this._Value = this.Max;
                }

            }
                XG_ClickedEvent handler = ClickedHandler;
                if (handler != null)
                {
                    handler(this);
                     
                }
             
        }
        public override void Update(GameTime gameTime)
        {       base.Update(gameTime);
        if (!Selfdisplay)
        {
            this._Value = this.DisplayValue;

        }
        
             this.Valuetext.Text = ": " + this.Value.ToString(stringValueFormat);
        
            
            
            if (this.repeat)
            {

               
                   if (this.ButtonMinus.Pressingbutton)
                {
                    if ((this.Max == 0 && this.Min == 0) || !(this._Value <= this.Min)||this.Loop)
                    {
                        this._Value -= this.Step;
                        this.NotifyClicked();
                    }
                    this.ButtonMinus.Pressingbutton = false;
                }
                if (this.ButtonPlus.Pressingbutton)
                {
                    if ((this.Max == 0 && this.Min == 0) || !(this._Value >= (this.Max - this.Step)) || this.Loop)
                    {
                        this._Value += this.Step;
                        this.NotifyClicked();
                    }
                    this.ButtonPlus.Pressingbutton = false;
                }
             

            }
     
           
        }

        public XG_Button ButtonMinus { get; set; }
        public XG_Button ButtonPlus { get; set; }

        public XG_ClickedEvent ClickedHandler { get; set; }

        public XG_Label Valuetext { get; set; }




        public bool reset { get; set; }

        public XG_Label LabelText { get; set; }
    }
}
