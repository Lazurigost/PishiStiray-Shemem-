using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models;
using PishiStiray.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PishiStiray.Views.Pages;

namespace PishiStiray.VeiwModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Product> cartItemsList;
        [ObservableProperty]
        private ObservableCollection<CartItem> cartProductsList;
        [ObservableProperty]
        private List<Delivery> pickupPoints;
        [ObservableProperty]
        private Product selectedCartItem;
        [ObservableProperty]
        private CartItem selectedCart;
        [ObservableProperty]
        private string totalPrice;

        private float? TP = 0;

        private readonly ProductService productService_;
        private readonly PageService pageService_;

        public CartPageViewModel(ProductService productService, PageService pageService)
        {
            pageService_ = pageService;
            productService_ = productService;
            cartProductsList = Cart.CartProductList;
            UpdateCart();
        }
        public async void UpdateCart()
        {
            //TP = 0;
            //foreach (CartItem CItem in cartProductsList)
            //{
            //    TP += CItem.product.Price;
            //}
            //totalPrice = TP.ToString();
            cartItemsList = await productService_.GetCartItemsAsync(cartProductsList);
            //pickupPoints = await productService_.GetPointsAsync();
        }
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
                cartItemsList.Remove(SelectedCartItem);
            }
            //UpdateCart();
        }
        [RelayCommand]
        private void BackFromCart() 
        {
            pageService_.ChangePage(new ProductsPage());
        }
    }
}
