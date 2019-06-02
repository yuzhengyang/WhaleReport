using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleReport.ReportDB.MySqlUtils
{
    public class MySqlHelper
    {
        public string ConnectionString { get; set; }
        public MySqlHelper(string cs)
        {
            ConnectionString = cs;
        }
        public DataTable Select(string sql, out int recordsAffected)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            MySqlDataReader dataReader = null;
            MySqlCommand command = null;
            try
            {
                conn.Open();
                command = conn.CreateCommand();
                command.CommandText = sql;
                dataReader = command.ExecuteReader();

                DataTable dt = new DataTable();
                recordsAffected = dataReader.RecordsAffected;
                DataTable schemaTable = dataReader.GetSchemaTable();

                //动态构建表，添加列
                foreach (DataRow dr in schemaTable.Rows)
                {
                    DataColumn dc = new DataColumn();
                    dc.DataType = dr[0].GetType();//设置列的数据类型
                    dc.ColumnName = dr[0].ToString();//设置列的名称
                    dt.Columns.Add(dc);//将该列添加进构造的表中
                }
                //读取数据添加进表中
                while (dataReader.Read())
                {
                    DataRow row = dt.NewRow();
                    //填充一行数据
                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                        row[i] = dataReader[i].ToString();

                    dt.Rows.Add(row);
                    row = null;
                }
                schemaTable = null;
                return dt;
            }
            catch (Exception e)
            {
                recordsAffected = -1;
                return null;
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed) dataReader.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public DataTable Group(DataTable dt, string name, string value, string group)
        {
            try
            {
                DataTable table = new DataTable();
                //动态构建表，添加列（添加基础列）
                //获取数据列类型
                Type valueType = null;
                int index = 0, nameIndex = 0, valueIndex = 0, groupIndex = 0;
                foreach (DataColumn item in dt.Columns)
                {
                    if (item.ColumnName == name)
                    {
                        nameIndex = index;
                        DataColumn dc = new DataColumn();
                        dc.DataType = name.GetType();//设置列的数据类型
                        dc.ColumnName = item.ToString();//设置列的名称
                        table.Columns.Add(dc);//将该列添加进构造的表中
                    }
                    if (item.ColumnName == value)
                    {
                        valueIndex = index;
                        valueType = item.DataType;
                    }
                    if (item.ColumnName == group)
                    {
                        groupIndex = index;
                    }
                    index++;
                }
                //动态构建表，添加列（添加分组列）
                foreach (DataRow item in dt.Rows)
                {
                    string _name = item.ItemArray[nameIndex].ToString();
                    string _group = item.ItemArray[groupIndex].ToString();
                    object _value = item.ItemArray[valueIndex];
                    if (table.Columns.IndexOf(_group) < 0)
                    {
                        DataColumn dc = new DataColumn();
                        dc.DataType = valueType;//设置列的数据类型
                        dc.ColumnName = _group;//设置列的名称
                        table.Columns.Add(dc);//将该列添加进构造的表中
                    }
                }
                //重整理表格
                foreach (DataRow item in dt.Rows)
                {
                    string _name = item.ItemArray[nameIndex].ToString();
                    string _group = item.ItemArray[groupIndex].ToString();
                    object _value = item.ItemArray[valueIndex];
                    int _index = -1;
                    if ((_index = table.Columns.IndexOf(_group)) >= 0)
                    {
                        DataRow[] rows = table.Select($"{name} = '{_name}'");
                        if (rows != null && rows.Length > 0)
                        {
                            rows[0][_index] = _value;
                        }
                        else
                        {
                            table.Rows.Add(new[] { _name });
                            table.Rows[table.Rows.Count - 1][_index] = _value;
                        }
                    }
                }
                return table;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
