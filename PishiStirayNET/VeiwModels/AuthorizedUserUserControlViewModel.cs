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
            }
            else
            {
                IsAuthorized = false;
            }

        }
        [RelayCommand]
        private void TestMethod()
        {
            CurrentUser.User = null;
            IsAuthorized = false;
            _pageService.ChangePage(new SignInPage());
        }
        [RelayCommand]
        private void LogOut()
        {
            CurrentUser.User = null;
            IsAuthorized = false;
            _pageService.ChangePage(new SignInPage());
        }

    }
}
