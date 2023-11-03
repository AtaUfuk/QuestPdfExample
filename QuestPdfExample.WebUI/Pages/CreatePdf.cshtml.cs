using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPdfExample.WebUI.Pages
{
	public class CreatePdfModel : PageModel
	{
		[BindProperty]
		public required string PdfText { get; set; }
		public void OnGet()
		{
		}

		public void OnPost()
		{
			QuestPDF.Settings.License = LicenseType.Community;
			string text = this.PdfText;
			Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(3, Unit.Centimetre);
					page.Header().Text("Static Header Parameter!!!").FontSize(16).SemiBold().FontColor(Colors.Red.Medium);
					page.Content()
					.PaddingVertical(1,Unit.Centimetre)
					.Column(c =>
					{
						c.Spacing(20);
						c.Item().Text(text);
					});
					page.Footer()
					.AlignCenter()
					.Text(t =>
					{
						t.Span("Sayfa ");
						t.CurrentPageNumber();
					});
				});
			}).GeneratePdfAndShow();
		}
	}
}
