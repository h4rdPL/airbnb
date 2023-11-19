namespace airbnb.Contracts.Authentication.LoginResponse
{
    public record struct AuthResponse(
        string FirstName,
        string Lastname,
        string Email
        );
}
