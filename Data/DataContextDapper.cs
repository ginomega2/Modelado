using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Modelado.Data
{

    public class DataContextDapper
    {
        private string _conectionString ;
        // private IConfiguration _config;

        public DataContextDapper(IConfiguration config)
        {
            // _config = config;
            
            _conectionString= config.GetConnectionString( "DefaultConnection");

        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_conectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_conectionString);
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_conectionString);
            return (dbConnection.Execute(sql) > 0 ? true : false);
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_conectionString);
            return dbConnection.Execute(sql);
        }



    }
}