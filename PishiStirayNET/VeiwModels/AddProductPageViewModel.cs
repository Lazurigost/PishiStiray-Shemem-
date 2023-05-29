using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Views.Pages;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PishiStiray.Services;
using System.ComponentModel.DataAnnotations;
using PishiStiray.Models.DbEntities;
using System.Windows.Media.Imaging;
using PishiStiray.Infrastructure;

namespace PishiStiray.VeiwModels
{
    public partial class AddProductPageViewModel : ObservableValidator
    {
        private readonly ProductService _productService;
        private readonly DeliveryService _deliveryService;
        private readonly PageService _pageService;
        private readonly SaveFileDialogService _saveFileDialogService;

        [ObservableProperty]
        private List<ProductCategory> productCategories;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private ProductCategory selectedCategory;

        [ObservableProperty]
        private List<Manufacturer> manufacturers;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private Manufacturer selectedManufacturer;

        [ObservableProperty]
        private List<int> deliveries;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private int? selectedDelivery;

        [ObservableProperty]
        private List<string> units;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string selectedUnit;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [MinLength(2, ErrorMessage = "Название должно быть длинее 2 символов")]
        [MaxLength(100, ErrorMessage = "Название не должно быть длинее 100 символов")]
        private string? title;


        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [MinLength(2, ErrorMessage = "Описание должно быть длинее 2 символов")]
        [MaxLength(100, ErrorMessage = "Описание не должно быть длинее 100 символов")]
        private string? description;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 1000000, ErrorMessage = "Не более 6 символов и только числа")]
        private float? price;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 100, ErrorMessage = "Не более 6 символов и только числа")]
        private float? currentDiscount;



        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        [Range(0, 1000000, ErrorMessage = "Не более 6 символов и только числа")]
        private int? maxCount;


        [ObservableProperty]
        private string selectedPath = "picture.png";

        [ObservableProperty]
        private BitmapImage imagePath = new(new Uri($"/Resources/picture.png", UriKind.Relative));



        public AddProductPageViewModel(ProductService productService, SaveFileDialogService saveFileDialogService, PageService pageService, DeliveryService deliveryService)
        {
            _productService = productService;
            _deliveryService = deliveryService;
            _saveFileDialogService = saveFileDialogService;
            _pageService = pageService;



            LoadProductData();


        }

        protected virtual async void LoadProductData()
        {
            try
            {
                ProductCategories = await _productService.GetProductCategoriesAsync();
                Manufacturers = await _productService.GetManufacturersAsync();
                Deliveries = new List<int>
                {
                    1,
                    2
                };
                Units = new List<string>
                {
                    "уп",
                    "шт"
                };

                if (Global.Product != null)
                {

                    SelectedCategory = ProductCategories[ProductCategories.IndexOf(Global.Product.ProductCategoryNavigation)];
                    SelectedManufacturer = Global.Product.ProductManufacturerNavigation;
                    SelectedDelivery = Global.Product.ProductDelivery;
                    SelectedUnit = Global.Product.ProductStatus;
                    Title = Global.Product.ProductName;
                    Description = Global.Product.ProductDescription;
                    Price = (float?)Global.Product.ProductCost;
                    CurrentDiscount = Global.Product.ProductCurrentDiscount;
                    MaxCount = Global.Product.ProductQuantityInStock;
                    SelectedPath = Global.Product.Image;

                }
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        private void AddPhoto()
        {
            SelectedPath = _saveFileDialogService.ImageSaveFileDialog();
            ImagePath = new(new Uri(Path.GetFullPath($"Resources/{SelectedPath}"), UriKind.Absolute));
        }

        [RelayCommand]
        private void GoBackPage()
        {
            _pageService.ChangePage(new ProductsPage());
        }
        [RelayCommand]
        private async void AddNewProduct()
        {
            ValidateAllProperties();

            if (HasErrors == false)
            {

                _productService.AddNewProduct(new ProductDB
                {
                    ProductArticleNumber = await _productService.GenerateArticle(),
                    ProductName = Title,
                    ProductDescription = Description,
                    ProductCategory = SelectedCategory.IdproductCategory,
                    ProductPhoto = SelectedPath,
                    ProductManufacturer = SelectedManufacturer.IdproductManufacturers,
                    ProductCost = (decimal)Price,
                    ProductCurrentDiscount = (sbyte?)CurrentDiscount,
                    ProductQuantityInStock = (int)MaxCount,
                    ProductStatus = SelectedUnit,
                    ProductDelivery = SelectedDelivery,
                });

                _pageService.ChangePage(new ProductsPage());
            }

        }
        
    }
}

