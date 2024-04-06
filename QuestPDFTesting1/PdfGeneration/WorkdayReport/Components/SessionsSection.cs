using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTesting1.Entities;

namespace QuestPDFTesting1.PdfGeneration.WorkdayReport.Components
{
    internal class SessionsSection : IComponent
    {
        private readonly List<Session> _sessions;

        public SessionsSection(List<Session> sessions)
        {
            _sessions = sessions;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
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
                        foreach (var session in _sessions)
                        {
                            table.Cell().Element(CellStyle).Text($"{index}");
                            table.Cell().Element(CellStyle).AlignMiddle().Text(session.Film.Name);
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{session.StartDateTime.ToString("dd/MM/yyyy HH:mm")}");
                            table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{(session.StartDateTime + session.Film.Duration).ToString("dd/MM/yyyy HH:mm")}");
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
    }
}
