using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTesting1.Entities;

namespace QuestPDFTesting1.PdfGeneration.WorkdayReport.Components
{
    public class GeneralSectionAsTable : SectionBase
    {
        private readonly Workday _workday;
        private readonly decimal _profit;

        public GeneralSectionAsTable(Workday workday, decimal profit)
        {
            _workday = workday;
            _profit = profit;
        }

        public override void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column
                    .Item()
                    .Component(new SectionHeader("General"));

                column.Item().Background(Colors.Grey.Lighten3).Padding(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                    });

                    Dictionary<string, string> items = new Dictionary<string, string>()
                    {
                        {"Cinema name", _workday.Cinema.Name},
                        {"Cinema address", _workday.Cinema.Adress},
                        {"Worker name", $"{_workday.User.LastName} {_workday.User.FisrtName}"},
                        {"Shift started in", _workday.StartDateTime.ToString("dd/MM/yyyy HH:mm")},
                        {"Shift finished in", FormatNullableDateTime(_workday.EndDateTime, "dd/MM/yyyy HH:mm")},
                        {"Profit", _profit.ToString()},
                    };

                    foreach (var item in items)
                    {
                        AddInfoSectionAsCell(table, item.Key, item.Value);
                    }
                });
            });
        }

        private void AddInfoSectionAsCell(TableDescriptor table, string title, string value)
        {
            table.Cell().Element(CellStyle).Text(text =>
            {
                text.Span(title).Bold();
                text.Span(": ").Bold();
                text.Span(value);
            });
        }

        private static string FormatNullableDateTime(DateTime? dateTime, string format)
            => dateTime?.ToString(format) ?? "N/A";
    }
}
