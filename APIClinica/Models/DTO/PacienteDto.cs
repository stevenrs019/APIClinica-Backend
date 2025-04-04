namespace APIClinica.Models.DTO
{
    public class PacienteDto
    {
        public string NOMBRE { get; set; }
        public string APELLIDO1 { get; set; }
        public string APELLIDO2 { get; set; }
        public string EDAD { get; set; }
        public string TELEFONO { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
        public string EMAIL { get; set; }
        public string CONTRASENA { get; set; }
        public bool ESTADO { get; set; }
    }

}
