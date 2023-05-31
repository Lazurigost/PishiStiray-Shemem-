using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Models.DbEntities;
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
    public partial class ManufacturerPageViewModel : ObservableValidator
    {
        private readonly ManufacturerService _manufacturersService;
        private readonly PageService _pageService;

        [ObservableProperty]
        [Required(ErrorMessage = "Заполните поле")]
        private string? _name;

        [ObservableProperty]
        private List<Manufacturer> _manufacturersList;

        [ObservableProperty]
        private Manufacturer _selectedManufacturer;

        [ObservableProperty]
        private bool _isChanged = false;

        public ManufacturerPageViewModel(ManufacturerService manufacturersService, PageService pageService)
        {
            _manufacturersService = manufacturersService;
            LoadDataAsync();
            _pageService = pageService;
        }

        private async void LoadDataAsync()
        {
            ManufacturersList = await _manufacturersService.GetAllManufacturersAsync();
            Name = string.Empty;
        }

        [RelayCommand]
        private void ChangeManufacturer()
        {
            IsChanged = true;
            Name = SelectedManufacturer.ManufacturerName;
        }

        [RelayCommand]
        private void CancelChange()
        {
            IsChanged = false;
            Name = string.Empty;
            SelectedManufacturer = null;
        }


        [RelayCommand]
        private async void SendManufacturer()
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.ManufacturerName = Name;

            if (IsChanged == true)
            {
                manufacturer.IdproductManufacturers = SelectedManufacturer.IdproductManufacturers;
                manufacturer.ManufacturerName = Name;
                _manufacturersService.ChangeManufacturer(manufacturer);
                IsChanged = false;
                await Task.Delay(200);
                LoadDataAsync();
                return;
            }
            else
            {

                _manufacturersService.AddManufacturer(manufacturer);
                await Task.Delay(90);
                LoadDataAsync();
            }

        }

        [RelayCommand]
        private void GoBackPage()
        {
            _pageService.ChangePage(new ProductsPage());
        }
    }
}
