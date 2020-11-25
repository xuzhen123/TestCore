using System;
using System.ComponentModel.DataAnnotations;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class Contract : Entity, IAggregateRoot
    {
        [Key]
        public int ContractId { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string Manager { get; private set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; private set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 详细通讯地址
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostalCode { get; private set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; private set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// 在线聊天工具类型/>。
        /// </summary>
        public ImType ImType { get; private set; }

        /// <summary>
        /// 聊天号码
        /// </summary>
        public string ImAddress { get; private set; }

        /// <summary>
        /// 状态，参见<see cref="ContractStatus"/>。
        /// </summary>
        public ContractStatus Status { get; private set; }

        /// <summary>
        /// 标记，参见<see cref="FlagStyle"/>。
        /// </summary>
        public FlagStyle Flag { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }

        /// <summary>
        /// 第一负责人
        /// </summary>
        public string Owner1 { get; private set; }

        /// <summary>
        /// 第二负责人
        /// </summary>
        public string Owner2 { get; private set; }

        /// <summary>
        /// 客服代表1
        /// </summary>
        public string Csr1 { get; private set; }

        /// <summary>
        /// 客服代表2
        /// </summary>
        public string Csr2 { get; private set; }

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

        public Contract(int contractId, string name, string manager, string idCard, string country, string province, string city, string address, string postalCode, string telephone, string email, ImType imType, string imAddress, ContractStatus status, FlagStyle flag, string remark, string owner1, string owner2, string csr1, string csr2, string employeeId, string employeeName, DateTime dateCreated, DateTime? lastUpdate)
        {
            this.ContractId = contractId;
            this.Name = name;
            this.Manager = manager;
            this.IdCard = idCard;
            this.Country = country;
            this.Province = province;
            this.City = city;
            this.Address = address;
            this.PostalCode = postalCode;
            this.Telephone = telephone;
            this.Email = email;
            this.ImType = imType;
            this.ImAddress = imAddress;
            this.Status = status;
            this.Flag = flag;
            this.Remark = remark;
            this.Owner1 = owner1;
            this.Owner2 = owner2;
            this.Csr1 = csr1;
            this.Csr2 = csr2;
            this.EmployeeId = employeeId;
            this.EmployeeName = employeeName;
            this.DateCreated = dateCreated;
            this.LastUpdate = lastUpdate;

            this.AddDomainEvent(new ContractCreatedEvent(this));
        }
    }
}
