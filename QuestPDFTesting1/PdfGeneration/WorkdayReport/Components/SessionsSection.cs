using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTesting1.Entities;

namespace QuestPDFTesting1.PdfGeneration.WorkdayReport.Components
{
    public class SessionsSection : SectionBase
    {
        private readonly List<Session> _sessions;

        public SessionsSection(List<Session> sessions)
        {
            _sessions = sessions;
        }

        public override void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column
                  .Item()
                  .Component(new SectionHeader("Sessions"));

                column.Item().Background(Colors.Grey.Lighten3).Padding(10).Table(table =>
                {
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
                        header.Cell().Text("Film").Bold();
                        header.Cell().AlignCenter().Text("Started");
                        header.Cell().AlignCenter().Text("Ended").Bold();
                        header.Cell().AlignCenter().Text("Sold").Bold();
                        header.Cell().AlignCenter().Text("Booked").Bold();
                        header.Cell().AlignCenter().Text("Unit price").Bold();
                    });

                    int index = 1;
                    foreach (var session in _sessions)
                    {
                        table.Cell().Element(CellStyle).AlignMiddle()
                            .Text($"{index}");
                        
                        table.Cell().Element(CellStyle).AlignMiddle()
                            .Text(session.Film.Name);
                        
                        table.Cell().Element(CellStyle).AlignCenter().AlignMiddle()
                            .Text($"{session.StartDateTime.ToString("dd/MM/yyyy HH:mm")}");
                        
                        table.Cell().Element(CellStyle).AlignCenter().AlignMiddle()
                            .Text($"{(session.StartDateTime + session.Film.Duration).ToString("dd/MM/yyyy HH:mm")}");
                        
                        table.Cell().Element(CellStyle).AlignCenter().AlignMiddle()
                            .Text($"{session.Tickets.Count(s => s.Status == TicketStatus.Sold)}");
                        
                        table.Cell().Element(CellStyle).AlignCenter().AlignMiddle()
                            .Text($"{session.Tickets.Count(s => s.Status == TicketStatus.Booked)}");
                        
                        table.Cell().Element(CellStyle).AlignCenter().AlignMiddle()
                            .Text($"{session.Price}$");

                        index++;
                    }
                });
            });
        }
    }
}
