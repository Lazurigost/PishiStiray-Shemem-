﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models;
using PishiStiray.Services;
using PishiStiray.Views.Pages;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PishiStiray.VeiwModels
{
    public partial class ProductsPageViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        private readonly PageService _pageService;

        #region Свойства

        [ObservableProperty]
        private string? searchQuery;

        [ObservableProperty]
        private List<string> orderList = new() { "По убыванию", "По возрастанию" };

        [ObservableProperty]
        private List<string> filtersList = new() { "Все диапазоны", "0 - 9,99%", "10 - 14,99%", "15% и более" };

        [ObservableProperty]
        private string? selectedFilter;

        [ObservableProperty]
        private string? selectedOrder;

        [ObservableProperty]
        private int currentProductsCount;

        [ObservableProperty]
        private int totalProductsCount;

        [ObservableProperty]
        private List<Product> productsList;

        [ObservableProperty]
        private Product selectedProduct;
        
        [ObservableProperty]
        private Visibility isAdmin;

        [ObservableProperty]
        private Visibility isManager;
        
        
        #endregion

        #region Changed методы
        partial void OnSearchQueryChanged(string? value)
        {
            UpdateProductsList();
        }

        partial void OnSelectedOrderChanged(string? value)
        {
            UpdateProductsList();
        }

        partial void OnSelectedFilterChanged(string? value)
        {
            UpdateProductsList();
        }
        

        #endregion



        public ProductsPageViewModel(ProductService productService, PageService pageService)
        {
            _productService = productService;
            _pageService = pageService;
            SelectedFilter = filtersList[0];
        }

        public async void UpdateProductsList()
        {
            if (CurrentUser.User != null)
            {
                if (CurrentUser.User.UserRole == 1)
                {
                    IsAdmin = Visibility.Visible;
                    IsManager = Visibility.Visible;
                }
                else if(CurrentUser.User.UserRole == 3)
                {
                    IsAdmin = Visibility.Collapsed;
                    IsManager = Visibility.Visible;
                }
                else
                {
                    IsAdmin = Visibility.Collapsed;
                    IsManager = Visibility.Collapsed;
                }
            }
            else
            {
                IsAdmin = Visibility.Collapsed;
                IsManager = Visibility.Collapsed;
            }

            //Получение списка
            List<Product> products = await _productService.GetProductsAsync();
            
            //Присваивание продукту фотографии
            foreach (var product in products)
            {               
                if (product.ProductPhoto == "C:\\Users\\МОиБД\\source\\repos\\PishiStiray-Shemem-\\PishiStirayNET\\bin\\Debug\\net7.0-windows\\Resources\\")
                {
                    product.ProductPhoto = "C:\\Users\\МОиБД\\source\\repos\\PishiStiray-Shemem-\\PishiStirayNET\\bin\\Debug\\net7.0-windows\\Resources\\picture.png";
                }
            }
            TotalProductsCount = products.Count;

            //Поиск
            if (SearchQuery != null)
            {
                products = products.Where(p => p.ProductName.ToLower().Trim().Contains(SearchQuery.ToLower().Trim())).ToList();
            }

            //Фильтрация
            switch (SelectedFilter)
            {
                case "Все диапазоны":

                    products = products.ToList();
                    break;

                case "0 - 9,99%":
                    products = products.Where(p => p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 9.99 || p.ProductDiscountAmount == null).ToList();
                    break;

                case "10 - 14,99%":
                    products = products.Where(p => p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount <= 14.99).ToList();
                    break;

                case "15% и более":
                    products = products.Where(p => p.ProductDiscountAmount >= 15).ToList();
                    break;
            }

            //Соритировка
            switch (SelectedOrder)
            {
                case "По возрастанию":

                    products = products.OrderBy( p => p.NewPrice).ToList();

                    break;

                case "По убыванию":
                    products = products.OrderByDescending(p => p.NewPrice).ToList();
                    break;
            }

            ProductsList = products;

            CurrentProductsCount = products.Count;
        }

        #region Команды

        //Добавление в корзину
        [RelayCommand]
        private void AddProductToCart()
        {
            if (SelectedProduct != null)
            {
                CartItem? cartItem = Global.CartProductList.SingleOrDefault(p => p.product.ProductArticleNumber == SelectedProduct.ProductArticleNumber);
                if (cartItem == null)
                {
                    Global.CartProductList.Add(new CartItem
                    {
                        product = SelectedProduct,
                        Count = 1
                        
                    });
                }
                else
                {
                    if (cartItem.Count < cartItem.product.ProductQuantityInStock) 
                    {
                        cartItem.Count++;
                    }
                    else
                    {
                        MessageBox.Show("Товар закончился");
                    }
                    
                }
            }
        }

        //Переход в корзину
        [RelayCommand]
        private void GoToCart() => _pageService.ChangePage(new CartPage());
        //Переход на страницу изменения продукта
        [RelayCommand]
        private void EditProductProceed()
        {
            if (SelectedProduct != null)
            {
                EditableProduct.editedProduct = SelectedProduct;
                _pageService.ChangePage(new EditProductPage());
            }
           
        }
        [RelayCommand]
        private void AddProductProceed() => _pageService.ChangePage(new AddProductPage());
        [RelayCommand]
        private async void RemoveProduct()
        {
            if (SelectedProduct != null)
            {
                _productService.DeleteProduct(SelectedProduct);
                await Task.Delay(90);
                UpdateProductsList();
            }
        }
        [RelayCommand]
        private void GoToOrders()
        {
            _pageService.ChangePage(new OrdersPage());
        }
        [RelayCommand]
        private void GoToManufacturers() => _pageService.ChangePage(new ManufacturerPage());
        [RelayCommand]
        private void GoToDeliveries() => _pageService.ChangePage(new ManufacturerPage());
        #endregion
    }
}
