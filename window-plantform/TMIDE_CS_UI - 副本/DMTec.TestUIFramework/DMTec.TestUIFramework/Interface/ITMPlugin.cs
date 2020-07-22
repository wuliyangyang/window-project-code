using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.Interface
{
    public interface ITMPlugin
    {

        /// <summary>
        /// Load plugin and  do some action related.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        int LoadPlugin(object sender, object arg);

        /// <summary>
        /// Unload plugin and do some action related.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        int UnloadPlugin(object sender, object arg);

        /// <summary>
        /// Register plugin and do some action related.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        int RegisterPlugin(object sender, object arg);

        /// <summary>
        /// Do some test action after loading plugin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        int SelfTestPlugin(object sender, object arg);

        /// <summary>
        /// Do some initialized action after loading plugin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        int InitPlugin(object sender, object arg);

    }
}
