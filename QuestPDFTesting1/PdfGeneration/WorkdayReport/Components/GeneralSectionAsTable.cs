﻿using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTesting1.Entities;

namespace QuestPDFTesting1.PdfGeneration.WorkdayReport.Components
{
    public class GeneralSectionAsTable : IComponent
    {
        private readonly Workday _workday;
        private readonly decimal _profit;

        public GeneralSectionAsTable(Workday workday, decimal profit)
        {
            _workday = workday;
            _profit = profit;
        }

        public void Compose(IContainer container)
        {
            container.Column(generalSection =>
            {
                generalSection.Item().Background(Colors.Grey.Medium).Height(30).AlignCenter().AlignMiddle().Text("General").FontSize(20).FontFamily("Arial").FontColor(Colors.White);

                generalSection.Item().Background(Colors.Grey.Lighten3).Padding(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                    });

                    AddInfoSectionAsCell(table, "Cinema name: ", _workday.Cinema.Name);
                    AddInfoSectionAsCell(table, "Cinema address: ", _workday.Cinema.Adress);
                    AddInfoSectionAsCell(table, "Worker name: ", $"{_workday.User.LastName} {_workday.User.FisrtName}");
                    AddInfoSectionAsCell(table, "Shift started in: ", _workday.StartDateTime.ToString("dd/MM/yyyy HH:mm"));
                    AddInfoSectionAsCell(table, "Shift finished in: ", _workday.EndDateTime.Value.ToString("dd/MM/yyyy HH:mm"));
                    AddInfoSectionAsCell(table, "Profit: ", _profit.ToString());
                });
            });
        }

        private void AddInfoSectionAsCell(TableDescriptor table, string title, string value)
        {
            table.Cell().Element(CellStyle).Text(text =>
            {
                text.Span(title).Bold();
                text.Span(value);
            });

            static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
        }
    }
}