using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using LicenseService.CQRS.Commands;
using LicenseService.CQRS.Queries;
using System.Threading.Tasks;

namespace LicenseService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "LicenseAPI")] // Require authentication for all actions
    public class LicenseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LicenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForLicense([FromBody] ApplyForLicenseCommand command)
        {
            var license = await _mediator.Send(command);
            return Ok(license);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLicense(string id, [FromQuery] string tenantId)
        {
            var license = await _mediator.Send(new GetLicenseByIdQuery { LicenseId = id, TenantId = tenantId });
            return Ok(license);
        }
    }
}
