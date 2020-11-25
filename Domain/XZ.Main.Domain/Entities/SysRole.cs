using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class SysRole : Entity, IAggregateRoot
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescribe { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
