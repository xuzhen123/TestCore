
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 标记样式。
    /// </summary>
    public enum FlagStyle
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 红色
        /// </summary>
        [Description("红色")]
        Red = 1,

        /// <summary>
        /// 黄色
        /// </summary>
        [Description("黄色")]
        Yellow = 2,

        /// <summary>
        /// 绿色
        /// </summary>
        [Description("绿色")]
        Green = 3,

        /// <summary>
        /// 青色
        /// </summary>
        [Description("青色")]
        Cyan = 4,

        /// <summary>
        /// 蓝色
        /// </summary>
        [Description("蓝色")]
        Blue = 5,

        /// <summary>
        /// 紫色
        /// </summary>
        [Description("紫色")]
        Purple = 6
    }
}
