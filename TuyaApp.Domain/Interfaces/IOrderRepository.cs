using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using TuyaApp.Domain.Entities;

namespace TuyaApp.Domain.Interfaces
{
   //no usar Delete porque las órdenes no se eliminan, se cancelan(regla de negocio).
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task AddAsync(Order order);

        Task<IEnumerable<Order>> GetAllAsync();

        Task UpdateAsync(Order order);
    }
}
