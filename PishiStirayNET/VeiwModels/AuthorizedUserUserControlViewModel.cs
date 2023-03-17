using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Services;
using PishiStiray.Views.Pages;

namespace PishiStiray.VeiwModels
{
    public partial class AuthorizedUserUserControlViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? gridForRow;

        [ObservableProperty]
        private string? exit;

        [ObservableProperty]
        private string? fullname;

        [ObservableProperty]
        private string? role;

        [ObservableProperty]
        private bool isAuthorized;


        private readonly PageService _pageService;

        public AuthorizedUserUserControlViewModel(PageService pageService)
        {

            _pageService = pageService;
            if (CurrentUser.User != null)
            {
                Fullname = $"{CurrentUser.User.UserSurname} {CurrentUser.User.UserName} {CurrentUser.User.UserPatronymic}";
                Role = CurrentUser.User.UserRole;
                IsAuthorized = true;
                exit = "Выйти";
                gridForRow = "1";
            }
            else
            {
                exit = "Вернуться";
                IsAuthorized = false;
                gridForRow = "0";
            }

        }
        [RelayCommand]
        private void LogOut()
        {
            CurrentUser.User = null;
            IsAuthorized = false;
            Cart.CartProductList.Clear();
            _pageService.ChangePage(new SignInPage());
        }

    }
}
