namespace Pizzastycznie.Authentication.DTO
{
    public enum UserRegistrationResult
    {
        UserExists = 0,
        BadEmail = 1,
        BadPassword = 2,
        Success = 3,
        DatabaseError = 4,
    }
}