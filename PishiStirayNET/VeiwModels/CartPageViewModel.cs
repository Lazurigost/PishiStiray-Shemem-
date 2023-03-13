using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models;
using PishiStiray.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PishiStiray.VeiwModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CartItem> cartProductsList;
        [ObservableProperty]
        private CartItem selectedCartItem;

        private readonly ProductService productService_;

        public CartPageViewModel(ProductService productService)
        {
            productService_ = productService;
        }
        public async void UpdateCart()
        {
            
        }
        [RelayCommand]
        private void RemoveFromCart()
        {
            if (SelectedCartItem != null) 
            {
                cartProductsList.Remove(SelectedCartItem);
            }
        }
    }
}
