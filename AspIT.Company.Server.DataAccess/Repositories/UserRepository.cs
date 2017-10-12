using System;
using System.Data;
using AspIT.Company.Common.Entities;

namespace AspIT.Company.Server.DataAccess.Repositories
{
    public class UserRepository : Repository
    {
        /// <summary>
        /// Finds a user by username
        /// </summary>
        /// <param name="user">The user to find</param>
        /// <returns></returns>
        public User Find(User user)
        {
            User result = user;
            DataSet dataSet = executor.Execute($"SELECT * FROM dbo.Users WHERE Username='{user.Username}'");
            if(dataSet.Tables.Count > 0)
            {
                DataTable table = dataSet.Tables[0];
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    user.Username = row.Field<string>("Username");
                    user.Password = row.Field<string>("Password");
                    user.IsLoggedIn = row.Field<bool>("IsLoggedIn");
                }
            }

            return result;
        }

        public void Update(User user)
        {
            executor.Execute($"UPDATE dbo.Users SET Username = '{user.Username}', Password = '{user.Password}', IsLoggedIn = {(user.IsLoggedIn ? 1 : 0)} WHERE Username='{user.Username}'");
        }
    }
}
