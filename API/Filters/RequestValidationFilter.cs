using Core.Concretes.Models;
using FluentValidation;

namespace API.Filters
{
    public class RequestValidationFilter<T> : IEndpointFilter where T : class
    {
        private readonly IValidator<T> validator;

        public RequestValidationFilter(IValidator<T> validator)
        {
            this.validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var argument = context.Arguments.FirstOrDefault(x => x is T) as T;
            if (argument == null)
            {
                return Results.BadRequest(new Reply
                {
                    Success = false,
                    Message = "İstek gövdesi (Request Body) boş veya geçersiz!"
                });
            }

            var validationResult = await validator.ValidateAsync(argument);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));

                return Results.BadRequest(new Reply
                {
                    Success = false,
                    Message = errorMessages
                });
            }

            return await next(context);
        }
    }
}
