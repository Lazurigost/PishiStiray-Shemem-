using Microsoft.EntityFrameworkCore;
using PishiStiray.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using PishiStiray.Models.DbEntities;

namespace PishiStiray.Services
{
    public class ProductService
    {
        private readonly TradeContext _context;

        public ProductService(TradeContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Product>> GetProductsAsync()
        {
            List<Models.Product> products = new();
            List<ProductDB> productDBs = await _context.Products.ToListAsync();
            await _context.ProductManufacturers.ToListAsync();

            await Task.Run(() =>
            {
                Debug.WriteLine(productDBs.Count);
                foreach (ProductDB product in productDBs)
                {
                    products.Add(new Models.Product
                    {
                        Article = product.ProductArticleNumber,
                        CurrentDiscount = product.ProductDiscountAmount,
                        Description = product.ProductDescription,
                        Image = product.ProductPhoto,
                        Price = ((float)product.ProductCost),
                        Manufacturer = product.ProductManufacturerNavigation.ManufacturerName,
                        Title = product.ProductName
                    });
                }
            });

            return products;
        }
        
        public async Task<ObservableCollection<Product>> GetCartItemsAsync(ObservableCollection<CartItem> cartItems)
        {
            ObservableCollection<Product> cartProducts = new();

            foreach (CartItem cartItem in cartItems)
            {
                cartProducts.Add(cartItem.product);
            }

            return cartProducts;
        }

        public async Task<List<Delivery>> GetPointsAsync() => await _context.Deliveries.AsNoTracking().ToListAsync();
    }
}
