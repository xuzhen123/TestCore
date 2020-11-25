
using System.ComponentModel.DataAnnotations;

namespace XZ.Css.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名不能为空")]
        [StringLength(36, MinimumLength = 2, ErrorMessage = "用户名格式不正确")]
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "密码格式不正确")]
        public string Password { get; set; }
    }
}
