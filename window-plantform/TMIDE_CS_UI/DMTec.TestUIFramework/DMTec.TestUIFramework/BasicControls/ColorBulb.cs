﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// The ColorBulb is a .Net control for Windows Forms that emulates an
    /// Color light with two states On and Off.  The purpose of the control is to 
    /// provide a sleek looking representation of an Color light that is sizable, 
    /// has a transparent background and can be set to different colors.  
    /// </summary>
    public partial class ColorBulb : UserControl
    {

        #region//Members//

        private Color _color;
        private bool _on = true;
        //private Color _reflectionColor = Color.FromArgb(180, 255, 255, 255);
        //private Color[] _surroundColor = new Color[] { Color.FromArgb(0, 255, 255, 255) };
        private Color _reflectionColor = Color.Transparent;
        private Color[] _surroundColor = new Color[] { Color.Transparent };

        private Timer _timer = new Timer();

        #region//Properties//

        /// <summary>
        /// Gets or Sets the color of the LED light
        /// </summary>
        [DefaultValue(typeof(Color), "153, 255, 54")]
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                this.DarkColor = _color;//ControlPaint.Dark(_color);
                this.DarkDarkColor = _color;// ControlPaint.DarkDark(_color);
                this.Invalidate();	// Redraw the control
            }
        }

        /// <summary>
        /// Dark shade of the LED color used for gradient
        /// </summary>
        public Color DarkColor { get; protected set; }

        /// <summary>
        /// Very dark shade of the LED color used for gradient
        /// </summary>
        public Color DarkDarkColor { get; protected set; }

        /// <summary>
        /// Gets or Sets whether the light is turned on
        /// </summary>
        public bool On
        {
            get { return _on; }
            set { _on = value; this.Invalidate(); }
        }

        #endregion//Properties...End//

        #endregion//Members...End//

        #region//Constructor//

        public ColorBulb()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer
            | ControlStyles.AllPaintingInWmPaint
            | ControlStyles.ResizeRedraw
            | ControlStyles.UserPaint
            | ControlStyles.SupportsTransparentBackColor, true);

            this.Color = Color.FromArgb(255, 153, 255, 54);
            _timer.Tick += new EventHandler(
                (object sender, EventArgs e) => { this.On = !this.On; }
            );
        }

        #endregion//Constructor...End//

        #region//Methods

        /// <summary>
        /// Handles the Paint event for this UserControl
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create an off screen graphics object for double buffering
            Bitmap offScreenBmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            using (System.Drawing.Graphics g = Graphics.FromImage(offScreenBmp))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                // Draw the control
                drawControl(g, this.On);
                // Draw the image to the screen
                e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
            }
        }

        /// <summary>
        /// Renders the control to an image
        /// </summary>
        private void drawControl(Graphics g, bool on)
        {
            // Is the bulb on or off
            Color lightColor = (on) ? this.Color : Color.Silver;//Color.FromArgb(150, this.DarkColor);
            Color darkColor = (on) ? this.DarkColor : Color.Transparent;//this.DarkDarkColor;

            // Calculate the dimensions of the bulb
            int width = this.Width - (this.Padding.Left + this.Padding.Right);
            int height = this.Height - (this.Padding.Top + this.Padding.Bottom);
            // Diameter is the lesser of width and height
            int diameter = Math.Min(width, height);
            // Subtract 1 pixel so ellipse doesn't get cut off
            diameter = Math.Max(diameter - 1, 1);

            // Draw the background ellipse
            var rectangle = new Rectangle(this.Padding.Left, this.Padding.Top, diameter, diameter);
            g.FillEllipse(new SolidBrush(darkColor), rectangle);

            // Draw the glow gradient
            var path = new GraphicsPath();
            path.AddEllipse(rectangle);
            var pathBrush = new PathGradientBrush(path);
            pathBrush.CenterColor = lightColor;
            //pathBrush.SurroundColors = new Color[] { Color.FromArgb(0, lightColor) };
            pathBrush.SurroundColors = new Color[] { Color.Transparent };

            g.FillEllipse(pathBrush, rectangle);

            // Draw the white reflection gradient
            var offset = Convert.ToInt32(diameter * .15F);
            var diameter1 = Convert.ToInt32(rectangle.Width * .8F);
            var whiteRect = new Rectangle(rectangle.X - offset, rectangle.Y - offset, diameter1, diameter1);
            var path1 = new GraphicsPath();
            path1.AddEllipse(whiteRect);
            var pathBrush1 = new PathGradientBrush(path);
            pathBrush1.CenterColor = _reflectionColor;
            pathBrush1.SurroundColors = _surroundColor;
            g.FillEllipse(pathBrush1, whiteRect);

            // Draw the border
            g.SetClip(this.ClientRectangle);
            if (this.On) g.DrawEllipse(new Pen(Color.FromArgb(85, Color.Black), 1F), rectangle);
        }

        /// <summary>
        /// Causes the Led to start blinking
        /// </summary>
        /// <param name="milliseconds">Number of milliseconds to blink for. 0 stops blinking</param>
        public void Blink(int milliseconds)
        {
            if (milliseconds > 0)
            {
                this.On = true;
                _timer.Interval = milliseconds;
                _timer.Enabled = true;
            }
            else
            {
                _timer.Enabled = false;
                this.On = false;
            }
        }

        #endregion
    
    }
}
