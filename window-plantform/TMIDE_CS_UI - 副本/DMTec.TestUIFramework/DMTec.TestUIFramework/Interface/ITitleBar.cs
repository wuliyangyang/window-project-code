using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework
{
    /// <summary>
    /// Interface of TitleBar control which used in test project.
    /// </summary>
    public interface ITitleBar
    {
        ///
        string TitleString { get; set; }
        Color TitleColor { get; set; }
        bool IsShowTitle { get; set; }
        ///
        string VersionString { get; set; }
        Color VersionColor { get; set; }
        bool IsShowVersion { get; set; }
        ///
        string OperatorRole { get; set; }
        Color OperatorRoleColor { get; set; }
        bool IsShowOperatorRole { get; set; }
        ///
        string OperatorName { get; set; }
        Color OperatorNameColor { get; set; }
        bool IsShowOperatorName { get; set; }
        ///
    }
}
