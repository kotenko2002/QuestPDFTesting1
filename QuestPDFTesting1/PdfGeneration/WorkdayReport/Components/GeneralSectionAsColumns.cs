using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTesting1.Entities;

namespace QuestPDFTesting1.PdfGeneration.WorkdayReport.Components
{
    public class GeneralSectionAsColumns : IComponent
    {
        private readonly Workday _workday;
        private readonly decimal _profit;

        public GeneralSectionAsColumns(Workday workday, decimal profit)
        {
            _workday = workday;
            _profit = profit;
        }

        public void Compose(IContainer container)
        {
            container.Column(generalSection =>
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
        }

        private void AddInfoSection(ColumnDescriptor column, string title, string value)
        {
            column.Item().Text(text =>
            {
                text.Span(title).Bold();
                text.Span(value);
            });
        }
    }
}
