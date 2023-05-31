using System;
using System.Collections.Generic;
using System.Diagnostics;
using PishiStiray.Models.DbEntities;

namespace PishiStiray;

public partial class ProductDB
{
    public string ProductArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public int ProductCategory { get; set; }

    public string? ProductPhoto { get; set; }

    public int ProductManufacturer { get; set; }

    public decimal? ProductCost { get; set; }

    public sbyte? ProductDiscountAmount { get; set; }

    public int ProductQuantityInStock { get; set; }

    public string ProductStatus { get; set; } = null!;

    public int? ProductCurrentDiscount { get; set; }

    public int? ProductDelivery { get; set; }

    public virtual ICollection<Orderproduct> Orderproducts { get; } = new List<Orderproduct>();

    public virtual ProductCategory ProductCategoryNavigation { get; set; } = null!;

    public virtual ProductDelivery? ProductDeliveryNavigation { get; set; }

    public virtual Manufacturer ProductManufacturerNavigation { get; set; } = null!;
    public decimal? NewPrice
    {
        get
        {
            if (ProductDiscountAmount != 0)
            {
                return ProductCost - (ProductCost * ((decimal?)Convert.ToSingle(ProductDiscountAmount) / 100));
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
