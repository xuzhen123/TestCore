using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XZ.Css.Models;
using XZ.Main.Domain;
using XZ.Main.Repository;

namespace XZ.Css.ViewComponents
{
    public class NavViewComponent : ViewComponent
    {
        ISysUserRepository _sysUserRepository;
        ISysUserAndRoleRepository _sysUserAndRole;
        ISysPermissionRepository _sysPermissionRepository;

        public NavViewComponent(ISysUserRepository sysUserRepository, ISysUserAndRoleRepository sysUserAndRole, ISysPermissionRepository sysPermissionRepository)
        {
            this._sysUserRepository = sysUserRepository;
            this._sysUserAndRole = sysUserAndRole;
            this._sysPermissionRepository = sysPermissionRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName)
        {
            SysUserPermissionModel model = null;

            if (User.Identity.IsAuthenticated)
            {
                model = new SysUserPermissionModel();
                SysUser sysUser = await _sysUserRepository.GetSysUserByNameAsync(User.Identity.Name);
                SysUserAndRole sysUserAndRole = await _sysUserAndRole.GetRoleAndSysUserByUserIdAsync(sysUser.SysUserId);
                List<SysPermission> sysPermissions = await _sysPermissionRepository.GetSysRoleAndAppsAsync(sysUserAndRole.RoleId, null);

                model.SysUserId = sysUser.SysUserId;
                model.SysUserName = sysUser.SysUserName;
                model.RoleId = sysUserAndRole.RoleId;
                model.IsAdmin = sysUser.IsAdmin;
                model.SysAppIds = sysPermissions.Select(m => m.SysAppId).ToArray();
            }

            return View(viewName, model);
        }
    }
}
