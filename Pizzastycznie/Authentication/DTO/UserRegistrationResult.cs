namespace Pizzastycznie.Authentication.DTO
{
    public enum UserRegistrationResult
    {
        UserExists = 0,
        BadEmail = 1,
        BadPassword = 2,
        BadName = 3,
        Success = 4,
        DatabaseError = 5,
    }
}