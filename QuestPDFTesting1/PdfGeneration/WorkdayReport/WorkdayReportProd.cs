using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTesting1.Entities;
using QuestPDFTesting1.PdfGeneration.WorkdayReport.Components;

namespace QuestPDFTesting1.PdfGeneration.WorkdayReport
{
    public class WorkdayReportProd : IDocument
    {
        private readonly Workday _workday;
        private readonly decimal _profit;

        public WorkdayReportProd(Workday workday, decimal profit)
        {
            _workday = workday;
            _profit = profit;
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);

                    page.Content().Element(ComposeContent);
                    page.Footer().Element(ComposeFooter);
                });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(25);

                List<Product> products = _workday.Cinema.Products.ToList();
                List<Session> sessions = _workday.Cinema.Films
                   .SelectMany(film => film.Sessions)
                   .ToList();

                column
                    .Item()
                    .Component(new GeneralSectionAsTable(_workday, _profit));

                column
                    .Item()
                    .Component(new ProductsSection(products));

                column
                    .Item()
                    .Component(new SessionsSection(sessions));
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" of ");
                    x.TotalPages();
                });
            });
        }
    }
}
