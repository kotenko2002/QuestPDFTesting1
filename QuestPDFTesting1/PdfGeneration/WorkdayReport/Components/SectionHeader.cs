using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDFTesting1.PdfGeneration.WorkdayReport.Components
{
    public class SectionHeader : IComponent
    {
        private readonly string _headerText;
        public SectionHeader(string headerText)
        {
            _headerText = headerText;   
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column
                    .Item()
                    .Background(Colors.Grey.Medium)
                    .Height(30)
                    .AlignCenter()
                    .AlignMiddle()
                    .Text(_headerText)
                    .FontSize(20)
                    .FontFamily("Arial")
                    .FontColor(Colors.White);
            });
        }
    }
}
