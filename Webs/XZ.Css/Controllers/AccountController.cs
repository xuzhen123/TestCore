using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using XZ.Core.Logs;
using XZ.Css.Models;
using XZ.Main.Domain;
using XZ.Main.Repository;

namespace XZ.Css.Controllers
{
    [AllowAnonymous, Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        ISysUserRepository _sysUser;
        ISysUserAndRoleRepository _sysUserAndRole;
        ISysPermissionRepository _sysRoleAndAppRepository;
        IMyLog _mylog;
        public AccountController(ISysUserRepository sysUser, ISysUserAndRoleRepository sysUserAndRole, ISysPermissionRepository sysRoleAndAppRepository, IMyLog mylog)
        {
            this._sysUser = sysUser;
            this._sysUserAndRole = sysUserAndRole;
            this._sysRoleAndAppRepository = sysRoleAndAppRepository;
            this._mylog = mylog;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                _mylog.ShowError("输入的邮箱不能为空");

                return View();
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                _mylog.ShowError("密码不能为空");

                return View();
            }

            SysUser getSysUser = _sysUser.GetSysUserByEmailAndPwd(model.Username, model.Password);
            if (getSysUser != null)
            {
                SysUserAndRole sysUserAndRole = await _sysUserAndRole.GetRoleAndSysUserByUserIdAsync(getSysUser.SysUserId);

                List<Claim> claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Sid,getSysUser.SysUserId.ToString()),
                     new Claim(ClaimTypes.Name,getSysUser.SysUserName),
                     new Claim(ClaimTypes.Email,getSysUser.Email),
                     new Claim(ClaimTypes.MobilePhone,getSysUser.Phone)
                };

                if (getSysUser.IsAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    List<SysPermission> sysRoleAndApps = await _sysRoleAndAppRepository.GetSysRoleAndAppsAsync(sysUserAndRole.RoleId, null);
                    sysRoleAndApps.ForEach(m => claims.Add(new Claim(ClaimTypes.Role, m.SysAppId)));
                }

                #region 1.Cookies身份认证
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                claimsPrincipal.AddIdentity(identity);
                await HttpContext.SignInAsync(claimsPrincipal);
                #endregion

                #region 2.JWT身份认证
                //var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")), SecurityAlgorithms.HmacSha256);
                //var jwtSecurityToken = new JwtSecurityToken(
                //    issuer: "localhost",
                //    audience: "localhost",
                //    claims: claims,
                //    expires: DateTime.Now.AddMinutes(15),
                //    signingCredentials: creds);
                //var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                #endregion

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("login");
        }
    }
}