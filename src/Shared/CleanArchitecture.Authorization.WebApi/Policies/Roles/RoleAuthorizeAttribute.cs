using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class RoleAuthorizeAttribute(Claims.Roles roles) : AuthorizeAttribute(policy: PolicyNames.PolicyName(roles));