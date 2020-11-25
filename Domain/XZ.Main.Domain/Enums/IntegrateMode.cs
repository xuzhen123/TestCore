using System;
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 集成方式。
    /// </summary>
    [Flags]
    public enum IntegrateMode
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 跳转
        /// </summary>
        [Description("跳转")]
        Redirect = 1,

        /// <summary>
        /// 后台接口
        /// </summary>
        [Description("后台接口")]
        Api = 2,

        /// <summary>
        /// 脚本弹窗
        /// </summary>
        [Description("脚本弹窗")]
        Dialog = 4,

        /// <summary>
        /// 托管支付
        /// </summary>
        [Description("托管支付")]
        HostedPayment = 8,

        /// <summary>
        /// 聚合支付
        /// </summary>
        [Description("聚合支付")]
        Aggregate = 16,
    }
}
