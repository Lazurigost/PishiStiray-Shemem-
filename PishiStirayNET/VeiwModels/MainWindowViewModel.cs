using CommunityToolkit.Mvvm.ComponentModel;

using PishiStiray.Services;
using PishiStiray.Views.Pages;
using System.Windows.Controls;

namespace PishiStiray.VeiwModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        private readonly PageService _pageService;



        #region Свойства


        [ObservableProperty]
        private Page pageSource;

        #endregion

        #region Команды

        #endregion

        public MainWindowViewModel(PageService pageService)
        {
            _pageService = pageService;
            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new OrdersPage());
        }

    }
}
