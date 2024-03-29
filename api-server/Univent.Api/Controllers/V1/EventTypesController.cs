﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univent.Api.Contracts.EventType.Requests;
using Univent.Api.Contracts.EventType.Responses;
using Univent.Application.EventTypes.Commands;
using Univent.Application.EventTypes.Queries;

namespace Univent.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventTypesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventTypesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventTypes()
        {
            var query = new GetAllEventTypes();
            var response = await _mediator.Send(query);
            var eventTypes = _mapper.Map<List<EventTypeResponse>>(response);

            return Ok(eventTypes);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateEventType(EventTypeCreate evType)
        {
            var command = _mapper.Map<CreateEventTypeCommand>(evType);
            var response = await _mediator.Send(command);
            var eventType = _mapper.Map<EventTypeResponse>(response);

            return CreatedAtAction(nameof(GetEventTypeById), new { id = response.EventTypeID }, eventType);
        }

        [HttpGet]
        [Route(ApiRoutes.EventTypes.IdRoute)]
        public async Task<IActionResult> GetEventTypeById(Guid id)
        {
            var query = new GetEventTypeById { EventTypeID = id };
            var response = await _mediator.Send(query);
            var eventType = _mapper.Map<EventTypeResponse>(response);

            return Ok(eventType);
        }

        [HttpPatch]
        [Route(ApiRoutes.EventTypes.IdRoute)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateEventType(Guid id, EventTypeUpdate updatedEventType)
        {
            var command = _mapper.Map<UpdateEventTypeCommand>(updatedEventType);
            command.EventTypeID = id;
            var response = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.EventTypes.IdRoute)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteEventType(Guid id)
        {
            var command = new DeleteEventTypeCommand { EventTypeID = id };
            var response = await _mediator.Send(command);

            return NoContent();
        }
    }
}
