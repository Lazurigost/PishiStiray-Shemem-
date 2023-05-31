using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PishiStiray.Models.DbEntities;
using PishiStiray.Models;
using PishiStiray.Infrastructure;
using System.Collections.ObjectModel;

namespace PishiStiray.Services
{
    public class OrderService
    {
        private readonly TradeContext _tradeContext;

        public OrderService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }
        //Получение всех точек доставки для последующих функций
        public async Task<List<Delivery>> GetDeliveriesAsync()
        {
            List<Delivery> deliveries = new List<Delivery>();

            await Task.Run(async () =>
            {
                deliveries = await _tradeContext.Deliveries.AsNoTracking().ToListAsync();
            });

            return deliveries;
        }
        //Создание заказа и запись в бд
        public async Task<Order> CreateOrder(List<CartItem> cartItems, int deliveryId)
        {
            int orderNumber = _tradeContext.Orderusers.Max(o => o.OrderId) + 1;
            int aquireCode = _tradeContext.Orderusers.Max(o => o.OrderAquireCode) + 1;

            await _tradeContext.Orderusers.AddAsync(new Orderuser
            {
                OrderId = orderNumber,
                OrderStatus = "Новый",
                OrderDeliveryDate = DateOnly.FromDateTime(DateTime.Now),
                OrderPickupPoint = deliveryId,
                OrdererFio = CurrentUser.User != null ? $"{CurrentUser.User.UserSurname} {CurrentUser.User.UserName} {CurrentUser.User.UserPatronymic}" : null,
                OrderAquireCode = aquireCode
            });

            List<Orderproduct> orderproductList = new List<Orderproduct>();
            foreach (CartItem cartItem in cartItems)
            {
                orderproductList.Add(new Orderproduct
                {
                    OrderId = orderNumber,
                    ProductArticleNumber = cartItem.product.ProductArticleNumber,
                    ProductAmount = cartItem.Count
                });
            }

            foreach (CartItem cartItem in cartItems)
            {
                ProductDB? product = await _tradeContext.Products.Where(p => p.ProductArticleNumber == cartItem.product.ProductArticleNumber).SingleOrDefaultAsync();

                if (product != null)
                {
                    product.ProductQuantityInStock -= cartItem.Count;
                }
            }

            await _tradeContext.Orderproducts.AddRangeAsync(orderproductList);
            await _tradeContext.SaveChangesAsync();

            return new Order
            {
                OrderId = orderNumber,
                OrderStatus = "Новый",
                OrderDeliveryDate = DateOnly.FromDateTime(DateTime.Now),
                OrderPickupPoint = deliveryId,
                OrdererFio = CurrentUser.User != null ? $"{CurrentUser.User.UserSurname} {CurrentUser.User.UserName} {CurrentUser.User.UserPatronymic}" : "Гость",
                OrderAquireCode = aquireCode,
                Products = await GetProducts(orderproductList),
                FullPrice = (float)orderproductList.Sum(i => i.ProductArticleNumberNavigation.ProductCost),
                Discount = (float)orderproductList.Sum(i => i.ProductArticleNumberNavigation.ProductDiscountAmount)
            };
        }
        private List<int> GetProductsQuatities(ICollection<Orderproduct> orderproducts)
        {
            List<int> quantities = new List<int>();

            foreach (var product in orderproducts.ToList())
            {
                quantities.Add(product.ProductArticleNumberNavigation.ProductQuantityInStock);
            }

            return quantities;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            List<Orderuser> order1s = await _tradeContext.Orderusers.ToListAsync();
            List<Order> orders = new List<Order>();
            foreach (Orderuser order1 in order1s)
            {

                orders.Add(new Order
                {
                    OrderId = order1.OrderId,
                    OrderAquireCode = order1.OrderAquireCode,
                    OrdererFio = order1.OrdererFio,
                    OrderDeliveryDate = order1.OrderDeliveryDate,
                    OrderPickupPoint = order1.OrderPickupPoint,
                    OrderPickupPointNavigation = order1.OrderPickupPointNavigation,
                    OrderStatus = order1.OrderStatus,
                    FullPrice = (float)order1.Orderproducts.ToList().Sum(op => op.ProductAmount * op.ProductArticleNumberNavigation.ProductCost),
                    Discount = (float)order1.Orderproducts.ToList().Sum(op => op.ProductAmount * (op.ProductArticleNumberNavigation.ProductCost / Convert.ToDecimal(100) * op.ProductArticleNumberNavigation.ProductDiscountAmount)),
                    ProductQuatities = GetProductsQuatities(order1.Orderproducts),
                    Products = await GetProducts(order1.Orderproducts)

                });

            }



            return orders;
        }
        //Получение продуктов для других функций
        private async Task<List<CartItem>> GetProducts(ICollection<Orderproduct> orderproducts) 
        {
            List<CartItem> products = new List<CartItem>();

            foreach (var product in orderproducts.ToList())
            {
                CartItem cartItem = new CartItem();
                cartItem.product = new Product
                {
                    ProductArticleNumber = product.ProductArticleNumberNavigation.ProductArticleNumber,
                    ProductManufacturer = product.ProductArticleNumberNavigation.ProductManufacturer,
                    Image = (product.ProductArticleNumberNavigation.ProductPhoto == null || string.IsNullOrWhiteSpace(product.ProductArticleNumberNavigation.ProductPhoto) == true) ? "picture.png" : product.ProductArticleNumberNavigation.ProductPhoto,
                    ProductDiscountAmount = product.ProductArticleNumberNavigation.ProductDiscountAmount,
                    ProductDelivery = product.ProductArticleNumberNavigation.ProductDelivery,
                    ProductCategory = product.ProductArticleNumberNavigation.ProductCategory,
                    ProductDescription = product.ProductArticleNumberNavigation.ProductDescription,
                    ProductName = product.ProductArticleNumberNavigation.ProductName,
                    ProductStatus = product.ProductArticleNumberNavigation.ProductStatus,
                    ProductCost = product.ProductArticleNumberNavigation.ProductCost,
                    ProductQuantityInStock = product.ProductArticleNumberNavigation.ProductQuantityInStock,
                    ProductManufacturerNavigation = product.ProductArticleNumberNavigation.ProductManufacturerNavigation,
                    ProductCategoryNavigation = product.ProductArticleNumberNavigation.ProductCategoryNavigation,
                };
                products.Add(cartItem);
                cartItem.Count = products.Count();
            }
            return products;
        }
        //Функция изменения заказа
        public async void ChangeOrder(Order order)
        {
            Orderuser newOrder = await _tradeContext.Orderusers.Where(o => o.OrderId == order.OrderId).FirstOrDefaultAsync();

            if (newOrder != null)
            {
                newOrder.OrderDeliveryDate = order.OrderDeliveryDate;
                newOrder.OrderStatus = order.OrderStatus;

                await _tradeContext.SaveChangesAsync();
            }
        }
    }
}
