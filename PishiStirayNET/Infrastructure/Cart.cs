using PishiStiray.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PishiStiray.Infrastructure
{
    public static class Cart
    {
        public static ObservableCollection<CartItem> CartProductList { get; set; } = new();
    }
}
