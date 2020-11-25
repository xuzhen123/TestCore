
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 商户状态。
    /// </summary>
    public enum MerchantStatus
    {
        /// <summary>
        /// 邀请中
        /// </summary>
        [Description("邀请中")]
        Inviting = 0,

        /// <summary>
        /// 等待审核
        /// </summary>
        [Description("待审核")]
        Pending = 1,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 2,

        /// <summary>
        /// 禁止交易
        /// </summary>
        [Description("禁止交易")]
        Prohibited = 3,

        /// <summary>
        /// 冻结
        /// </summary>
        [Description("冻结")]
        Freezed = 4
    }
}
