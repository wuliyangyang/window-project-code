using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    public class AutoSizeFontLabel : Label
    {
        // Original height before Dock property set
        int ciPreDockHeight;
        // Original Distance to Bottom - used when set to AnchorStyles.Bottom
        int ciOrigDistanceToBottom;

        public AutoSizeFontLabel():base()
        {
            this.Margin = new Padding(1);
            //this.Font = new Font(this.Font.FontFamily, 20);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F);
        }

        private bool IsNoNeedOverride()
        {
            return (null == Parent || Disposing);
        }

        private Font GetAutoSizeFont(int height, Font originalFont)
        {
            float targetHeight = (height < 8) ? (float)8 : (float)height;

            Font font = new Font(originalFont.FontFamily,
                                 originalFont.Size,
                                 originalFont.Style,
                                 GraphicsUnit.Pixel);

            float fontEmSize = font.FontFamily.GetEmHeight(font.Style);
            if (fontEmSize <= 0) fontEmSize = (float)8;

            float fontLineSpacing = font.FontFamily.GetLineSpacing(font.Style);
            if (fontLineSpacing <= 0) fontLineSpacing = (float)1;

            float emSize = (targetHeight - 7) * fontEmSize / fontLineSpacing;
            if (emSize <= 0) emSize = (float)20;

            font = new Font(font.FontFamily, emSize, font.Style, GraphicsUnit.Pixel);

            return font;
        }

        [System.ComponentModel.Category("Layout")]
        [System.ComponentModel.Description("Set the TextBox.Height.")]
        public int FlexibleHeight
        {
            get { return this.Height; }
            set
            {
                if (IsNoNeedOverride()) return;
                if (value != this.Height)
                {
                    this.Height = value;
                    ciOrigDistanceToBottom = Parent.ClientSize.Height + this.Top - value;
                    this.Font = GetAutoSizeFont(value, this.Font);
                }
            }
        }

        //public override DockStyle Dock
        //{
        //    get
        //    {
        //        return base.Dock;
        //    }
        //    set
        //    {
        //        if(false == IsNoNeedOverride())
        //        {
        //            // if this docking change should affect the height
        //            if ((value & DockStyle.Left) == DockStyle.Left ||
        //                (value & DockStyle.Right) == DockStyle.Right ||
        //                (value & DockStyle.Fill) == DockStyle.Fill)
        //            {
        //                // and if the base.dock is NOT ALREADY set to a height-adjusting
        //                // DockStyle, then get the original height.
        //                if ((base.Dock & DockStyle.Left) != DockStyle.Left &&
        //                    (base.Dock & DockStyle.Right) != DockStyle.Right &&
        //                    (base.Dock & DockStyle.Fill) != DockStyle.Fill)
        //                    ciPreDockHeight = Height;
        //            }
        //        }
        //        base.Dock = value;
        //    }
        //}

        //protected override void OnDockChanged(EventArgs e)
        //{
        //    // If the parent does not exist, we're set to multi-line
        //    // or we are disposing, do default
        //    if (IsNoNeedOverride())
        //    {
        //        base.OnDockChanged(e);
        //        return;
        //    }

        //    // if this docking change is bottom or none, set the height back to 
        //    // the original pre-dock value.
        //    if ((this.Dock & DockStyle.Bottom) == DockStyle.Bottom ||
        //        (this.Dock & DockStyle.None) == DockStyle.None)
        //    {
        //        this.Font = GetAutoSizeFont(ciPreDockHeight, this.Font);
        //    }
        //    base.OnDockChanged(e);
        //}


        //// Intercept OnParentChanged to set distance to bottom for Anchoring
        //// and add an event subscription to Parent.ClientSizeChanged
        //protected override void OnParentChanged(EventArgs e)
        //{
        //    // If the parent does not exist, we're set to multi-line
        //    // or we are disposing, do default
        //    if (IsNoNeedOverride())
        //    {
        //        base.OnDockChanged(e);
        //        return;
        //    }
        //    ciOrigDistanceToBottom = Parent.ClientSize.Height - this.Bottom;
        //    ciPreDockHeight = this.Height;
        //    Parent.ClientSizeChanged += new EventHandler(Parent_ClientSizeChanged);
        //    base.OnParentChanged(e);
        //}

        //// Event Handler for Parent.ClientSizeChanged
        //// If the parent size changes, we may need to adjust the textbox size
        //// if it is docked or anchored.
        //void Parent_ClientSizeChanged(object sender, EventArgs e)
        //{
        //    // If the parent does not exist, we're set to multi-line
        //    // or we are disposing, do default
        //    if (IsNoNeedOverride())
        //    {
        //        base.OnDockChanged(e);
        //        return;
        //    }

        //    if ((this.Dock & DockStyle.Left) == DockStyle.Left ||
        //        (this.Dock & DockStyle.Right) == DockStyle.Right ||
        //        (this.Dock & DockStyle.Fill) == DockStyle.Fill ||
        //        (this.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
        //        this.Font = GetAutoSizeFont(Parent.ClientSize.Height, this.Font);
        //}


        protected override void OnSizeChanged(EventArgs e)
        {
            if(IsNoNeedOverride())
            {
                base.OnSizeChanged(e); return;
            }

            //int height = this.Height;
            //switch (this.Dock)
            //{
            //    case DockStyle.Fill:
            //    case DockStyle.Left:
            //    case DockStyle.Right:
            //        height = Parent.ClientSize.Height;
            //        this.Font = GetAutoSizeFont(height, this.Font);
            //        break;
            //    default:
            //        // Check for Anchoring that should change the height.
            //        // If so, set the height based on the original distance to bottom.
            //        if ((this.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
            //        {
            //            height = Parent.ClientSize.Height - ciOrigDistanceToBottom;
            //            this.Font = GetAutoSizeFont(height, this.Font);
            //        }
            //        break;
            //}
            this.Font = GetAutoSizeFont(this.Height, this.Font);
            base.OnSizeChanged(e);
        }

    }
}
