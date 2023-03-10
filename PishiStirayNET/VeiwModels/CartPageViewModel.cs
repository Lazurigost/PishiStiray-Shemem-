using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStirayNET.Infrastructure;
using PishiStirayNET.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace PishiStirayNET.VeiwModels
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
