using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.DataModel
{
    /// <summary>
    /// The enum of DUT Test Status.
    /// </summary>
    public enum DutTestStatus
    {
        [DescriptionAttribute("IDLE")]
        IDLE = 1,
        [DescriptionAttribute("Testing")]
        Testing = 2,
        [DescriptionAttribute("TestDone")]
        TestDone = 3,
        [DescriptionAttribute("TestFail")]
        TestFail = 4,
        [DescriptionAttribute("TestPass")]
        TestPass = 5

    }

    /// <summary>
    /// The enum of LED Status.
    /// </summary>
    public enum LedState
    {
        [DescriptionAttribute("OK")]
        OK = 0,
        [DescriptionAttribute("ERROR")]
        ERROR = 1,
        [DescriptionAttribute("WARN")]
        WARN = 2
    }



    /// <summary>
    /// ErrorCode.
    /// </summary>
    public enum ErrorCode
    {
        //Sequencer Related...
        [Description("Sequencer HeartBeat Lost")]
        ERR_SEQ_HB_LOST = 501,
        [Description("Sequence End Lost")]
        ERR_SEQ_END_LOST = 502,
        [Description("Sequence End OverTime")]
        ERR_SEQ_END_OVERTIME = 503,

        //StateMachine Related...
        [Description("StateMachine HeartBeat Lost")]
        ERR_SM_HB_LOST = 511,
        [Description("Can not start test because stateMachine connect to MTCP failed.")]
        ERR_SM_NOT_TEST_FAIL_CNT = 512,
        [Description("Can not start test because sequencer load CSV failed.")]
        ERR_SM_NOT_TEST_FAIL_LOAD = 513,
        [Description("Can not start test because create CSV failed.")]
        ERR_SM_NOT_TEST_FAIL_CREATE_CSV = 514,
        [Description("Can not start test because failed to request CSV.")]
        ERR_SM_NOT_TEST_FAIL_REQ = 515,
        [Description("Can not start test because stateMachine's unknown error.")]
        ERR_SM_NOT_TEST_FAIL_UNKNOW = 516,

        //Engine Related...
        [Description("Engine HeartBeat Lost")]
        ERR_EGN_HB_LOST = 521,

    }


    public enum ChamberType
    {
        [Description("C1-1")]
        C11 = 1,
        [Description("C4-1")]
        C41 = 2,
        [Description("C4-2")]
        C42 = 3,
        [Description("C5")]
        C5 = 4,
        [Description("C6")]
        C6 = 5
    }

    public enum TesterType
    {
        [Description("IQC")]
        IQC = 1,
        [Description("OQC")]
        OQC = 2
    }

    public enum ZSocketType
    {
        [Description("Publisher")]
        PUB = 1,
        [Description("Subscriber")]
        SUB = 2,
        [Description("Requester")]
        REQ = 3,
        [Description("Replier")]
        REP = 4
    }

    public enum DutTestResult
    {
        [Description("Pass")]
        PUB = 1,
        [Description("Fail")]
        SUB = 2,
        [Description("Untest")]
        REQ = 3,
        [Description("Unknown")]
        REP = 4
    }

    /// <summary>
    /// Self defined MessageBox type.
    /// </summary>
    public enum MessageBoxType
    {
        [Description("Information")]
        Info = 0,
        [Description("Warning")]
        Warn = 1,
        [Description("Error")]
        Error = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public enum OperatorLevels
    {
        [Description("Operator")]
        Operator = 0,
        [Description("Administrator")]
        Administrator = 1,
        [Description("Engineer")]
        Engineer = 2,
        //             [Description("SuperAdministrator")]
        //             SuperAdministrator = 3,
    }
}
