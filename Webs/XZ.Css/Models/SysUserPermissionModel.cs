using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XZ.Css.Models
{
    public class SysUserPermissionModel
    {
        public int SysUserId { get; set; }
        public string SysUserName { get; set; }
        public int RoleId { get; set; }
        public bool IsAdmin { get; set; }
        public string[] SysAppIds { get; set; }
        public bool IsPermission(string sysAppId)
        {
            if (this.IsAdmin)
            {
                return true;
            }

            return this.SysAppIds.Contains(sysAppId);
        }
    }
}
