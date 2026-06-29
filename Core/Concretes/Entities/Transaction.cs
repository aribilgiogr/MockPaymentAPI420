using Core.Abstracts.Bases;
using Core.Concretes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Entities
{
    public class Transaction : BaseEntity
    {
        public required string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string CardHolderName { get; set; } = string.Empty;
        public string MaskedCardNumber { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
