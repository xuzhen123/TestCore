using System;
using System.ComponentModel.DataAnnotations;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class MerchantUser : Entity, IAggregateRoot
    {
        [Key]
        public int UserId { get; private set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public int ContractId { get; private set; }

        /// <summary>
        /// 商户编号
        /// </summary>
        public int MerchantId { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Activated { get; private set; }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        public bool Administrative { get; private set; }

        /// <summary>
        /// 是否强制更改密码
        /// </summary>
        public bool Forced { get; private set; }

        /// <summary>
        /// 错误尝试次数
        /// </summary>
        public int ErrorCount { get; private set; }

        /// <summary>
        /// 第三方用户唯一标识
        /// </summary>
        public string OpenId { get; private set; }

        /// <summary>
        /// 下次更改密码日期
        /// </summary>
        public DateTime DateUpdatePassword { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdate { get; private set; }

        public MerchantUser(int userId, int contractId, int merchantId, string fullName, string email, string password, bool enabled, bool activated, bool administrative, bool forced, int errorCount, string openId, DateTime dateUpdatePassword, DateTime dateCreated, DateTime? lastUpdate)
        {
            this.UserId = userId;
            this.ContractId = contractId;
            this.MerchantId = merchantId;
            this.FullName = fullName;
            this.Email = email;
            this.Password = password;
            this.Enabled = enabled;
            this.Activated = activated;
            this.Administrative = administrative;
            this.Forced = forced;
            this.ErrorCount = errorCount;
            this.OpenId = openId;
            this.DateUpdatePassword = dateUpdatePassword;
            this.DateCreated = dateCreated;
            this.LastUpdate = lastUpdate;

            this.AddDomainEvent(new MerchantUserCreatedEvent(this));
        }   
    }
}
