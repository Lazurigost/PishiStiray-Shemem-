using PishiStiray.Models;
using System.Collections.Generic;

namespace PishiStiray.Infrastructure
{
    public static class Cart
    {
        public static List<CartItem> CartProductList { get; set; } = new();
    }
}
