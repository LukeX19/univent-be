﻿using Newtonsoft.Json;
using Univent.Application.Exceptions;

namespace Univent.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            CustomErrorResponse? customException = new CustomErrorResponse(exception);

            switch(exception)
            {
                //exception is IdentityUserNotFoundException or ObjectNotFoundException
                case IdentityUserNotFoundException:
                case ObjectNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case InvalidModelStateException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case IdentityUserAlreadyExistsException:
                    statusCode = StatusCodes.Status409Conflict;
                    break;

                case EventUpdateNotPossibleException:
                case EventDeleteNotPossibleException:
                    statusCode = StatusCodes.Status403Forbidden;
                    break;

                default:
                    break;
            }

            httpContext.Response.StatusCode = statusCode;

            if(customException is null)
            {
                await httpContext.Response.CompleteAsync();
            }
            else
            {
                //_logger.LogError(exception.Message);
                httpContext.Response.ContentType = "application/json;charset=utf-8";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(customException));
            }
        }
    }
}
