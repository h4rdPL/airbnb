namespace airbnb.Contracts.Authentication
{
    public record struct CreateCommentsRequest(
            string UserFirstName,
            string Comment,
            int NumberOfStars,
            DateTime CreatedAt
        );
}
