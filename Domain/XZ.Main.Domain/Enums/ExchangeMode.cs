using System;
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 交互模式。
    /// </summary>
    [Flags]
    public enum ExchangeMode
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 同步
        /// </summary>
        [Description("同步")]
        Sync = 1,

        /// <summary>
        /// 异步
        /// </summary>
        [Description("异步")]
        Async = 2,
    }
}
