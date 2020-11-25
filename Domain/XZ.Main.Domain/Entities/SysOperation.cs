using System;
using System.ComponentModel.DataAnnotations;
using XZ.Domain;

namespace XZ.Main.Domain
{
    public class SysOperation : Entity, IAggregateRoot
    {
        [Key]
        public string SysAppId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
