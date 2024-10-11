using System.Text;

namespace Veto.Pdf.Service.Helper
{
    public class PdfTemplateGenerator
    {
        public static string GetHTMLString(PdfBuilderOptions pdfBuilderOptions)
        {
            var sb = new StringBuilder();

            sb.Append($@"
            <html>
				<head>
					<meta charset=""utf - 8"">
				</head>

				<body>
					<header>
						<h1>  {pdfBuilderOptions.CompanyBuilderOptions?.CompanyName} </h1>
						<h3>   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -    </h3>
						{BuildCompanyInfo(pdfBuilderOptions.CompanyBuilderOptions)}
					</header>

					<article>
						{BuildRecepientInfo(pdfBuilderOptions.RecepientBuilderOptions)}

						{BuildInvoiceInfo(pdfBuilderOptions)}

						<table class=""inventory"">
							<thead>
								<tr>
									<th><span contenteditable>Description</span></th>
									<th><span contenteditable>Unit price</span></th>
									<th><span contenteditable>Qty</span></th>
									<th><span contenteditable>Price/Rs.</span></th>
								</tr>
							</thead>
							<tbody>
								{BuildInvoiceRows(pdfBuilderOptions.RowsBuilderOptions)}
							</tbody>
						</table>
			
						<h4>   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -   </h4>

						{BuildSummary(pdfBuilderOptions.SummaryBuilderOptions)}

						<h4>   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -   </h4>

					</article>
				</body>
			</html>");

            return sb.ToString();
        }

        private static string BuildCompanyInfo(PdfCompanyBuilderOptions? companyBuilderOptions)
        {
			if (companyBuilderOptions == null) 
			{ 
				return string.Empty;
			}

            return $@"            
			<address contenteditable>
                <p> {companyBuilderOptions.EmployeeName}  </p>

                <p> {companyBuilderOptions.AddressOne}  </p>

                <p> {companyBuilderOptions.AddressTwo} </p>

                <p> {companyBuilderOptions.Telephone} </p>

            </address>
			<span><img alt="""" src={companyBuilderOptions.ComapnyImageSource}><input type=""file"" accept=""image/*""></span>";
        }

        private static string BuildRecepientInfo(PdfRecepientBuilderOptions? recepientBuilderOptions)
        {
            if (recepientBuilderOptions == null)
            {
                return string.Empty;
            }

            return $@"            
			<address contenteditable>

                <p>{recepientBuilderOptions.FirstName}  </p>
                <p> PetId : {recepientBuilderOptions.Id}  </p>
                <p>{recepientBuilderOptions.LastName}  </p>
                
                <p> {recepientBuilderOptions.Telephone} </p>

            </address>";
        }

        private static string BuildInvoiceInfo(PdfBuilderOptions? pdfBuilderOptions)
        {
            if (pdfBuilderOptions == null)
            {
                return string.Empty;
            }

            return $@"            
			<table class=""meta"">
				<tr>
					<th><span contenteditable>Amount Due</span></th>
					<td class=""DueValue""><span id = ""prefix"" contenteditable>Rs. </span><span>{pdfBuilderOptions?.SummaryBuilderOptions?.DueAmount.ToString("F")}</span></td>
				</tr>
				<tr>
					<td><span contenteditable>Inv no - M{pdfBuilderOptions?.InvoiceNumber?.Substring(1)}</span></td>
				</tr>
				<tr>
					<td><span contenteditable>{pdfBuilderOptions?.InvoiceDate.ToString("yyyy-MMM-dd")}</span></td>
				</tr>
				
			</table>";
        }

        private static string BuildInvoiceRows(IEnumerable<PdfRowBuilderOptions>? pdfRowsBuilderOptions)
        {
            if (pdfRowsBuilderOptions == null)
            {
                return string.Empty;
            }

            var salesDetails = new StringBuilder();
			foreach (var row in pdfRowsBuilderOptions) 
			{
                salesDetails.Append($@"
                    <tr>
						<td><span contenteditable>{row.Name}</span></td>
						<td><span data-prefix></span><span contenteditable>{row.Price.ToString("F")}</span></td>
						<td><span contenteditable>{row.Quantity}</span></td>
						<td><span data-prefix></span><span>{row.Total.ToString("F")}</span></td>
					</tr>");
            }

            return salesDetails.ToString();
        }

        private static string BuildSummary(PdfSummaryBuilderOptions? summaryBuilderOptions)
		{
            if (summaryBuilderOptions == null)
            {
                return string.Empty;
            }

            return $@"<table class=""balance"">
                <tr>
					<th><span contenteditable>Sub Total</span></th>
					<td><span data-prefix> </span><span>{summaryBuilderOptions.SubTotal.ToString("F")}</span></td>
				</tr>
                <tr>
					<th><span contenteditable>Discount</span></th>
					<td><span data-prefix> </span><span>{summaryBuilderOptions.Discount.ToString("F")}</span></td>
				</tr>
				<tr>
					<th><span contenteditable>Total</span></th>
					<td><span data-prefix> </span><span>{summaryBuilderOptions.Total.ToString("F")}</span></td>
				</tr>
				<tr>
					<th><span contenteditable>Amount Paid</span></th>
					<td><span data-prefix> </span><span contenteditable>{summaryBuilderOptions.PaidAmount.ToString("F")}</span></td>
				</tr>
				<tr>
					<th><span contenteditable>Balance Due</span></th>
					<td><span data-prefix> </span><span>{((-1) * (summaryBuilderOptions.DueAmount)).ToString("F")}</span></td>
				</tr>
			</table>";
		}
    }

	public class PdfBuilderOptions
	{
		public string? InvoiceNumber {  get; set; }
		public DateTime InvoiceDate { get; set; }
		public int RowsAdjustment {  get; set; }

		public PdfCompanyBuilderOptions? CompanyBuilderOptions { get; set; }
		public PdfRecepientBuilderOptions? RecepientBuilderOptions { get; set; }
		public IEnumerable<PdfRowBuilderOptions>? RowsBuilderOptions { get; set; }
		public PdfSummaryBuilderOptions? SummaryBuilderOptions {  get; set; }
	}

    public class PdfCompanyBuilderOptions
    {
		public string? CompanyName { get; set; }
		public string? EmployeeName { get; set; }
		public string? AddressOne { get; set; }
		public string? AddressTwo { get; set; }
		public string? Telephone { get; set; }
		public string? ComapnyImageSource { get; set; }
    }

    public class PdfRecepientBuilderOptions
    {
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int Id { get; set; }
		public string? Telephone { get; set; }
    }

	public class PdfRowBuilderOptions
	{
		public string? Name { get; set; }
		public decimal Price { get; set; }
		public decimal Quantity { get; set; }
		public decimal Total { get; set; }
	}

	public class PdfSummaryBuilderOptions
	{
		public decimal SubTotal { get; set; }
		public decimal Discount { get; set; }
		public decimal Total { get; set; }
		public decimal PaidAmount { get; set; }
		public decimal DueAmount { get; set; }
	}
}
