using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIT.Company.Server.DataAccess.Repositories
{
    public abstract class Repository
    {
        protected QueryExecutor executor;

        /// <summary>
        /// Initializes a new instance of the Repository class
        /// </summary>
        public Repository()
        {
            executor = new QueryExecutor(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AspITCompany");
        }
    }
}
