using System;
using System.Net.Sockets;
using AspIT.Company.Common.Entities;
using AspIT.Company.Clients.Communications.Enums;
using AspIT.Company.Server.DataAccess.Repositories;

namespace AspIT.Company.Clients.Communications
{
    public static class LoginHandler
    {
        /// <summary>
        /// Gets the currently logged in user
        /// </summary>
        public static User CurrentUser { get; private set; }

        /// <summary>
        /// Attempts to login a user
        /// </summary>
        /// <param name="user">The user to login</param>
        public static LoginResult AttemptLogin(User user)
        {
            // Check user details
            UserRepository repository = new UserRepository();
            User dbUser = repository.Find(user);

            if(dbUser.Username == string.Empty)
            {
                return LoginResult.UserDoesNotExist;
            }

            if(user.Password != dbUser.Password)
            {
                return LoginResult.WrongUsernameOrPassword;
            }

            if (TestServerConnection() == "Refused")
            {
                return LoginResult.ServerRefusedClient;
            }

            User loggedInUser = new User(user.Username, user.Password, true);
            repository.Update(loggedInUser);
            CurrentUser = loggedInUser;
            return LoginResult.Success;
        }

        /// <summary>
        /// Logging out the current user
        /// </summary>
        public static void Logout()
        {
            UserRepository repository = new UserRepository();
            repository.Update(new User(CurrentUser.Username, CurrentUser.Password, false));
            CurrentUser = new User(string.Empty, string.Empty);
        }

        // TODO: Remove when done testing
        private static string TestServerConnection()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 27013);
                return "Success";
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(SocketException))
                {
                    return "Refused";
                }

                return "Unknown";
            }
        }
    }
}
