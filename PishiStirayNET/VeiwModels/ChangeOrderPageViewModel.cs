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
    public partial class ChangeOrderPageViewModel : ObservableValidator
    {
        private readonly PageService _pageService;
        private readonly OrderService _orderService;


        [ObservableProperty]
        private Order changedOrder;

        [ObservableProperty]
        private List<OrderStatus> statuses;

        [ObservableProperty]
        private OrderStatus selectedStatus;

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
            Statuses = await _orderService.GetOrderStatuses();
            ChangedOrder = ChangableOrder.order;

            foreach (var stat in statuses)
            {
                if (stat.IdOrderStatus == ChangedOrder.OrderStatus)
                {
                    SelectedStatus = stat;
                    break;
                }
            }

            SelectedEndDate = ChangedOrder.OrderDeliveryDateEnd;
        }


        [RelayCommand]
        private async void SaveOrder()
        {
            ChangedOrder.OrderStatus = SelectedStatus.IdOrderStatus;
            ChangedOrder.OrderDeliveryDateEnd = SelectedEndDate;
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
