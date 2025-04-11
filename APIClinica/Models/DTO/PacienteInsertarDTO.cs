namespace APIClinica.Models.DTO
{
    public class PacienteInsertarDto
    {
        public string NOMBRE { get; set; }
        public string APELLIDO1 { get; set; }
        public string APELLIDO2 { get; set; }
        public string EDAD { get; set; }
        public string TELEFONO { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
    }
}
