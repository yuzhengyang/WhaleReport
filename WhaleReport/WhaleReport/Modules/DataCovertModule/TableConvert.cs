using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WhaleReport.Modules.DataCovertModule
{
    public class TableConvert
    {
        public static List<Dictionary<string, object>> Table2Dictionary(DataTable table)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            if (table != null &&
                    table.Rows != null && table.Rows.Count > 0 &&
                    table.Columns != null && table.Columns.Count > 0)
            {
                Dictionary<string, object> row = null;
                foreach (DataRow item in table.Rows)
                {
                    row = new Dictionary<string, object>();
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        string _name = table.Columns[i].ToString();
                        object _value = item[i];
                        row.Add(_name, _value);
                    }
                    result.Add(row);
                }
            }
            return result;
        }
    }
}