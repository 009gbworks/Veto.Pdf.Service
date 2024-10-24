namespace Veto.Pdf.Models
{
    public class PdfBuilderOptions
    {
        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int RowsAdjustment { get; set; }

        public CompanyBuilderOptions? CompanyBuilderOptions { get; set; }
        public RecepientBuilderOptions? RecepientBuilderOptions { get; set; }
        public IEnumerable<InvoiceRowBuilderOptions>? RowsBuilderOptions { get; set; }
        public SummaryBuilderOptions? SummaryBuilderOptions { get; set; }
    }
}
