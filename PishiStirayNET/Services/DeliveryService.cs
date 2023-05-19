using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.Services
{
    public class DeliveryService
    {
        private readonly TradeContext _tradeContext;
        public DeliveryService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }
        public async Task<List<Delivery>> GetDeliveriesAsync()
        {
            return await _tradeContext.Deliveries.ToListAsync();
        }
    }
}
