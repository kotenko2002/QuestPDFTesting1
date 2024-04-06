using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using QuestPDFTesting1.Entities;
using QuestPDFTesting1.PdfGeneration;
using QuestPDFTesting1.PdfGeneration.WorkdayReport;

namespace QuestPDFTesting1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            Workday workday = SeedData.GetExtendedInitializedWorkday2();

            //var document = new WorkDayReportDocument(workday, 980)
            var document = new WorkdayReportProd(workday, 980);
            document.ShowInPreviewer();



            //var ticket = new CinemaTicket
            //{
            //    MovieTitle = "Назва Фільму",
            //    SessionTime = DateTime.Now,
            //    Seat = "Ряд 5, Місце 8"
            //    // Ініціалізуйте інші властивості
            //};

            //GenerateTicket(ticket, "D:\\University\\Degree project\\QuestPDFTesting1\\ticket.pdf");
        }

        public static void GenerateTicket(CinemaTicket ticket, string filePath)
        {
            var document = new CinemaTicketDocument(ticket);
            document.ShowInPreviewer();
        }
    }
}
