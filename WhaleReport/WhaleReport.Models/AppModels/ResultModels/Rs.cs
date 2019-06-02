using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleReport.Models.AppModels.ResultModels
{
    public class Rs : Dictionary<string, object>
    {
        private Rs() { }
        private Rs(int code, string msg)
        {
            Add("code", code);
            Add("msg", msg);
        }

        #region OK
        public static Rs Ok()
        {
            return new Rs(0, "操作成功");
        }
        public static Rs Ok(string msg)
        {
            return new Rs(0, msg);
        }
        #endregion

        #region ERROR
        public static Rs Error()
        {
            return new Rs(1, "操作失败");
        }

        public static Rs Error(string msg)
        {
            return new Rs(1, msg);
        }
        #endregion
    }
}
