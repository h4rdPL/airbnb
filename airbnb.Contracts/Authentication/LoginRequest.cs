namespace airbnb.Contracts.Authentication
{
    public record struct LoginRequest(
        string Email,
        string Password
        );
}
