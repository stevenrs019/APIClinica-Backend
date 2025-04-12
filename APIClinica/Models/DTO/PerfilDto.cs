namespace APIClinica.Models.DTO
{
    public class PerfilDto
    {
        public string EMAIL { get; set; } = string.Empty;

        public string NOMBRE { get; set; } = string.Empty;
        public string APELLIDO1 { get; set; } = string.Empty;
        public string APELLIDO2 { get; set; } = string.Empty;

        public string EDAD { get; set; } = string.Empty;
        public string TELEFONO { get; set; } = string.Empty;

        public DateTime FECHA_NACIMIENTO { get; set; }
    }
}
