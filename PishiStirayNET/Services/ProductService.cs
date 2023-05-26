﻿using Microsoft.EntityFrameworkCore;
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
        //Получение списка всех продуктов
        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new();
            List<ProductDB> productDBs = await _context.Products.ToListAsync();
            await _context.ProductManufacturers.ToListAsync();
            await _context.Deliveries.AsNoTracking().ToListAsync();

            await Task.Run(() =>
            {
                foreach (ProductDB product in productDBs)
                {
                    products.Add(new Product
                    {
                        ProductArticleNumber = product.ProductArticleNumber,
                        ProductDiscountAmount = product.ProductDiscountAmount,
                        ProductDescription = product.ProductDescription,
                        Image = (product.ProductPhoto == null || string.IsNullOrWhiteSpace(product.ProductPhoto) == true) ? "picture.png" : product.ProductPhoto,
                        ProductCost = product.ProductCost,
                        ProductManufacturerNavigation = product.ProductManufacturerNavigation,
                        ProductManufacturer = product.ProductManufacturer,
                        ProductName = product.ProductName,
                        ProductQuantityInStock = product.ProductQuantityInStock,
                        ProductCategoryNavigation = product.ProductCategoryNavigation,
                        ProductDelivery = product.ProductDelivery,
                        ProductCurrentDiscount = product.ProductCurrentDiscount
                    });
                }
            });
            return products;
            
        }
        public async Task<List<Manufacturer>> GetManufacturersAsync() => await _context.ProductManufacturers.ToListAsync();
        public async Task<List<ProductCategory>> GetCategoriesAsync() => await _context.ProductCategories.ToListAsync();
        
        public async Task<ObservableCollection<Product>> GetCartItemsAsync(ObservableCollection<CartItem> cartItems)
        {
            ObservableCollection<Product> cartProducts = new();
            await Task.Run(() =>
            {
                foreach (CartItem cartItem in cartItems)
                {
                    cartProducts.Add(cartItem.product);
                }

                
            });
            return cartProducts;
        }
        //Изменение продукта
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
    }
}
