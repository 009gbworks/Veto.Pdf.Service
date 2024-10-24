namespace Veto.Pdf.Models
{
    public class InvoiceRowBuilderOptions
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
