using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models;
using PishiStiray.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PishiStiray.Views.Pages;
using System.Security.Policy;

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
        private CartItem selectedCart;

        [ObservableProperty]
        private decimal? totalPrice = 0;

        [ObservableProperty]
        private float? finalPrice = 0;
        #endregion

        private readonly ProductService productService_;
        private readonly PageService pageService_;

        public CartPageViewModel(ProductService productService, PageService pageService)
        {
            pageService_ = pageService;
            productService_ = productService;
            cartProductsList = Cart.CartProductList;
            UpdateCart();
        }

        //Обновление корзины
        public async void UpdateCart()
        {
            cartItemsList = await productService_.GetCartItemsAsync(cartProductsList);

            totalPrice = 0;
            finalPrice = 0;

            foreach (var cartItem in cartProductsList) 
            {
                totalPrice += cartItem.product.ProductCost;
                //finalPrice += cartItem.product.NewPrice;
            }

            //pickupPoints = await productService_.GetPointsAsync();
        }
        #region Команды

        //Удаление из корзины
        [RelayCommand]
        private void RemoveFromCart()
        {
            if (SelectedCartItem != null) 
            {
                
                foreach (CartItem CItem in cartProductsList)
                {
                    if (SelectedCartItem == CItem.product)
                    {
                        cartProductsList.Remove(CItem);
                        
                        break;
                    }
                }
                totalPrice -= selectedCartItem.ProductCost;
                //finalPrice -= selectedCartItem.NewPrice;
                cartItemsList.Remove(SelectedCartItem);
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
