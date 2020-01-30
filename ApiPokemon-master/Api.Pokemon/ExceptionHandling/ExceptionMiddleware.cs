using System;
using System.Net;
using System.Threading.Tasks;
using Api.Pokemon.Data;
using Api.Pokemon.Data.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.Pokemon.ExceptionHandling
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate Next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.Next(context);
            }
            catch (Exception ex)
            {
                await ExceptionResponseAsync(context, ex);
            }
        }

        private static async Task ExceptionResponseAsync(HttpContext context, Exception ex)
        {
            Error response;
            switch (ex)
            {
                case AddingException val:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new Error
                    {
                        Message = "Такой покемон уже существует"
                    };
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new Error
                    {
                        Message = "Непредвиденная ошибка"
                    };
                    break;
            }
            var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
