using Azylee.Core.DataUtils.CollectionUtils;
using Azylee.Core.DataUtils.StringUtils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhaleReport.MainDB.DAO;
using WhaleReport.Models;
using WhaleReport.Models.DBModels.UserModels;

namespace WhaleReport.Modules.AuthorizeModule
{
    public class Authorize : AuthorizeAttribute
    {
        static List<AuthorizeModel> auths = null;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAccess = false;
            //读取权限配置信息
            ReadAuthsInfo();
            //权限已配置
            if (ListTool.HasElements(auths))
            {
                string controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
                string action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();

                List<AuthorizeModel> current = GetCurrentAuth(controller, action);
                isAccess = CheckCurrentAuth(httpContext, current, controller, action);
            }
            //权限未配置，阻止所有请求
            if (!isAccess) httpContext.Response.StatusCode = 403;
            return isAccess;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                filterContext.Result = new RedirectResult("/Home/NotAuthorized");
            }
        }
        /// <summary>
        /// 读取数据库权限验证配置信息
        /// </summary>
        private void ReadAuthsInfo()
        {
            //if (!Ls.Ok(auths))
            {
                using (Muse db = new Muse())
                {
                    auths = db.GetAll<AuthorizeModel>(null, false).ToList();
                }
            }
        }

        /// <summary>
        /// 获取当前Controller-Action权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private List<AuthorizeModel> GetCurrentAuth(string controller, string action)
        {
            List<AuthorizeModel> result = new List<AuthorizeModel>();
            try
            {
                var list1 = auths.Where(x => x.Controller == controller && x.Action == action).ToList();
                if (Ls.Ok(list1)) result.AddRange(list1);
                var list2 = auths.Where(x => x.Controller == controller && x.Action.Contains("*")).ToList();
                if (Ls.Ok(list2)) result.AddRange(list2);
            }
            catch { }
            return result;
        }
        private bool CheckCurrentAuth(HttpContextBase httpContext, List<AuthorizeModel> list, string controller, string action)
        {
            bool result = false;
            if (Ls.Ok(list))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = userManager.FindById(httpContext.User.Identity.GetUserId());

                foreach (var item in list)
                {
                    //通配符验证方式
                    if (item.Controller == controller && item.Action == "*")
                    {
                        if (item.IsEnable)
                        {
                            if (item.AllowAnonymous)
                            {
                                result = true;
                            }
                            else
                            {
                                if (currentUser != null && userManager.IsInRole(currentUser.Id, item.Role)) result = true;
                            }
                        }
                    }

                    //标准验证方式
                    if (item.Controller == controller && item.Action == action)
                    {
                        if (item.IsEnable)
                        {
                            if (item.AllowAnonymous)
                            {
                                result = true;
                            }
                            else
                            {
                                if (currentUser != null && userManager.IsInRole(currentUser.Id, item.Role)) result = true;
                            }
                        }
                    }
                    if (result) break;//如果已验证通过，直接跳过其他验证
                }
            }
            return result;
        }
    }
}