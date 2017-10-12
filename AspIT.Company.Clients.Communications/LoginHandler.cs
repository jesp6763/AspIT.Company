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

            if(dbUser == null)
            {
                return LoginResult.UserDoesNotExist;
            }

            if(user.Password != dbUser.Password)
            {
                return LoginResult.WrongUsernameOrPassword;
            }

            if (ConnectionHandler.TestServerConnection() == ConnectionResult.ConnectionRefused)
            {
                return LoginResult.ServerRefusedClient;
            }

            if(dbUser.IsLoggedIn)
            {
                return LoginResult.UserAlreadyLoggedIn;
            }

            dbUser.IsLoggedIn = true;
            CurrentUser = dbUser;
            repository.Update(CurrentUser);
            return LoginResult.Success;
        }

        /// <summary>
        /// Logging out the current user
        /// </summary>
        public static void Logout()
        {
            UserRepository repository = new UserRepository();
            CurrentUser.IsLoggedIn = false;
            repository.Update(CurrentUser);
            CurrentUser = null;
        }
    }
}
