using System;
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 邮件回执。
    /// </summary>
    [Flags]
    public enum ReceiptTo
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 管理员
        /// </summary>
        [Description("商户管理员")]
        Administrator = 1,

        /// <summary>
        /// 业务处理权限
        /// </summary>
        [Description("具有相应业务处理权限的用户")]
        Operators = 2,

        /// <summary>
        /// 网站负责人
        /// </summary>
        [Description("对应支付应用的负责人")]
        AppOwners = 4
    }
}
