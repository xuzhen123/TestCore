using System;
using System.ComponentModel;

namespace XZ.Main.Domain
{
    /// <summary>
    /// 邮件提醒。
    /// </summary>
    [Flags]
    public enum EmailAlert
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 支付成功时发送支付成功提醒
        /// </summary>
        [Description("支付成功")]
        OrderSuccess = 1,

        /// <summary>
        /// 支付失败时发送支付失败提醒
        /// </summary>
        [Description("支付失败")]
        OrderFailure = 2,

        /// <summary>
        /// 退款审核成功发送退款成功提醒
        /// </summary>
        [Description("退款成功")]
        RefundSuccess = 4,

        /// <summary>
        /// 运单上传后发送已发货提醒
        /// </summary>
        [Description("运单上传")]
        WaybillUpload = 8,

        /// <summary>
        /// 回复客户问题后发送问题回复提醒
        /// </summary>
        [Description("回复客户问题")]
        AnswerQuestions = 16,

        /// <summary>
        /// 自动提醒延迟发(3天后)
        /// </summary>
        [Description("自动提醒延迟发(3天后)")]
        DelayDelivery = 32

    }
}
