using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class SysUser : Entity, IAggregateRoot
    {
        [Key]
        public int SysUserId { get; set; }
        public string SysUserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
