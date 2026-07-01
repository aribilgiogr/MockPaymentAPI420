namespace Core.Concretes.DTOs
{
    public class PaymentRequest
    {
        public string OrderId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string CardHolderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string ExpirationMonth { get; set; } = string.Empty;
        public string ExpirationYear { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;
    }

    public class RefundRequest
    {
        public string OrderId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
