
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 在线聊天工具类型。
    /// </summary>
    public enum ImType
    {
        /// <summary>
        /// QQ
        /// </summary>
        [Description("QQ")]
        QQ = 0,

        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        Weixin = 1,

        /// <summary>
        /// Skype
        /// </summary>
        [Description("Skype")]
        Skype = 2,

        /// <summary>
        /// Whats App
        /// </summary>
        [Description("WhatsApp")]
        WhatsApp = 3,
    }
}
