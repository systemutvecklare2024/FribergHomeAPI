namespace FribergHomeAPI.Results
{
    // Author: Christoffer
    public record LoginResult(string? Token, string? Email, string? UserId, int? AgentId);
}
