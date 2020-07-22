using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.MessageCenter
{
    /// <summary>
    /// 
    /// </summary>
    [DescriptionAttribute("")]
    public enum InternalMessageTypes
    {
        [DescriptionAttribute("The msg will display on splash screen.")]
        SplashScreenMsg = 0,
        [DescriptionAttribute("The msg will be used when login level changed.")]
        LoginLevelChangedMsg,

    }
}
