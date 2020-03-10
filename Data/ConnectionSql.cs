using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace testEvolution.Data
{
    public class ConnectionSql
    {
        public SqlConnection Connection { get; }
        public IConfiguration Configuration { get; set; }
        public ConnectionSql()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            Connection = new SqlConnection();
            Connection.ConnectionString = Configuration.GetConnectionString("connectionString");
        }
    }
}









//Connection connection = new Connection();
//SqlCommand sqlCmd = new SqlCommand();
//SqlDataReader reader = null;
//sqlCmd.CommandType = CommandType.Text;
//sqlCmd.CommandText = "SELECT * FROM users";
//sqlCmd.Connection = connection.connection;
//connection.connection.Open();
//reader = sqlCmd.ExecuteReader();
//while (reader.Read())
//{
//    Console.WriteLine($"username = {reader.GetValue(1)}");
//}
//connection.connection.Close();