using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDFTesting1
{
    public class CinemaTicketDocument : IDocument
    {
        public CinemaTicket Ticket { get; set; }

        public CinemaTicketDocument(CinemaTicket ticket)
        {
            Ticket = ticket;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    //page.Size(PageSizes.A6.Landscape());
                    page.Size(PageSizes.A6);
                    page.Margin(25);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                //row
                //.ConstantItem(100)
                //.Height(100)
                //.Image("D:\\University\\Degree project\\QuestPDFTesting1\\QuestPDFTesting1\\filmstrip.jpg");

                row
                    .RelativeItem()
                    .AlignCenter()
                    .Text($"Кінотеатральний Білет")
                    .FontFamily("Arial")
                    .FontSize(20)
                    .Bold();
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column
                    .Item()
                    .AlignCenter()
                    .Text($"Фільм: {Ticket.MovieTitle}")
                    .FontFamily("Arial")
                    .FontSize(16);
                column
                    .Item()
                    .AlignCenter()
                    .Text($"Час: {Ticket.SessionTime
                    .ToString("g")}")
                    .FontFamily("Arial")
                    .FontSize(16);
                column
                    .Item()
                    .AlignCenter()
                    .Text($"Місце: {Ticket.Seat}")
                    .FontFamily("Arial")
                    .FontSize(16);
            });
        }
    }
}
