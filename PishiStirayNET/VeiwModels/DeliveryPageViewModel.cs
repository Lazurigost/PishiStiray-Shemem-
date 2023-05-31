using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Services;
using PishiStiray.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PishiStiray.VeiwModels
{
    public partial class DeliveryPageViewModel : ObservableValidator
    {
        private readonly ProductDeliveriesService _deliveriesService;
        private readonly PageService _pageService;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string? _name;

        [ObservableProperty]
        private List<ProductDelivery> _deliveriesList;

        [ObservableProperty]
        private ProductDelivery _selectedDelivery;

        [ObservableProperty]
        private bool _isChanged = false;

        public DeliveryPageViewModel(ProductDeliveriesService deliveriesService, PageService pageService)
        {
            _deliveriesService = deliveriesService;
            LoadDataAsync();
            _pageService = pageService;
        }

        private async void LoadDataAsync()
        {
            DeliveriesList = await _deliveriesService.GetAllDeliveriesAsync();
            Name = string.Empty;
        }

        [RelayCommand]
        private void ChangeDelivery()
        {
            IsChanged = true;
            Name = SelectedDelivery.DeliveryName;
        }

        [RelayCommand]
        private async void SaveDelivery()
        {
            ProductDelivery delivery = new();
            delivery.DeliveryName = Name;

            if (IsChanged == true)
            {
                delivery.IdproductDeliveries = SelectedDelivery.IdproductDeliveries;
                _deliveriesService.ChangeDelivery(delivery);
                IsChanged = false;
                await Task.Delay(200);
                LoadDataAsync();
                return;
            }
            else
            {

                _deliveriesService.AddDelivery(delivery);
                await Task.Delay(90);
                LoadDataAsync();
            }

        }

        [RelayCommand]
        private void CancelChange()
        {
            IsChanged = false;
            Name = string.Empty;
            SelectedDelivery = null;
        }

        [RelayCommand]
        private void GoBackPage()
        {
            _pageService.ChangePage(new ProductsPage());
        }
    }
}
