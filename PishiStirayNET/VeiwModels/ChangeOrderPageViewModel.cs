using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models;
using PishiStiray.Services;
using PishiStiray.Views.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.VeiwModels
{
    public partial class ChangeOrderPageViewModel : ObservableObject
    {
        private readonly PageService _pageService;
        private readonly OrderService _orderService;


        [ObservableProperty]
        private Order changedOrder;

        [ObservableProperty]
        private List<string> statuses = new();

        [ObservableProperty]
        private string selectedStatus;

        [ObservableProperty]
        private DateTime selectedEndDate;

        public ChangeOrderPageViewModel(OrderService orderService, PageService pageService)
        {
            _orderService = orderService;

            LoadData();
            _pageService = pageService;
        }

        private async void LoadData()
        {
            await Task.Delay(100);
            ChangedOrder = Global.Order;
            Statuses.Add("Новый");
            Statuses.Add("Завершен");

            foreach (var stat in statuses)
            {
                if (stat == ChangedOrder.OrderStatus)
                {
                    SelectedStatus = stat;
                    break;
                }
            }

            SelectedEndDate = ChangedOrder.OrderDeliveryDate.ToDateTime(TimeOnly.Parse("10:00 PM"));
        }


        [RelayCommand]
        private async void SaveOrder()
        {
            ChangedOrder.OrderStatus = SelectedStatus;
            ChangedOrder.OrderDeliveryDate = DateOnly.FromDateTime(SelectedEndDate);
            _orderService.ChangeOrder(ChangedOrder);
            await Task.Delay(100);
            _pageService.ChangePage(new OrdersPage());
        }

        [RelayCommand]
        private void GoBackPage()
        {
            _pageService.ChangePage(new OrdersPage());
        }
    }
}
