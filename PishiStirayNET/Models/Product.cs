using System.IO;
using System.Windows.Media;

namespace PishiStiray.Models
{
    public class Product:ProductDB
    {
        public string? Article { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public float? Price { get; set; }

        public float? CurrentDiscount { get; set; }

        private string _image;

        public string Image 
        {
            get
            {
                return _image;
            }
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                {
                    _image = "picture.png";
                    return;
                }
                _image = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return Path.GetFullPath(@$"Resources\{Image}");
            }
        }
        public float? NewPrice
        {
            get
            {
                if (CurrentDiscount != 0)
                {
                    return Price - (Price * (CurrentDiscount / 100));
                }
                return 0;
            }
        }
        public bool HaveDiscount
        {
            get
            {
                return NewPrice != null;
            }
        }

    }
}
