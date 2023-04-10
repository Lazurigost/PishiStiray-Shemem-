using Microsoft.EntityFrameworkCore;
using PishiStiray.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using PishiStiray.Models.DbEntities;
using System.IO;
using System;
using System.Linq;

namespace PishiStiray.Services
{
    public class ProductService
    {
        private readonly TradeContext _context;

        public ProductService(TradeContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDB>> GetProductsAsync()
        {
            List<ProductDB> products = new();
            List<ProductDB> productDBs = await _context.Products.ToListAsync();
            await _context.ProductManufacturers.ToListAsync();
            await _context.Deliveries.AsNoTracking().ToListAsync();

            await Task.Run(() =>
            {
                Debug.WriteLine(productDBs.Count);
                foreach (ProductDB product in productDBs)
                {
                    products.Add(new ProductDB
                    {
                        ProductArticleNumber = product.ProductArticleNumber,
                        ProductDiscountAmount = product.ProductDiscountAmount,
                        ProductDescription = product.ProductDescription,
                        ProductPhoto = Path.GetFullPath(@$"Resources\{product.ProductPhoto}"),
                        ProductCost = product.ProductCost,
                        ProductManufacturer = product.ProductManufacturer,
                        ProductName = product.ProductName
                    });
                }
            });
            return products;
            
        }
        
        public async Task<ObservableCollection<ProductDB>> GetCartItemsAsync(ObservableCollection<CartItem> cartItems)
        {
            ObservableCollection<ProductDB> cartProducts = new();
            await Task.Run(() =>
            {
                foreach (CartItem cartItem in cartItems)
                {
                    cartProducts.Add(cartItem.product);
                }

                
            });
            return cartProducts;
        }

        public async Task<List<Delivery>> GetPointsAsync() => await _context.Deliveries.AsNoTracking().ToListAsync();

        public async void ChangeProduct(ProductDB productDB)
        {
            ProductDB? product = await _context.Products.Where(p => p.ProductArticleNumber == productDB.ProductArticleNumber).SingleOrDefaultAsync();

            if (product != null) 
            {
                product.ProductManufacturer = productDB.ProductManufacturer;
                product.ProductPhoto = productDB.ProductPhoto;
                product.ProductDiscountAmount = productDB.ProductDiscountAmount;
                product.ProductDelivery = productDB.ProductDelivery;
                product.ProductCategory = productDB.ProductCategory;
                product.ProductDescription = productDB.ProductDescription;
                product.ProductName = productDB.ProductName;
                product.ProductStatus = productDB.ProductStatus;
                product.ProductCost = productDB.ProductCost;
                product.ProductQuantityInStock = productDB.ProductQuantityInStock;

                _context.SaveChanges();
            }
        }
        public async Task<List<ProductCategory>> GetCategoriesAsync() => await _context.ProductCategories.AsNoTracking().ToListAsync();
    }
}
