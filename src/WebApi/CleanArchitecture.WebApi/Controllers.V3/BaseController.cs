using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers.V3;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase;