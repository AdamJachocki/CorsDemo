using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace WebApiWithCors.Services.Cors
{
    public class OriginCorsPolicyProvider : ICorsPolicyProvider
    {
        private readonly CorsOptions _corsOptions;
        public OriginCorsPolicyProvider(IOptions<CorsOptions> corsOptions)
        {
            _corsOptions = corsOptions.Value;
        }

        public Task<CorsPolicy?> GetPolicyAsync(HttpContext context, string? policyName)
        {
            var origin = context.Request.Headers.Origin;
            var policy = _corsOptions.GetPolicy(origin);

            if (policy == null)
                policy = _corsOptions.GetPolicy(policyName ?? _corsOptions.DefaultPolicyName);
            
            return Task.FromResult(policy);
        }
    }
}
