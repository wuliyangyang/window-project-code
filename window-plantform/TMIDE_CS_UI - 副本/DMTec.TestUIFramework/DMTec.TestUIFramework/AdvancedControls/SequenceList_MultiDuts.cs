using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.BasicControls
{
    public class SequenceList_MultiDuts
    {

    }

    /// <summary>
    /// The Info Data of One Test Sequence(TestPlan) Item.
    /// </summary>
    public class TestSequenceItemInfo
    {
        /// <summary>
        /// The Group Property of Test Plan Item.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// The Identification Property of Test Plan Item.
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// The Description Property of Test Plan Item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Function Name Property of Test Plan Item.
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// The Timeout Property of Test Plan Item.
        /// </summary>
        public string Timeout { get; set; }

        /// <summary>
        /// The First Param Property of Test Plan Item.
        /// </summary>
        public string Param1 { get; set; }

        /// <summary>
        /// The Second Param Property of Test Plan Item.
        /// </summary>
        public string Param2 { get; set; }

        /// <summary>
        /// The Unit Property of Test Plan Item.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// The Low Limit Property of Test Plan Item.
        /// </summary>
        public string Low { get; set; }

        /// <summary>
        /// The High Limit Property of Test Plan Item.
        /// </summary>
        public string High { get; set; }

        /// <summary>
        /// The Key Property of Test Plan Item.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The Value Property of Test Plan Item.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// New One Test Sequence Item Data Info Class.
        /// </summary>
        public TestSequenceItemInfo()
        {

            Group = "";
            Tid = "";
            Description = "";
            Function = "";
            Timeout = "";
            Param1 = "";
            Param2 = "";
            Unit = "";
            Low = "";
            High = "";
            Key = "";
            Value = "";
        }

    }

    public class TestDataListItemInfo : TestSequenceItemInfo
    {
        /// <summary>
        /// The Property of UUT's Count.
        /// </summary>
        public int UUTCount { get; private set; }

        /// <summary>
        /// The List of UUT Test Item Value.
        /// </summary>
        public List<string> UUTList { get; set; }

        public TestDataListItemInfo(int uutCount)
            :base()
        {
            UUTCount = (uutCount > 0) ? uutCount : 1;
            UUTList = new List<string>(UUTCount);

        }


    }

}
