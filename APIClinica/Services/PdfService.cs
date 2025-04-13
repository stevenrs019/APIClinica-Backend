using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace APIClinica.Services
{
    public class PdfService
    {
        public byte[] GenerarRecetaPdf(string nombrePaciente, string diagnostico, string receta, string nombreMedico)
        {
            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logo-clinica.png");
            byte[]? logoBytes = File.Exists(logoPath) ? File.ReadAllBytes(logoPath) : null;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header
                    page.Header().Element(header =>
                    {
                        header.Row(row =>
                        {
                            row.RelativeColumn().Column(col =>
                            {
                                col.Item().Text("Clínica Salud Integral").Bold().FontSize(18).FontColor(Colors.Blue.Medium);
                                col.Item().Text("Receta Médica").FontSize(14).Italic();
                            });

                            if (logoBytes != null)
                            {
                                row.ConstantColumn(80)
                                    .Height(50) // Establece altura máxima
                                    .Image(logoBytes)
                                    .FitWidth(); // Ajusta el ancho y recorta si es necesario
                            }
                        });
                    });

                    // Contenido principal
                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        col.Item().Text("📌 Paciente:").Bold();
                        col.Item().PaddingLeft(20).PaddingBottom(10).Text(nombrePaciente);

                        col.Item().Text("👨‍⚕️ Médico:").Bold();
                        col.Item().PaddingLeft(20).PaddingBottom(10).Text(nombreMedico);

                        col.Item().PaddingTop(10).Text("📋 Diagnóstico:").Bold();
                        col.Item().PaddingLeft(20).PaddingBottom(10).Text(diagnostico);

                        col.Item().Text("💊 Receta:").Bold();
                        col.Item().PaddingLeft(20).PaddingBottom(10).Text(receta);

                        
                    });

                    // Footer
                    page.Footer().AlignCenter().Text("Clínica Salud Integral © - Documento generado automáticamente");
                });
            });

            return document.GeneratePdf();
        }
    }
}
