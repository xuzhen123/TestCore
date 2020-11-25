
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 合同状态。
    /// </summary>
    public enum ContractStatus
    {
        /// <summary>
        /// 正常。
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 废除。
        /// </summary>
        [Description("废除")]
        Abolish = 1,
    }
}
