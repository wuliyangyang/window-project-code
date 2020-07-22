using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using DMTec.TestUIFramework.DataModel;
using System.Drawing;
//
namespace DMTec.TestUIFramework.Common
{

    #region//Delegates//

    #region//Basic Delegates//

    public delegate void StringHandler(string str);

    public delegate void IntHandler(int val);

    public delegate void ColorHandler(Color c);

    public delegate void BoolHandler(bool b);

    #endregion//Basic Delegates...End//

    public delegate void MsgHandler(object sender, string msg);

    public delegate void TestResultHandler(string testerId, string chamberId, string dutSn, bool testResult);

    public delegate void ChamberEnableHandler(string testerId, string chamberId, bool isEnable);

    public delegate void TestInfoHandler(string testerId, string chamberId, string msg);

    public delegate void TestErrorHandler(string testerId, string chamberId, ErrorEventArgs arg);

    public delegate void ItemContinuousFailHandler(object sender, ContinuousItemFailArgs arg);

    public delegate void StatisticsClearHandler(object sender, EventArgs arg);

    #endregion//Delegates//

}
