namespace APIClinica.Models
{
    public class Persona
    {
        public int ID_PERSONA { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO1 { get; set; }
        public string APELLIDO2 { get; set; }
        public string EDAD { get; set; }
        public string TELEFONO { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
    }
}
