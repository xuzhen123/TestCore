
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 结算周期类型。
    /// </summary>
    public enum SettleType
    {
        /// <summary>
        /// 动态周期。
        /// </summary>
        [Description("动态周期")]
        Dynamic = 0,

        /// <summary>
        /// 固定周期。
        /// </summary>
        [Description("固定周期")]
        Fixed = 1,
    }
}
