//using Azylee.Core.DataUtils.CollectionUtils;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using WhaleReport.Models.UserModels;

//namespace WhaleReport.Modules.AuthorizeModule
//{
//    public class Authorize : AuthorizeAttribute
//    {
//        static List<AuthorizeModel> auths = null;
//        protected override bool AuthorizeCore(HttpContextBase httpContext)
//        {
//            //读取权限配置信息
//            ReadAuthsInfo();
//            //权限已配置
//            if (ListTool.HasElements(auths))
//            {
//                string controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
//                string action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
//                //string name = httpContext.User.Identity.Name;
//                Users user = httpContext.Session["currentUser"] as User;
//                List<UserInRole> uirs = httpContext.Session["currentUserInRoles"] as List<UserInRole>;

//                //获取匹配当前 Controller->Action 的权限配置信息，并验证功能开启
//                AuthorizeModel now = auths.FirstOrDefault(x => x.Controller == controller && x.Action == action);
//                if (now != null && !now.IsForbidden)
//                {
//                    //需要登录，并已登陆
//                    if (now.IsLogin && user == null)
//                    {
//                        return false;
//                    }
//                    //获取准许权限
//                    string[] roles = now.GetRoles();
//                    if (ListTool.HasElements(roles))
//                    {
//                        int access = 0;
//                        foreach (var role in roles)
//                        {
//                            if (uirs.Any(x=>x.RoleId == role))
//                            {
//                                access++;
//                                break;
//                            }
//                        }
//                        if (access > 0)
//                            return true;
//                    }
//                    else
//                    {
//                        return true;
//                    }
//                }
//            }
//            //权限未配置，阻止所有请求
//            httpContext.Response.StatusCode = 403;
//            return false;
//        }
//        public override void OnAuthorization(AuthorizationContext filterContext)
//        {
//            base.OnAuthorization(filterContext);
//            if (filterContext.HttpContext.Response.StatusCode == 403)
//            {
//                filterContext.Result = new RedirectResult("/Home/NotAuthorized");
//            }
//        }
//        /// <summary>
//        /// 读取数据库权限验证配置信息
//        /// </summary>
//        private void ReadAuthsInfo()
//        {
//            if (auths == null)
//            {
//                using (Muse db = new Muse("DefaultDB"))
//                {
//                    auths = db.GetAll<AuthorizeModel>(null, false).ToList();
//                }
//            }
//        }
//    }
//}