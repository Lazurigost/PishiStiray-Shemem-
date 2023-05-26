using Microsoft.VisualBasic.ApplicationServices;
using PishiStiray.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.Infrastructure
{
    public static class Global
    {
        public static User User { get; set; }
        public static Product Product { get; set; } = null;
        public static ObservableCollection<CartItem> CartProductList { get; set; } = new();
        public static Order Order { get; set; } = new();
    }
}
