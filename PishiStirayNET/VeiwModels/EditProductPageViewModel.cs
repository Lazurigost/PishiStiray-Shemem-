using CommunityToolkit.Mvvm.ComponentModel;
using PishiStiray.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.VeiwModels
{
    public partial class EditProductPageViewModel : ObservableObject
    {
        private readonly ProductService productService_;
        private readonly PageService pageService_;

        public EditProductPageViewModel(ProductService productService, PageService pageService)
        {
            pageService_ = pageService;
            productService_ = productService;
        }

    }
}
