﻿using AutoMapper;
using FinXp.Domain.Interfaces.Service;
using FinXp.Domain.Model;
using FinXp.Domain.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace FinXp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NegotiationController(
    INegotiationService negotiateService,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> SaveNegotiationAsync([FromBody] NegotiationRequest negotiationProduction)
    {
        var negotiation = mapper.Map<NegotiationProduct>(negotiationProduction);

        var result = await negotiateService.SaveNegotiationAsync(negotiation);

        if (result.IsSuccess && result.Data)
        {
            return Created();
        }
        else if(result.Data is false)
        {
            return UnprocessableEntity();
        }
        else
        {
            return Problem(result.Error.Message);
        }
    }
}
