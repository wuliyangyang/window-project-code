using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class CustomAttribute : Attribute
    {
        public CustomAttribute(bool browsable)
        {
            this.Browsable = browsable;
        }
        public bool Browsable { get; set; }
    }
}
