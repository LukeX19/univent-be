﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univent.Api.Contracts.University.Requests;
using Univent.Api.Contracts.University.Responses;
using Univent.Application.Universities.Commands;
using Univent.Application.Universities.Queries;

namespace Univent.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UniversitiesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UniversitiesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUniversities()
        {
            var query = new GetAllUniversities();
            var response = await _mediator.Send(query);
            var universities = _mapper.Map<List<UniversityResponse>>(response);

            return Ok(universities);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateUniversity([FromBody] UniversityCreate univ)
        {
            var command = _mapper.Map<CreateUniversityCommand>(univ);
            var response = await _mediator.Send(command);
            var university = _mapper.Map<UniversityResponse>(response);

            return CreatedAtAction(nameof(GetUniversityById), new { id = response.UniversityID }, university);
        }

        [HttpGet]
        [Route(ApiRoutes.Universities.IdRoute)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUniversityById(Guid id)
        {
            var query = new GetUniversityById { UniversityID = id };
            var response = await _mediator.Send(query);
            var university = _mapper.Map<UniversityResponse>(response);

            return Ok(university);
        }

        [HttpPatch]
        [Route(ApiRoutes.Universities.IdRoute)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateUniversity(Guid id, UniversityUpdate updatedUniversity)
        {
            var command = _mapper.Map<UpdateUniversityCommand>(updatedUniversity);
            command.UniversityID = id;
            var response = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.Universities.IdRoute)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteUniversity(Guid id)
        {
            var command = new DeleteUniversityCommand { UniversityID = id };
            var response = await _mediator.Send(command);

            return NoContent();
        }
    }
}
