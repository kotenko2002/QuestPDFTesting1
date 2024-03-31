using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTesting1.Entities;

namespace QuestPDFTesting1.PdfGeneration
{
    public class WorkDayReportDocument : IDocument
    {
        private readonly Workday _workday;
        private readonly decimal _profit;

        public WorkDayReportDocument(Workday workday, decimal profit)
        {
            _workday = workday;
            _profit = profit;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" of ");
                    x.TotalPages();
                });
            });
        }

        private void ComposeHeader(IContainer container)
        {
            
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(25);

                column.Item().Column(generalSection =>
                {
                    generalSection.Item().Background(Colors.Grey.Medium).Height(30).AlignCenter().AlignMiddle().Text("General").FontSize(20).FontFamily("Arial").FontColor(Colors.White);

                    generalSection.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(innerColumn =>
                    {
                        AddInfoSection(innerColumn, "Cinema name: ", _workday.Cinema.Name);
                        AddInfoSection(innerColumn, "Cinema address: ", _workday.Cinema.Adress);
                        AddInfoSection(innerColumn, "Worker name: ", $"{_workday.User.LastName} {_workday.User.FisrtName}");
                        AddInfoSection(innerColumn, "Shift started in: ", _workday.StartDateTime.ToString("dd/MM/yyyy HH:mm"));
                        AddInfoSection(innerColumn, "Shift finished in: ", _workday.EndDateTime.Value.ToString("dd/MM/yyyy HH:mm"));
                        AddInfoSection(innerColumn, "Profit: ", _profit.ToString());
                    });
                });

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
                        foreach (var product in _workday.Cinema.Products)
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

                column.Item().Column(sessionsSection =>
                {
                    sessionsSection.Item().Background(Colors.Grey.Medium).Height(30).AlignCenter().AlignMiddle().Text("Sessions").FontSize(20).FontFamily("Arial").FontColor(Colors.White);

                    sessionsSection.Item().Background(Colors.Grey.Lighten3).Padding(10).Table(table =>
                    {
                        var headerStyle = TextStyle.Default.SemiBold();

                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(25);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(1.25f);
                            columns.RelativeColumn(1.25f);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("#");
                            header.Cell().Text("Film").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Started").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Ended").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Sold").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Booked").Style(headerStyle);
                            header.Cell().AlignCenter().Text("Unit price").Style(headerStyle);
                        });

                        int index = 1;
                        foreach (var session in _workday.Cinema.Films.SelectMany(film => film.Sessions))
                        {
                            table.Cell().Element(CellStyle).Text($"{index}");
                            table.Cell().Element(CellStyle).AlignMiddle().Text(session.Film.Name);
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{session.StartDateTime}");
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{session.StartDateTime + session.Film.Duration}");
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{session.Tickets.Count(s => s.Status == TicketStatus.Sold)}");
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{session.Tickets.Count(s => s.Status == TicketStatus.Booked)}");
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{session.Price}$");

                            index++;

                            static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                        }
                    });
                });
            });
        }

        private void AddInfoSection(ColumnDescriptor column, string title, string value)
        {
            column.Item().Text(text =>
            {
                text.Span(title).Bold();
                text.Span(value);
            });
        }

        private static void CellStyle(IContainer container)
        {
            container.BorderBottom(1).BorderColor("#CCCCCC").Padding(5);
        }
    }
}
