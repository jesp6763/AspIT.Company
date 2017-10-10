using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AspIT.Company.Server.DataAccess
{
    public class QueryExecutor
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the QueryExecutor class
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        public QueryExecutor(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        public string ConnectionString { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Executes a sql query
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <returns></returns>
        public DataSet Execute(string query)
        {
            DataSet result = new DataSet();

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using(SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd))
                    {
                        sqlAdapter.Fill(result);

                    }
                }
            }
            return result;
        }
        #endregion
    }
}
