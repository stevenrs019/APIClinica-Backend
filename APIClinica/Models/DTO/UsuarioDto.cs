namespace APIClinica.Models.DTO
{
    public class UsuarioDto
    {
        public string NOMBRE { get; set; }
        public string APELLIDO1 { get; set; }
        public string APELLIDO2 { get; set; }
        public string EDAD { get; set; }
        public string TELEFONO { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
        public string EMAIL { get; set; }
        public string CONTRASENA { get; set; }
        public int ID_ROL { get; set; }
    }
}
