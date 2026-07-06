using Core.Concretes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Middlewares
{
    public class ApiKeyHandler
    {
        private readonly RequestDelegate next;
        private const string API_KEY_HEADER_NAME = "X-Api-Key";

        public ApiKeyHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var response = new Reply
                {
                    Success = false,
                    Message = "API anahtarı bulunamadı!"
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }

            var apiKey = configuration.GetSection("ApiKey").Value;

            if (apiKey == null || !apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var response = new Reply
                {
                    Success = false,
                    Message = "API anahtarı geçersiz!"
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }

            await next(context);
        }
    }
}
