using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Models;
using PishiStiray.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PishiStiray.Views.Pages;
using System.Linq;
using System.Threading.Tasks;

namespace PishiStiray.VeiwModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        #region Свойства

        [ObservableProperty]
        private ObservableCollection<Product> cartItemsList;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        private ObservableCollection<CartItem> cartProductsList;

        [ObservableProperty]
        private List<Delivery> pickupPoints;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(MakeOrderCommand))]
        private Delivery? selectedPickupPoint;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(MakeOrderCommand))]
        private CartItem? selectedCartItem;



        [ObservableProperty]
        private Delivery selectedPoint;

        [ObservableProperty]
        private CartItem selectedCart;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        [NotifyPropertyChangedFor(nameof(TotalCount))]
        [NotifyPropertyChangedFor(nameof(TotalDiscount))]
        [NotifyPropertyChangedFor(nameof(ResultCost))]
        private int? count;
        //Итоговое кол-во товаров в списке
        public int? TotalCount
        {
            get => CartProductsList.Sum(item => item.Count);
        }
        //Общая цена всей корзины
        public float? TotalPrice
        {
            get => CartProductsList.Sum(item => item.Cost);
        }
        //Сумма, сэкономленая с учётом скидок
        public float? TotalDiscount
        {
            get => CartProductsList.Sum(item => item.Discount);
        }
        //Итоговая цена
        public float? ResultCost
        {
            get => TotalPrice - TotalDiscount;
        }
        #endregion

        #region Services
        private readonly ProductService productService_;
        private readonly PageService pageService_;
        private readonly DeliveryService deliveryService_;
        private readonly OrderService orderService_;
        private readonly SaveFileDialogService saveFileDialogService_;
        private readonly DocumentService documentService_;
        #endregion

        public CartPageViewModel(DeliveryService deliveryService, ProductService productService, PageService pageService, OrderService orderService, SaveFileDialogService saveFileDialogService, DocumentService documentService)
        {
            pageService_ = pageService;
            productService_ = productService;
            deliveryService_ = deliveryService;
            orderService_ = orderService;
            saveFileDialogService_ = saveFileDialogService;
            documentService_ = documentService;
            CartProductsList = Cart.CartProductList;

            Task.Run(async () =>
            {
                PickupPoints = await deliveryService_.GetDeliveriesAsync();
            });

            UpdateCart();
        }
        //Удаление при изменении кол-ва товара до 0
        partial void OnCountChanged(int? value)
        {
            if (Count == 0)
            {
                CartProductsList.Remove(SelectedCartItem);
            }
        }
        //Переключение выбранного итема
        partial void OnSelectedCartItemChanged(CartItem? value)
        {
            if (SelectedCartItem != null)
            {
                Count = SelectedCartItem.Count;
            }
        }
        //Обновление корзины
        public async void UpdateCart()
        {
            cartProductsList = Global.CartProductList;
        }
        #region Команды
        //Увеличение кол-ва товара
        [RelayCommand]
        private void IncreaseSelectedCartItemCount()
        {
            if (SelectedCartItem != null && SelectedCartItem.Count > 0 && Count < SelectedCartItem.product.ProductQuantityInStock)
            {
                SelectedCartItem.Count++;
                Count = SelectedCartItem.Count;
            }
        }
        //Уменьшение кол-ва товара
        [RelayCommand]
        private void DecreaseSelectedCartItemCount()
        {
            if (SelectedCartItem != null && SelectedCartItem.Count > 0)
            {
                --SelectedCartItem.Count;
                Count = SelectedCartItem.Count;
            }
        }

        //Удаление из корзины
        [RelayCommand]
        private void RemoveFromCart()
        {
            if (SelectedCartItem != null)
            {
                foreach (CartItem CItem in CartProductsList)
                {
                    if (SelectedCartItem == CItem)
                    {
                        CItem.Count = 0;
                        Global.CartProductList.Remove(CItem);
                        CartProductsList.Remove(CItem);
                        break;
                    }
                }
                UpdateCart();
            }
        }

        //Возвращение по кнопке
        [RelayCommand]
        private void BackFromCart()
        {
            pageService_.ChangePage(new ProductsPage());
        }
        //Создание заказа и чека
        [RelayCommand(CanExecute = nameof(CanMakeOrder))]
        private async void MakeOrder()
        {
            Order order = await orderService_.CreateOrder(CartProductsList.ToList(), SelectedPickupPoint.IdpickupPoint);

            string selectedFolder = "";
            selectedFolder = saveFileDialogService_.PDFSaveFileDialog();
            if (selectedFolder != "no folder")
            {
                documentService_.CreateDocument(order, selectedFolder);
            }
            CartProductsList.Clear();
            pageService_.ChangePage(new ProductsPage());
        }
        //Валидация заказа
        private bool CanMakeOrder()
        {
            if (SelectedPickupPoint != null && cartProductsList.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
