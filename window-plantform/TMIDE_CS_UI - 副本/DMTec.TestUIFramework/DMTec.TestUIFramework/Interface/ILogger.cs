using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.Interface
{
    /// <summary>
    /// Interface that include LogInfo/LogWarn/LogError.
    /// </summary>
    [DescriptionAttribute("Interface that include LogInfo/LogWarn/LogError.")]
    public interface ILogger
    {
        void LogInfo(object sender, string info);

        void LogWarn(object sender, string warn);

        void LogError(object sender, string error);
    }
}
