using Core.Concretes.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Business.Middlewares
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate next;

        public GlobalExceptionHandling(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new Reply
                {
                    Success = false,
                    Message = ex.Message
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
