using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Models
{
    public class Reply
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class Reply<T> : Reply
    {
        public T? Data { get; set; }
    }
}
