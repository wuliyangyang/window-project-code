using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.DataModel
{
    /// <summary>
    /// The internal message types.
    /// </summary>
    public enum InternalMessageType
    {
        #region//SystemLevelMessage(0~999)//

        [DescriptionAttribute("This type of message will show in splash screen when module loading.")]
        SYS_ModuleLoading = 0,
        //ToBeContinue...
        #endregion//SystemLevelMessage(0~999)//

        #region//SequencerRelatedMessage(1000~1999)//

        [DescriptionAttribute("This type of message load sequencer test started event data.")]
        SEQ_TestStart = 1000,
        [DescriptionAttribute("This type of message load sequencer test ended event data.")]
        SEQ_TestEnd,
        [DescriptionAttribute("This type of message load sequencer item start event data.")]
        SEQ_TestItemStart,
        [DescriptionAttribute("This type of message load sequencer item ended event data.")]
        SEQ_TestItemEnd,
        [DescriptionAttribute("This type of message load sequencer attribute found event data.")]
        SEQ_AttributeFound,
        [DescriptionAttribute("This type of message load sequencer report error event data.")]
        SEQ_ReportError,
        [DescriptionAttribute("This type of message load sequencer uop detected event data.")]
        SEQ_UopDectec,
        [DescriptionAttribute("This type of message load sequencer item listed event data.")]
        SEQ_ItemList,
        [DescriptionAttribute("This type of message load sequencer heartbeat event data.")]
        SEQ_Heartbeat,
        //ToBeContinue...
        #endregion//SequencerRelatedMessage(1000~1999)//

        #region//StateMachineRelatedMessage(2000~2999)//

        [DescriptionAttribute("This type of message load state machine idle event data.")]
        SM_State_Idle,
        [DescriptionAttribute("This type of message load state machine load event data.")]
        SM_State_Load,
        [DescriptionAttribute("This type of message load state machine testing event data.")]
        SM_State_Testing,
        [DescriptionAttribute("This type of message load state machine test done event data.")]
        SM_State_TestDone,
        [DescriptionAttribute("This type of message load state machine unload event data.")]
        SM_State_Unload,
        [DescriptionAttribute("This type of message load state machine error event data.")]
        SM_State_Error,
        [DescriptionAttribute("This type of message load state machine not test event data.")]
        SM_NoTest,
        [DescriptionAttribute("This type of message load state machine not test fail count event data.")]
        SM_NoTest_FailCnt,
        [DescriptionAttribute("This type of message load state machine not test fail load event data.")]
        SM_NoTest_FailLoad,
        [DescriptionAttribute("This type of message load state machine not test fail csv event data.")]
        SM_NoTest_FailCsv,
        [DescriptionAttribute("This type of message load state machine not test fail req event data.")]
        SM_NoTest_FailReq,
        [DescriptionAttribute("This type of message load state machine not test fail unknown event data.")]
        SM_NoTest_FailUnknow,

        //ToBeContinue...
        #endregion//StateMachineRelatedMessage(2000~2999)//

        #region//TestEngineRelatedMessage(3000~3999)//
        //ToBeContinue...

        #endregion//TestEngineRelatedMessage(3000~3999)//

    }

}
