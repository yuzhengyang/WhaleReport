using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleReport.Models.UserModels
{
    public class AuthorizeModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 所需权限
        /// </summary>
        public string Roles { get; set; }
        /// <summary>
        /// 功能禁用
        /// </summary>
        public bool IsForbidden { get; set; }
        /// <summary>
        /// 需要登录
        /// </summary>
        public bool IsLogin { get; set; }
        public string[] GetRoles()
        {
            if (Roles != null)
                return Roles.Split(',');
            return null;
        }
    }
}
