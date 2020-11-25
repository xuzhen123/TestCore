using System;
using System.ComponentModel.DataAnnotations;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class Merchant : Entity, IAggregateRoot
    {
        [Key]
        public int MerchantId { get; private set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public int ContractId { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 级别，参见<see cref="MerchantLevel"/>。
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 结算币种
        /// </summary>
        public string SettleCurrency { get; private set; }

        /// <summary>
        /// 结算周期类型，参见 <see cref="SettleType"/>。
        /// </summary>
        public int SettleType { get; private set; }

        /// <summary>
        /// 使用 DCC 交易
        /// </summary>
        public bool AllowDcc { get; private set; }

        /// <summary>
        /// 允许无运单结算
        /// </summary>
        public bool RequireWaybill { get; private set; }

        /// <summary>
        /// 基础结算周期
        /// </summary>
        public int SettleDelay { get; private set; }

        /// <summary>
        /// 当前结算周期
        /// </summary>
        public int? SettleDelay2 { get; private set; }

        /// <summary>
        /// 押金比例
        /// </summary>
        public decimal DepositRate { get; private set; }

        /// <summary>
        /// 押金周期
        /// </summary>
        public int DepositDelay { get; private set; }

        /// <summary>
        /// 折扣比例
        /// </summary>
        public decimal DiscountRate { get; private set; }

        /// <summary>
        /// 安全资金比例
        /// </summary>
        public decimal? SafeRate { get; private set; }

        /// <summary>
        /// 最大交易金额
        /// </summary>
        public decimal? MaxAmount { get; private set; }

        /// <summary>
        /// 拒付单笔手续费
        /// </summary>
        public decimal? ChargebackFee { get; private set; }

        /// <summary>
        /// 调单单笔手续费
        /// </summary>
        public decimal? CopyRequestFee { get; private set; }

        /// <summary>
        /// 客户邮件提醒设置，参见<see cref="EmailAlert"/>。
        /// </summary>
        public int EmailAlerts { get; private set; }

        /// <summary>
        /// 商户邮件回执设置，参见<see cref="ReceiptTo"/>。
        /// </summary>
        public int ReceiptTos { get; private set; }

        /// <summary>
        /// 集成模式，参见<see cref="IntegrateMode"/>。
        /// </summary>
        public int IntegrateModes { get; private set; }

        /// <summary>
        /// 交互模式，参见<see cref="ExchangeMode"/>。
        /// </summary>
        public int ExchangeModes { get; private set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; private set; }

        /// <summary>
        /// 标记，参见<see cref="FlagStyle"/>。
        /// </summary>
        public int Flag { get; private set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; private set; }

        /// <summary>
        /// 商户状态，参见<see cref="MerchantStatus"/>。
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? DateAudited { get; private set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeId { get; private set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        public string EmployeeName { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdate { get; private set; }

        public Merchant(int merchantId, int contractId, string name, int level, string settleCurrency, int settleType, bool allowDcc, bool requireWaybill, int settleDelay, int? settleDelay2, decimal depositRate, int depositDelay, decimal discountRate, decimal? safeRate, decimal? maxAmount, decimal? chargebackFee, decimal? copyRequestFee, int emailAlerts, int receiptTos, int integrateModes, int exchangeModes, string secretKey, int flag, string remark, int status, DateTime? dateAudited, string employeeId, string employeeName, DateTime dateCreated, DateTime? lastUpdate)
        {
            this.MerchantId = merchantId;
            this.ContractId = contractId;
            this.Name = name;
            this.Level = level;
            this.SettleCurrency = settleCurrency;
            this.SettleType = settleType;
            this.AllowDcc = allowDcc;
            this.RequireWaybill = requireWaybill;
            this.SettleDelay = settleDelay;
            this.SettleDelay2 = settleDelay2;
            this.DepositRate = depositRate;
            this.DepositDelay = depositDelay;
            this.DiscountRate = discountRate;
            this.SafeRate = safeRate;
            this.MaxAmount = maxAmount;
            this.ChargebackFee = chargebackFee;
            this.CopyRequestFee = copyRequestFee;
            this.EmailAlerts = emailAlerts;
            this.ReceiptTos = receiptTos;
            this.IntegrateModes = integrateModes;
            this.ExchangeModes = exchangeModes;
            this.SecretKey = secretKey;
            this.Flag = flag;
            this.Remark = remark;
            this.Status = status;
            this.DateAudited = dateAudited;
            this.EmployeeId = employeeId;
            this.EmployeeName = employeeName;
            this.DateCreated = dateCreated;
            this.LastUpdate = lastUpdate;

            this.AddDomainEvent(new MerchantCreatedEvent(this));
        }
    }
}
