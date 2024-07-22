// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bengbenz.Embassy.eServices.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseApiController(ILogger logger, IMediator mediator) : ControllerBase
{
    protected ILogger Logger { get; } = logger;
    protected IMediator Mediator { get; } = mediator;
}
