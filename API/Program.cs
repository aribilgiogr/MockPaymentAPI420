using API.Filters;
using Business.Middlewares;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessServices(builder.Configuration);

var app = builder.Build();

app.UseBusinessServices();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/process/payment", async (PaymentRequest request, IPaymentService paymentService) =>
{
    var transaction = new Transaction
    {
        OrderId = request.OrderId,
        Amount = request.Amount,
        CardHolderName = request.CardHolderName,
        MaskedCardNumber = $"**** **** **** {request.CardNumber[^4..]}"
    };
    var result = await paymentService.ProcessPaymentAsync(transaction);
    return result.Success ? Results.Ok(result) : Results.BadRequest(result);
}).AddEndpointFilter<RequestValidationFilter<PaymentRequest>>();

app.MapPost("/api/process/refund", async (RefundRequest request, IPaymentService paymentService) =>
{
    var result = await paymentService.ProcessRefundAsync(request.OrderId, request.Amount);
    return result.Success ? Results.Ok(result) : Results.BadRequest(result);
}).AddEndpointFilter<RequestValidationFilter<RefundRequest>>();

app.Run();