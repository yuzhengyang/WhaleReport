using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleReport.Models.DBModels.UserModels
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
        public string Role { get; set; }
        /// <summary>
        /// 功能可用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 允许匿名（允许不登录）
        /// </summary>
        public bool AllowAnonymous { get; set; }
    }
}
