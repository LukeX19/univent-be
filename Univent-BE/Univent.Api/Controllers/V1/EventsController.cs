﻿using Microsoft.AspNetCore.Mvc;

namespace Univent.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class EventsController : Controller
    {
        [HttpGet]
        [Route(ApiRoutes.Events.IdRoute)]
        public IActionResult GetByID(int id)
        {
            return Ok();
        }
    }
}
