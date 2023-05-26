using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using PishiStiray.Models;
using System;

namespace PishiStiray.Services
{
    public class DocumentService
    {
        private readonly TradeContext _tradeContext;

        public DocumentService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }
        //Функция создания и записи документа
        public async void CreateDocument(Order order, string path)
        {
            PdfWriter writer = new($"{path}//Товарный чек от {DateOnly.FromDateTime(DateTime.Now).ToString("D")}.pdf");
            PdfDocument pdf = new(writer);
            Document document = new(pdf);

            PdfFont font = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\Arial.ttf", PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_NOT_EMBEDDED);

            var content = new Paragraph($"ООО Пиши-Стирай")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(font)
                .SetFontSize(16);

            document.Add(content);

            content = new Paragraph($"Получатель - {order.OrdererFio}")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(font)
                .SetFontSize(14);

            document.Add(content);

            content = new Paragraph($"Состав заказа")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(font)
                .SetFontSize(14)
                .SetBold();

            document.Add(content);

            Table table = new(6);
            table.SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.BOTTOM);
            table.SetFont(font);
            table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);

            table.AddCell("Изображение");
            table.AddCell("Название");
            table.AddCell("Описание");
            table.AddCell("Производитель");
            table.AddCell("Цена");
            table.AddCell("Скидка");

            foreach(var product in order.Products)
            {
                table.AddCell(new Image(ImageDataFactory.Create(product.product.ImageUrl)).SetHeight(100).SetWidth(100));
                table.AddCell(product.product.ProductName);
                table.AddCell(product.product.ProductDescription);
                table.AddCell(product.product.ProductManufacturerNavigation.ManufacturerName);
                table.AddCell(product.product.ProductCost.ToString() + "₽");
                table.AddCell(product.product.ProductDiscountAmount.ToString() + "%");
            }

            document.Add(table);

            content = new Paragraph($"Стоимость заказа: {order.FullPrice} ₽")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFont(font)
                .SetFontSize(14);
            document.Add(content);
            content = new Paragraph($"Скидка: {order.Discount} ₽")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFont(font)
                .SetFontSize(14);
            document.Add(content);
            content = new Paragraph($"Итоговая стоимость: {order.FullPrice - order.Discount} ₽")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFont(font)
                .SetFontSize(14);
            document.Add(content);
            content = new Paragraph($"Код получения: {order.OrderAquireCode}")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(font)
                .SetFontSize(14);
            document.Add(content);


            document.Close();
        }
    }
}
