using CommunityToolkit.Mvvm.ComponentModel;

namespace PishiStiray.Models
{
    public partial class CartItem : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Cost))]
        public ProductDB? product;

        [ObservableProperty]
        private int count;

        public float? Cost => (float?)(Product.ProductCost * Count);
        public float? Discount => (float?)(Product.ProductCost * Count) - (float?)Product?.NewPrice * Count;
    }
}
