using FluentValidation;

namespace API.Filters
{
    public class RequestValidationFilter<T>: IEndpointFilter where T : class
    {
        private readonly IValidator<T> validator;

        public RequestValidationFilter(IValidator<T> validator)
        {
            this.validator = validator;
        }

        public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
