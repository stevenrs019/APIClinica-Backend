using APIClinica.Models.DTO;
using APIClinica.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly PdfService _pdfService;

        public NotificacionController(EmailService emailService, PdfService pdfService)
        {
            _emailService = emailService;
            _pdfService = pdfService;
        }


        [HttpPost("enviar-email")]
        public async Task<IActionResult> EnviarCorreo([FromBody] EmailDto email)
        {
            if (string.IsNullOrWhiteSpace(email.To) || string.IsNullOrWhiteSpace(email.Subject) || string.IsNullOrWhiteSpace(email.HtmlContent))
            {
                return BadRequest(new { message = "Todos los campos del correo son obligatorios." });
            }

            var success = await _emailService.SendAppointmentEmailAsync(email.To, email.Subject, email.HtmlContent);

            if (success)
                return Ok(new { message = "Correo enviado con éxito." });
            else
                return StatusCode(500, new { message = "Error al enviar el correo." });
        }

        [HttpPost("enviar-receta")]
        public async Task<IActionResult> EnviarReceta([FromBody] RecetaDto receta)
        {
            var pdfBytes = _pdfService.GenerarRecetaPdf(receta.NombrePaciente, receta.Diagnostico, receta.Receta, receta.NombreMedico);

            var success = await _emailService.SendEmailWithAttachmentAsync(
                receta.Email,
                "Receta Médica - Clínica Salud Integral",
                $"<p>Estimado/a {receta.NombrePaciente}, adjuntamos tu receta médica.</p>",
                pdfBytes,
                "receta.pdf"
            );

            if (success)
                return Ok(new { message = "Receta enviada con éxito." });
            else
                return StatusCode(500, new { message = "Error al enviar la receta." });
        }

    }
}
