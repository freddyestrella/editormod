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
    public class XG_LabeledSlider : XG_Control
    {
        public XG_Label Label { get; protected set; }
        public XG_HSlider Slider { get; protected set; }
        public XG_Label ValueLabel { get; protected set; }
        public string ValueLabelFormat { get; set; }
        public float OldValue;
        public float INValue;
        public float Value { get { return Slider.Value; } set { Slider.Value = value; } }
        public float Scale { get { return Slider.Scale; } set { Slider.Scale = value; } }

        public XG_LabeledSlider(Rectangle rect, int labelWidth, string text, int valueLabelWidth, float value, float min, float max, XG_ClickedEvent clickedHandler = null)
            : base(rect, true)
        {
            ClickedHandler = clickedHandler;
            ValueLabelFormat = "F3";

            Rectangle partRect = rect;
            partRect.X = partRect.Y = 0; // parent relative

            partRect.Width = labelWidth;
            Label = new XG_Label(partRect, text, GUIAlignment.Right | GUIAlignment.VCenter);
            Children.Add(Label);

            partRect.X += partRect.Width + 1;
            partRect.Width = rect.Width - (labelWidth + valueLabelWidth + 2);
            Slider = new XG_HSlider(partRect, 0.0f);
            Slider.SetRange(value, min, max);
            Children.Add(Slider);

            partRect.X = Slider.Rectangle.X + Slider.Rectangle.Width + 1;
            partRect.Width = valueLabelWidth;
            ValueLabel = new XG_Label(partRect, "0.000");
            Children.Add(ValueLabel);
        }
        void NotifyClicked()
        {
            XG_ClickedEvent handler = ClickedHandler;
            if (handler != null)
            {
                handler(this);
               
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (this.Value != this.OldValue)
            {
                this.OldValue = this.Value;
                this.NotifyClicked();

            }
            ValueLabel.Text = Slider.Value.ToString(ValueLabelFormat);
            base.Update(gameTime);
        }

        public XG_ClickedEvent ClickedHandler { get; set; }
    }
}
