using CommunityToolkit.Mvvm.ComponentModel;
using PishiStiray.Infrastructure;
using PishiStiray.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.VeiwModels
{
    public partial class EditProductPageViewModel : ObservableObject
    {
        #region Сервисы
        private readonly ProductService productService_;
        private readonly PageService pageService_;
        #endregion
        [ObservableProperty]
        private ProductDB? product;
        [ObservableProperty]
        private List<ProductDB> products;
        public EditProductPageViewModel(ProductService productService, PageService pageService)
        {
            pageService_ = pageService;
            productService_ = productService;

            product = EditableProduct.editedProduct;
            
        }
    }
}
