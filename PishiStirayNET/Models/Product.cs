using System.IO;
using System.Windows.Media;

namespace PishiStiray.Models
{
    public class Product:ProductDB
    {
        public string Image { get; set; }

        public string ImageUrl 
        {
            get
            {
                return Path.GetFullPath(@$"Resources\{Image}");
            }
        }
        public new float? NewPrice
        {
            get
            {
                if (ProductCurrentDiscount != 0)
                {
                    return (float?)(ProductCost - (ProductCost * (ProductCurrentDiscount / 100)));
                }
                return 0;
            }
        }
        public new bool HaveDiscount
        {
            get
            {
                return NewPrice != null;
            }
        }

    }
}
