using System;
using System.ComponentModel.DataAnnotations;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class SysPermission : Entity, IAggregateRoot
    {
        [Key]
        public int OperId { get; set; }
        public int RoleId { get; set; }
        public string SysAppId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
