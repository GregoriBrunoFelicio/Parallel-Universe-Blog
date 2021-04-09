using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Shared
{
    public class FakePolicyEvaluator : IPolicyEvaluator
    {
        public virtual async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            const string testScheme = "FakeScheme";
            var principal = new ClaimsPrincipal();

            principal.AddIdentity(new ClaimsIdentity(new[] {
                new Claim("Name", "name"),
                new Claim("Email", "email@email"),
                new Claim(ClaimTypes.Role, "Admin"),
            }, testScheme));

            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal,
                new AuthenticationProperties(), testScheme)));
        }

        public virtual async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy,
            AuthenticateResult authenticationResult, HttpContext context, object resource) =>
            await Task.FromResult(PolicyAuthorizationResult.Success());
    }
}