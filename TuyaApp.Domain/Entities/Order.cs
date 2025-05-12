using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyaApp.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsCanceled { get; private set; }
        public Customer? Customer { get; private set; }
        protected Order() { }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
            IsCanceled = false;
        }

        public void Cancel()
        {
            if (IsCanceled)
                throw new InvalidOperationException("La orden ya está cancelada.");
            IsCanceled = true;
        }
    }
}

