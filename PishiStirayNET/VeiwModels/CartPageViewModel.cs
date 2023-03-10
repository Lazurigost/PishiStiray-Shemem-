using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace PishiStiray.VeiwModels
{
    public partial class CartPageViewModel : ObservableObject
    {

        [ObservableProperty]
        private List<CartItem> cartProductsList;
        [ObservableProperty]
        private CartItem selectedCartItem;

        public CartPageViewModel()
        {
            cartProductsList = Cart.CartProductList;
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
