using CommunityToolkit.Mvvm.ComponentModel;
using PishiStiray.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.Models
{
    public partial class Order : Orderuser
    {
        [ObservableProperty]
        private float fullPrice;

        [ObservableProperty]
        private float discount;

        public float? Price
        {
            get => FullPrice - Discount;
        }

        [ObservableProperty]
        private List<int> productQuatities;

        [ObservableProperty]
        private List<CartItem> products;
    }
}
