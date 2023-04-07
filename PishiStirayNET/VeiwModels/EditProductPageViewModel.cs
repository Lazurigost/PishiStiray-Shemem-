using CommunityToolkit.Mvvm.ComponentModel;
using PishiStiray.Infrastructure;
using PishiStiray.Services;
using System;
using System.Collections.Generic;

namespace PishiStiray.VeiwModels
{
    public partial class EditProductPageViewModel : ObservableObject
    {
        #region Сервисы
        private readonly ProductService productService_;
        private readonly PageService pageService_;
        #endregion
        [ObservableProperty]
        private ProductDB? product;
        [ObservableProperty]
        private List<ProductDB> products;
        [ObservableProperty]
        private string? title;
        [ObservableProperty]
        private string? article;
        [ObservableProperty]
        private string? description;
        [ObservableProperty]
        private decimal? cost;
        [ObservableProperty]
        private decimal? discount;
        [ObservableProperty]
        private int? amount;
        public EditProductPageViewModel(ProductService productService, PageService pageService)
        {
            pageService_ = pageService;
            productService_ = productService;

            product = EditableProduct.editedProduct;
            LoadProductData();
        }
        protected virtual async void LoadProductData()
        {
            try
            {
                if (EditableProduct.editedProduct != null)
                {
                    Title = EditableProduct.editedProduct.ProductName;
                    Article = EditableProduct.editedProduct.ProductArticleNumber;
                    Description = EditableProduct.editedProduct.ProductDescription;
                    Cost = EditableProduct.editedProduct.ProductCost;
                    Discount = (decimal?)Convert.ToSingle(EditableProduct.editedProduct.ProductDiscountAmount);
                    Amount = EditableProduct.editedProduct.ProductCurrentDiscount;
                }
                
            }
            catch(Exception ex)
            {

            }
        }
    }
}
