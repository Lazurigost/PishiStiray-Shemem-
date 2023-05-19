using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models;
using PishiStiray.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PishiStiray.Views.Pages;
using System.Security.Policy;
using System.Windows.Controls;
using System.Linq;
using System.Threading.Tasks;

namespace PishiStiray.VeiwModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        #region Свойства

        [ObservableProperty]
        private ObservableCollection<ProductDB> cartItemsList;

        [ObservableProperty]
        private ObservableCollection<CartItem> cartProductsList;

        [ObservableProperty]
        private List<Delivery> pickupPoints;

        [ObservableProperty]
        private ProductDB selectedCartItem;

        [ObservableProperty]
        private Delivery selectedPoint;

        [ObservableProperty]
        private CartItem selectedCart;
        
        [ObservableProperty]
        private decimal? totalPrice = 0;

        [ObservableProperty]
        private decimal? finalPrice = 0;
        #endregion

        private readonly ProductService productService_;
        private readonly PageService pageService_;
        private readonly DeliveryService deliveryService_;

        public CartPageViewModel(DeliveryService deliveryService,ProductService productService, PageService pageService)
        {
            pageService_ = pageService;
            productService_ = productService;
            deliveryService_ = deliveryService;
            cartProductsList = Cart.CartProductList;

            Task.Run(async () =>
            {
                pickupPoints = await deliveryService_.GetDeliveriesAsync();
            });

            UpdateCart();
        }

        //Обновление корзины
        public async void UpdateCart()
        {
            CartItemsList = await productService_.GetCartItemsAsync(cartProductsList);

            TotalPrice = 0;
            FinalPrice = 0;

            foreach (var cartItem in CartProductsList)
            {
                TotalPrice += cartItem.product.ProductCost;
                FinalPrice += cartItem.product.NewPrice;
            }
        }
        #region Команды

        //Удаление из корзины
        [RelayCommand]
        private void RemoveFromCart()
        {
            if (SelectedCartItem != null) 
            {
                foreach (CartItem CItem in CartProductsList)
                {
                    if (SelectedCartItem == CItem.product)
                    {
                        CartProductsList.Remove(CItem);
                        break;
                    }
                }
                CartItemsList.Remove(SelectedCartItem);
                UpdateCart();
            }
        }

        //Возвращение по кнопке
        [RelayCommand]
        private void BackFromCart() 
        {
            pageService_.ChangePage(new ProductsPage());
        }
        #endregion
    }
}
