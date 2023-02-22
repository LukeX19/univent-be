﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Univent.Api.Contracts.Error;

namespace Univent.Api.Filters
{
    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly string _key;

        public ValidateGuidAttribute(string key)
        {
            _key = key;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue(_key, out var value))
                return;
            if (Guid.TryParse(value?.ToString(), out var guid))
                return;

            var apiError = new ErrorResponse
            {
                StatusCode = 400,
                StatusMessage = "Bad Request",
                Timestamp = DateTime.Now,
            };
            apiError.Errors.Add($"The identifier for {_key} does not have a correct GUID format");

            context.Result = new ObjectResult(apiError);
        }
    }
}