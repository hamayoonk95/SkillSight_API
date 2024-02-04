public class RoleMatchingService : IRoleMatchingService
{
    // Matches user answers to a role and returns the matched role's name
    public Task<string> MatchUserToRoleAsync(Dictionary<string, string> answers)
    {
        // Dictionary to keep track of scores for each role
        var roleScores = new Dictionary<string, int>();

        // Loop through all answers and calculate scores for each role
        foreach (var answer in answers.Values)
        {
            // Split the answer into roles and trim any whitespace
            var roles = answer.Split(",").Select(r => r.Trim());

            // Increment score for each role in the answer
            foreach (var role in roles)
            {
                if (!roleScores.ContainsKey(role))
                {
                    roleScores[role] = 0;
                }
                roleScores[role]++;
            }
        }

        // Find the maximum score among all roles
        var maxCount = roleScores.Max(r => r.Value);
        // Find all roles with the maximum score
        var topRoles = roleScores.Where(r => r.Value == maxCount).Select(r => r.Key).ToList();

        // Select the first role with the maximum score or a default value if no roles found
        var matchedRole = topRoles.FirstOrDefault() ?? string.Empty;

        return Task.FromResult(matchedRole);
    }
}