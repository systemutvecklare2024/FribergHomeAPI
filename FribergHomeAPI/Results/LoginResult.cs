namespace FribergHomeAPI.Results
{
    // Author: Christoffer
    public record LoginResult(bool Success, string? Token, string? Email, string? UserId, int? AgentId, IEnumerable<string>? Errors)
    {
        public static LoginResult Failed(string failed) => 
            new (false, null, null, null, null, new[] { failed } );
        public static LoginResult Failed(IEnumerable<string> errors) => 
            new (false, null, null, null, null, errors );
        public static LoginResult SuccessResult(string token, string email, string userId, int agentId) => 
            new (true, token, email, userId, agentId, Enumerable.Empty<string>() );
    }
}
