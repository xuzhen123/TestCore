
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 商户级别。
    /// </summary>
    public enum MerchantLevel
    {
        /// <summary>
        /// 普通。
        /// <para>Logo</para>
        /// </summary>
        [Description("Logo")]
        Normal = 0,

        /// <summary>
        /// 风险。
        /// <para>N-Logo</para>
        /// </summary>
        [Description("N-Logo")]
        Risk = 1,

        /// <summary>
        /// 普通且网站免审。
        /// <para>Logo</para>
        /// </summary>
        [Description("Logo Without Audit")]
        NormalWithoutAudit = 2,

        /// <summary>
        /// 风险且网站免审。
        /// <para>Logo</para>
        /// </summary>
        [Description("N-Logo Without Audit")]
        RiskWithoutAudit = 3,
    }
}
