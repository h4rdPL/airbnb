namespace airbnb.Contracts.Authentication
{
    public record struct CreateCommentResponse(
            string UserFirstName,
            string Comment,
            int NumberOfStars,
            DateTime CreatedAt
        );
}
