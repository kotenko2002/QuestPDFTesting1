using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTesting1.Entities;

namespace QuestPDFTesting1.PdfGeneration.WorkdayReport.Components
{
    internal class ProductsSection : IComponent
    {
        private readonly List<Product> _products;

        public ProductsSection(List<Product> products)
        {
            _products = products;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Column(productsSection =>
                {
                    productsSection.Item().Background(Colors.Grey.Medium).Height(30).AlignCenter().AlignMiddle().Text("Products").FontSize(20).FontFamily("Arial").FontColor(Colors.White);

                    productsSection.Item().Background(Colors.Grey.Lighten3).Padding(10).Table(table =>
                    {
                        var headerStyle = TextStyle.Default.SemiBold();

                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(25);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("#");
                            header.Cell().Text("Product").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Amount").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Income").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Sold").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Unit price").Style(headerStyle);
                        });

                        int index = 1;
                        foreach (var product in _products)
                        {
                            table.Cell().Element(CellStyle).Text($"{index}");
                            table.Cell().Element(CellStyle).Text(product.Name);
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text(product.Amount.ToString());
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text(product.IncomeAmount.ToString());
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text(product.SoldAmount.ToString());
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{product.Price}$");

                            index++;

                            static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                        }
                    });
                });
            });
        }
    }
}
