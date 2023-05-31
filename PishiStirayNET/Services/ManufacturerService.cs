using Microsoft.EntityFrameworkCore;
using PishiStiray.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.Services
{
    public class ManufacturerService
    {
        private readonly TradeContext _tradeContext;

        public ManufacturerService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }

        public async Task<List<Manufacturer>> GetAllManufacturersAsync()
        {
            return await _tradeContext.ProductManufacturers.ToListAsync();
        }

        public async void ChangeManufacturer(Manufacturer manufacturer)
        {
            Manufacturer newManufacturer = await _tradeContext.ProductManufacturers.Where(m => m.IdproductManufacturers == manufacturer.IdproductManufacturers).FirstOrDefaultAsync();

            if (newManufacturer != null)
            {
                newManufacturer.ManufacturerName = manufacturer.ManufacturerName;
                await _tradeContext.SaveChangesAsync();
            }
        }


        public async void AddManufacturer(Manufacturer manufacturer)
        {
            manufacturer.IdproductManufacturers = await _tradeContext.ProductManufacturers.MaxAsync(m => m.IdproductManufacturers) + 1;
            _tradeContext.ProductManufacturers.Add(manufacturer);
            await _tradeContext.SaveChangesAsync();
        }
    }
}
