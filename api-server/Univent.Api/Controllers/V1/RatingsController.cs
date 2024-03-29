﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univent.Api.Contracts.Rating.Requests;
using Univent.Api.Contracts.Rating.Responses;
using Univent.Application.Ratings.Commands;
using Univent.Application.Ratings.Queries;

namespace Univent.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RatingsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RatingsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            var query = new GetAllRatings();
            var response = await _mediator.Send(query);
            var ratings = _mapper.Map<List<RatingResponse>>(response);
        
            return Ok(ratings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating(RatingCreate rat)
        {
            var command = _mapper.Map<CreateRatingCommand>(rat);
            var response = await _mediator.Send(command);
            var rating = _mapper.Map<RatingResponse>(response);

            return CreatedAtAction(nameof(GetRatingById), new { id = response.RatingID }, rating);
        }

        [HttpGet]
        [Route(ApiRoutes.Ratings.UserIdRoute)]
        public async Task<IActionResult> GetAverageRatingByUserId(Guid id)
        {
            var query = new GetAverageRatingByUserId { UserProfileID = id };
            var response = await _mediator.Send(query);
            var value = _mapper.Map<AverageRatingResponse>(response);

            return Ok(value);
        }

        [HttpGet]
        [Route(ApiRoutes.Ratings.IdRoute)]
        public async Task<IActionResult> GetRatingById(Guid id)
        {
            var query = new GetRatingById { RatingID = id };
            var response = await _mediator.Send(query);
            var rating = _mapper.Map<RatingResponse>(response);

            return Ok(rating);
        }

        [HttpDelete]
        [Route(ApiRoutes.Ratings.IdRoute)]
        public async Task<IActionResult> DeleteRating(Guid id)
        {
            var command = new DeleteRatingCommand { RatingID = id };
            var response = await _mediator.Send(command);

            return NoContent();
        }
    }
}
