using Application.CrossCuttingConcerns.Exceptions.Extensions;
using Application.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Application.CrossCuttingConcerns.Exceptions.ExceptionProblemDetails;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Application.CrossCuttingConcerns.Exceptions
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ex= ex.InnerException == null ? ex : ex.InnerException;
                await Handle(ex,context);
            }
        }
        public Task Handle(Exception ex,HttpContext context)
        {
            context.Response.ContentType = "application/json";
            if (ex.GetType() == typeof(BusinessException)) return HandleBusiness((BusinessException)ex, context);
            if (ex.GetType() == typeof(AuthenticationException)) return HandleAuthentication((AuthenticationException)ex, context);
            if (ex.GetType() == typeof(AuthorizationException)) return HandleAuthorization((AuthorizationException)ex, context);
            if (ex.GetType() == typeof(ValidationException)) return HandleValidation((ValidationException)ex, context);
            return HandleGeneral(ex, context);
        }
        public Task HandleBusiness(BusinessException exception,HttpContext httpContext)
        {
            ProblemDetails problemDetails = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = exception.GetType().Name,
                Detail=exception.Message
            };

            return httpContext.Response.WriteAsync(problemDetails.ToJsonString());
        }
        public Task HandleAuthentication(AuthenticationException exception,HttpContext httpContext)
        {
            ProblemDetails problemDetails = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = exception.GetType().Name,
                Detail = exception.Message
            };

            return httpContext.Response.WriteAsync(problemDetails.ToJsonString());
        }
        public Task HandleAuthorization(AuthorizationException exception, HttpContext httpContext)
        {
            ProblemDetails problemDetails = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = exception.GetType().Name,
                Detail = exception.Message
            };

            return httpContext.Response.WriteAsync(problemDetails.ToJsonString());
        }
        public Task HandleValidation(ValidationException exception,HttpContext httpContext)
        {
            ValidationProblemDetail problemDetails = new ValidationProblemDetail()
            {
                Status = (int)HttpStatusCode.UnprocessableEntity,
                Title = exception.GetType().Name,
                Detail = exception.Message,
                Errors = exception.Errors.ToDictionary()
            };

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
        public Task HandleGeneral(Exception exception,HttpContext httpContext)
        {
            ProblemDetails problemDetails = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = exception.GetType().Name,
                Detail = exception.Message
            };

            return httpContext.Response.WriteAsync(problemDetails.ToJsonString());
        }
    }
}
