using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models.DbEntities;
using PishiStiray.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PishiStiray.VeiwModels
{
    public partial class EditProductPageViewModel : ObservableObject
    {
        #region Сервисы
        private readonly ProductService productService_;
        private readonly PageService pageService_;
        private readonly SaveFileDialogService saveFileDialogService_;
        #endregion
        
        #region Свойства
        [ObservableProperty]
        private string? productImage;
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
        private int amount;
        [ObservableProperty]
        private ObservableCollection<string> units;
        [ObservableProperty]
        private List<Manufacturer> manufacturers;
        [ObservableProperty]
        private List<ProductCategory> productCategories;
        #endregion

        public EditProductPageViewModel(ProductService productService,SaveFileDialogService saveFileDialogService ,PageService pageService)
        {
            pageService_ = pageService;
            productService_ = productService;
            saveFileDialogService_ = saveFileDialogService;

            product = EditableProduct.editedProduct;
            LoadProductData();
        }
        protected virtual async void LoadProductData()
        {
            Units = new ObservableCollection<string>
            {
                "Шт.",
                "Уп."
            };
            try
            {
                ProductCategories = await productService_.GetCategoriesAsync();
                Manufacturers = await productService_.GetManufacturersAsync();
                if (EditableProduct.editedProduct != null)
                {
                    ProductImage = EditableProduct.editedProduct.ProductPhoto;
                    Title = EditableProduct.editedProduct.ProductName;
                    Article = EditableProduct.editedProduct.ProductArticleNumber;
                    Description = EditableProduct.editedProduct.ProductDescription;
                    Cost = EditableProduct.editedProduct.ProductCost;
                    Discount = (decimal?)Convert.ToSingle(EditableProduct.editedProduct.ProductDiscountAmount);
                    Amount = EditableProduct.editedProduct.ProductQuantityInStock;
                }
                
            }
            catch(Exception ex)
            {}
        }
        [RelayCommand]
        private void EditProduct()
        {
            EditableProduct.editedProduct.ProductName = Title;
            

            productService_.ChangeProduct(EditableProduct.editedProduct);
        }
    }
}
