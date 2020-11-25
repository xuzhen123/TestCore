using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class SysUserAndRole : Entity, IAggregateRoot
    {
        [Key]
        public int ID { get; set; }
        public int SysUserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
