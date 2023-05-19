using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.Services
{
    public class DocumentService
    {
        private readonly TradeContext _tradeContext;

        public DocumentService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }

        //public async void CreateDocument(Order order, string path)
    }
}
