using System;
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
            else if(user.Password != dbUser.Password)
            {
                return LoginResult.WrongUsernameOrPassword;
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
    }
}
