namespace airbnb.Contracts.Authentication
{
    public record struct AuthenticationRequest(
            string FirstName,
            string LastName,
            string Email, 
            string Password,
            string RepeatedPassword
        );
}
