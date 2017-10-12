namespace AspIT.Company.Clients.Communications.Enums
{
    public enum LoginResult
    {
        Success,
        WrongUsernameOrPassword,
        UserDoesNotExist,
        UserAlreadyLoggedIn,
        ServerRefusedClient
    }
}
