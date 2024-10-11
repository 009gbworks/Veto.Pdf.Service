using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("Pdf")]
        public async Task<IActionResult> GetPdf()
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

            //return Ok("Successfully created PDF document.");
            //return File(file, "application/pdf", "EmployeeReport.pdf"); USE THIS RETURN STATEMENT TO DOWNLOAD GENERATED PDF DOCUMENT
            return File(file, "application/pdf");
        }
    }
}
