using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using testEvolution.Interfaces;

namespace testEvolution.Data
{
    public class BaseData : ConnectionSql
    {
        public SqlCommand sqlCommand { get; set; }
        public SqlDataReader reader { get; set; }
        public BaseData()
        {
            sqlCommand = new SqlCommand();
            reader = null;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = Connection;
        }
    }
}
