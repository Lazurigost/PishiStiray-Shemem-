using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Infrastructure;
using PishiStiray.Services;
using PishiStiray.Views.Pages;

namespace PishiStiray.VeiwModels
{
    public partial class AuthorizedUserUserControlViewModel : ObservableObject
    {
        #region Свойства
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
        #endregion

        private readonly PageService _pageService;

        public AuthorizedUserUserControlViewModel(PageService pageService)
        {

            _pageService = pageService;
            if (CurrentUser.User != null)
            {
                Fullname = $"{CurrentUser.User.UserSurname} {CurrentUser.User.UserName} {CurrentUser.User.UserPatronymic}";
                //Role = CurrentUser.User.UserRoleNavigation.RoleName.ToString();
                switch (CurrentUser.User.UserRole)
                {
                    case 1:
                        Role = "Администратор";
                        break;
                    case 2:
                        Role = "Клиент";
                        break;
                    case 3:
                        Role = "Менеджер";
                        break;

                }
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

