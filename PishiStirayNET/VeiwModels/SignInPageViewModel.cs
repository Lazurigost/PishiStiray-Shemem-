
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PishiStiray.Services;
using PishiStiray.Views.Pages;
using System.Threading.Tasks;
using System.Windows;

namespace PishiStiray.VeiwModels
{
    partial class SignInPageViewModel : ObservableObject
    {

        private readonly UserService _userService;
        private readonly PageService _pageService;

        #region Свойства

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignInCommand))]
        private string login;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignInCommand))]
        private string password;

        [ObservableProperty]
        private string? errorMessage;


        #endregion

        public SignInPageViewModel(UserService userService, PageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
        }




        #region Команды
        //Авторизация
        [RelayCommand(CanExecute = nameof(CanSignIn))]
        private async void SignIn()
        {
            await Task.Run(async () =>
            {
                if (await _userService.Authorization(login, password) == true)
                {
                    ErrorMessage = string.Empty;
                    await Application.Current.Dispatcher.InvokeAsync(async () => _pageService.ChangePage(new ProductsPage()));
                }
                else
                {
                    ErrorMessage = "Неверный логин или пароль";
                }
            });


        }
        //Валидация
        private bool CanSignIn()
        {
            if (string.IsNullOrWhiteSpace(login) == true || string.IsNullOrEmpty(password) == true)
            {
                return false;
            }
            return true;
        }

        //Переход на страницу продуктов
        [RelayCommand]
        private void GoToProductsPage() => _pageService.ChangePage(new ProductsPage());


        #endregion
    }
}
