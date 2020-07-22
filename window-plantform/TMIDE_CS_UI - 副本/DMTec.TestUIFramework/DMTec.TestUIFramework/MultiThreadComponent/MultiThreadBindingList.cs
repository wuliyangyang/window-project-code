using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// BindingList's Extension
    /// Use This Class You Can Operate BindingList To Binding UI Control Cross-thread.
    /// JimmyGong designed for the TestUIFramework.
    /// </summary>
    public class MultiThreadBindingList<T> : BindingList<T>
    {
        /// <summary>
        /// The MultiThread Context That Need Synchronization.
        /// </summary>
        public SynchronizationContext SynchronizationContext { get; set; }

        /// <summary>
        /// Use This Method To Do All The SynchronizationContext Operation.
        /// </summary>
        private void Excute(Action action, object state = null)
        {
            if (SynchronizationContext == null) action();
            else SynchronizationContext.Post(d => action(), state);
        }

        /// <summary>
        /// Add One New Item To BindingList.
        /// </summary>
        /// <param name="item">The New Item To Be Add.</param>
        public new void Add(T item)
        {
            Excute(() => base.Add(item));
        }

        /// <summary>
        /// Remove One Existing Item In BindingList.
        /// </summary>
        /// <param name="item">The Existing Item To Be Remove.</param>
        public new void Remove(T item)
        {
            Excute(() => base.Remove(item));
        }
    }
}
