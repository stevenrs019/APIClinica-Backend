namespace APIClinica.Models.DTO
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;         // Destinatario
        public string Subject { get; set; } = string.Empty;    // Asunto
        public string HtmlContent { get; set; } = string.Empty; // Contenido HTML
    }
}
