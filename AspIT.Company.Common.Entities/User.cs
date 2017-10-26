using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspIT.Company.Common.Entities
{
    //TEST THIS IS JUSt A TEST
    [Serializable]
    public class User
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the logged in state
        /// </summary>
        public bool IsLoggedIn { get; set; }


        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User(string username, string password, bool isLoggedIn) : this(username,password)
        {
            IsLoggedIn = isLoggedIn;
        }
    }
}
