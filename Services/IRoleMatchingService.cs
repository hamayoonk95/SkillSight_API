// IRoleMatchingService.cs
public interface IRoleMatchingService
{
    Task<string> MatchUserToRoleAsync(Dictionary<string, string> answers);
}
