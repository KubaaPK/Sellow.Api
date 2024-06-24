using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellow.Modules.Auth.Core.Features;
using Sellow.Shared.Infrastructure.Api;

namespace Sellow.Modules.Auth.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
internal sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /users
    ///     {
    ///        "email": "jan@kowalski.pl",
    ///        "username": "jankowalski22",
    ///        "password": "as1asasdf@@@"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">User has been successfully created.</response>
    /// <response code="400">Request body validation failed.</response>
    /// <response code="409">User with given credentials already exists.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost("users")]
    [ProducesResponseType(201)]
    public async Task<IResult> CreateUser([FromBody] CreateUser command, CancellationToken cancellationToken = default)
    {
        var userId = await _sender.Send(command, cancellationToken);

        return Results.Created($"{Request.GetActionUrl()}/{userId}", null);
    }
}