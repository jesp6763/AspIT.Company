using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIT.Company.Common.Entities
{
    public struct User
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; }
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; }
        /// <summary>
        /// Gets or sets the logged in state
        /// </summary>
        public bool IsLoggedIn { get; }


        public User(string username, string password)
        {
            Username = username;
            Password = password;
            IsLoggedIn = false;
        }

        public User(string username, string password, bool isLoggedIn) : this(username,password)
        {
            IsLoggedIn = isLoggedIn;
        }
    }
}
