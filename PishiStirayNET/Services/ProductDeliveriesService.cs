using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.Services
{
    public partial class ProductDeliveriesService
    {
        private readonly TradeContext _tradeContext;

        public ProductDeliveriesService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }

        public async Task<List<ProductDelivery>> GetAllDeliveriesAsync()
        {
            return await _tradeContext.ProductDeliveries.ToListAsync();
        }

        public async void ChangeDelivery(ProductDelivery delivery)
        {
            ProductDelivery newDelivery = await _tradeContext.ProductDeliveries.Where(d => d.IdproductDeliveries.Equals(delivery.IdproductDeliveries)).FirstOrDefaultAsync();

            if (newDelivery != null)
            {
                newDelivery.DeliveryName = delivery.DeliveryName;
                await _tradeContext.SaveChangesAsync();
            }
        }

        public async void AddDelivery(ProductDelivery delivery)
        {
            delivery.IdproductDeliveries = await _tradeContext.ProductDeliveries.MaxAsync(d => d.IdproductDeliveries) + 1;
            await _tradeContext.ProductDeliveries.AddAsync(delivery);
            await _tradeContext.SaveChangesAsync();
        }
    }
}
