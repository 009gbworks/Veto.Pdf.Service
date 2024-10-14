using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Veto.Pdf.Service.Helper;

namespace Veto.Pdf.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DinkToPdfController : ControllerBase
    {
        private readonly IConverter _converter;

        private readonly ILogger<DinkToPdfController> _logger;

        public DinkToPdfController(ILogger<DinkToPdfController> logger, IConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        [HttpGet("TestApi")]
        public async Task<IActionResult> TestService()
        {
            return Ok("Veto Pdf service running");
        }

        [HttpGet("DefaultPdf")]
        public async Task<IActionResult> GetDefaultPdf()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur mauris eget ultrices  iaculis. Ut                               odio viverra, molestie lectus nec, venenatis turpis.",
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            var file = _converter.Convert(doc);
            return File(file, "application/pdf");
        }

        [HttpGet("ThermalPrintPdf")]
        public async Task<IActionResult> GetThermalPrintPdf([FromQuery] PdfBuilderOptions pdfBuilderOptions)
        {
            var paperSize = new PechkinPaperSize("95", (110 + pdfBuilderOptions.RowsAdjustment * 6.2).ToString());
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = paperSize,
                Margins = new MarginSettings { Left = 0.5, Right = 0.5, Top = 1 },
                DocumentTitle = "PDF Report",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = PdfTemplateGenerator.GetHTMLString(pdfBuilderOptions),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");
        }
    }
}
