using Asp.Versioning;
using CleanArchitecture.WebApi.Shared.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion(Versions.V1)]
public abstract class BaseController : ControllerBase
{
}
